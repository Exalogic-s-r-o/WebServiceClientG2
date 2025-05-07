using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EXC = Exa.OBERON.ServicesGen2.Client.Exceptions.ExaException;
using Exa.OBERON.ServicesGen2.Client.Models;
using System.Security.Cryptography;

namespace Exa.OBERON.ServicesGen2.Client.Services
{
    public class BaseService
    {
        #region EVENTS

        public Action<string> JSONLog;

        #endregion

        public Exa.OBERON.ServicesGen2.Client.WebServiceClient WebServiceClient;

        public BaseService(Exa.OBERON.ServicesGen2.Client.WebServiceClient webServiceClient)
        {
            WebServiceClient = webServiceClient;
        }

        #region PRIVATE METHODS

        /// <summary>
        /// Nastavenia pre serializáciu JSON aj deserialize JSON.
        /// </summary>
        private static readonly JsonSerializerSettings JsonSerializerSetting = new JsonSerializerSettings()
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Error,
            Error = delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
            {
                args.ErrorContext.Handled = true;
            }
        };

        /// <summary>
        /// Všeobecný dopyt na lokálny Http client. Vytvára logovanie.
        /// </summary>
        /// <param name="u_Description">Popis dopytu (popis vrchnej metódy, ktorá volá túto metódu).</param>
        /// <param name="u_Request"></param>
        /// <param name="u_TimeOut">Timeout daného volania (v sekundách) a čakania na odpoveď.</param>
        [DebuggerNonUserCode]
        protected async Task<(EXC exc, JObject rsp)> RequestAsync(string u_Description,
                                                                  HttpRequestMessage u_Request,
                                                                  uint u_TimeOut = 12)
        {
            EXC myEx = EXC.GetDefault();
            HttpResponseMessage m_Response = null;
            string m_RequestContentStr = null;
            string m_ResponseContentStr = null;
            JObject m_ResponseJson = null;
            CancellationTokenSource cts = null;
            

            try
            {

                cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(u_TimeOut));

                if (u_Request.Content != null)
                {
                    m_RequestContentStr = await u_Request.Content.ReadAsStringAsync();
                }

                m_Response = await this.WebServiceClient.HttpClient.SendAsync(u_Request, HttpCompletionOption.ResponseHeadersRead, cancellationToken: cts.Token);

                if (!m_Response.IsSuccessStatusCode)
                {
                    string m_ReasonPharse = m_Response.ReasonPhrase;
                    if (string.IsNullOrWhiteSpace(m_ReasonPharse))
                    {
                        m_ReasonPharse = $"Result code {m_Response.StatusCode}";
                    }

                    myEx = EXC.Get(m_ReasonPharse);
                    return (myEx, default(JObject));
                }

                if (m_Response.Content != null)
                {
                    string m_MediaType = m_Response.Content.Headers.ContentType?.MediaType.ToLower() ?? "/json";
                    m_ResponseContentStr = await m_Response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!string.IsNullOrWhiteSpace(m_MediaType))
                    {
                        if (m_MediaType.Contains("/json"))
                        {
                            m_ResponseJson = JObject.Parse(m_ResponseContentStr);
                            m_ResponseContentStr = m_ResponseJson.ToString();
                            myEx.Result = true;
                        }
                    }
                    else
                    {
                        // The content type was not set.
                    }
                }

                myEx.Result = true;
            }
            catch (HttpRequestException ex)
            {
                string message;
                switch ((ex.InnerException as WebException)?.Status ?? WebExceptionStatus.UnknownError)
                {
                    case WebExceptionStatus.ConnectFailure:
                        message = "Spojenie sa nepodarilo vytvoriť.";
                        break;
                    case WebExceptionStatus.TrustFailure:
                        // The remote certificate is invalid according to the validation procedure.
                        // Klient nezvalidoval certifikát v metóde ValidateCertificate, t.j. odmietol ho.
                        message = "Spojenie sa nepodarilo vytvoriť (certifikát servera bol zamietnutý).";
                        break;
                    case WebExceptionStatus.NameResolutionFailure:
                        message = $"Pre '{u_Request.RequestUri.Authority}' sa nepodarilo nájsť IP adresu.";
                        break;
                    default: message = "Požiadavku sa nepodarilo odoslať."; break;
                }

                myEx = EXC.Get(message);
            }

            catch (System.Threading.Tasks.TaskCanceledException)
            {
                myEx = EXC.Get($"Server '{this.WebServiceClient.HttpClient.BaseAddress.ToString()}' nie je v tejto chvíli dostupný (neodpovedá).",
                                u_ErrNumber: (int)Exceptions.enm_ErrNumbers.TimeOut);
            }
            catch (Exception ex)
            {

                myEx = EXC.Get($"RequestAsync '{u_Description}' error: {ex.Message}");
            }
            finally
            {
                try
                {


                    JSONLog?.Invoke($"RequestAsync '{u_Description}'\n" +
                        $"Request: {u_Request.ToString()}\n" +
                        $"RequestContent: {m_RequestContentStr}\n" +
                        $"Response: {m_Response?.ToString()}\n" +
                        $"ResponseContent: {m_ResponseContentStr}\n" +
                        $"ResponseJson: {m_ResponseJson?.ToString()}");

                    u_Request?.Dispose();
                    m_Response?.Dispose();
                }
                catch (Exception)
                {
                }
                try
                {
                    if (cts != null)
                    {
                        cts.Dispose();
                        cts = null;
                    }
                }
                catch (Exception)
                {
                }
            }
            return (myEx, m_ResponseJson);
        }

        /// <summary>
        /// Všeobecný GET dopyt na lokálny Http client.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="u_Description">Popis dopytu (popis vrchnej metódy, ktorá volá túto metódu).</param>
        /// <param name="u_ApiPath"></param>
        /// <param name="u_TimeOut">Timeout daného volania (v sekundách) a čakania na odpoveď.</param>
        protected async Task<ResultModel<T>> GetResultAsync<T>(string u_Description,
                                                               string u_ApiPath,
                                                               uint u_TimeOut = 12)
        {
            EXC myEx = EXC.GetDefault();

            var response = new ResultModel<T>();

            if (this.WebServiceClient.IsInitialized == false)
            {
                myEx = EXC.Get("WebServiceClient nie je inicializovaný.");
                response.FromExaException(myEx);
                return response;
            }

            try
            {
                HttpRequestMessage m_Request = new HttpRequestMessage(HttpMethod.Get, u_ApiPath);
                if (string.IsNullOrEmpty(this.WebServiceClient.UserData) == false)
                {
                    m_Request.Headers.Add("userData", this.WebServiceClient.UserData);
                }

                var m_Result = await this.RequestAsync(u_Description, m_Request, u_TimeOut: u_TimeOut);
                if (m_Result.rsp != null)
                {
                    // Prišla odpoveď nastav údaje o výsledku. Prijaté údaje zo servera.
                    return m_Result.rsp.ToObject<ResultModel<T>>(Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSetting))
                            ?? throw new Exception("Response is null");
                }

                // Odpoveď neprišla, nastav chyby z lokálneho http clienta.
                myEx = m_Result.exc;
            }
            catch (Exception ex)
            {
                myEx = EXC.Get($"GetAsync '{u_Description}' error: {ex.Message}");
            }

            response.FromExaException(myEx);
            return response;
        }

        protected async Task<T> GetAsync<T>(string u_Description,
                                            string u_ApiPath,
                                            uint u_TimeOut = 12)
        {
            EXC myEx = EXC.GetDefault();

            try
            {
                HttpRequestMessage m_Request = new HttpRequestMessage(HttpMethod.Get, u_ApiPath);
                if (string.IsNullOrEmpty(this.WebServiceClient.UserData) == false)
                {
                    m_Request.Headers.Add("userData", this.WebServiceClient.UserData);
                }

                var m_Result = await this.RequestAsync(u_Description, m_Request, u_TimeOut: u_TimeOut);
                if (m_Result.rsp != null)
                {
                    // Prišla odpoveď nastav údaje o výsledku. Prijaté údaje zo servera.
                    return m_Result.rsp.ToObject<T>(Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSetting));

                }

                // Odpoveď neprišla, nastav chyby z lokálneho http clienta.
                myEx = m_Result.exc;
            }
            catch (Exception ex)
            {
                myEx = EXC.Get($"GetAsync '{u_Description}' error: {ex.Message}");
            }

            return default(T);
        }


        protected async Task<ResultModel<string>> GetAsync(string u_Description,
                                                        string u_ApiPath,
                                                        uint u_TimeOut = 12)
        {
            ResultModel<string> result = new ResultModel<string>();

            try
            {
                string pingresult = await this.WebServiceClient.HttpClient.GetStringAsync(u_ApiPath);

                if (pingresult == string.Empty)
                {
                    result.FromExaException(EXC.Get($"Na volanie '{u_ApiPath}' prišla prázdna odpoveď."));
                    return result;
                }

                result.data = pingresult;
                result.result = true;
            }
            catch (System.TimeoutException)
            {
                // Timeout - služba nie je dostupná.
                result.FromExaException(EXC.Get($"Chyba pri volaní '{u_ApiPath}'. 'TIMEOUT' "));
            }
            catch (HttpRequestException ex)
            {
                string message;
                switch ((ex.InnerException as WebException)?.Status ?? WebExceptionStatus.UnknownError)
                {
                    case WebExceptionStatus.ConnectFailure:
                        message = "Spojenie sa nepodarilo vytvoriť.";
                        break;
                    case WebExceptionStatus.TrustFailure:
                        // The remote certificate is invalid according to the validation procedure.
                        // Klient nezvalidoval certifikát v metóde ValidateCertificate, t.j. odmietol ho.
                        message = "Spojenie sa nepodarilo vytvoriť (certifikát servera bol zamietnutý).";
                        break;
                    case WebExceptionStatus.NameResolutionFailure:
                        message = $"The name resolver service could not resolve the host name.";
                        break;
                    default: message = "Požiadavku sa nepodarilo odoslať."; break;
                }

                result.FromExaException(EXC.Get(message));
            }

            catch (System.Threading.Tasks.TaskCanceledException)
            {
                result.FromExaException(EXC.Get($"Server '{this.WebServiceClient.HttpClient.BaseAddress.ToString()}' nie je v tejto chvíli dostupný (neodpovedá).",
                                u_ErrNumber: (int)Exceptions.enm_ErrNumbers.TimeOut));
            }
            catch (Exception ex)
            {
                result.FromExaException(EXC.Get($"Chyba pri volaní '{u_ApiPath}'. '{ex.Message}' "));
            }
            return result;
        }

        /// <summary>
        /// Všeobecný POST z lokálneho http clienta.
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="u_Description">Popis dopytu (popis vrchnej metódy, ktorá volá túto metódu).</param>
        /// <param name="u_ApiPath"></param>
        /// <param name="u_Request">Objekt, ktorý sa má serializovať.</param>
        /// <param name="u_RequestRootName">Meno root (hlavného) elementu, do ktorého bude vložený celý serializovaný objekt. 
        /// Spravidla sa zadáva meno argumentu pri POST volaní.</param>
        /// <param name="u_TimeOut">Timeout daného volania (v sekundách) a čakania na odpoveď.</param>
        protected async Task<ResultModel<T>> PostAsync<T>(string u_Description,
                                                          string u_ApiPath,
                                                          object u_Request,
                                                          string u_RequestRootName = null,
                                                          uint u_TimeOut = 12)
        {
            EXC myEx = EXC.GetDefault();

            var response = new ResultModel<T>();

            if (this.WebServiceClient.IsInitialized == false)
            {
                myEx = EXC.Get("WebServiceClient nie je inicializovaný.");
                response.FromExaException(myEx);
                return response;
            }

            try
            {
                HttpRequestMessage m_Request = new HttpRequestMessage(HttpMethod.Post, u_ApiPath);
                if (string.IsNullOrEmpty(this.WebServiceClient.UserData) == false)
                {
                    m_Request.Headers.Add("userData", this.WebServiceClient.UserData);
                }

                // Vytvor JSON data.
                var m_JsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(u_Request, settings: JsonSerializerSetting);

                if (string.IsNullOrEmpty(u_RequestRootName) == false)
                {
                    // Ak je zadaný root name, tak ho pridaj do JSON-u.
                    m_JsonRequest = "{\"" + u_RequestRootName + "\":" + m_JsonRequest + "}";
                }

                m_Request.Content = new StringContent(content: m_JsonRequest,
                                                      encoding: Encoding.UTF8,
                                                      mediaType: "application/json");

                var m_Result = await RequestAsync(u_Description, m_Request, u_TimeOut: u_TimeOut);
                if (m_Result.rsp != null)
                {
                    // Prišla odpoveď nastav údaje o výsledku. Prijaté údaje zo servera.
                    return m_Result.rsp.ToObject<ResultModel<T>>(Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSetting))
                        ?? throw new Exception("Response is null");
                }

                // Odpoveď neprišla, nastav chyby z lokálneho http clienta.
                myEx = m_Result.exc;
            }
            catch (Exception ex)
            {
                myEx = EXC.Get($"PostAsync '{u_Description}' error: {ex.Message}");
            }

            response.FromExaException(myEx);
            return response;
        }

        /// <summary>
        /// Všeobecný DELETE z lokálneho http clienta.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="u_Description">Popis dopytu (popis vrchnej metódy, ktorá volá túto metódu).</param>
        /// <param name="u_ApiPath"></param>
        /// <param name="u_Request"></param>
        /// <param name="u_TimeOut">Timeout daného volania (v sekundách) a čakania na odpoveď.</param>
        protected async Task<ResultModel<T>> DeleteAsync<T>(string u_Description,
                                                          string u_ApiPath,
                                                          object u_Request,
                                                          uint u_TimeOut = 12)
        {
            EXC myEx = EXC.GetDefault();

            try
            {
                HttpRequestMessage m_Request = new HttpRequestMessage(HttpMethod.Delete, u_ApiPath);
                if (string.IsNullOrEmpty(this.WebServiceClient.UserData) == false)
                {                    
                    m_Request.Headers.Add("userData", this.WebServiceClient.UserData);
                }

                // Vytvor JSON data
                var m_JsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(u_Request, settings: JsonSerializerSetting);

                m_Request.Content = new StringContent(content: m_JsonRequest,
                                                      encoding: Encoding.UTF8,
                                                      mediaType: "application/json");

                var m_Result = await RequestAsync(u_Description, m_Request, u_TimeOut: u_TimeOut);
                if (m_Result.rsp != null)
                {
                    // Prišla odpoveď nastav údaje o výsledku. Prijaté údaje zo servera.
                    return m_Result.rsp.ToObject<ResultModel<T>>(Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSetting))
                            ?? throw new Exception("Response is null");
                }

                // Odpoveď neprišla, nastav chyby z lokálneho http clienta.
                myEx = m_Result.exc;
            }
            catch (Exception ex)
            {
                myEx = EXC.Get($"DeleteAsync '{u_Description}' error: {ex.Message}");
            }

            var response = new ResultModel<T>();
            response.FromExaException(myEx);
            return response;
        }

        #endregion

    }
}

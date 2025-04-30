using Exa.OBERON.ServicesGen2.Client.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EXC = Exa.OBERON.ServicesGen2.Client.Exceptions.ExaException;

namespace Exa.OBERON.ServicesGen2.Client.Services
{
    public class LoginService : BaseService
    {

        #region CONSTANTS

        private const string CONST_LOGIN_SALT = "/v1/user/login/salt";
        private const string CONST_LOGIN = "v1/user/login";

        #endregion

        #region CONSTRUCTOR

        public LoginService(Exa.OBERON.ServicesGen2.Client.WebServiceClient webServiceClient) : base(webServiceClient)
        {

        }

        #endregion

        #region METHODS

        /// <summary>Vráti tzv. SALT, ktorý je používaný pri hashovaní hesla pri prihlásení používateľa. Volá sa tesne pred metódou LoginUser.</summary>
        /// <remarks>
        /// Pri prihlásení používateľa nezabezpečenou komunikáciou (HTTP - nezabezpečená, HTTPS -> zabezpečená) môže pri odpočúvaní komunikácie útočník pomerne ľahko zistiť prihlasovacie údaje (hlavne heslo).
        /// Čiastočne to rieši systém zasielania hesla len vo forme HASH (OBERON používa SHA-1), avšak pri slabých (krátkych) heslách je veľmi jednoduché pomocou hashovacích tabuliek (databáz) získať aj takého heslo.
        /// Z tohto dôvodu je možné pri prihlásení zasielaný HASH hesla "obohatiť" o tento SALT a tým znemožniť využitie hashovacích tabuliek.	
        /// </remarks>
        /// <param name="userName">Meno používateľa pre ktorý sa salt generuje a následne pri ďalšom prihlásení (metóda LoginUser) použije.</param>
        /// <returns>V ResultValue.Data vracia SALT. Ide o textovú hodnotu, ku ktorej sa pripojí heslo -> z týchto hodnôt sa následne vytvorí HASH (typ SHA-1), ktorý sa už použije pri prihlásení v parametri 'password'.</returns>
        /// <example>Url:  http://address:port/v1/user/login/salt</example>
        public async Task<ResultModel<string>> LoginSalt(string userName)
        {

            ResultModel<string> result = new ResultModel<string>();

            try
            {
                Models.Login.LoginSalt loginSalt = new Models.Login.LoginSalt();
                loginSalt.userName = userName;

                var loginResult = await this.PostAsync<string>(u_Description: "LoginSalt",
                                                              u_ApiPath: CONST_LOGIN_SALT,
                                                              u_Request: loginSalt);
                if (loginResult == null)
                {
                    result.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_LOGIN_SALT}'."));
                    return result;
                }

                result.result = true;
                result.data = loginResult.data;

            }
            catch (System.TimeoutException)
            {
                // Timeout - služba nie je dostupná.
                result.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_LOGIN_SALT}'. 'TIMEOUT' "));
            }
            catch (Exception ex)
            {
                result.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_LOGIN_SALT}'. '{ex.Message}' "));
            }
            return result;
        }

        /// <summary>
        /// <summary>Prihlási používateľa do webovej služby (záleží však na nastavení spôsobu autentifikácie služby). Je potrebné volať na začiatku komunikácie s webovou službou, nakoľko sa generuje tzv. GUID pre ďalšiu komunikáciu (ten musí byť súčasťou hlavičky danej požiadavky).</summary>
        /// <remarks>
        /// Pri prihlásení používateľa nezabezpečenou komunikáciou (HTTP - nezabezpečená, HTTPS -> zabezpečená) môže pri odpočúvaní komunikácie útočník pomerne ľahko zistiť prihlasovacie údaje (hlavne heslo).
        /// Čiastočne to rieši systém zasielania hesla len vo forme HASH (OBERON používa SHA-1), avšak pri slabých (krátkych) heslách je veľmi jednoduché pomocou hashovacích tabuliek (databáz) získať aj takého heslo.
        /// Z tohto dôvodu je možné pri prihlásení zasielaný HASH hesla "obohatiť" o SALT a tým znemožniť využitie hashovacích tabuliek.
        /// Po úspešnom prihlásení sa v ďalších požiadavkách na webovú službu musí v hlavičke dopytu uvádzať daný GUID (userData), podľa ktorého sa overuje daná požiadavka.  
        /// </remarks>
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>     
        public async Task<ResultModel<Models.Login.UserInfo>> Login(string userName,
                                                                    string password,
                                                                    string loginTag = null,
                                                                    string languageCode = null)
     
        {

            ResultModel<Models.Login.UserInfo> result = new ResultModel<Models.Login.UserInfo>();

            try
            {
                // Vytvoriť hash hesla pomocou SHA-1.
                Models.Login.LoginSalt loginSalt = new Models.Login.LoginSalt();
                loginSalt.userName = userName;

                var saltResult = await this.PostAsync<string>(u_Description: "LoginSalt",
                                                               u_ApiPath: CONST_LOGIN_SALT,
                                                               u_Request: loginSalt);
                if (saltResult == null)
                {
                    result.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_LOGIN_SALT}'."));
                    return result;
                }

                var passwordHash = this.PasswordHashSHA1(password, 
                                                         saltResult.data);

                if (string.IsNullOrEmpty(passwordHash) == true)
                {
                    result.FromExaException(EXC.Get($"Chyba pri vytváraní hash hesla '{password}'."));
                    return result;
                }

                // Volanie login.
                Models.Login.UserLoginArg login = new Models.Login.UserLoginArg();
                login.UserName = userName;
                login.Password = passwordHash;
                login.ApplicationName = WebServiceClient.ApplicationName;
                login.ApplicationVersion = WebServiceClient.ApplicationVersion;
                login.LoginTag = loginTag;
                login.LanguageCode = languageCode;

                var loginResult = await this.PostAsync<Models.Login.UserInfo>(u_Description: "Login",
                                                                              u_ApiPath: CONST_LOGIN,
                                                                              u_Request: login,
                                                                              u_RequestRootName: "userLoginArg");
                if (loginResult == null)
                {
                    result.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_LOGIN}'."));
                    return result;
                }

                result.result = true;
                // Uložiť user GUID do webovej služby. Slúži na identifikáciu používateľa pri ďalších volaniach.
                this.WebServiceClient.UserData = loginResult.data.GUID;

                result.data.ShortName = loginResult.data.ShortName;
                result.data.UserName = loginResult.data.UserName;
                result.data.UserIDNum = loginResult.data.UserIDNum;
                result.data.DocumentName = loginResult.data.DocumentName;
                result.data.BranchName = loginResult.data.BranchName;
                result.data.PasswordType = loginResult.data.PasswordType;
                result.data.GUID = loginResult.data.GUID;

            }
            catch (System.TimeoutException)
            {
                // Timeout - služba nie je dostupná.
                result.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_LOGIN}'. 'TIMEOUT' "));
            }
            catch (Exception ex)
            {
                result.FromExaException(EXC.Get($"Chyba pri volaní '{CONST_LOGIN}'. '{ex.Message}' "));
            }

            return result;
        }

        /// <summary>
        /// Vytvorí hash hesla pomocou SHA-1.
        /// </summary>
        /// <param name="u_Password"></param>
        /// <param name="u_Salt"></param>
        /// <returns></returns>
        private string PasswordHashSHA1(string u_Password, 
                                        string u_Salt)
        {
            try
            {
                string tmp_Password = string.Empty;
                if (string.IsNullOrEmpty(u_Salt) == true)
                {
                    // Heslo bez SALT.
                    tmp_Password = u_Password;
                }
                else
                {
                    // Je zadaný SALT - Heslo "zväčšiť" o SALT - k saltu pripojiť heslo.
                    tmp_Password = u_Salt + u_Password;
                }

                var m_SHA1 = System.Security.Cryptography.SHA1.Create();
                var m_bInput = Encoding.Default.GetBytes(tmp_Password);
                byte[] m_bOutput;
                string tmp_PasswordHashWtthSHA1;
                m_bOutput = m_SHA1.ComputeHash(m_bInput);
                tmp_PasswordHashWtthSHA1 = BitConverter.ToString(m_bOutput);
                tmp_PasswordHashWtthSHA1 = tmp_PasswordHashWtthSHA1.Replace("-", "");
                tmp_PasswordHashWtthSHA1 = tmp_PasswordHashWtthSHA1.ToLower();

                return tmp_PasswordHashWtthSHA1;
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
    }
}

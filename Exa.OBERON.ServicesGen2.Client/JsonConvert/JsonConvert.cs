using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Exa.OBERON.ServicesGen2.Client
{
    public class JsonConvert
    {

        /// <summary>
        /// Serializuje objekt (triedu s príznakom [System.Runtime.Serialization.DataContract]) do hodnoty typu STRING.
        /// </summary>        
        public static string ObjectToJson(object ObjectToConvert)
        {
            string ReturnValue = null;

            try
            {
                // Vytvor JSON data
                ReturnValue = Newtonsoft.Json.JsonConvert.SerializeObject(ObjectToConvert);
            }
            catch (Exception)
            {
            }
            return ReturnValue;
        }

        /// <summary>
        /// Serializuje STRING hodnotu ako JSON do objektu (triedu s príznakom [System.Runtime.Serialization.DataContract]).
        /// </summary>        
        public static T JsonToObject<T>(string JsonValue)
        {

            try
            {
                // Vytvor JSON data
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(JsonValue);
            }
            catch (Exception)
            {
            }
            return default(T);
        }


        /// <summary>
        /// Skonvertuje hodnotu typu DateTime do formátu podľa normy ISO 8601 (pre webservice OBERON), t.j. yyyy-MM-ddTHH:mm:ss.
        /// Webservice používa pre dátum v DataContract String (nie DateTime).
        /// </summary>        
        public static string DateToJsonString(DateTime Value)
        {
            string ReturnValue = "0001-01-01T00:00:00";

            try
            {              
                if (Value != null)
                {
                    ReturnValue = Value.ToString("yyyy-MM-ddTHH:mm:ss");
                }                                  
            }
            catch (Exception)
            {
            }
            return ReturnValue;
        }


        /// <summary>
        /// Konverzia dátumu a času z textového formátu (podľa normy ISO 8601), napr. "yyyy-MM-ddTHH:mm:ss" na dátum a čas (ako DateTime).
        /// </summary>
        /// <param name="Value">JSON date string vo formáte "yyyy-MM-ddTHH:mm:ss" alebo "yyyy-MM-ddTHH:mm:ss.fffZ".</param>
        /// <param name="removeTimeInformation">TRUE - odstrániť časovú informáciu.</param>       
        public static DateTime JsonDateStringToDate(string Value, bool removeTimeInformation = false)
        {
            {
                if (string.IsNullOrEmpty(Value) || Value.StartsWith("0001-01-01"))
                {
                    // Prázdny dátum
                    return DateTime.MinValue;
                }

                DateTime date = default;

                try
                {
                    if (Value.EndsWith("z", StringComparison.OrdinalIgnoreCase))
                    {
                        date = DateTime.ParseExact(Value,
                                                    "yyyy-MM-ddTHH:mm:ss.fffZ",
                                                    CultureInfo.InvariantCulture,
                                                    DateTimeStyles.AdjustToUniversal);
                    }
                    else if (Value.Contains("+"))
                    {
                        if (DateTimeOffset.TryParse(Value,
                                                    null,
                                                    DateTimeStyles.AssumeLocal | DateTimeStyles.RoundtripKind,
                                                    out DateTimeOffset dtOffset))
                        {
                            date = dtOffset.DateTime;
                        }
                        else
                        {
                            DateTime.TryParseExact(Value,
                                                    "yyyy-MM-ddTHH:mm:sszzzz",
                                                    null,
                                                    DateTimeStyles.RoundtripKind,
                                                    out date
                            );
                        }
                    }
                    else
                    {
                        date = DateTime.ParseExact(Value,
                                                    "yyyy-MM-ddTHH:mm:ss",
                                                    CultureInfo.InvariantCulture);
                    }

                    // Čas
                    if (removeTimeInformation ==true && date.TimeOfDay.TotalSeconds > 0)
                    {
                        // Odstránenie časovej informácie
                        date = date.Date;
                    }
                }
                catch (Exception)
                {
                }

                return date;
            }
        }
    }
}

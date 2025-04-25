using System;
using System.Collections.Generic;
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


    }
}

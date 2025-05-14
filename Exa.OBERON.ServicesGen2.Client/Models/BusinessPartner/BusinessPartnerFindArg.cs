using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Exa.OBERON.ServicesGen2.Client.Models.BusinessPartner
{
    /// <summary>Trieda obsahuje údaje pre vyhľadanie obchodného partnera. Podporuje aj fultextové vyhľadávanie, 
    /// ktoré ignoruje diakritiku.
    /// Okrem určenia spôsobu vyhľadania je možné nastaviť aj rozsah vrátencýh údajov (z dôvodu výkonu).</summary>
    public class BusinessPartnerFindArg
    {

        /// <summary>Typ hodnoty podľa, ktorej sa má vyhľadať obchodný partner. Je možné vybrať z možností, ktoré určuje <see cref="enm_BusinessPartnerFindMethod"/>.</summary>
        [JsonProperty(Required = Required.Always, DefaultValueHandling = DefaultValueHandling.Include)]
        public int FindMethod { get; set; }

        /// <summary>Hodnota, podľa ktorej sa má obchodný partner vyhľadať.</summary>
        [JsonProperty(Required = Required.Always, DefaultValueHandling = DefaultValueHandling.Include)]
        public string FindValue { get; set; }

        /// <summary>TRUE - Budú vrátené aj prevádzky obchodného partnera.</summary>
        [JsonProperty(Required = Required.Always, DefaultValueHandling = DefaultValueHandling.Include)]
        public bool GetBranches { get; set; } = false;

        /// <summary>
        /// TRUE - vráti zoznam pripoených dokumentov (obrázkov alebo iných príloh) ku skladovej karte. Údaje môže vracať aj bez binárnych údajov.
        /// FALSE - zoznam pripojených dokumentov a obrázkov nebude vrátený.</summary>
        public bool GetFileRepository { get; set; } = false;

        /// <summary>
        /// Určuje, či sa majú načítať aj binárne údaje (najčastejšie obrázkov) pripojených dokumentov a obrázkov (príloh). 
        /// Z dôvodu výkonu je možné obmedziť načítanie binárnych údajov pri prvom načítaní obchodného partnera, 
        /// ktoré je možné načítať dodatočne.
        /// </summary>
        public int GetFileRepositoryBinaryDataMode { get; set; }

        /// <summary>
        /// Filter na načítanie binárnych údajov pripojených dokumentov - zoznam jednoznačných identifikátorov. 
        /// </summary>
        public List<long> GetFileRepositoryBinaryDataFilterIDNums { get; set; }

        /// <summary>Určuje spôsob vyhľadania obchodného partnera.</summary>
        public enum enm_BusinessPartnerFindMethod : byte
        {
            /// <summary>Vyhľadá obchodného partnera podľa jednoznačného identifikátora systému OBERON (číslo IDNum).</summary>
            Identificator = 0,
            /// <summary>Vyhľadá obchodného partnera podľa IČO.</summary>
            IdentificationNumber = 1,
            /// <summary>Vyhľadá obchodného partnera podľa čiarového kódu.</summary>
            BarCode = 2,
            /// <summary>Vyhľadá obchodného partnera podľa časti názvu - názov obsahuje hľadané hoddnoty, ktoré môžu byť oddelené medzerou.</summary>
            NameContains = 8,
            /// <summary>Vyhľadá obchodného partnera podľa názvu. Názov musí byť zadaný presne, t.j. musí sa zhodovať s názvom obchodného partnera.</summary>
            Name = 9
        }

    }
}

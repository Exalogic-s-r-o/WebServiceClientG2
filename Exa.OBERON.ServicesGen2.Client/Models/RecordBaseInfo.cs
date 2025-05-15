using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Exa.OBERON.ServicesGen2.Client.Models
{

    /// <summary>Spravidla sa používa v návratovej hodnote <see cref="ResultValue"></see> vo vlastnosti Data (Data As RecordBaseInfo). Je využívaná hlavne ako časť návratovej hodnoty pri metódach, ktoré vytvárajú nové záznamy - po vytvorení záznamu sa vracia jeho jednoznačný identifikátor a napr. číslo dokladu.</summary>
    public class RecordBaseInfo
    {

        /// <summary>Jednoznačný identifikátor nového záznamu.</summary>
        public long IDNum { get; set; }

        /// <summary>Jednoznačný identifikátor typu GUID nového záznamu, ak evidencia podporuje daný typ identifikátora.</summary>
        public string GUID { get; set; }

        /// <summary>Spravidla číslo nového záznamu, napr. pri objednávke číslo novej vytvorenej objednávky.</summary>
        public string Value { get; set; }

        /// <summary>Ďalšie doplňujúce info k novovytvorenému záznamu.</summary>
        public string Info { get; set; }

    }
}

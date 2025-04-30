using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Exa.OBERON.ServicesGen2.Client.Models.Login
{

    /// <summary>Obsahuje údaje o prihlásenom používateľovi, napr. meno uvádzané na dokladoch, prihlasovacie meno ak sa prihlasuje cez PIN a ďalšie údaje.
    /// Je to návratová hodnota prihlásenia používateľa do webovej služby.</see>.</summary>
    public partial class UserInfo
    {

        /// <summary>
        /// V prípade úspešného prihlásenia tzv. GUID daného používateľa (session používateľa), ktorý je nevyhnutný pre ďalšiu komunikáciu (musí byť súčasťou hlavičky danej požiadavky). 
        /// </summary>
        public string GUID { get; set; }

        /// <summary>Prihlasovacie meno používateľa do systému OBERON.</summary>
        public string UserName { get; set; }

        /// <summary>Jednoznačný identifikátor používateľa v systéme OBERON. Používa sa napr. na detekciu, 
        /// či daný záznam má označený daný používateľ.</see>/>).</summary>
        public long UserIDNum { get; set; }

        /// <summary>
        /// Typ hesla, napr. štandardné alebo PIN.
        /// </summary>
        public int PasswordType { get; set; }

        /// <summary>Meno používateľa uvádzané na dokladoch.</summary>
        public string DocumentName { get; set; }

        /// <summary>Skratka používateľa.</summary>
        public string ShortName { get; set; }

        /// <summary>Prevádzka používateľa. Doklady sa v systéme OBERON môžu členiť na prevádzky (názov prevádzky môže ovplyvňovať aj číslovanie dokladov, napr. faktúr).</summary>
        public string BranchName { get; set; }

    }
}

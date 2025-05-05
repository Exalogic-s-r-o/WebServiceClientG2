using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Exa.OBERON.ServicesGen2.Client.Models.BillJournal
{

    /// <summary>
    /// Trieda sa používa ako návratová hodnota pre načítanie súhnnu predaja.
    /// </summary>
    public class BillJournalSummaryItemsData
    {

        /// <summary>
        /// Obsahuje zoznam zosumarizovaných predaných položiek.
        /// </summary>
        public List<BillJournalSummaryItem> Items { get; set; }
              
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Exa.OBERON.ServicesGen2.Client.Models.BillJournal
{

    /// <summary>
    /// Trieda sa používa ako návratová hodnota pre načítanie súhrnu predaja.
    /// Obsahuje súhrn predaja jednej skladovej (predanej) položky.
    /// </summary>
    public class BillJournalSummaryItem
    {
        /// <summary>Sklad (meno), z ktorého predaná položka pochádza. Ak údaj nie je zadaný, ide o neskladovú položku.</summary>     
        public string StockName { get; set; }

        /// <summary>Jednoznačný identifikátor skladovej karty. Ak je nula, ide o neskladovú položku.</summary>
        public long IDNumStockCard { get; set; }

        /// <summary>Číslo skladovej (predanej) položky.</summary>
        public string ItemNumber { get; set; }

        /// <summary>Názov skladovej (predanej) položky.</summary>
        public string ItemName { get; set; }

        /// <summary>Názov variantu karty.</summary>
        public string VariantName { get; set; }

        /// <summary>Merná jednotka.</summary>
        public string Unit { get; set; }

        /// <summary>Klasifikácia produkcie.</summary>
        public string KP { get; set; }

        /// <summary>Čiarový kód.</summary>
        public string Barcode { get; set; }

        /// <summary>Skladová skupina - priradenie položky ku skladovej skupine.</summary>
        public string StockGroup { get; set; }

        ///<summary>Tovarová skupina - priradenie položky ku tovarovej skupine. Umožňuje členiť predané položky (tržby) podľa ďalšieho hľadiska.</summary>
        public string CommodityGroup { get; set; }
        
        /// <summary>Stromové kategórie skladovej karty.</summary>
        public string[] TreeCategories { get; set; }

        /// <summary>Colný sadzobník - číslo (kód) nominálnej nomenklatúry.</summary>
        public string CustomsTariffCode { get; set; }

        /// <summary>Účtovná (skladová) predkontácia.</summary>
        public int AccountCoding { get; set; }

        /// <summary>Kód pri predaji na pokladnici OBERON.</summary>
        public string CashRegisterCode { get; set; }

        /// <summary>Celkový obrat s DPH danej položky (po zľave).</summary>
        public decimal Turnover_WithVAT { get; set; }

        /// <summary>Celkový obrat s DPH danej položky pred zľavou.</summary>
        public decimal Turnover_WithVAT_WithoutDiscount { get; set; }

        /// <summary>Celkový obrat danej položky bez DPH (po zľave).</summary>
        public decimal Turnover_WithoutVAT { get; set; }

        /// <summary>Predané množstvo v základnej mernej jednotke.</summary>
        public decimal Turnover_Amount { get; set; }

    }
}

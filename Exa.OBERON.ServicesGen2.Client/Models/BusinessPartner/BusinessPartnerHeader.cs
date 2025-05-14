using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

namespace Exa.OBERON.ServicesGen2.Client.Models.BusinessPartner
{
    public abstract class BusinessPartnerHeader
    {

        public BusinessPartnerHeader()
        {
            this.Address = new AddressInfo();
        }

        /// <summary>
        /// Zoznam dostupných stĺpcov (databázových polí), s ktorými je možné v evidencii pracovať, napr. zotriediť záznamy pri načítaní, identifikáciu filtrov a podobne.
        /// </summary>
        public enum enm_BookColumns : int
        {

            /// <summary>Nezadané, neuvedené ...</summary>
            None = 0,

            /// <summary>IČO obchodného partnera.</summary>
            IdentificationNumber = 2,

            /// <summary>DIČ obchodného partnera.</summary>
            TaxNumber = 3,

            /// <summary>IČ DPH obchodného partnera.</summary>
            VATNumber = 4,

            /// <summary>Názov obchodného partnera (pomenovanie firmy).</summary>
            Name = 11,

            /// <summary>Obec obchodného partnera.</summary>
            City = 12,

            /// <summary>Skupina - skupina obchodného partnera.</summary>
            Group = 21,

            /// <summary>Telefónne číslo obchodného partnera.</summary>
            Phone = 25,

            /// <summary>Dátum a čas poslednej zmeny.</summary>
            DateTime_LastUpdate = 91

            // --------------------------------------------------------------------------------------------------------
            // --- Nasledujú špeciálne stĺpce používané len v špeciálnych filtroch
            // --------------------------------------------------------------------------------------------------------


            // ''' <summary>Len vysporiadané - špeciálny virtuálny stĺpec, používa sa pri identifikácii filtra.</summary>
            // SpecialFilter_Settled = 1001

            // ''' <summary>Len nevysporiadané - špeciálny virtuálny stĺpec, používa sa pri identifikácii filtra.</summary>
            // SpecialFilter_Unsettled = 1002

        }

        /// <summary>Typ predajcu, napr. maloobchod, veľkoobchod. Používa sa aj v hlásení SBL pre colný úrad.</summary>
        public enum enm_BussinesPartner_Vendor_Type
        {
            /// <summary>Neuvedené.</summary>
            Not_Set = 99,
            /// <summary>Veľkoobchod.</summary>
            Wholesaler = 1,
            /// <summary>Maloobchod.</summary>
            Retailer = 0
        }

        /// <summary>Jednoznačný identifikátor záznamu v systéme OBERON.</summary>
        public long IDNum { get; set; }

        /// <summary>Názov obchodného partnera.</summary>
        public string Name { get; set; }

        /// <summary>Hlavná adresa obchodného partnera (sídlo).</summary>
        public AddressInfo Address { get; set; }

        /// <summary>Identifikačné číslo partnera tzv. IČO.</summary>
        public string IdentificationNumber { get; set; }

        /// <summary>Identifikačné číslo partnera pre DPH tzv. IČ DPH.</summary>
        public string IdentificationNumberVat { get; set; }

        /// <summary>Daňové identifikačné číslo partnera tzv. DIČ.</summary>
        public string IdentificationNumberTax { get; set; }

        /// <summary>Telefónne číslo - hlavné kontaktné tel. číslo obchodného partnera.</summary>
        public string PhoneNumber { get; set; }

        /// <summary>E-mail - hlavná e-mailová adresa obchodného partnera.</summary>
        public string Email { get; set; }

        /// <summary>Kontaktná osoba.</summary>
        public string Contact { get; set; }

        /// <summary>Skupiny, do ktorých patrí obchodný partner. Môže byť priradený k jednej, alebo aj k viacerým skupinám. 
        /// Od skupiny môžu závisieť predajné ceny skladových položiek (pri používaní rozšírenej cenotvorby).</summary>
        public string[] PartnerGroups { get; set; }

        /// <summary>Internetová stránka.</summary>
        public string WebPage { get; set; }

        /// <summary>Vernostný systém - číslo vernostnej karty, napr. hodnota čiarového kódu EAN13.</summary>
        public string LSCardNumber { get; set; }

        /// <summary>Vernostný systém - aktuálny počet bodov na vernostej karte.</summary>
        public decimal LSTotalPoints { get; set; }

        /// <summary>Vernostný systém - dátum platnosti karty. Ak nebude dátum zadaný, platnosť karty je neobmedzená.</summary>
        public string LSExpirationDate { get; set; }

        /// <summary>Vernostný systém - predvolená cenová hladina. Ak bude 0, bude použítá podľa nastavenia systému.</summary>
        public byte LSPriceLevel { get; set; }

        /// <summary>Vernostný systém - percento zľavy z predajnej ceny.</summary>
        public decimal LSDiscountPercent { get; set; }

        /// <summary>Kredit Limit - hodnota (v základnej mene systému), ktorá umožňuje obchodnému partnerovi odobrať tovar aj bez zaplatenia (na faktúru).
        /// Pri prekročení sumy odobratého tovaru je možné používateľa upozorniť, aby odberateľovi tovar vo vyššej výške nevydal. 
        public decimal CreditLimit { get; set; }

        /// <summary>Colný úrad - číslo povolenia na predaj alkoholu.</summary>
        public string CustomsOfficeNumberSBL { get; set; }

        /// <summary>Typ predajcu, (napr. veľkoobchod). Používa sa aj v hlásení SBL pre colný úrad.</summary>
        public enm_BussinesPartner_Vendor_Type CustomsOfficeVendorType { get; set; }

        /// <summary>
        /// Zoznam dostupných stĺpcov (databázových polí), s ktorými je možné v evidencii pracovať, napr. zotriediť záznamy pri načítaní, identifikáciu filtrov a podobne.
        /// Tu sa nepoužíva, je len z dôvodu generovanie WebService na strane klienta (aby vygenerovalo aj tento ENUM - musí byť niekde použité).
        /// </summary>
        [CompilerGenerated()]
        public enm_BookColumns BookColumn { get; set; }


        /// <summary>
        /// Obsahuje údaje o adrese.
        /// </summary>
        public partial class AddressInfo
        {

            /// <summary>Ulica.</summary>
            public string Street;

            /// <summary>Mesto/Obec.</summary>
            public string City;

            /// <summary>Poštové smerovacie číslo.</summary>
            public string PostalCode;

        }
    }
}

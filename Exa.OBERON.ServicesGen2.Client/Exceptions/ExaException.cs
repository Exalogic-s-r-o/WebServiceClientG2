using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Exa.OBERON.ServicesGen2.Client.Exceptions
{
    /// <summary>Zoznam spoločných a často používaných chýb.</summary>
    public enum enm_ErrNumbers : int
    {

        /// <summary>OK.</summary>
        [System.ComponentModel.Description("OK.")]
        OK = 0,

        /// <summary>Iná chyba.</summary>
        [System.ComponentModel.Description("Iná chyba (preddefinovaný popis pre túto chybu nie je k dispozícii).")]
        Other = 1,

        /// <summary>Chyba pri inicializácii okna, formulár nebude možné zobraziť.</summary>
        [System.ComponentModel.Description("Chyba pri inicializácii okna, formulár nebude možné zobraziť.")]
        FormConstructorError = 9000,


        /// <summary>Hodnota mimo povolený rozsah.</summary>
        [System.ComponentModel.Description("Hodnota je mimo rozsah korektných hodnôt.")]
        ValueOutOfRange = 9020,

        /// <summary>Hodnota nie je zhodná.</summary>
        [System.ComponentModel.Description("Hodnota nie je zhodná.")]
        ValueNotEqual = 9025,


        /// <summary>Nedostatočné práva používateľa.</summary>
        [System.ComponentModel.Description("Nedostatočné práva používateľa.")]
        UserPrivilegeInsufficient = 9090,

        /// <summary>Chyba týkajúca sa licencie programu.</summary>
        [System.ComponentModel.Description("Nedostatočné licenčné oprávnenie.")]
        Licence = 9100,

        /// <summary>Dátum je mimo obdobia, napr. dátum nie je z aktuálneho obdobia, prípadne je mimo povolený rozsah.</summary>
        [System.ComponentModel.Description("Dátum nie je s korektného obdobia.")]
        DateOutOfRange = 9120,


        /// <summary>Funkcionalita nie je implementovaná.</summary>
        [System.ComponentModel.Description("Funkcionalita nie je implementovaná.")]
        NotImplemented = 9150,

        /// <summary>Funkcionalita nie je podporovaná.</summary>
        [System.ComponentModel.Description("Funkcionalita nie je podporovaná.")]
        NotSupported = 9160,

        /// <summary>Nebolo nájdené, neexistuje.</summary>
        [System.ComponentModel.Description("Hodnota nebola nájdená.")]
        NotFound = 9170,

        /// <summary>Akcia nebola vykonaná, napr. záznam nebol naimportovaný, aktualizovaný a podobne.</summary>
        [System.ComponentModel.Description("Akcia nebola vykonaná, napr. záznam nebol naimportovaný, aktualizovaný a podobne.")]
        NotSuccessful = 9175,

        /// <summary>Záznam (napr. súbor na disku) už existuje. Vyskytuje sa napr. pri operáciách kde nie je možné aby existovali zhodné záznamy.</summary>
        [System.ComponentModel.Description("Záznam už existuje (napr. súbor na disku).")]
        AlreadyExists = 9177,


        /// <summary>Nebol od-chytený event handler, pričom to daný objekt nevyhnutne potrebuje.</summary>
        [System.ComponentModel.Description("Povinné spracovanie udalosti nebolo vykonané.")]
        NeedHandleEvent = 9180,



        /// <summary>Zdroj údajov nebol inicializovaný.</summary>
        [System.ComponentModel.Description("Zdroj údajov nie je inicializovaný.")]
        SourceNotInitialized = 9190,

        /// <summary>Zdrojové údaje sú prázdne (boli inicializované ale prázdne).</summary>
        [System.ComponentModel.Description("Zdrojové údaje sú prázdne.")]
        SourceIsEmpty = 9191,

        /// <summary>Zdrojové údaje nie sú prázdne (napr. ak nebolo povolené/požiadané prepísanie existujúcich údajov).</summary>
        [System.ComponentModel.Description("Údaje nie sú prázdne, a nebolo požadované ich prepísanie.")]
        SourceNotEmpty = 9192,



        /// <summary>Vstupné hodnoty nie sú zadané napr. nezadaný hodnotový parameter funkcie.</summary>
        [System.ComponentModel.Description("Zadaná vstupná hodnota nie je zadaná.")]
        InputValueIsEmpty = 9200,

        /// <summary>Vrátená hodnota je nulová, prázdna, neboli vrátené žiadne údaje, metódou neboli vygenerované žiadne záznamy a podobne.</summary>
        [System.ComponentModel.Description("Vrátená hodnota je nulová, prázdna, neboli vrátené žiadne údaje, metódou neboli vygenerované žiadne záznamy a podobne.")]
        ReturnValueIsEmpty = 9205,

        /// <summary>Výsledok metódy môže obsahovať viac hodnôt, pričom je vrátená len jedna z nich.</summary>
        [System.ComponentModel.Description("Výsledok metódy môže obsahovať viac hodnôt, pričom je vrátená len jedna z nich.")]
        ExistMoreValues = 9208,

        /// <summary>Pri danej akcii (najčastejšie mazanie záznamov) existuje k danému záznamu relácia (väzba na inú evidenciu).</summary>
        [System.ComponentModel.Description("Pri danej akcii (najčastejšie mazanie záznamov) existuje k danému záznamu relácia (väzba na inú evidenciu).")]
        RelationExist = 9209,



        // --- Chyby pre bežiace/prebiehajúce operácie ---

        /// <summary>Chyba pri pripojení.</summary>
        [System.ComponentModel.Description("Chyba pri pripojení.")]
        ConnectionError = 9205,

        /// <summary>Vypršal čas operácie.</summary>
        [System.ComponentModel.Description("Časový limit operácie vypršal.")]
        TimeOut = 9210,

        /// <summary>IsBusy - Daný objekt je v tejto chvíli zaneprázdnený, vykonáva inú úlohu.</summary>
        [System.ComponentModel.Description("Objekt je v tejto chvíli zaneprázdnený.")]
        IsBusy = 9211,

        /// <summary>IsRunning - Pokus o opätovné spustenie už bežiacej úlohy, procesu.</summary>
        [System.ComponentModel.Description("Pokus o opätovné spustenie bežiaceho procesu.")]
        IsRunning = 9212,

        /// <summary>Požadovaný zdroj (napr. sériový port, disk) nie je pripravený na prácu.</summary>
        [System.ComponentModel.Description("Nie je pripravené na prácu.")]
        NotReady = 9215,



        /// <summary>Akcia bola zrušená používateľom.</summary>
        [System.ComponentModel.Description("Akcia bola zrušená používateľom.")]
        ActionCanceledByUser = 9999
    }


    /// <summary>
    /// Trieda obsahuje obecnú návratovú hodnotu volania danej metódy. Obsahuje návratovú hodnotu (Result) daného volania,
    /// v prípade chyby kód chyby ako aj textový popis chyby.
    /// </summary>
    [DebuggerStepThrough]
    public class ExaException
    {
        #region CONSTANTS

        public const string C_DEFAULT_DESCRIPTION_EMPTY = "Popis pre túto chybu nie je k dispozícii.";

        #endregion

        #region ENUMS

        /// <summary>
        /// Typ chyby, bližšie určuje ako sa zachová napr. zobrazenie MsgBox-u.
        /// </summary>
        public enum enm_ExceptionTypes : int
        {
            /// <summary>
            /// Príznak sa používa pre inicializáciu objektu (ExaException) metódou 'GetDefault', ktorý neobsahuje chybu napr. aby bolo možné nastaviť 'Result = True'. POZOR: Nepoužívať tento príznak (chyba bude sa nebude vyskytovať v u_InnerException), používa ho len metóda 'GetDefault'.
            /// </summary>
            [System.ComponentModel.Description("Default")]
            Default = 0,

            /// <summary>
            /// Napr. MsgBox zobrazí chybu s vlastným popisom + popis chyby.
            /// </summary>
            [System.ComponentModel.Description("Standard")]
            Standard = 1,

            /// <summary>
            /// Validačná chyba, hodnoty nie sú zadané korektne. MsgBox zobrazí len popis chyby s objektu ExaException.Description.
            /// </summary>
            [System.ComponentModel.Description("Validation")]
            Validation = 2,

            /// <summary>
            /// MsgBox sa už nesmie zobraziť, pretože už bol zobrazený (napr. v nižšej DLL-ke).
            /// </summary>
            [System.ComponentModel.Description("NoMsgBox")]
            NoMsgBox = 3
        }

        #endregion

        #region CONSTRUCTOR

        public ExaException()
        {        
        }

        public ExaException(string u_Message,
            int u_ErrNumber)
        {
            this.Message = u_Message;
            this.ErrNumber = u_ErrNumber;     
        }
        #endregion

        #region PROPERTIES

        /// <summary>Kód chyby.</summary>
        public int ErrNumber { get; set; } = 0;

        /// <summary>Výsledok FALSE - Chyba, TRUE - Bez chyby.</summary>
        public bool Result { get; set; }
      
        /// <summary>
        /// Text (popis) chyby.
        /// </summary>
        /// <returns>Textová hodnota chyby.</returns>
        public string Message { get; set; }
       
       
        #endregion

        #region METHODS

       
        /// <summary>Vráti chybu ako čistý text.</summary>
        /// <returns>Text chyby.</returns>
        [DebuggerStepThrough]
        public override string ToString()
        {
            string m_Str;
            if (Result == false)
                m_Str = "Err";          
            else
                m_Str = "OK";
            if (ErrNumber != 0)
                m_Str += ", ErrNum: " + ErrNumber.ToString();        
            return m_Str;
        }


        /// <summary>
        /// Vytvorí objekt 'ExaException' so zadanmi parametrami
        /// </summary>
        /// <param name="u_Message"></param>
        /// <param name="u_ExceptionType"></param>
        /// <param name="u_ErrNumber"></param>
        /// <param name="u_InnerException"></param>
        /// <returns></returns>
        public static ExaException Get(string u_Message,
                                       int u_ErrNumber = 0)
        {
            return new ExaException(
                                u_Message: u_Message,
                                u_ErrNumber: u_ErrNumber);
        }


        /// <summary>
        /// Vráti inštanciu objektu chyby (Result = FALSE).
        /// </summary>
        [DebuggerStepThrough]
        public static ExaException GetDefault()
        {
            return new ExaException(null, 0);
        }


        #endregion

    }
}

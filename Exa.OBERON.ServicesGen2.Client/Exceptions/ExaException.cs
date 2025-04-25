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

        public const int C_DEFAULT_ERROR_NUMBER = 0;
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
        public ExaException(string u_Message,
            int u_ErrNumber,
            enm_ExceptionTypes u_ExceptionTypes = enm_ExceptionTypes.Default,
            System.Exception u_InnerException = null)
        {
            this.Message = u_Message == null ? u_InnerException?.Message : u_Message;
            this.InnerException = u_InnerException;
            this.ErrNumber = u_ErrNumber;
            this.ExceptionType = u_ExceptionTypes;

            if (u_InnerException != null)
                Trace.TraceError(u_InnerException.Message);
        }
        #endregion

        #region PROPERTIES

        /// <summary>
        /// Vnútorná chyba - chyba, ktorá vznikla na nižšej úrovni.
        /// </summary>
        public System.Exception InnerException { get; set; }

        /// <summary>Kód chyby.</summary>
        public int ErrNumber { get; set; } = 0;

        /// <summary>Výsledok FALSE - Chyba, TRUE - Bez chyby.</summary>
        public bool Result { get; set; }

        /// <summary>Používa sa ako pomocný výsledok funkcie, ktorá prebehla úspešne ale je potrebné upozorniť na nejaký problém.</summary>
        /// <remarks>Text warning-u je možné zadať do vlastnosti description.</remarks>
        public bool IsWarning { get; set; }

        /// <summary>
        /// Je možné použiť pre vrátenie ľubovolnej návratovej hodnoty, narp. pri úspešnom volaní funkcie, napr. ID vytvoreného záznamu a podobe.
        /// </summary>
        public string ExceptionData { get; set; }

        /// <summary>
        /// Indikuje typ chyby, varovania alebo len správy, ktorá sa má zaznamenať do správ o priebehu (LOG-u).
        /// </summary>
        public TraceEventType TraceEventType
        {
            [DebuggerStepThrough]
            get
            {
                if (Result == true)
                    return TraceEventType.Information;
                else if (IsWarning == true)
                    return TraceEventType.Warning;
                else
                    return TraceEventType.Error;
            }
        }

        private string prp_Message;
        /// <summary>
        /// Text (popis) chyby.
        /// </summary>
        /// <returns>Textová hodnota chyby.</returns>
        public string Message
        {
            get
            {
                return prp_Message;
            }

            set
            {
                prp_Message = value;
            }
        }

        protected enm_ExceptionTypes prp_ExceptionType = enm_ExceptionTypes.Standard;
        /// <summary>
        /// Umožňuje určiť ako bude program zobrazovať hlásenie o chybe.
        /// </summary>
        public enm_ExceptionTypes ExceptionType
        {
            get
            {
                return this.prp_ExceptionType;
            }
            set
            {
                this.prp_ExceptionType = value;

                // Vymaž popis ktorý nie je zadaný
                if (this.prp_ExceptionType != enm_ExceptionTypes.Default)
                {
                    if (this.prp_Message == C_DEFAULT_DESCRIPTION_EMPTY)
                        this.prp_Message = null;
                }
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Nastaví popis chyby podľa zadaného parametra a zmení príznak 'IsWarning = TRUE', tiež umožňuje nastaviť aj kód chyby.
        /// </summary>
        /// <param name="u_ErrNumber">Kód chyby, najčastejšie <see cref="enm_ErrNumbers"/>.</param>
        [DebuggerStepThrough]
        public void SetWarning(string u_Message, int u_ErrNumber = 0)
        {
            IsWarning = true;
            Message = u_Message;

            if (u_ErrNumber != 0)
            {
                ErrNumber = u_ErrNumber;
            }
        }

        /// <summary>Vráti chybu ako čistý text.</summary>
        /// <returns>Text chyby.</returns>
        [DebuggerStepThrough]
        public override string ToString()
        {
            string m_Str;
            if (Result == false)
                m_Str = "Err";
            else if (IsWarning)
                m_Str = "Warn";
            else
                m_Str = "OK";
            if (ErrNumber != 0)
                m_Str += ", ErrNum: " + ErrNumber.ToString();
            if (InnerException != null)
                m_Str += ", " + InnerException.Message;
            return m_Str;
        }


        /// <summary>
        /// Vráti kompletný popis chyby, vrátane všetkých 'InnerExceptions'.
        /// </summary>
        public string GetFullMessage()
        {
            try
            {
                var tmp_SB_FullDescription = new StringBuilder();

                // Aktuálna chyba
                if ((Message ?? "") != (string.Empty ?? ""))
                    tmp_SB_FullDescription.Append(Message);

                // Vnorené chyby
                var m_CurInnerException = InnerException;

                do
                {
                    if (m_CurInnerException == null)
                        break;
                    if ((m_CurInnerException.Message ?? "") == (string.Empty ?? ""))
                        break;
                    if (tmp_SB_FullDescription.ToString().Contains(m_CurInnerException.Message) == false)
                    {
                        tmp_SB_FullDescription.AppendLine();
                        tmp_SB_FullDescription.Append(m_CurInnerException.Message);
                    }

                    m_CurInnerException = m_CurInnerException.InnerException;
                }
                while (true);

                return tmp_SB_FullDescription.ToString();
            }

            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Vráti triedu štandardnej chyby podľa daných parametrov - textu chyby a objektu <see cref="Exception"/>.
        /// </summary>
        /// <param name="u_Description">Text (popis) chyby.</param>
        /// <param name="u_InnerException">Trieda chyby, ku ktorej sa vzťahuje popis chyby.</param>
        /// <param name="u_ParamArray">Hodnoty, ktoré je možné automaticky vložiť do prvého parametra (používa sa príkaz String.Format).</param>
        /// <returns></returns>
        public static ExaException Get(string u_FmtMessage, System.Exception u_InnerException, params object[] u_ParamArray)
        {
            if (u_ParamArray.Length > 0)
            {
                try
                {
                    u_FmtMessage = string.Format(u_FmtMessage, u_ParamArray);
                }
                catch (System.Exception ex)
                {
                    u_FmtMessage = u_FmtMessage + " (Chybné parametre)" + ex.Message;
                }
            }
            return new ExaException(
                            u_Message: u_FmtMessage,
                            u_ErrNumber: u_InnerException.HResult,
                            u_ExceptionTypes: enm_ExceptionTypes.Default,
                            u_InnerException: u_InnerException);
        }

        /// <summary>
        /// Vráti triedu štandardnej chyby PPEEKK podľa daných parametrov - textu chyby a objektu <see cref="Exception"/>.
        /// </summary>
        /// <param name="u_FmtMessage">Text (popis) chyby. Ak je zadaná parameter array, mal by obsahovať formátovacie znaky.</param>
        /// <param name="u_ExaException">Trieda chyby, ku ktorej sa vzťahuje popis chyby.</param>
        /// <param name="u_ParamArray">Hodnoty, ktoré je možné automaticky vložiť do prvého parametra (používa sa príkaz String.Format).</param>
        public static ExaException Get(string u_FmtMessage, ExaException u_ExaException, params object[] u_ParamArray)
        {
            if (u_ParamArray.Length > 0)
            {
                try
                {
                    u_FmtMessage = string.Format(u_FmtMessage, u_ParamArray);
                }
                catch (System.Exception ex)
                {
                    u_FmtMessage = u_FmtMessage + " (Chybné parametre)" + ex.Message;
                }
            }
            return new ExaException(
                            u_Message: u_FmtMessage,
                            u_ErrNumber: u_ExaException.ErrNumber,
                            u_ExceptionTypes: u_ExaException == null ? enm_ExceptionTypes.Default : u_ExaException.ExceptionType,
                            u_InnerException: new System.Exception(u_FmtMessage));
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
                                        int u_ErrNumber = C_DEFAULT_ERROR_NUMBER,
                                        ExaException.enm_ExceptionTypes u_ExceptionType = enm_ExceptionTypes.Default,
                                        System.Exception u_InnerException = null)
        {
            return new ExaException(
                                u_Message: u_Message,
                                u_ErrNumber: u_ErrNumber,
                                u_ExceptionTypes: u_ExceptionType,
                                u_InnerException: u_InnerException);
        }


        /// <summary>
        /// Vráti inštanciu objektu chyby (Result = FALSE).
        /// </summary>
        [DebuggerStepThrough]
        public static ExaException GetDefault()
        {
            return new ExaException(null, 0);
        }

        public static ExaException GetOK(string u_WarningDesc = null, int u_ErrNumber = 0)
        {
            ExaException myEx = new ExaException(null, u_ErrNumber) { Result = true };

            if (string.IsNullOrWhiteSpace(u_WarningDesc) == false)
            {
                myEx.SetWarning(u_WarningDesc, u_ErrNumber);
            }

            return myEx;
        }


        /// <summary>
        /// Vyvolá chybu, umožňuje vyskočiť z bloku Try-Catch.
        /// </summary>
        /// <param name="u_Message">Textový popis chyby.</param>
        /// <param name="u_ErrNumber">Kód chyby.</param>
        [DebuggerStepThrough]
        public static void Throw(string u_Message, int u_ErrNumber = 0)
        {
            throw new Exa.OBERON.ServicesGen2.Client.Exceptions.ApplicationSystemException(u_Message, u_ErrNumber);
        }

        #endregion

    }
}

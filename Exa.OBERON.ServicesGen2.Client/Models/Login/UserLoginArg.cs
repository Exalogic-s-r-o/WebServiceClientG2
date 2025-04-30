using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Exa.OBERON.ServicesGen2.Client.Models.Login
{

    /// <summary>
    /// Parameter pre metódu prihlásenia používateľa do webovej služby.
    /// </summary>
    public class UserLoginArg
    {

        /// <summary>
        /// Meno používateľa. Pri použití prihlásením cez PIN (v OBERON-e sa zadáva do poľa Kód prístupoveho kľúča) sa 
        /// predpokladá, že meno použivateľa je neznáme - vtedy treba uviesť akýkoľvek textový idetifikátor, ktorý sa použije 
        /// pre získanie SALT, napr. ID zariadenia></see>).
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Hash hesla alebo PIN-u, prípadne hash (typu SHA-1) hesla doplnený o SALT (k salt-u sa pripojí heslo a následne sa vytvorí hash).
        /// V odôvodnených prípadoch je možné zasielať heslo aj ako čistý text, znižuje to však bezpečnosť webovej služby (umožňuje útočníkovi získať heslo - záleží však aj na nastavení spôsobu zabezpečenia webovej služby).
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Typ hesla, napr. štandardné alebo PIN.
        /// </summary>
        public int PasswordType { get; set; }

        /// <summary>
        /// Pomenovanie aplikácie, ktorá sa pripája k webovej službe.</see>. 
        /// Tým je možné napr. overiť zoznam pripojených externých zariadení do webovej služby.
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Číslo verzie aplikácie, ktorá sa pripája k webovej službe (odporúčame vo formáte x.x.x).</see>. 
        /// Tým je možné napr. overiť zoznam pripojených externých zariadení do webovej služby.
        /// </summary>
        public string ApplicationVersion { get; set; }

        /// <summary>Ľubovoľná textová hodnota - danú hodnotu si webová služba uloží a v prípade potreby poskytne klientovi.</see>. Tým je možné napr. overiť zoznam pripojených externých zariadení do webovej služby.</summary>
        public string LoginTag { get; set; }

        /// <summary>
        /// Kód jazyka pre preklad údajov, ktorých preklad aplikácia umožňuje napr. názvy skladových položiek, doplňujúce texty, skupiny (kategórie) v jedálnom lístku a podobne. 
        /// Ako hodnotu kódu jazyka je možné použiť dvojmiestny ISO kód jazyka podľa normy ISO-639-1.
        /// </summary>
        public string LanguageCode { get; set; }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Exa.OBERON.ServicesGen2.Client.Models.BillJournal
{

    /// <summary>
    /// Trieda sa používa ako parameter pre načítanie sumárneho zoznamu predaných položiek.
    /// </summary>
    public class BillJournalSummaryItemsArg
    {

        /// <summary>
        /// Názov jednej, alebo viacerých pokladníc, z ktorej údaje majú byť vrátené
        /// </summary>
        public List<String> CashRegisterNames { get; set; }

        /// <summary>
        /// Meno používateľa. Pri použití prihlásením cez PIN (v OBERON-e sa zadáva do poľa Kód prístupoveho kľúča) sa 
        /// predpokladá, že meno použivateľa je neznáme - vtedy treba uviesť akýkoľvek textový idetifikátor, ktorý sa použije 
        /// pre získanie SALT, napr. ID zariadenia></see>).
        /// </summary>
        public string DateTimeFrom { get; set; }

        /// <summary>
        /// Hash hesla alebo PIN-u, prípadne hash (typu SHA-1) hesla doplnený o SALT (k salt-u sa pripojí heslo a následne sa vytvorí hash).
        /// V odôvodnených prípadoch je možné zasielať heslo aj ako čistý text, znižuje to však bezpečnosť webovej služby (umožňuje útočníkovi získať heslo - záleží však aj na nastavení spôsobu zabezpečenia webovej služby).
        /// </summary>
        public string DateTimeTo { get; set; }

        /// <summary>
        /// TRUE - ak sa používajú aj varianty, jedna skladová karta bude vrátená pre každý variant. 
        /// FALSE - jedna skladová karta bude v zozname len jedenkrát, t.j. všetky varianty budú spolu v jednej položke.
        /// </summary>
        public bool SummaryByVariant { get; set; }

    }
}

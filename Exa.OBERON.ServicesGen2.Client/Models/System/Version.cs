using System;
using System.Collections.Generic;
using System.Text;

namespace Exa.OBERON.ServicesGen2.Client.Models.System
{
    public class Version
    {

        /// <summary>
        /// Verzia rozhrania webovej služby vo formáte x.x.x.
        /// </summary>
        public string ApiVersion;

        /// <summary>
        /// Dátum poslednej zmeny v rozhraní webovej služby vo formáte dd.mm.yyyy.
        /// </summary>
        public string ReleaseDate;

        /// <summary>
        /// Verzia OBERON-u, napr. Apríl/2014, pod ktorou je služba OBERON Web spustená.
        /// </summary>
        public string OBERONVersion;

		/// <summary>
		/// Dátum vydania verzie OBERON-u, pod ktorou je služba OBERON Web spustená.
		/// </summary>
		public string OBERONReleaseDate;

    }
}

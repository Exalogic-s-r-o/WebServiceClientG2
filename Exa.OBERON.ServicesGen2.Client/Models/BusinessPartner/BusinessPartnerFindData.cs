using System;
using System.Collections.Generic;
using System.Text;

namespace Exa.OBERON.ServicesGen2.Client.Models.BusinessPartner
{
    /// <summary>
    /// Trieda obsahuje nájdené údaje - zoznam nájdených (načítaných) obchodných partnerov.
    /// </summary>
    public class BusinessPartnerFindData
    {

        public BusinessPartnerFindData()
        {
            this.Items = new List<BusinessPartner>();
        }

        /// <summary>
        /// Zoznam načítaných (nájdených) obchodných partnerov.
        /// </summary>
        public List<BusinessPartner> Items { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Exa.OBERON.ServicesGen2.Client.Models.BusinessPartner
{
    /// <summary>
    /// Obsahuje údaje pre vytvorenie nového obchodného partnera - hlavičku záznamu, prípadne aj prevádzky.
    /// </summary>
    public class BusinessPartnerAddArg
    {

        public BusinessPartnerAddArg()
        {
            this.BusinessPartner = new BusinessPartner();
        }
        public BusinessPartner BusinessPartner { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Exa.OBERON.ServicesGen2.Client.Exceptions
{
    /// <summary>
    /// Systémová exception aj s kódom chyby (HResult).
    /// </summary>
    public class ApplicationSystemException : System.Exception
    {

        public ApplicationSystemException(string u_Message, int u_HRseult) : base(message: u_Message)
        {

            HResult = u_HRseult;
        }

    }
}

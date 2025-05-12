using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Exa.OBERON.ServicesGen2.Client.Models
{
    /// <summary>
    /// Návratová hodnota daného volania. Obsahuje výsledný result, text chyby a spravidla aj nejakú návratovú hodnotu (Data ako T).
    /// </summary>
    /// <typeparam name="T">Obsahuje návratovú hodnotu - dátového typu (prípadne objekt) podľa daného volania.</typeparam>

    public class ResultModel<T>
    {
        [DebuggerStepThrough]
        public ResultModel()
        {

        }

        #region PROPERTIES
        public bool result { get; set; }
        public int errNumber { get; set; }
        public string description { get; set; }
        public T data { get; set; }

        #endregion


        public void FromExaException(Exa.OBERON.ServicesGen2.Client.Exceptions.ExaException exaException)
        {
            result = exaException.Result;
            errNumber = exaException.ErrNumber;
            description = exaException.Message;
        }

        public void FromResult(bool result,
                               int errNumber,
                               string description,
                               bool isWarning)
        {
            this.result = result;
            this.errNumber = errNumber;
            this.description = description;
        }

        public Exa.OBERON.ServicesGen2.Client.Exceptions.ExaException ToExaException()
        {
            Exa.OBERON.ServicesGen2.Client.Exceptions.ExaException myEx;
            myEx = new Exa.OBERON.ServicesGen2.Client.Exceptions.ExaException(u_Message: description, u_ErrNumber: errNumber)
            {
                Result = result,
            };
            return myEx;
        }

    }
}

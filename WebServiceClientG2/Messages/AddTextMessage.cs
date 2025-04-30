using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceClientG2.Messages
{
    public class AddTextMessage : ValueChangedMessage<string>
    {
        public AddTextMessage(string value) : base(value)
        {

        }

    }
}

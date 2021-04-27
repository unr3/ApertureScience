using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.Library.Messaging.Abstraction
{
   public interface IMessage
    {
        string MessageId { get; }
        object Body { get;  }
        string MessageType { get; }
        TBody BodyAs<TBody>();
    }
}

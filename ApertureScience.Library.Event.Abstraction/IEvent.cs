using System;
using System.Collections.Generic;
using System.Text;

namespace ApertureScience.Library.Event.Abstraction
{
   public interface IEvent
    {
        string Id { get;}
        string Name { get; }
        DateTime DateTimeUTC { get; }
        

    }
}

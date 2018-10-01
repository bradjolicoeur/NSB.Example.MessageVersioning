using System;
using System.Collections.Generic;
using System.Text;

namespace NSB.Example.Contracts.Events
{
    public interface IExampleEvent
    {
        string Message { get; set; }
    }
}

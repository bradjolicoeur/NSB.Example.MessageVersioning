using System;
using System.Collections.Generic;
using System.Text;

namespace NSB.Example.Contracts.Events
{
    public interface IExampleEventV2 : IExampleEvent
    {
        string NewProperty { get; set; }
    }
}

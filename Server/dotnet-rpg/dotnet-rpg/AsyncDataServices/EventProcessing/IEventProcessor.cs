using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.AsyncDataServices.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}

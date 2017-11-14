using CurrencyTrader.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyTrader.AdoNet
{
    //This class will allow the store trades function on the GUI to work
    //asynchronously with the other functions on the GUI.
    public class AsyncTradeStorage : ITradeStorage
    {
        private readonly ILogger logger;
        private ITradeStorage SynchTradeStorage;

        public AsyncTradeStorage(ILogger logger)
        {
            this.logger = logger;
            SynchTradeStorage = new AdoNetTradeStorage(logger);
        }
        public void Persist(IEnumerable<TradeRecord> trades)
        {
            logger.LogInfo("Starting synchronous trade storage.");
            //This task separates the asynchronity from the rest of the 
            //GUI's functions.
            Task.Run(()=> SynchTradeStorage.Persist(trades));
        }
    }
}

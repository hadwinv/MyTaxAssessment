using System.Collections.Generic;
using Assessment.Payspace.Tax.Interface.Logic;

namespace Assessment.Payspace.Tax.Logic.Core
{
    public class TaxController<K, T> : ITaxController<K, T>
       where K : Key
       where T : TaxEventHandler
    {
        private readonly Dictionary<string, T> _eventHandlers;

        public TaxController()
        {
            _eventHandlers = new Dictionary<string, T>();
        }

        public void AddEventHandler(K key, T typeHandler)
        {
            _eventHandlers.Add(key.TaxCode, typeHandler);
        }

        public decimal ExecuteEvent(string source, decimal amount)
        {
            return _eventHandlers[source].TaxCalculator(amount);
        }
    }
}

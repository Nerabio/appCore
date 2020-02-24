using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AppServices.ChainOfResponsibility
{
    public class ChainCreator<T> : IChainCreator<T> where T : class, IChainHandler
    {
        private  IEnumerable<T> _handlers;

        private List<T> _handler = new List<T>();

        public ChainCreator(IEnumerable<T> handlers)
        {
            _handlers = handlers;
        }

        public T GetChain()
        {     
            foreach (T item in this._handlers) {
                var index = this._handlers.ToList().IndexOf(item);
                T nextElement = null;
                if (index != this._handlers.Count() - 1) {
                    index++;
                    nextElement = this._handlers.ElementAt(index); 
                }
                
                var x = item.SetNext(nextElement) as T;
                this._handler.Add(item);
            }

            return this._handler.FirstOrDefault() as T;
        }
    }
}

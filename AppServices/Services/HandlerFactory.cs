using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppServices.Services
{
    public class HandlerFactory<T> : IHandlerFactory<T> where T : class, IHandler
    {
        private readonly IEnumerable<T> _handlers;

        public HandlerFactory(IEnumerable<T> handlers)
        {
            _handlers = handlers;
        }

        public T GetHandler(string parameter)
        {
            return _handlers?.FirstOrDefault(h => h.CanHandle(parameter));
        }
    }

}

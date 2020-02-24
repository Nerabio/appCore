using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AppServices.ChainOfResponsibility
{
    // Поведение цепочки по умолчанию может быть реализовано внутри базового
    // класса обработчика.
    abstract public class AbstractHandler : IAbstractHandler
    {
        public IChainHandler _nextHandler;

        public IChainHandler SetNext(IChainHandler handler)
        {
            this._nextHandler = handler;

            // Возврат обработчика отсюда позволит связать обработчики простым
            // способом, вот так:
            // monkey.SetNext(squirrel).SetNext(dog);
            return handler;
        }

        public virtual object Handle(object request)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(request);
            }
            else
            {
                return null;
            }
        }
    }
}

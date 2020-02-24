using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.ChainOfResponsibility
{
    public interface IAbstractHandler
    {
        IChainHandler SetNext(IChainHandler handler);

        object Handle(object request);

    }
}

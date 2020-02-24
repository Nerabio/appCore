using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.ChainOfResponsibility
{
    public interface IChainCreator<T> where T : IChainHandler
    {
        T GetChain();
    }
}

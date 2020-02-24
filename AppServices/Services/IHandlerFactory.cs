using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Services
{
    public interface IHandlerFactory<T> where T : IHandler
    {
        T GetHandler(string parameter);
    }
}

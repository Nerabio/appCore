using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Services
{
    public interface IHandler
    {
        bool CanHandle(string parameter);
    }
}

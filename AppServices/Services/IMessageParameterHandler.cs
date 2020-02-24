using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Services
{
    public interface IMessageParameterHandler : IHandler
    {
        string GetValue(string str);
    }

}

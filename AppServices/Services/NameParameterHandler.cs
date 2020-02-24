using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Services
{
    public class NameParameterHandler : IMessageParameterHandler
    {
        public bool CanHandle(string parameter) => parameter == "xxx";

        public string GetValue(string str)
        {
            return str;
        }
    }

}

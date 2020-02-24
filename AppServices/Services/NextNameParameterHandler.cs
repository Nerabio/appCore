using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Services
{
    public class NextNameParameterHandler : IMessageParameterHandler
    {
        public bool CanHandle(string parameter) => parameter == "yyy";

        public string GetValue(string str)
        {
            return str;
        }
    }

}

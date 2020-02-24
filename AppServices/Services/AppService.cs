using System;
using System.Collections.Generic;
using System.Text;

namespace AppServices.Services
{
    public class AppService: IAppService
    {
        public string Send()
        {
            return "AppService say string";
        }
    }
}

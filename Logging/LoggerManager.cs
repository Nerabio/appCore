using System;
using System.Reflection;

namespace Logging
{
    public class LoggerManager
    {
        public static ILogger Create()
        {
            return new Logger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public static ILogger Create(Type type)
        {
            return new Logger(type);
        }
    }
}

using System.Collections.Generic;

namespace CL7Logger.Core
{
    public class CL7LoggerManager
    {
        public CL7LoggerManager()
        {
            ConnectionAttempts = new List<string>();
        }

        public List<string> ConnectionAttempts { get; private set; }
    }
}

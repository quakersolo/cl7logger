using System.Collections.Generic;

namespace CL7Logger.Common
{
    internal class ConnectionStringManager
    {
        public ConnectionStringManager()
        {
            ConnectionAttempts = new List<string>();
        }

        public List<string> ConnectionAttempts { get; private set; }
    }
}

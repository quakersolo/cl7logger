using System.Collections.Generic;

namespace CLogger.Common
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

using CL7Logger.Common;

namespace CLogger
{
    public class CLogOptions
    {
        public CLogOptions()
        {
            LogginInfo = new CLogInfo();
        }

        public CLogInfo LogginInfo { get; private set; }
        public string ConnectionString { get; set; }

        public string Path { get; set; } = "/log";
    }
}

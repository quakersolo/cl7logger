using CL7Logger.Common;

namespace CL7Logger
{
    public class CL7LogOptions
    {
        public CL7LogOptions()
        {
            LogginInfo = new CL7LogginInfo();
        }

        public CL7LogginInfo LogginInfo { get; private set; }
        public string ConnectionString { get; set; }

        public string Path { get; set; } = "/Logentries";
    }
}

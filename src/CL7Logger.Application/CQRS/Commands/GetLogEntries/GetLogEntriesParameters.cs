namespace CL7Logger.Application.CQRS.Commands.GetLogEntries
{
    public class GetLogEntriesParameters
    {
        public string ApplicationName { get; set; }
        public string ProcessId { get; set; }
        public int LogLevel { get; set; }
        public string User { get; set; }
    }
}

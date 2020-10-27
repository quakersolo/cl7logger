namespace CL7Logger.Persistence.Helpers
{
    public static class LoggerSqlHelper
    {
        public static string EnsureLogTable()
        {
            string tableName = "LogEntries";
            string query = string.Format(@"
if not exists (select 1 from sysobjects where xtype = 'U' and name = '{0}')
begin 
	create table {0}
	(
		""Id"" uniqueidentifier default newid() primary key,
		""ApplicationName"" varchar(50) not null,
		""TraceId"" uniqueidentifier not null,
		""LogLevel"" int not null,
		""LogLevelName"" varchar(50) not null,
		""Message"" varchar(5000) not null,
		""Detail"" varchar(5000),
		""Host"" varchar(50),
		""User"" varchar(50),
		""CreatedAt"" datetime not null
	)
end", tableName);

            return query;
        }
    }
}

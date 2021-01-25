namespace CLogger.Repositories.Helpers
{
    internal static class SqlCommandText
    {
        public const string LogEntryInsert = @"
INSERT INTO LogEntries (Id, ApplicationName, TraceId, LogEntryType, LogEntryTypeName, Message, Detail, Host, UserId, CreatedAt) 
VALUES(@Id, @ApplicationName, @TraceId, @LogEntryType, @LogEntryTypeName, @Message, @Detail, @Host, @UserId, @CreatedAt);";

        public const string CreateTableIfNotExists = @"
IF NOT EXISTS (select 1 from sysobjects where xtype = 'U' and name = 'LogEntries')
BEGIN 
	CREATE TABLE LogEntries
	(
		""Id"" uniqueidentifier primary key,
		""ApplicationName"" varchar(50) not null,
		""TraceId"" uniqueidentifier not null,
		""LogEntryType"" int not null,
		""LogEntryTypeName"" varchar(50) not null,
		""Message"" varchar(max) not null,
		""Detail"" varchar(max),
		""Host"" varchar(50),
		""UserId"" varchar(50),
		""CreatedAt"" datetime not null
	)
END";

        public const string LogEntryList = @"
SELECT TOP 200 Id, ApplicationName, TraceId, LogEntryType, LogEntryTypeName, Message, Detail, Host, UserId, CreatedAt 
FROM LogEntries
WHERE 
	(ApplicationName = @ApplicationName)
	AND (@LogEntryId = '00000000-0000-0000-0000-000000000000' or Id=@LogEntryId)
	AND (@TraceId = '00000000-0000-0000-0000-000000000000' or TraceId=@TraceId)
	AND (@LogEntryType = -99 OR LogEntryType = @LogEntryType)
ORDER BY CreatedAt DESC;";
    }
}

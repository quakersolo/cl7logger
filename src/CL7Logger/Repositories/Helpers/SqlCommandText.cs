﻿namespace CL7Logger.Repositories.Helpers
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
		""Message"" varchar(5000) not null,
		""Detail"" varchar(5000),
		""Host"" varchar(50),
		""UserId"" varchar(50),
		""CreatedAt"" datetime not null
	)
END";

    }
}
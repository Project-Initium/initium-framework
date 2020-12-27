CREATE TABLE [$schema$].[User]
(
        [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY
    ,   [ExternalRef] NVARCHAR(100) NOT NULL
)

GO

CREATE INDEX [IX_$schema$_User_ExternalRef] ON [$schema$].[User] ([ExternalRef])

GO

CREATE VIEW [$schema$].[vwUser]
AS
SELECT *
FROM [$schema$].[User]

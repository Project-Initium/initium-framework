CREATE TABLE [dbo].[Tenant]
(
    Id           uniqueidentifier not null primary key,
    Identifier   nvarchar(max)    not null,
    Name         nvarchar(max)    not null,
    WhenDisabled datetime2
)

GO

CREATE VIEW [dbo].[vwTenant]
AS
SELECT
        t.Id
     ,	t.Name
     ,	t.Identifier
     ,	t.WhenDisabled
FROM [dbo].[Tenant] t

GO

CREATE PROCEDURE [dbo].[uspGetTenantInfoById]
    @id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT
            t.Id
         ,	t.Identifier
         ,	t.Name
    FROM [dbo].[Tenant] t
    WHERE t.Id = @id
END

GO

CREATE PROCEDURE [dbo].[uspGetTenantInfoByIdentifier]
    @identifier NVARCHAR(MAX)
AS
BEGIN
    SELECT
            t.Id
         ,	t.Identifier
         ,	t.Name
    FROM [dbo].[Tenant] t
    WHERE t.Identifier = @identifier
END

GO

CREATE PROCEDURE [dbo].[uspGetAllTenantInfos]
AS
BEGIN
    SELECT
            t.Id
         ,	t.Identifier
         ,	t.Name
    FROM [dbo].[Tenant] t
END

GO
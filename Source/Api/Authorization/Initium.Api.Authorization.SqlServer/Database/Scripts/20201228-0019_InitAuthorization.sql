IF OBJECT_ID(N'dbo.Resource', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Resource]
    (
            [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY
        ,   [Name] NVARCHAR(100) NOT NULL
        ,   [NormalizedName] NVARCHAR(100) NOT NULL
        ,   [ParentResourceId] UNIQUEIDENTIFIER NULL
        ,   [FeatureCode] NVARCHAR(50) NULL
        ,   CONSTRAINT [FK_Resource_Resource] FOREIGN KEY ([dbo]) REFERENCES [AccessProtection].[Resource]([Id])
    )

    GO

    CREATE INDEX [IX_Resource_ParentResourceId] ON [dbo].[Resource] ([ParentResourceId])

    GO

END;

GO

CREATE TABLE [$schema$].[Role]
(
        [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY
    ,	[Name] NVARCHAR(100) NOT NULL
)

GO

CREATE TABLE [$schema$].[RoleResource]
(
        [RoleId] UNIQUEIDENTIFIER NOT NULL
    ,   [ResourceId] UNIQUEIDENTIFIER NOT NULL
    ,   PRIMARY KEY ([RoleId], [ResourceId])
    ,   CONSTRAINT [FK_$schema$_RoleResource_ResourceId] FOREIGN KEY ([ResourceId]) REFERENCES [$schema$].[Resource]([Id])
    ,   CONSTRAINT [FK_$schema$_RoleResource_Tenant] FOREIGN KEY ([TenantId]) REFERENCES [$schema$].[Tenant]([Id])
)

GO

CREATE VIEW [$schema$].vwAuthorizedUser
AS 
SELECT *
FROM [$schema$].[User]

GO 

CREATE TABLE [$schema$].UserRole
{
        [UserId] UNIQUEIDENTIFIER NOT NULL
    ,   [RoleId] UNIQUEIDENTIFIER NOT NULL
    ,   PRIMARY KEY ([UserId], [RoleId])
    ,   CONSTRAINT [FK_$schema$_UserRole_User] FOREIGN KEY ([UserId]) REFERENCES [$schema$].[User]([Id])
    ,   CONSTRAINT [FK_$schema$_UserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [$schema$].[Role]([Id])
}
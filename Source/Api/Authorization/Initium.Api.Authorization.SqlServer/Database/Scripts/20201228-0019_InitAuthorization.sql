IF OBJECT_ID(N'dbo.Resource', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Resource]
    (
            [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY
        ,   [Name] NVARCHAR(100) NOT NULL
        ,   [NormalizedName] NVARCHAR(100) NOT NULL
        ,   [ParentResourceId] UNIQUEIDENTIFIER NULL
        ,   CONSTRAINT [FK_Resource_Resource] FOREIGN KEY ([ParentResourceId]) REFERENCES [dbo].[Resource]([Id])
    );

    CREATE INDEX [IX_Resource_ParentResourceId] ON [dbo].[Resource] ([ParentResourceId]);
END

GO

CREATE TABLE [$schema$].[Role]
(
        [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY
    ,	[Name] NVARCHAR(100) NOT NULL
)

GO

CREATE VIEW [$schema$].[vwRole]
AS
SELECT *
FROM [$schema$].[Role]

GO

CREATE TABLE [$schema$].[RoleResource]
(
        [RoleId] UNIQUEIDENTIFIER NOT NULL
    ,   [ResourceId] UNIQUEIDENTIFIER NOT NULL
    ,   PRIMARY KEY ([RoleId], [ResourceId])
    ,   CONSTRAINT [FK_$schema$_RoleResource_ResourceId] FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[Resource]([Id])
)

GO


CREATE VIEW [$schema$].[vwRoleResource]
AS
SELECT *
FROM [$schema$].[RoleResource]

GO

CREATE VIEW [$schema$].vwAuthorizedUser
AS 
SELECT *
FROM [$schema$].[User]

GO 

CREATE TABLE [$schema$].UserRole
(
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_$schema$_UserRole_User] FOREIGN KEY ([UserId]) REFERENCES [$schema$].[User] ([Id]),
    CONSTRAINT [FK_$schema$_UserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [$schema$].[Role] ([Id])
)

GO


CREATE VIEW [$schema$].[vwUserRole]
AS
SELECT *
FROM [$schema$].[UserRole]

GO
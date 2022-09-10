USE Adventure
GO

IF NOT EXISTS(SELECT *
              FROM sysobjects
              WHERE name = 'UserAdventure'
                and xtype = 'U')
    BEGIN
        CREATE TABLE UserAdventure
        (
            UserId UNIQUEIDENTIFIER NOT NULL,
            TreeId UNIQUEIDENTIFIER NOT NULL,
            NodeId UNIQUEIDENTIFIER NOT NULL,
            CONSTRAINT PK_UserAdventure_1 PRIMARY KEY CLUSTERED
                (NodeId ASC)
        )
    END
GO

CREATE NONCLUSTERED INDEX [IX_UserAdventure] ON UserAdventure
    (UserId ASC,
     TreeId ASC)
GO


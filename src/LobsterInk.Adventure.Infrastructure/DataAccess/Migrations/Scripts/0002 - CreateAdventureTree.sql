USE Adventure
GO

IF NOT EXISTS(SELECT *
              FROM sysobjects
              WHERE name = 'AdventureTree'
                and xtype = 'U')
    BEGIN
        CREATE TABLE AdventureTree
        (
            TreeId    UNIQUEIDENTIFIER NOT NULL,
            Name      NVARCHAR(MAX)    NOT NULL,
            UserId    UNIQUEIDENTIFIER NOT NULL,
            CreatedOn DATETIME2(7)     NOT NULL,
            CONSTRAINT [PK_AdventureTree] PRIMARY KEY CLUSTERED
                (TreeId ASC)
        )
    END
GO

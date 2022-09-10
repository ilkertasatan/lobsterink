USE Adventure
GO

IF NOT EXISTS(SELECT *
              FROM sysobjects
              WHERE name = 'AdventureTreeNode'
                and xtype = 'U')
    BEGIN
        CREATE TABLE AdventureTreeNode
        (
            NodeId       UNIQUEIDENTIFIER NOT NULL,
            Name         NVARCHAR(MAX)    NOT NULL,
            ParentNodeId UNIQUEIDENTIFIER NULL,
            TreeId       UNIQUEIDENTIFIER NOT NULL,
            CONSTRAINT [PK_AdventureTreeNode] PRIMARY KEY CLUSTERED
                (NodeId ASC)
        )
    END
GO

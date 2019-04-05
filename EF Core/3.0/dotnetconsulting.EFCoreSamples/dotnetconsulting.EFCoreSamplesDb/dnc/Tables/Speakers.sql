CREATE TABLE [dnc].[Speakers] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Homepage]  NVARCHAR (MAX) NULL,
    [Infos]     NVARCHAR (MAX) DEFAULT (N'(Keine Infos)') NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    [Created]   DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [IsDeleted] BIT            DEFAULT ((0)) NOT NULL,
    [Updated]   DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Speakers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


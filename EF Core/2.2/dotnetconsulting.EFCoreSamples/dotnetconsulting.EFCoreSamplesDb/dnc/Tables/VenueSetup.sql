CREATE TABLE [dnc].[VenueSetup] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [Created]     DATETIME2 (7)  DEFAULT ('0001-01-01T00:00:00.000') NOT NULL,
    [IsDeleted]   BIT            DEFAULT ((0)) NOT NULL,
    [Updated]     DATETIME2 (7)  NULL,
    CONSTRAINT [PK_VenueSetup] PRIMARY KEY CLUSTERED ([Id] ASC)
);


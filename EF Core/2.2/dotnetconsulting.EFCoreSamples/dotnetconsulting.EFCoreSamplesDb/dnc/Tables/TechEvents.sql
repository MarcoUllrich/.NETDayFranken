CREATE TABLE [dnc].[TechEvents] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Begin]        DATETIME2 (7)   NOT NULL,
    [End]          DATETIME2 (7)   NOT NULL,
    [ImageUrl]     NVARCHAR (MAX)  NULL,
    [Name]         NVARCHAR (MAX)  NULL,
    [Price]        DECIMAL (18, 2) NOT NULL,
    [Venue]        NVARCHAR (MAX)  NULL,
    [VenueSetupId] INT             NULL,
    [WebSite]      NVARCHAR (MAX)  NULL,
    [SecretCode]   VARCHAR (50)    NULL,
    [Created]      DATETIME2 (7)   DEFAULT ('0001-01-01T00:00:00.000') NOT NULL,
    [IsDeleted]    BIT             DEFAULT ((0)) NOT NULL,
    [Updated]      DATETIME2 (7)   NULL,
    CONSTRAINT [PK_TechEvents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TechEvents_VenueSetup_VenueSetupId] FOREIGN KEY ([VenueSetupId]) REFERENCES [dnc].[VenueSetup] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TechEvents_VenueSetupId]
    ON [dnc].[TechEvents]([VenueSetupId] ASC) WHERE ([VenueSetupId] IS NOT NULL);


CREATE TABLE [dnc].[Sessions] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [ContentDescription] NVARCHAR (300) NOT NULL,
    [Begin]              TIME (7)       NOT NULL,
    [Created]            DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [Difficulty]         INT            NOT NULL,
    [Duration]           INT            NOT NULL,
    [End]                DATETIME2 (7)  NOT NULL,
    [EventId]            INT            NULL,
    [SpeakerId]          INT            NOT NULL,
    [TechEventId]        INT            NOT NULL,
    [Title]              NVARCHAR (450) NULL,
    [IsDeleted]          BIT            DEFAULT ((0)) NOT NULL,
    [Updated]            DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Sessions_TechEvents_TechEventId] FOREIGN KEY ([TechEventId]) REFERENCES [dnc].[TechEvents] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Sessions_TechEventId]
    ON [dnc].[Sessions]([TechEventId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Sessions_Title]
    ON [dnc].[Sessions]([Title] ASC);


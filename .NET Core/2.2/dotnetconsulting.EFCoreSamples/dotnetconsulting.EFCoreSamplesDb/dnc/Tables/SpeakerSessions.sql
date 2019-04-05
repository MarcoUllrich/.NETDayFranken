CREATE TABLE [dnc].[SpeakerSessions] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [SessionId] INT            NOT NULL,
    [SpeakerId] INT            NOT NULL,
    [Tag]       NVARCHAR (MAX) NULL,
    [Created]   DATETIME2 (7)  DEFAULT ('0001-01-01T00:00:00.000') NOT NULL,
    [IsDeleted] BIT            DEFAULT ((0)) NOT NULL,
    [Updated]   DATETIME2 (7)  NULL,
    CONSTRAINT [PK_SpeakerSessions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SpeakerSessions_Sessions_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [dnc].[Sessions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SpeakerSessions_Speakers_SpeakerId] FOREIGN KEY ([SpeakerId]) REFERENCES [dnc].[Speakers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_SpeakerSessions_SessionId]
    ON [dnc].[SpeakerSessions]([SessionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SpeakerSessions_SpeakerId]
    ON [dnc].[SpeakerSessions]([SpeakerId] ASC);


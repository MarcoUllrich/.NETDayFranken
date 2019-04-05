CREATE   VIEW [dnc].[vwSpeakerStats]
AS
SELECT s.[Name],
       (SELECT COUNT(*) FROM [dnc].[SpeakerSessions] WHERE[SpeakerId] = s.[Id])  AS 'SessionCount'
FROM[dnc].[Speakers] s;
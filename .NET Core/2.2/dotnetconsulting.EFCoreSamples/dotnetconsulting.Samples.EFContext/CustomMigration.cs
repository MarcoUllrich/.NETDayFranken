// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using Microsoft.EntityFrameworkCore.Migrations;
using dotnetconsulting.Samples.EFContext.SqlResources;

namespace dotnetconsulting.Samples.EFContext.Migrations
{
    public static class MigrationExtentions
    {
        public static void ApplyCustomUp(this MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlResource("dotnetconsulting.Samples.EFContext.SqlResources.StringLikeUp.sql");
            migrationBuilder.SqlResource("dotnetconsulting.Samples.EFContext.SqlResources.GetSpeakerUp.sql");
            migrationBuilder.SqlResource("dotnetconsulting.Samples.EFContext.SqlResources.vwSpeakerStatsUp.sql");
            // migrationBuilder.Sql("ALTER TABLE [dnc].[TechEvents] ADD SecretCode VARCHAR(50);");
        }

        public static void ApplyCustomDown(this MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlResource("dotnetconsulting.Samples.EFContext.SqlResources.StringLikeDown.sql");
            migrationBuilder.SqlResource("dotnetconsulting.Samples.EFContext.SqlResources.GetSpeakerDown.sql");
            migrationBuilder.SqlResource("dotnetconsulting.Samples.EFContext.SqlResources.vwSpeakerStatsDown.sql");
            // migrationBuilder.Sql("ALTER TABLE [dnc].[TechEvents] DROP COLUMN SecretCode;");
        }
    }
}
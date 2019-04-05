// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;
using System.Reflection;

namespace dotnetconsulting.Samples.EFContext.SqlResources
{
    public static class MigrationBuilderExtentions
    {
        public static void SqlResource(this MigrationBuilder migrationBuilder, Assembly Assembly, string ResourceName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(ResourceName))
                using (StreamReader reader = new StreamReader(stream))
                    migrationBuilder.Sql(reader.ReadToEnd());
        }

        public static void SqlResource(this MigrationBuilder migrationBuilder, string ResourceName)
        {
            migrationBuilder.SqlResource(Assembly.GetExecutingAssembly(), ResourceName);
        }
    }
}
// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using dotnetconsulting.Samples.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using dotnetconsulting.Samples.Domains;
using System.Data;
using System.Diagnostics;

namespace dotnetconsulting.Samples.Gui.DemoJobs
{
    public class DirectSql : IDemoJob
    {
        private readonly ILogger<DirectSql> _logger;
        private readonly SamplesContext1 _efContext;

        public DirectSql(ILogger<DirectSql> logger, SamplesContext1 efContext)
        {
            _logger = logger;
            _efContext = efContext;
        }

        public string Title => "Direct SQL";

        public void Run()
        {
            Debugger.Break();

            #region Entitäten per Stored Procedure laden
            Console.Clear();
            // Parameter definieren
            SqlParameter searchTerm = new SqlParameter("@SearchTerm", SqlDbType.VarChar, 50);
            searchTerm.Value = "*dolo*";

            // Ausführen
            List<Speaker> query1 = _efContext.Speakers
                .FromSql("EXEC dbo.usp_GetSpeaker @SearchTerm", searchTerm)
                .ToList();

            foreach (Speaker speaker in query1)
            {
                Console.WriteLine(speaker);
            }
            #endregion

            #region ExecuteSqlCommand
            Console.Clear();
            const string COMMANDUPDATE = @"UPDATE dnc.Speakers SET Infos = Infos + @Infos WHERE Id = @Id;";

            // Parameter definieren
            SqlParameter id = new SqlParameter("@Id", 3);
            SqlParameter infos = new SqlParameter("@Infos", "Neue Info");

            // Ausführen
            int rowsAffected  = _efContext.Database.ExecuteSqlCommand(COMMANDUPDATE, id, infos);
            Console.WriteLine($"rosAffected = {rowsAffected}");
            #endregion

            #region ADO.NET Core
            Console.Clear();
            const string COMMANDADONET = @"BACKUP DATABASE [dotnetconsulting.EFCoreSamples] TO DISK = 'c:\temp\EFCoreSamples.bak' WITH INIT;";

            // IDbCommand-Instanz vom Kontext geben lassen
            using (IDbCommand cmd = _efContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = COMMANDADONET;

                // Verbindung zur Datenbank öffnen
                _efContext.Database.OpenConnection();

                // Ausführen
                cmd.ExecuteNonQuery();
            }
            #endregion
        }
    }
}

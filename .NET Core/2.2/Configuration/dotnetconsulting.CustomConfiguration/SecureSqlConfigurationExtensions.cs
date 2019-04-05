// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using System.Data.SqlClient;

namespace dotnetconsulting.CustomConfiguration
{
    // Erweiterungsmethoden für das bequeme Anfügen
    public static class SqlConfigurationExtensions
    {
        public static IConfigurationBuilder AddSqlDatabase(this IConfigurationBuilder builder, string ConnectionString)
        {
            IConfigurationSource source = new SqlDatabaseConfigurationSource(ConnectionString);
            builder.Sources.Add(source);
            return builder;
        }
    }

    // Configuration Source, zum speichern des ConnectionString
    public class SqlDatabaseConfigurationSource : IConfigurationSource
    {
        private readonly string connectionString;

        public SqlDatabaseConfigurationSource(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            IConfigurationProvider configurationProvider = new SqlDatabaseConfigurationProvider(connectionString);
            return configurationProvider;
        }
    }

    // Kern für das Einlesen der Konfigurationswerte
    public class SqlDatabaseConfigurationProvider : IConfigurationProvider
    {
        private readonly string connectionString;
        private readonly IDictionary<string, string> values = new Dictionary<string, string>();

        public SqlDatabaseConfigurationProvider(string ConnectionString)
        {
            connectionString = ConnectionString;
        }

        public IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath)
        {
            // Eine Hierarchie wird nicht unterstützt
            return new List<string>();
        }

        private IChangeToken changeToken;
        public IChangeToken GetReloadToken()
        {
            if (changeToken == null)
                changeToken = new SqlDatabaseChangeToken();
            return changeToken;
        }

        public void Load()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT [Key], [Value] FROM dbo.Configuration;";

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string key = dr.GetString(0);
                            string value = dr.IsDBNull(1) ? null : dr.GetString(1);
                            values.Add(key, value);
                        }
                    }
                }
            }
        }

        public void Set(string key, string value)
        {
            if (!values.ContainsKey(key))
                throw new ArgumentOutOfRangeException("key");

            values[key] = value;
        }

        public bool TryGet(string key, out string value)
        {
            return values.TryGetValue(key, out value);
        }
    }

    // Change Token für Änderungen
    public class SqlDatabaseChangeToken : IChangeToken, IDisposable
    {
        public bool HasChanged => false;

        public bool ActiveChangeCallbacks => false;

        public void Dispose()
        {
        }

        public IDisposable RegisterChangeCallback(Action<object> callback, object state)
        {
            return this;
        }
    }
}
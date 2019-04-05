// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using dotnetconsulting.CustomConfiguration;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static System.Console;

namespace dotnetconsulting.GUI
{
    class Program
    {
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?tabs=basicconfiguration

        static void Main(string[] args)
        {
            // Einfaches Beispiel
            SimpleExample();

            // Mehere (Json-)Provider
            MultipleJsonProviders();

            // Enviroment Beispiel
            EnviromentVarToSelectConfig(args);

            // Azure Key Value
            AzureKeyValue();

            // Vollständiges Beispiel
            FullExample(args);

            // Custom Provider
            SqlCustomProvider();

            WriteLine("== Fertig ==");
            ReadKey();
        }

        private static void SimpleExample()
        {
            Debugger.Break();

            // Konfiguration vorbereiten & Einsatz bereit machen
            IConfigurationBuilder builder = new ConfigurationBuilder()
                // Umgebungsvariablen hinzufügen
                .AddEnvironmentVariables()
                // Json-Datei hinzufügen
                .AddJsonFile("Config.json");

            // Konfiguration abschließen
            IConfigurationRoot config = builder.Build();

            // Zugriff
            string windowHeight = config["App:Window:Height"];
            WriteLine($"windowHeight = {windowHeight}");
        }

        private static void MultipleJsonProviders()
        {
            Debugger.Break();

            // Konfiguration vorbereiten & Einsatz bereit machen
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings2.json")
                .AddJsonFile("appsettings.Development.json")
                .Build();

            string appName = config["AppName"];
            WriteLine($"AppName = {appName}");
        }

        private static void EnviromentVarToSelectConfig(string[] args)
        {
            Debugger.Break();

            // Konfiguration vorbereiten
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                // NuGet: Microsoft.Extensions.Configuration.EnvironmentVariables
                .AddCommandLine(args)
                .AddEnvironmentVariables();

            // Build() liest die Werte
            IConfigurationRoot config = configBuilder.Build();

            string enviroment = config.GetValue("EnviromentName", "Production");

            configBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                // NuGet: Microsoft.Extensions.Configuration.Json
                .AddJsonFile($"appsettings.{config.GetValue("EnviromentName", "Production")}.json");
            config = configBuilder.Build();
            // ...
        }

        private static void AzureKeyValue()
        {
            Debugger.Break();

            // Konfiguration vorbereiten
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.Development.json");

            // Werte lesen
            IConfigurationRoot config = configBuilder.Build();

            // NuGet: Microsoft.Extensions.Configuration.AzureKeyVault
            config = configBuilder
            .AddAzureKeyVault($"https://{config["AzureKeyVault:Url"]}.vault.azure.net/",
                                config["AzureKeyVault:ClientId"],
                                config["AzureKeyVault:ClientSecret"])
            .Build();

            // Zugriff
            // ...
        }

        private static void FullExample(string[] args)
        {
            Debugger.Break();

            // Konfiguration vorbereiten
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                // NuGet: Microsoft.Extensions.Configuration.CommandLine
                .AddCommandLine(args);

            // Build() liest die Werte aller Quellen ein
            IConfigurationRoot config = configBuilder.Build();

            configBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                // NuGet: Microsoft.Extensions.Configuration.Json
                .AddJsonFile($"appsettings.{config.GetValue("Env", "Production")}.json");
            config = configBuilder.Build();

            // Connectionstring abrufen...
            config = configBuilder.Build();
            string connectionString = config["ConnectionStrings:Main"];

            // ...damit Konfiguration aus Datenbank aktivieren
            config = configBuilder
                .AddSqlDatabase(connectionString)
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"App:Window:Height", "740"},
                    {"App:Window:Width", "900"},
                    {"App:Window:Top", "10"},
                    {"App:Window:Left", "10"},
                    {"App:Window:Title:Color", "Green" }
                })
                // NuGet: Microsoft.Extensions.Configuration.EnvironmentVariables
                .AddEnvironmentVariables()
                // NuGet: Microsoft.Extensions.Configuration.CommandLine
                .AddCommandLine(args)
                // NuGet: Microsoft.Extensions.Configuration.Ini
                .AddIniFile("Config.ini")
                // NuGet: Microsoft.Extensions.Configuration.Xml
                .AddXmlFile("Config.xml")
                // NuGet: Microsoft.Extensions.Configuration.AzureKeyVault
                //.AddAzureKeyVault($"https://{config["Vault"]}.vault.azure.net/",
                //                  config["ClientId"],
                //                  config["ClientSecret"])
                .Build();

            // Nicht definierter Wert
            int smptPort = config.GetValue("Smtp:Port", 25);

            // Einfacher Zugriff (ohne Binder)
            string conString = config["ConnectionStrings:Main"];

            // JSON-File
            int c = config.GetValue("App:MainWindow:Top", 0);
            // Aus Ini-Datei
            string wetter = config.GetValue<string>("IniSection:Wetter");
            // Aus XML-Datei
            // Konfiguration auf Enum mappen
            PaperOrientation paperOrientation = config.GetValue("setting:Orientation:value", PaperOrientation.Portrait);
            // Umgebungsvariabel
            string systemdrive = config.GetValue("SystemDrive", "X:");
            // Programm-Argumente
            string enviroment = config.GetValue("Env", "Production");
            // Aus der Datebank (Custom)
            string color = config.GetValue<string>("Color");

            // Konfiguration auf POCOs mappen
            var appWindow = new AppWindows();
            config.GetSection("App:Window").Bind(appWindow);

            // Aus Private-Setter-Eigenschaften binden
            appWindow = config.GetSection("App:Window").Get<AppWindows>(
                o => o.BindNonPublicProperties = true
                );
        }

        private static void SqlCustomProvider()
        {
            Debugger.Break();

            // Konfiguration vorbereiten
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // NuGet: Microsoft.Extensions.Configuration.CommandLine
                .AddJsonFile("appSettings2.json");

            // Build() liest die Werte aller Quellen ein
            IConfigurationRoot config = configBuilder.Build();

            // Connectionstring abrufen...
            string connectionString = config["ConnectionStrings:Main"];

            // SqlCustomProvider erstellen
            configBuilder = new ConfigurationBuilder()
                .AddSqlDatabase(connectionString);
            config = configBuilder.Build();

            // Werte abrufen
            string config1 = config["SystemDrive"];
            string config2 = config["Color"];

            WriteLine($"config1 = {config1}");
            WriteLine($"config2 = {config2}");
        }

        #region Misc
        public class AppWindows
        {
            public int Height { get; set; }
            public int Width { get; set; }
            public int Top { get; set; }
            public int Left { get; set; }
            public Content Title { get; set; }

            public override string ToString()
            {
                return $"T:{Top}, L:{Left}, H:{Height}, W:{Width}, Title:Color:{Title.Color}";
            }

            public class Content
            {
                public string Color { get; set; }
            }
        }

        public enum PaperOrientation
        {
            Landscape,
            Portrait,
        }
        #endregion
    }
}
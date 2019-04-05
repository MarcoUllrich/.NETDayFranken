// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace dotnetconsulting.ASPNETSecret
{
    // https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?tabs=visual-studio

    // Dotnet CLI
    //dotnet user-secrets set Geheimnis "My Name is Bond, James Bond"
    //dotnet user-secrets list
    //dotnet user-secrets remove Geheimnis
    //dot net user-secret clear

    // Windows: %APPDATA%\microsoft\UserSecrets\<userSecretsId>\secrets.json
    // Linux: ~/.microsoft/usersecrets/<userSecretsId>/secrets.json
    // macOS: ~/.microsoft/usersecrets/<userSecretsId>/secrets.json

    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            // Konfiguration vorbereiten & Einsatz bereit machen
            IConfigurationBuilder builder = new ConfigurationBuilder();

            // NuGet: Microsoft.Extensions.Configuration.UserSecrets
            builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            string secret = Configuration["Geheimnis"] ?? "?";

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Geheimnis: {secret}");
            });
        }
    }
}
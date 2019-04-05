﻿// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using dotnetconsulting.ServiceAndInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using dotnetconsulting.SMTPService;
using dotnetconsulting.GUI.SmtpDemoServices;
using System.Diagnostics;

namespace dotnetconsulting.GUI
{
    class Program
    {
        // https://docs.microsoft.com/de-de/aspnet/core/fundamentals/dependency-injection

        // Nuget: Autofac.Extensions.DependencyInjection
        // Nuget: Autofac
        static void Main(string[] args)
        {
            // IoC & DI konfigurieren
            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // ConfigureServiceWithMissingDependency(serviceCollection);

            // Demp App starten
            IServiceProvider services = serviceCollection.BuildServiceProvider();

            Application application = new Application(services);
            application.Run();

            Console.WriteLine("== Fertig ==");
            Console.ReadKey();
        }

        static private void ConfigureServices(IServiceCollection serviceCollection)
        {
            Debugger.Break();

            // ServiceCollection konfigurieren
            serviceCollection.AddSingleton<IPostageService, GermanPostageService>();
            serviceCollection.AddTransient<IOrderService, SnailMailOrderService>();
            serviceCollection.AddTransient<IPayment, PayPal>();

            //serviceCollection.AddSingleton<IOrderService, EMailOrderService>();
            //serviceCollection.AddScoped<IOrderService, EMailOrderService>();
            //serviceCollection.AddTransient<IOrderService, EMailOrderService>();

            serviceCollection.AddSingleton<IOrderService>(new EMailOrderService("127.0.0.1")
            {
                Sender = "tkansy@dotnetconsulting.eu"
            });

            // Komplexerer Ansatz für unterschiedliche SMTP-Server a la EF DbContext
            // Mocked SMTP-Service konfigurieren
            serviceCollection.AddSmtpServer<MockedSmtpService>(o =>
            {
                // Konkrete Werte aus Konfigration, Quelle jedoch beliebig und hier unwichtig
                o.SetHostAndPort("localhost", 25);
                o.Sender = "mocked@dotnetconsultng.eu";
            });

            // Real SMTP-Service konfigurieren
            serviceCollection.AddSmtpServer<ProdSmtpServer>(o =>
            {
                // Konkrete Werte aus Konfigration, Quelle jedoch beliebig und hier unwichtig
                o.SetHostAndPort("192.168.1.1", 25);
                o.Sender = "produktion@dotnetconsultng.eu";
            });
        }

        static private void ConfigureServiceWithMissingDependency(IServiceCollection serviceCollection)
        {
            // Fügt, IUseMissing hinzu, von der eine Abhängigkeit (IMissing) fehlt
            serviceCollection.AddTransient<IUseMissing, UseMissing>();
        }
    }
}
// Disclaimer
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
    public class Application
    {
        private readonly IServiceProvider services;

        public Application(IServiceProvider services)
        {
            this.services = services;
        }

        public void Run()
        {
            Debugger.Break();

            Console.WriteLine("== Bestellungen ==");

            IOrderService orderService = services.GetService<IOrderService>();

            orderService.PlaceOrder("Wattestäbchen", 10);
            orderService.PlaceOrder("Taschentuch", 1);
            orderService.PlaceOrder("Ölfaß", 10);

            Console.WriteLine();

            // Unterschiedliche SMTP-Services nutzen
            Console.WriteLine("== MockedSmtpService ==");
            ISmtpService mockedSmtpService = services.GetService<MockedSmtpService>();
            mockedSmtpService.Send("ente@hausen.de", "Wichtig!", "...");

            Console.WriteLine("== RealSmtpService ==");
            ISmtpService realSmtpService = services.GetService<ProdSmtpServer>();
            realSmtpService?.Send("ente@hausen.de", "Wichtig!", "...");

            // Via Interface. Leider nein. ISmtpService ist nicht registiert
            Console.WriteLine("== ISmtpService ==");
            ISmtpService iSmtpService = services.GetService<ISmtpService>();
            iSmtpService?.Send("ente@hausen.de", "Wichtig!", "...");
            // Wirft eine Exception (System.InvalidOperationException) statt NULL
            // iSmtpService = services.GetRequiredService<ISmtpService>();

            // Diese Komponente hat eine fehlende Abhängigkeit => Exception (immer!)
            IUseMissing useMissing = services.GetService<IUseMissing>();

            // Scope erzeugen (via ASP.NET Core als Request)
            using (IServiceScope scope = services.CreateScope())
            {
                ISmtpService smtpServiceScope = scope.ServiceProvider.GetService<ISmtpService>();
                IOrderService orderServiceScope = services.GetService<IOrderService>();

                // ...
            }

            Debugger.Break();
        }
    }
}
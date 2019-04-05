// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu
using Microsoft.Extensions.DependencyInjection;
using System;

namespace dotnetconsulting.SMTPService
{
    public static class SmtpServiceCollectionExtensions
    {
        public static void AddSmtpServer<T>(
            this IServiceCollection serviceCollection,
            Action<SmtpServiceConfigBuilder> smtpServiceConfigBuilder,
            bool RegisterInterface = false
            ) where T : SmtpServiceBase
        {
            // Ohne den generischen Parameter wird die konkrete Klasse
            // und nicht ISmtpService registiert
            if (RegisterInterface)
                serviceCollection.AddSingleton<ISmtpService>(o =>
                {
                    SmtpServiceConfigBuilder optionBuilder = new SmtpServiceConfigBuilder();

                    // Optimierung: Einträge in ein Dictionary<typeof<T>, SmtpServiceConfig>
                    smtpServiceConfigBuilder(optionBuilder);

                    return (T)Activator.CreateInstance(typeof(T), new object[] { optionBuilder.Build() });
                });
            else
                serviceCollection.AddSingleton(o =>
                {
                    SmtpServiceConfigBuilder optionBuilder = new SmtpServiceConfigBuilder();

                    // Optimierung: Einträge in ein Dictionary<typeof<T>, SmtpServiceConfig>
                    smtpServiceConfigBuilder(optionBuilder);

                    return (T)Activator.CreateInstance(typeof(T), new object[] { optionBuilder.Build() });
                });
        }
    }
}
// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using dotnetconsulting.SMTPService;
using System;

namespace dotnetconsulting.GUI.SmtpDemoServices
{
    public class FastSmtpService : SmtpServiceBase
    {
        public FastSmtpService(SmtpServiceConfig SmtpServiceConfig) : base(SmtpServiceConfig)
        { }

        public override void Send(string Recipient, string Subject, string Message)
        {
            Console.WriteLine($"Message to {Recipient}");
            Console.WriteLine($"Hostname: {_SmtpServiceConfig.Hostname}");
            Console.WriteLine($"Port: {_SmtpServiceConfig.Port}");
            Console.WriteLine($"Sender: {_SmtpServiceConfig.Sender}");
        }
    }
}
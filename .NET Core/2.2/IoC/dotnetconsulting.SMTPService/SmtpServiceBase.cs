// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

namespace dotnetconsulting.SMTPService
{
    public abstract class SmtpServiceBase : ISmtpService
    {
        protected SmtpServiceConfig _SmtpServiceConfig;

        public SmtpServiceBase(SmtpServiceConfig SmtpServiceConfig)
        {
            _SmtpServiceConfig = SmtpServiceConfig;
        }

        public abstract void Send(string Recipient, string Subject, string Message);
    }

    public interface ISmtpService
    {
        void Send(string Recipient, string Subject, string Message);
    }
}
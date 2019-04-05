// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using System;
using System.Diagnostics;

namespace dotnetconsulting.ServiceAndInterfaces
{
    public class SnailMailOrderService : IOrderService
    {
        private readonly IPostageService postageService;

        public SnailMailOrderService(IPostageService PostageService)
        {
            postageService = PostageService;
        }

        public string Sender { get; set; }

        public void PlaceOrder(string Article, int quantity)
        {
            Console.WriteLine($"Bestellen {quantity:N0} von {Article} via Post von {Sender}");
            Debug.Print($"Bestellen {quantity:N0} von {Article} via Post von {Sender}");

            Guid stampGuid = postageService.GetStamp(1.00m);
        }
    }

    public class EMailOrderService : IOrderService
    {
        private readonly string smtpHost;

        public EMailOrderService(string SmtpHost)
        {
            smtpHost = SmtpHost;
        }

        public string Sender { get; set; }

        public void PlaceOrder(string Article, int quantity)
        {
            Console.WriteLine($"Bestellen {quantity:N0} von {Article} via EMail ({smtpHost}) von {Sender}");
            Debug.Print($"Bestellen {quantity:N0} von {Article} via EMail ({smtpHost}) von {Sender}");
        }
    }

    public class GermanPostageService : IPostageService
    {
        private readonly IPayment _payment;

        public GermanPostageService(IPayment Payment)
        {
            _payment = Payment;
        }

        public Guid GetStamp(decimal Amount)
        {
            Console.WriteLine($"GetStamp {Amount:N2}");
            Debug.Print($"GetStamp {Amount:N2}");

            return Guid.NewGuid();
        }
    }

    public class PayPal : IPayment
    {
        public void Pay(decimal amount)
        {
            throw new NotImplementedException();
        }
    }

    public class UseMissing : IUseMissing
    {
        private readonly IMissing _missing;

        public UseMissing(IMissing missing)
        {
            _missing = missing;
        }
    }
}
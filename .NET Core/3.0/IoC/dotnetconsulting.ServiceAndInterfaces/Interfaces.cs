// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using System;

namespace dotnetconsulting.ServiceAndInterfaces
{
    public interface IOrderService
    {
        void PlaceOrder(string Article, int quantity);
        string Sender { get; set; }
    }

    public interface IPostageService
    {
        Guid GetStamp(decimal Amount);
    }

    public interface IPayment
    {
        void Pay(decimal amount);
    }

    public interface IMissing
    {
        void Dummy();
    }

    public interface IUseMissing
    { /* Leer */ }
}
﻿// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetconsulting.SMTPService
{
    public class SmtpServiceConfig
    {
        public int Port { get; internal set; }
        public string Hostname { get; internal set; }
        public string Sender { get; internal set; }
        public string DropFolder { get; set; }
        public bool UseSSL { get; set; }
    }
}
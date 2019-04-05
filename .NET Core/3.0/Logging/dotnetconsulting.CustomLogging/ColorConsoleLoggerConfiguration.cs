// Disclaimer
// Dieser Quellcode ist als Vorlage oder als Ideengeber gedacht. Er kann frei und ohne 
// Auflagen oder Einschränkungen verwendet oder verändert werden.
// Jedoch wird keine Garantie übernommen, das eine Funktionsfähigkeit mit aktuellen und 
// zukünftigen API-Versionen besteht. Der Autor übernimmt daher keine direkte oder indirekte 
// Verantwortung, wenn dieser Code gar nicht oder nur fehlerhaft ausgeführt wird.
// Für Anregungen und Fragen stehe ich jedoch gerne zur Verfügung.

// Thorsten Kansy, www.dotnetconsulting.eu

using System;

namespace dotnetconsulting.CustomLogging
{
    public class ColorConsoleLoggerConfiguration
    {
        public ConsoleColor TraceColor { get; set; } = ConsoleColor.DarkGray;
        public ConsoleColor DebugColor { get; set; } = ConsoleColor.Gray;
        public ConsoleColor InformationColor { get; set; } = ConsoleColor.Green;
        public ConsoleColor WarningColor { get; set; } = ConsoleColor.DarkRed;
        public ConsoleColor ErrorColor { get; set; } = ConsoleColor.Red;
        public ConsoleColor CriticalColor { get; set; } = ConsoleColor.Red;
        public ConsoleColor ScopeColor { get; set; } = ConsoleColor.Magenta;
    }
}
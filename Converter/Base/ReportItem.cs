using System;

namespace Base
{
	public class ReportItem
    {
        public enum MessageType
        {
            Warning,
            Error
        }

        public readonly string Message;

        public readonly MessageType Type;

        public readonly DateTime Time;

        public readonly ReportRow Row;

        public ReportItem(ReportRow row, string message, MessageType type, DateTime time)
        {
            Row = row;
            Message = message ?? throw new ArgumentNullException();
            Type = type;
            Time = time;
        }

        public override string ToString()
        {
            return $"[{Type}, {Time:T}] {Message}";
        }
    }
}

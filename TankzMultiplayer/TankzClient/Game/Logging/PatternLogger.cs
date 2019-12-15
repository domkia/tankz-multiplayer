using System;

namespace TankzClient
{
    public class PatternLogger : ConsoleLogger
    {
        public readonly string Pattern;

        public PatternLogger(string pattern, ConsoleColor color = ConsoleColor.Gray)
        {
            Pattern = pattern.ToLower();
            base.Color = color;
        }

        public override void Handle(string message)
        {
            string lower = message.ToLower();
            if (lower.StartsWith(Pattern))
            {
                Console.ForegroundColor = Color;
                Console.WriteLine(message);

            }
            else
            {
                base.Handle(message);
            }
        }
    }
}

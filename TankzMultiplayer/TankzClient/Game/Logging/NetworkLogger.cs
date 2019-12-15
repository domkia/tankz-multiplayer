using System;

namespace TankzClient
{
    public class NetworkLogger : ConsoleLogger
    {
        public override void Handle(string message)
        {
            string lower = message.ToLower();
            if (message.StartsWith("network"))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(message);
                Console.ResetColor();
            }
            else
            {
                base.Handle(message);
            }
        }
    }
}

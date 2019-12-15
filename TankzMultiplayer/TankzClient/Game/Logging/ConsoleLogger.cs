using System;

namespace TankzClient
{
    public abstract class ConsoleLogger : IConsoleChain
    {
        protected ConsoleColor Color = ConsoleColor.White;

        protected IConsoleChain next = null;

        public virtual void Handle(string message)
        {
            if (next != null)
            {
                next.Handle(message);
            }
        }

        public IConsoleChain SetNext(IConsoleChain nextInChain)
        {
            this.next = nextInChain;
            return next;
        }
    }
}

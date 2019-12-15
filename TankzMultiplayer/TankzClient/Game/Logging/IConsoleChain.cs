namespace TankzClient
{
    public interface IConsoleChain
    {
        IConsoleChain SetNext(IConsoleChain next);
        void Handle(string message);
    }
}

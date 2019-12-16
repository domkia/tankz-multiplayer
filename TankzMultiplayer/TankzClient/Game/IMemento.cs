using System;

namespace TankzClient.Game
{
    public interface IMemento
    {
        string GetName();

        TankConfig GetState();
        DateTime GetDate();
    }
}

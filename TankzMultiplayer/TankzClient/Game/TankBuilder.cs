using System;
using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class BaseBuilder
    {
        public virtual Tank Build()
        {
            Console.WriteLine("BaseBuilder klase");
            return new Tank(new Vector2(0,0), new Vector2(0,0));
        }
    }
    abstract class TankBuilder : BaseBuilder
    {
        public const string spritesPath = "../../res/tanks/";

        public const int CHASSIS_COUNT = 4;
        public const int COLOR_COUNT = 4;
        public const int TURRET_COUNT = 3;
        public const int TRACKS_COUNT = 4;

        public Tank tank = null;

        public TankBuilder(bool isPlayer)
        {
            Console.WriteLine($"BUILDER: new TankBuilder()");
            if (isPlayer)
            {
                // Player controller tank
                tank = new PlayerTank(new Vector2(0f, 0f), new Vector2(64f, 48f));
            }
            else
            {
                tank = new Tank(new Vector2(0f, 0f), new Vector2(64f, 48f));
            }
        }

        public sealed override Tank Build()
        {
            SetChassis();
            SetTracks();
            SetTurret();

            if (isFlagNeeded())
            {
                SetFlag();
            }

            Console.WriteLine("BUILDER TankBuilder: Build()");
            return tank;
        }

        public abstract TankBuilder SetChassis();
        public abstract TankBuilder SetTracks();
        public abstract TankBuilder SetTurret();
        public abstract TankBuilder SetFlag();
        public abstract bool isFlagNeeded();

        
    }
}

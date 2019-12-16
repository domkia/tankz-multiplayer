using System;
using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class CustomizableTankBuilder : ITankBuilder
    {
        const string spritesPath = "../../res/tanks/";

        const int CHASSIS_COUNT = 4;
        const int COLOR_COUNT = 4;
        const int TURRET_COUNT = 3;
        const int TRACKS_COUNT = 4;

        private Tank tank = null;

        public CustomizableTankBuilder(bool isPlayer)
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

        public Tank Build()
        {
            if (tank == null)
            {
                throw new Exception("Incomplete tank: No parts attached");
            }
            if (tank.FindChild<TankBarrel>() == null)
            {
                throw new Exception("Incomplete tank: Tank without a barrel is not a tank");
            }
            if (tank.FindChild<TankChassis>() == null)
            {
                throw new Exception("Incomplete tank: Tank has no chassis");
            }
            if (tank.children.Count == 2)
            {
                throw new Exception("Incomplete tank: You forgot to add tracks");
            }

            Console.WriteLine("BUILDER TankBuilder: Build()");
            return tank;
        }

        public ITankBuilder SetChassis(int colorId = 0, int id = 0)
        {
            if (id < 0 || id >= CHASSIS_COUNT)
                throw new IndexOutOfRangeException();

            if (colorId < 0 || colorId >= COLOR_COUNT)
                throw new IndexOutOfRangeException();

            if (tank.FindChild<TankChassis>() != null)
            {
                throw new Exception("Tank already has a chassis");
            }

            Console.WriteLine("BUILDER TankBuilder: SetChassis()");
            string path = string.Format($"{spritesPath}chassis_{colorId}_{id}.png");
            TankChassis chassis = new TankChassis(Image.FromFile(path),
                    tank.transform.position,
                    new Vector2(64f, 48f));
            chassis.SetParent(tank);
            return this;
        }

        public ITankBuilder SetTracks(int id = 0)
        {
            if (id < 0 || id >= TRACKS_COUNT)
                throw new IndexOutOfRangeException();

            Console.WriteLine("BUILDER TankBuilder: SetTracks()");
            string path = string.Format($"{spritesPath}tracks_{id}.png");
            Sprite tracks = new Sprite(
                Image.FromFile(path),
                tank.transform.position,
                new Vector2(56f, 24f));
            tracks.SetParent(tank);
            tracks.transform.SetPosition(new Vector2(0f, 20f));
            return this;
        }

        public ITankBuilder SetTurret(int id = 0)
        {
            if (id < 0 || id >= TURRET_COUNT)
                throw new IndexOutOfRangeException();

            if (tank.FindChild<TankBarrel>() != null)
            {
                throw new Exception("Tank already has a barrel");
            }

            Console.WriteLine("BUILDER TankBuilder: SetTurret()");
            string path = string.Format($"{spritesPath}turret_{id}.png");
            TankBarrel turret = new TankBarrel(
                tank,
                Image.FromFile(path),
                tank.transform.position,
                new Vector2(64f, 8f));
            turret.SetParent(tank);
            turret.transform.SetPosition(new Vector2(0f, -20f));
            return this;
        }
    }
}
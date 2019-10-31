using System;
using System.Drawing;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class TankBuilder : ITankBuilder
    {
        const string spritesPath = "../../res/tanks/";

        const int CHASSIS_COUNT = 4;
        const int COLOR_COUNT = 4;
        const int TURRET_COUNT = 3;
        const int TRACKS_COUNT = 4;

        private Tank tank = new Tank(null, new Vector2(0f, 0f), new Vector2(64f, 48f));

        public Tank Build()
        {
            if (tank == null)
                throw new Exception("Incomplete tank");
            return tank;
        }

        public ITankBuilder SetChassis(int colorId = 0, int id = 0)
        {
            if (id < 0 || id >= CHASSIS_COUNT)
                throw new IndexOutOfRangeException();

            if (colorId < 0 || colorId >= COLOR_COUNT)
                throw new IndexOutOfRangeException();

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

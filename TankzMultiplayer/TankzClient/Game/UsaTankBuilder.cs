using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class UsaTankBuilder : TankBuilder
    {
        private int chassisColorId = 0;
        private int chassisId = 0;
        private int tracksId = 0;
        private int barrelId = 0;

        public UsaTankBuilder(bool isPlayer) : base(isPlayer)
        {
            
        }

        public override bool isFlagNeeded()
        {
            return true;
        }

        public override TankBuilder SetChassis()
        {
            if (chassisId < 0 || chassisId >= CHASSIS_COUNT)
                throw new IndexOutOfRangeException();

            if (chassisColorId < 0 || chassisColorId >= COLOR_COUNT)
                throw new IndexOutOfRangeException();

            if (tank.FindChild<TankChassis>() != null)
            {
                throw new Exception("Tank already has a chassis");
            }

            Console.WriteLine("BUILDER TankBuilder: SetChassis()");
            string path = string.Format($"{spritesPath}chassis_{chassisColorId}_{chassisId}.png");
            TankChassis chassis = new TankChassis(Image.FromFile(path),
                    tank.transform.position,
                    new Vector2(64f, 48f));
            chassis.SetParent(tank);
            return this;
        }

        public override TankBuilder SetFlag()
        {
            Console.WriteLine("Flag was set");
            return this;
        }

        public override TankBuilder SetTracks()
        {
            if (tracksId < 0 || tracksId >= TRACKS_COUNT)
                throw new IndexOutOfRangeException();

            Console.WriteLine("BUILDER TankBuilder: SetTracks()");
            string path = string.Format($"{spritesPath}tracks_{tracksId}.png");
            Sprite tracks = new Sprite(
                Image.FromFile(path),
                tank.transform.position,
                new Vector2(56f, 24f));
            tracks.SetParent(tank);
            tracks.transform.SetPosition(new Vector2(0f, 20f));
            return this;
        }

        public override TankBuilder SetTurret()
        {
            if (barrelId < 0 || barrelId >= TURRET_COUNT)
                throw new IndexOutOfRangeException();

            if (tank.FindChild<TankBarrel>() != null)
            {
                throw new Exception("Tank already has a barrel");
            }

            Console.WriteLine("BUILDER TankBuilder: SetTurret()");
            string path = string.Format($"{spritesPath}turret_{barrelId}.png");
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

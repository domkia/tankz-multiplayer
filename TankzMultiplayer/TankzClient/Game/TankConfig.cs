using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankzClient.Game
{
    public class TankConfig
    {
        private int CurrColor { get; set; }
        private int CurrChassis { get; set; }
        private int CurrTurret { get; set;}
        private int CurrTracks { get; set; }
        public TankConfig(int color, int chasis, int turret, int tracks)
        {
            this.CurrColor = color;
            this.CurrChassis = chasis;
            this.CurrTurret = turret;
            this.CurrTracks = tracks;
        }
        public void setColor(int id)
        {
            this.CurrColor = id;
        }
        public int getColor()
        {
            return CurrColor;
        }
        public void setChassis(int id)
        {
            this.CurrChassis = id;
        }
        public int getChassis()
        {
            return CurrChassis;
        }
        public void setTurret(int id)
        {
            this.CurrTurret = id;
        }
        public int getTurret()
        {
            return CurrTurret;
        }
        public void seTracks(int id)
        {
            this.CurrTracks = id;
        }
        public int getTracks()
        {
            return CurrTracks;
        }

        public override string ToString()
        {
            return string.Format("Color: {0}, Chasis: {1}, Turret:{2}, Tracks:{3}", CurrColor, CurrChassis, CurrTurret, CurrTracks);
        }
    }
}

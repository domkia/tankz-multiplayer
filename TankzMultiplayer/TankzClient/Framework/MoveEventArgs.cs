using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankzClient.Framework
{
    public class MoveEventArtgs : EventArgs
    {
        public float X { get; set; }
        public float Y { get; set; }
        public string ConnID { get; set; }
    }
}

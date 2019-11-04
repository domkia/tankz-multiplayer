using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankzClient.Framework
{
    public class RotateEventArgs : EventArgs
    {
        public float Angle { get; set; }
        public string ConnID { get; set; }
    }
}

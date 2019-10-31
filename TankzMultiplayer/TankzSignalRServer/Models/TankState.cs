﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TankzSignalRServer.Models
{
    public class TankState
    {
        public int ID { get; set; }
        public Tank Tank { get; set; }
        public float Pos_X { get; set; }
        public float Pos_Y { get; set; }
        public int Health { get; set; }
        public int Fuel { get; set; }
    }
}
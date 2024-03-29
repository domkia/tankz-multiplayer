﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TankzSignalRServer.Models
{
    public class Player
    {
        public int ID { get; set; }
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool ReadyState { get; set; }
        public Tank Tank { get; set; }
        public TankState TankState { get; set; }
    }
}

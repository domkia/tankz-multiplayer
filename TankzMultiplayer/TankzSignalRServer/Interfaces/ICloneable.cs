using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TankzSignalRServer.Interfaces
{
    interface ICloneable<T>
    {
        T Clone();
    }
}

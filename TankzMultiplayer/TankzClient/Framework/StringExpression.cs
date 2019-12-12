using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankzClient.Framework
{
    class StringExpression : IExpression
    {
        private string value;

        public StringExpression(string newValue)
        {
            value = newValue;
        }

        public string executeMethod()
        {
            return value;
        }
    }
}

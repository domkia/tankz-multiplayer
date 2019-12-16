using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankzClient.Game
{
    class ConcreteMemento : IMemento
    {
        private TankConfig _state;
        private DateTime _date;

        public ConcreteMemento(TankConfig state)
        {
            this._state = state;
            this._date = DateTime.Now;
        }

        // The Originator uses this method when restoring its state.
        public TankConfig GetState()
        {
            return this._state;
        }

        // The rest of the methods are used by the Caretaker to display
        // metadata.
        public string GetName()
        {
            return $"{this._date} / ({this._state.ToString()})...";
        }

        public DateTime GetDate()
        {
            return this._date;
        }
    }

}

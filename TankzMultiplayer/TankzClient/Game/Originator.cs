using System;
using TankzClient.Models;

namespace TankzClient.Game
{
    class Originator
    {
        private TankConfig _state;

        public Originator(TankConfig state)
        {
            this._state = state;
            Console.WriteLine("Originator: My initial state is: " + state.ToString());
        }

        public IMemento Save()
        {
            return new ConcreteMemento(new TankConfig(_state.getColor(), _state.getChassis(), _state.getTurret(), _state.getTracks()));
        }

        public void Restore(IMemento memento)
        {
            if (!(memento is ConcreteMemento))
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }

            this._state = memento.GetState();
            Console.Write($"Originator: My state has changed to: {_state}");
        }
    }
}

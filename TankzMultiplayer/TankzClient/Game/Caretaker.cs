using System;
using System.Collections.Generic;
using System.Linq;

namespace TankzClient.Game
{
    class Caretaker
    {
        private List<IMemento> _mementos = new List<IMemento>();

        private Originator _originator = null;

        public Caretaker(Originator originator)
        {
            this._originator = originator;
        }
        public IMemento getCurrent()
        {
            if (_mementos.Count != 0)
            {
                return _mementos.Last();
            }
            else return null;
        }

        public void Backup()
        {
            this._mementos.Add(this._originator.Save());
        }

        public void Undo()
        {
            if (this._mementos.Count == 0)
            {
                return;
            }

            var memento = this._mementos.Last();
            this._mementos.Remove(memento);
            try
            {
                this._originator.Restore(memento);
            }
            catch (Exception)
            {
                this.Undo();
            }
        }

        public void ShowHistory()
        {
            foreach (var memento in this._mementos)
            {
                Console.WriteLine(memento.GetName());
            }
        }
    }
}

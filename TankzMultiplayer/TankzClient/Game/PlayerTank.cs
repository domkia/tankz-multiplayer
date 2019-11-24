using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using TankzClient.Framework;
using System;

namespace TankzClient.Game
{
    /// <summary>
    /// Player controlled tank.
    /// Using input player can go through different
    /// tank phases and execute various tank commands.
    /// </summary>
    public class PlayerTank : Tank
    {
        protected Stack<ITankCommand> commands;

        protected TankPhase currentPhase;

        public PlayerTank(Vector2 position, Vector2 size) 
            : base(position, size)
        {
            commands = new Stack<ITankCommand>();
            SetPhase(new TankIdle(this));
        }
        public override int SortingLayer => 10;

        public void StartTurn()
        {
            SetPhase(new TankMovement(this));
        }

        public void SetPhase(TankPhase phase)
        {
            Console.WriteLine("STATE PlayerTank: SetPhase()");
            if (phase is TankIdle)
            {
                commands.Clear();
            }
            currentPhase = phase;
        }

        public void AddCommand(ITankCommand command)
        {
            commands.Push(command);
        }

        public void Undo()
        {
            if (commands.Count == 0)
            {
                return;
            }

            // Undo all commands of same type
            Type undoType = commands.Peek().GetType();
            while (commands.Count > 0 && commands.Peek().GetType() == undoType)
            {
                ITankCommand undoCommand = commands.Pop();
                undoCommand.Undo();
            }

            Console.WriteLine("COMMAND PlayerTank: Undo()");

            // Change back to initial phase
            if (commands.Count > 0)
            {
                if (commands.Peek().GetType() == typeof(TankMoveCommand))
                {
                    SetPhase(new TankMovement(this));
                }
            }
            else
            {
                SetPhase(new TankMovement(this));
            }
        }

        public override void Update(float deltaTime)
        {
            if (Input.IsKeyDown(Keys.Z))
            {
                Console.WriteLine("Pressed Z Key");
                Undo();
            }

            if (currentPhase != null)
            {
                currentPhase.Update(deltaTime);
            }
        }

        public override void Render(Graphics context)
        {
            base.Render(context);
            context.DrawString(string.Format($"Tank Phase: {currentPhase.GetType().Name}"), SystemFonts.MenuFont, Brushes.LightGreen, new Point(0, 50));
        }
    }
}

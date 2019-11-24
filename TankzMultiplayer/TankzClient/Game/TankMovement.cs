using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;
using System.Windows.Forms;
using TankzClient.Models;

namespace TankzClient.Game
{
    class TankMovement : TankPhase
    {
        public readonly float Speed = 50f;

        int movDir;
        Vector2 position;

        public TankMovement(PlayerTank tank) : base (tank)
        {
            
        }

        private void MoveTank(float deltaTime)
        {
            if (movDir == 0)
            {
                return;
            }

            // Create a new tank move command
            ITankCommand moveCommand = new TankMoveCommand(tank as ITank);
            tank.AddCommand(moveCommand);
            moveCommand.Execute(movDir * deltaTime * Speed);
        }

        public override void Update(float deltaTime)
        {
            if (Input.IsKeyDown(System.Windows.Forms.Keys.Left))
            {
                movDir = -1;
            }
            else if (Input.IsKeyDown(System.Windows.Forms.Keys.Right))
            {
                movDir = 1;
            }
            else
            {
                movDir = 0;
            }

            // Got to the next phase
            if (Input.IsKeyDown(System.Windows.Forms.Keys.Space))
            {
                Tank mytank = GameplayScene.tankDict[NetworkManager.Instance.myConnId()];
                TankState state = mytank.State;
                state.Pos_X = tank.transform.position.x;
                state.Pos_Y = tank.transform.position.y;
                NetworkManager.Instance.SavePos(tank.transform.position);
                tank.SetPhase(new TankAiming(tank));
            }

            MoveTank(deltaTime);
        }
    }
}

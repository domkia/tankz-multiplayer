using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;

namespace TankzClient.Game
{
    class TankAiming : TankPhase
    {
        public readonly float RotationSpeed = 90.0f;

        int rotDir = 0;
        bool isKeyDown = false;

        public TankAiming(PlayerTank tank) : base (tank)
        {

        }

        private void RotateBarrel(float deltaTime)
        {
            if (rotDir == 0)
            {
                return;
            }

            // Calculate new angle
            float newAngle = tank.Angle;
            newAngle += rotDir * RotationSpeed * deltaTime;
            if (newAngle > 180)
            {
                newAngle = 180;
            }
            else if (newAngle < 0)
            {
                newAngle = 0;
            }

            if (tank.Angle != newAngle)
            {
                // Create new tank command
                ITankCommand rotateCommand = new TankAimCommand(tank as ITank);
                tank.AddCommand(rotateCommand);
                rotateCommand.Execute(newAngle);
            }
        }

        private void AdjustPower(float deltaTime)
        {
            // Get input (up / down arrows)
            float amount = 0f;
            if (Input.IsKeyDown(System.Windows.Forms.Keys.Up))
            {
                amount = deltaTime;
            }
            else if (Input.IsKeyDown(System.Windows.Forms.Keys.Down))
            {
                amount = -deltaTime;
            }

            // Adjust power
            if (amount != 0f)
            {
                float power = tank.Power + amount;
                power = Utils.Clamp(0.0f, 1.0f, power);

                // Create new tank command
                ITankCommand powerCommand = new TankPowerCommand(tank);
                tank.AddCommand(powerCommand);
                powerCommand.Execute(power);
            }
        }

        //TODO: Fix input to be smooth without spagetti
        public override void Update(float deltaTime)
        {
            if (Input.IsKeyDown(System.Windows.Forms.Keys.Left) || Input.IsKeyDown(System.Windows.Forms.Keys.Right))
            {
                isKeyDown = true;
                if (Input.IsKeyDown(System.Windows.Forms.Keys.Left))
                {
                    rotDir = 1;
                }
                else if (Input.IsKeyDown(System.Windows.Forms.Keys.Right))
                {
                    rotDir = -1;
                }
            }
            else if (Input.IsKeyUp(System.Windows.Forms.Keys.Left) || Input.IsKeyUp(System.Windows.Forms.Keys.Right))
            {
                rotDir = 0;
                isKeyDown = false;
            }

            RotateBarrel(deltaTime);
            AdjustPower(deltaTime);
            
            if (Input.IsKeyDown(System.Windows.Forms.Keys.W))
            {
                tank.SetPhase(new TankWeaponSelection(tank));
            }
            else if (Input.IsKeyDown(System.Windows.Forms.Keys.Space))
            {
                tank.Shoot();
                tank.SetPhase(new TankIdle(tank));
            }
        }
    }
}

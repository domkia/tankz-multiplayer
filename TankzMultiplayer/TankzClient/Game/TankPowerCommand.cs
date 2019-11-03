namespace TankzClient.Game
{
    /// <summary>
    /// Tank power selection command. When you undo this
    /// command, the shooting power level resets
    /// </summary>
    class TankPowerCommand : ITankCommand
    {
        private readonly float startPower;
        private readonly ITank tank;

        public TankPowerCommand(ITank tank)
        {
            this.tank = tank;
            startPower = tank.Power;
        }

        public void Execute(float power)
        {
            this.tank.SetPower(power);
        }

        public void Undo()
        {
            this.tank.SetPower(startPower);
        }
    }
}

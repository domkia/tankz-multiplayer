using System.Drawing;
using TankzClient.Framework;
using TankzClient.Models;

namespace TankzClient.Game
{
    public class TankScene : Scene
    {
        public override void Load()
        {
            PlayerTank playerTank = new TankBuilder(true)
                .SetChassis(0, 0)
                .SetTurret(0)
                .SetTracks(0)
                .Build() as PlayerTank;
            CreateEntity(playerTank);
            playerTank.transform.SetPosition(new Vector2(200, 300));
            playerTank.StartTurn();

            TankState state = new TankState();
            state.Pos_X = playerTank.transform.position.x;
            state.Pos_Y = playerTank.transform.position.y;
            playerTank.UpdateTankState(state);

            Tank npcTank = new TankBuilder(false)
                .SetChassis(1, 1)
                .SetTurret(1)
                .SetTracks(1)
                .Build();
            CreateEntity(npcTank);
            npcTank.transform.SetPosition(new Vector2(500, 300));
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.SlateBlue);
            base.Render(context);
        }
    }
}

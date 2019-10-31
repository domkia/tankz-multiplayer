using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankzClient.Framework;
using TankzClient.Models;

namespace TankzClient.Game
{
    class GameplayScene : Scene
    {
        GameplayUI gameplayUI;
        Background background;
        Terrain terrain;
        Player[] players;
        
        public override void Load()
        {
            players = NetworkManager.Instance.GetPlayerList();
            foreach (Player player in players)
            {
                Tank newTank = new TankBuilder()
                     .SetChassis(player.Tank.Color_id, player.Tank.Chasis_id)
                     .SetTurret(player.Tank.Chasis_id)
                     .SetTracks(player.Tank.Trucks_id)
                     .Build();
                CreateEntity(newTank);
                newTank.transform.SetPosition(new Vector2(player.TankState.Pos_X, player.TankState.Pos_Y));
            }
            gameplayUI = CreateEntity(new GameplayUI()) as GameplayUI;
            background = CreateEntity(new Background()) as Background;
            terrain = CreateEntity(new Terrain()) as Terrain;
            
            
            
            /*
            Tank usaTank = new TankBuilder()
                .SetChassis(1, 1)
                .SetTurret(1)
                .SetTracks(0)
                .Build();
            CreateEntity(usaTank);
            usaTank.transform.SetPosition(new Vector2(500, 100));
            */
        }

        public override void Render(Graphics context)
        {

            base.Render(context);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }
    }
}

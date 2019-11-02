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
        Grenade grenade;
        float startPositionX = -10;
        float startPositionY = -10;
        float currentTime = 0;

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
            
            //background = CreateEntity(new Background()) as Background;

            //terrain = CreateEntity(new Terrain()) as Terrain;

            gameplayUI = CreateEntity(new GameplayUI()) as GameplayUI;
            grenade = new ProjectileFactory().Create("grenade") as Grenade;
            grenade.transform.SetPosition(new Vector2(300f, 300f));
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

            currentTime += deltaTime;
            if (grenade != null)
            {
                if (startPositionX == -10)
                {
                    startPositionX = grenade.transform.position.x;
                }
                if (startPositionY == -10)
                {
                    startPositionY = grenade.transform.position.y;
                }
                
                float lastX = grenade.transform.position.x;
                float lastY = grenade.transform.position.y;
                if (lastY <= 300f)
                {
                    grenade.transform.SetPosition(calculatePos(50f, -9.8f, (float)(50 * (Math.PI / 180.0)), new Vector2(startPositionX, startPositionY), currentTime));
                    //Console.WriteLine(grenade.transform.position.x + " " + grenade.transform.position.y);
                }
            }
        }
        private Vector2 calculatePos(float speed, float gravity, float angle, Vector2 currentPos, float time)
        {
            float x = currentPos.x;
            float y = currentPos.y;
            x = (float)(x + speed * time * Math.Cos(angle));
            y = (float)(y - (speed * time * Math.Sin(angle)) - (0.5f * gravity * Math.Pow(time, 2)));
            return new Vector2(x, y);
        }
    }
}

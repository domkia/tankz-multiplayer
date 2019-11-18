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
        Background background;
        Terrain terrain;
        Grenade grenade;
        Crate crate;
        PlayerTank tank;
        float startPositionX = -10;
        float startPositionY = -10;
        float currentTime = 0;

        public static Dictionary<string, Game.Tank> tankDict = new Dictionary<string, Game.Tank>();

        public override void Load()
        {
            NetworkManager.Instance.OnTankConfigsReceived += SpawnTanks;
            NetworkManager.Instance.OnShootStart += Instance_OnShootStart;
            NetworkManager.Instance.ProjectileMoved += Instance_ProjectileMoved;
            NetworkManager.Instance.ProjectileExplosion += Instance_ProjectileExplosion;
            background = CreateEntity(new Background()) as Background;

            //terrain = CreateEntity(new Terrain()) as Terrain;

            

            crate = CreateEntity(new Crate(Image.FromFile("../../res/crates/crate_0.png"), new Vector2(200f, 200f), grenade, new Vector2(50f, 50f))) as Crate;
            crate.transform.SetPosition(new Vector2(200f, 300f));
            crate.transform.SetSize(new Vector2(50f, 50f));
            crate.SetActive(false);
        }

        private void Instance_ProjectileExplosion()
        {
            ParticleEmitter particle = new ParticleFactory().Create("explosion") as ParticleEmitter;
            particle.transform.SetPosition(grenade.transform.position);
            particle.Emit();
            grenade.Destroy();
            grenade = null;
        }

        private void Instance_ProjectileMoved(Vector2 obj)
        {
            grenade.transform.SetPosition(obj);
        }

        private void Instance_OnShootStart(Vector2 obj)
        {
            grenade = new ProjectileFactory().Create("grenade") as Grenade;
            grenade.transform.SetPosition(obj);
        }

        private void SpawnTanks(List<Player> playersAndTanks)
        {
            foreach (Player player in playersAndTanks)
            {
                if (player.ConnectionId == NetworkManager.Instance.myConnId())
                {
                    tank = new TankBuilder(true)
                         .SetChassis(player.Tank.Color_id, player.Tank.Chasis_id)
                         .SetTurret(player.Tank.Chasis_id)
                         .SetTracks(player.Tank.Trucks_id)
                         .Build() as PlayerTank;
                    CreateEntity(tank);
                    tank.UpdateTankState(player.TankState);
                    tankDict.Add(player.ConnectionId, tank);
                }
                else
                {
                    Tank NPCTank = new TankBuilder(false)
                        .SetChassis(player.Tank.Color_id, player.Tank.Chasis_id)
                        .SetTurret(player.Tank.Chasis_id)
                        .SetTracks(player.Tank.Trucks_id)
                        .Build();
                    CreateEntity(NPCTank);
                    NPCTank.UpdateTankState(player.TankState);
                    tankDict.Add(player.ConnectionId, NPCTank);
                }
            }
            NetworkManager.Instance.OnTankConfigsReceived -= SpawnTanks;

            NetworkManager.Instance.OnTurnStarted += Instance_PlayerChanged;
            NetworkManager.Instance.PlayerMoved += Instance_PlayerMoved;
            NetworkManager.Instance.BarrelRotate += Instance_BarrelRotate;
            NetworkManager.Instance.OnCrateSpawned += Instance_OnCrateSpawned;

            CreateEntity(new GameplayUI());


        }

        private void Instance_OnCrateSpawned(Models.Crate obj)
        {
            Crate crate = CreateEntity(new Crate(Image.FromFile("../../res/crates/crate_0.png"), new Vector2(100, 100), null, new Vector2(50, 50))) as Crate;
            NetworkManager.Instance.OnCrateDestroyed += crate.OnDestroy; 
        }

        public override void Render(Graphics context)
        {

            base.Render(context);
        }

        public override void Update(float deltaTime)
        {

            base.Update(deltaTime);
            
            currentTime += deltaTime;
        }
        private void Instance_BarrelRotate(object sender, RotateEventArgs e)
        {
            Tank movedTank = tankDict[e.ConnID];
            movedTank.SetAngle(e.Angle);
            Console.WriteLine(e.ConnID + " Rotated");
        }

        private void Instance_PlayerMoved(object sender, MoveEventArtgs e)
        {
            Vector2 pos = new Vector2(e.X, e.Y);
            Tank movedTank = tankDict[e.ConnID];

            // Update tank state
            TankState state = movedTank.State;
            state.Pos_X = e.X;
            state.Pos_Y = e.Y;
            movedTank.UpdateTankState(state);

            Console.WriteLine("Moved");
        }

        private void Instance_PlayerChanged(object sender, EventArgs e)
        {
            if (NetworkManager.Instance.IsMyTurn)
            {
                PlayerTank tank = tankDict[NetworkManager.Instance.myConnId()] as PlayerTank;
                tank.StartTurn();
                Console.WriteLine("My turn start");
            }
            else
            {
                Console.WriteLine("other player started the turn");
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

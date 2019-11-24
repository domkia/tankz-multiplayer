using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TankzClient.Framework;
using TankzClient.Models;

namespace TankzClient.Game
{
    class TankEditScene : Scene
    {
        private Sprite logo;
        private Button colorDown;
        private Button colorUp;
        private Button chassisDown;
        private Button chassisUp;
        private Button turretDown;
        private Button turretUp;
        private Button tracksDown;
        private Button tracksUp;
        private Button backButton;
        private int currColor = 0;
        private int currChassis = 0;
        private int currTurret =0;
        private int currTracks =0;

        Tank NPCTank;

        private Button saveConfig;

        public override void Load()
        {
            logo = CreateEntity(new Sprite(Image.FromFile("../../res/logo.png"), new Vector2(380, 80), new Vector2(500, 250))) as Sprite;
            UpdateTank(0, 0, 0, 0);
            //Color edits
            colorDown = CreateEntity(new Button(300, 260, 15, 15, null, "<")) as Button;
            colorUp = CreateEntity(new Button(400, 260, 15, 15, null, ">")) as Button;
            colorDown.OnClickCallback += ColorDown_OnClickCallback;
            colorUp.OnClickCallback += ColorUp_OnClickCallback;

            //Chassis edits
            chassisDown = CreateEntity(new Button(300, 300, 15, 15, null, "<")) as Button;
            chassisUp = CreateEntity(new Button(400, 300, 15, 15, null, ">")) as Button;
            chassisDown.OnClickCallback += ChassisDown_OnClickCallback;
            chassisUp.OnClickCallback += ChassisUp_OnClickCallback;

            //Turret edits
            turretDown = CreateEntity(new Button(300, 280, 15, 15, null, "<")) as Button;
            turretUp = CreateEntity(new Button(400, 280, 15, 15, null, ">")) as Button;
            turretDown.OnClickCallback += TurretDown_OnClickCallback;
            turretUp.OnClickCallback += TurretUp_OnClickCallback;

            //Tracks edits
            tracksDown = CreateEntity(new Button(300, 320, 15, 15, null, "<")) as Button;
            tracksUp = CreateEntity(new Button(400, 320, 15, 15, null, ">")) as Button;
            tracksDown.OnClickCallback += TracksDown_OnClickCallback;
            tracksUp.OnClickCallback += TracksUp_OnClickCallback;

            saveConfig = CreateEntity(new Button(300, 360, 120, 15, null, "Save tank configuration")) as Button;
            saveConfig.OnClickCallback += SaveConfig_OnClickCallback;

            backButton = CreateEntity(new Button(10, 400, 40, 15, null, "BACK")) as Button;
            backButton.OnClickCallback += BackButton_OnClickCallback;
        }

        private void BackButton_OnClickCallback()
        {
            SceneManager.Instance.LoadScene<MainMenuScene>();
        }

        private void TracksUp_OnClickCallback()
        {
            if(currTracks < 3)
            {
                UpdateTank(currColor, currChassis, currTurret, ++currTracks);
            }
        }

        private void TracksDown_OnClickCallback()
        {
            if (currTracks > 0)
            {
                UpdateTank(currColor, currChassis, currTurret, --currTracks);
            }
        }

        private void TurretUp_OnClickCallback()
        {
            if (currTurret < 2)
            {
                UpdateTank(currColor, currChassis, ++currTurret, currTracks);
            }
        }

        private void TurretDown_OnClickCallback()
        {
            if (currTurret > 0)
            {
                UpdateTank(currColor, currChassis, --currTurret, currTracks);
            }
        }

        private void ChassisUp_OnClickCallback()
        {
            if (currChassis < 3)
            {
                UpdateTank(currColor, ++currChassis, currTurret, currTracks);
            }
        }

        private void ChassisDown_OnClickCallback()
        {
            if (currChassis > 0)
            {
                UpdateTank(currColor, --currChassis, currTurret, currTracks);
            }
        }

        private void ColorUp_OnClickCallback()
        {
            if (currColor < 3)
            {
                UpdateTank(++currColor, currChassis, currTurret, currTracks);
            }
        }

        public override void Unload()
        {
            base.Unload();
        }

        private void ColorDown_OnClickCallback()
        {
            if (currColor >0)
            {
                UpdateTank(--currColor, currChassis, currTurret, currTracks);
            }
        }

        public void UpdateTank(int color, int chassis, int turret, int tracks)
        {
            SceneManager.Instance.CurrentScene.DestroyEntity(NPCTank);
            NPCTank = new TankBuilder(false)
                        .SetChassis(color, chassis)
                        .SetTurret(turret)
                        .SetTracks(tracks)
                        .Build();
            CreateEntity(NPCTank);
            TankState state = new TankState { Pos_X = 360, Pos_Y = 300 };
            NPCTank.UpdateTankState(state);
        }

        private void SaveConfig_OnClickCallback()
        {
            throw new NotImplementedException();
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.FromArgb(77, 120, 78));
            base.Render(context);
        }
    }
}

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
        private Button undoConfig;
        private Button showHistory;
        private TankConfig config;
        Originator originator;
        Caretaker caretaker;

        Tank NPCTank;

        private Button saveConfig;

        public override void Load()
        {
            config = new TankConfig(0, 0, 0, 0);
            originator = new Originator(config);
            caretaker = new Caretaker(originator);
            caretaker.Backup();
            logo = CreateEntity(new Sprite(Image.FromFile("../../res/logo.png"), new Vector2(380, 80), new Vector2(500, 250))) as Sprite;
            UpdateTank();
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

            undoConfig = CreateEntity(new Button(300, 390, 120, 15, null, "Undo tank configuration")) as Button;
            undoConfig.OnClickCallback += UndoConfig_OnClickCallback;


            showHistory = CreateEntity(new Button(300, 420, 120, 15, null, "Show config history")) as Button;
            showHistory.OnClickCallback += ShowHistory_OnClickCallback; ;

            backButton = CreateEntity(new Button(10, 400, 40, 15, null, "BACK")) as Button;
            backButton.OnClickCallback += BackButton_OnClickCallback;
        }

        private void ShowHistory_OnClickCallback()
        {
            caretaker.ShowHistory();
        }

        private void BackButton_OnClickCallback()
        {
            SceneManager.Instance.LoadScene<LoginScene>();
        }

        private void TracksUp_OnClickCallback()
        {
            if(config.getTracks() < 3)
            {
                config.seTracks(config.getTracks()+1);
                UpdateTank();
            }
        }

        private void TracksDown_OnClickCallback()
        {
            if (config.getTracks() > 0)
            {
                config.seTracks(config.getTracks() - 1);
                UpdateTank();
            }
        }

        private void TurretUp_OnClickCallback()
        {
            if (config.getTurret() < 2)
            {
                config.setTurret(config.getTurret() + 1);
                UpdateTank();
            }
        }

        private void TurretDown_OnClickCallback()
        {
            if (config.getTurret() > 0)
            {
                config.setTurret(config.getTurret() - 1);
                UpdateTank();
            }
        }

        private void ChassisUp_OnClickCallback()
        {
            if (config.getChassis() < 3)
            {
                config.setChassis(config.getChassis() + 1);
                UpdateTank();
            }
        }

        private void ChassisDown_OnClickCallback()
        {
            if (config.getChassis() > 0)
            {
                config.setChassis(config.getChassis() - 1);
                UpdateTank();
            }
        }

        private void ColorUp_OnClickCallback()
        {
            if (config.getColor() < 3)
            {
                config.setColor(config.getColor() + 1);
                UpdateTank();
            }
        }

        public override void Unload()
        {
            base.Unload();
        }

        private void ColorDown_OnClickCallback()
        {
            if (config.getColor() >0)
            {
                config.setColor(config.getColor() - 1);
                UpdateTank();
            }
        }

        public void UpdateTank()
        {
            SceneManager.Instance.CurrentScene.DestroyEntity(NPCTank);
            NPCTank = new CustomizableTankBuilder(false)
                        .SetChassis(config.getColor(), config.getChassis())
                        .SetTurret(config.getTurret())
                        .SetTracks(config.getTracks())
                        .Build();
            CreateEntity(NPCTank);
            TankState state = new TankState { Pos_X = 360, Pos_Y = 300 };
            NPCTank.UpdateTankState(state);
        }

        private void SaveConfig_OnClickCallback()
        {
            caretaker.Backup();
        }

        private void UndoConfig_OnClickCallback()
        {
            caretaker.Undo();
            ConcreteMemento memento = caretaker.getCurrent() as ConcreteMemento;
            if (memento != null)
            config = memento.GetState();
            UpdateTank();
        }

        public override void Render(Graphics context)
        {
            context.Clear(Color.FromArgb(77, 120, 78));
            base.Render(context);
            context.DrawString(config.ToString(), new Font(FontFamily.GenericMonospace, 15f, FontStyle.Bold), Brushes.Black, new Point(125, 450));
        }
    }
}

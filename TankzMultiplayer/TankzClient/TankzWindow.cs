using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using TankzClient.Framework;
using TankzClient.Game;

namespace TankzClient
{
    public partial class TankzClient : Form
    {
        public TankzClient()
        {
            InitializeComponent();

            // Use double buffering in order to prevent flickering
            DoubleBuffered = true;

            // Start the actuall game
            GameStart();
        }

        private void GameStart()
        {
            // Initialize your game here...
            // ...

            // Load starting scene
            SceneManager.Instance.LoadScene<TerrainScene>();
            NetworkManager.Instance.ConnectToServer();
            
            // Begin counting frames
            Thread updateThread = new Thread(GameLoop);
            Thread networkThread = new Thread(NetworkLoop);
            updateThread.IsBackground = true;
            updateThread.Start();
            networkThread.IsBackground = true;
            networkThread.Start();
        }
        private void NetworkLoop()
        {
            // Checks for response
            NetworkManager.Instance.ReceiveResponse();
        }

        /// <summary>
        /// Cap the framerate at which the game is updated
        /// </summary>
        private void GameLoop()
        {
            // Lock game at 30 FPS, reasonable enough for this kind of game
            const int FPS = 30;
            const float frameTime = 1.0f / FPS;
            long frameMs = (long)TimeSpan.FromSeconds(frameTime).TotalMilliseconds;
            long startTime = 0;

            // Create timer
            Stopwatch timer = new Stopwatch();
            timer.Start();

            while (true)
            {
                startTime = timer.ElapsedMilliseconds;

                // Update and redraw the game
                Update(frameTime);
                Input.Reset();


                // Wait for the next frame
                // while (timer.ElapsedMilliseconds - startTime < frameMs) ; //padaryti sleep
                Thread.Sleep(33);
            }
        }

        /// <summary>
        /// Update game logic
        /// </summary>
        /// <param name="deltaTime">Time since last frame</param>
        private void Update(float deltaTime)
        {
            // Update active scene
            SceneManager.Instance.CurrentScene.Update(deltaTime);

            // Request window redraw
            Invalidate();
        }

        /// <summary>
        /// Rendering stuff
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics context = e.Graphics;

            // Render the currently active scene
            SceneManager.Instance.CurrentScene.Render(context);

            base.OnPaint(e);
        }

        /// <summary>
        /// Mouse input
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            //NetworkManager.Instance.SendRequest(e.Location.ToString());
            //NetworkManager.Instance.SendRequest("meeting");
            Input.HandleMouseClick(e);
        }

        /// <summary>
        /// Keyboard input
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            Input.HandleKeyDown(e.KeyCode);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            Input.HandleKeyUp(e.KeyCode);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Input.HandleMousePosition(e.Location.X, e.Location.Y);
        }
    }
}

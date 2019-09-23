﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

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

            // Begin counting frames
            Thread updateThread = new Thread(GameLoop);
            updateThread.IsBackground = true;
            updateThread.Start();
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

            while (Application.AllowQuit)
            {
                startTime = timer.ElapsedMilliseconds;

                // Update and redraw the game
                Update(frameTime);

                // Wait for the next frame
                while (timer.ElapsedMilliseconds - startTime < frameMs) ;
            }
        }

        /// <summary>
        /// Update game logic
        /// </summary>
        /// <param name="deltaTime">Time since last frame</param>
        private void Update(float deltaTime)
        {
            // Update your game logic here...
            // ...

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
            // Render your game here ...
            // ...

            //TODO: remove later
            // just for testing
            Random rand = new Random();
            context.Clear(Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255)));

            base.OnPaint(e);
        }

        /// <summary>
        /// Mouse input
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            // Handle mouse clicks here ...
            // ...
        }

        /// <summary>
        /// Keyboard input
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Handle keyboard input here ...
            // ...
        }
    }
}

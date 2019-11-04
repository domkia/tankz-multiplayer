﻿using System;
using Microsoft.AspNetCore.SignalR.Client;

namespace TankzClient.Framework
{
    /// <summary>
    /// Server-side methods
    /// </summary>
    partial class NetworkManager
    {
        public void ChangeReadyState()
        {
            _connection.InvokeAsync("ChangeReadyState");
        }

        public void EndTurn(float angle, float power)
        {
            Console.WriteLine("Ended turn");
            _connection.InvokeAsync("EndTurn", angle, power);
        }

        /// <summary>
        /// Sends set name method request for server
        /// </summary>
        /// <param name="name">wanted name</param>
        public void SetName(string name)
        {
            _connection.InvokeAsync("SetName", name);
        }

        /// <summary>
        /// Send updated tank position
        /// </summary>
        public void SetPos(Vector2 newpos)
        {
            _connection.InvokeAsync("SetPos", newpos.x, newpos.y);
        }

        /// <summary>
        /// Send tank angle
        /// </summary>
        public void SetAngle(float angle)
        {
            _connection.InvokeAsync("SetAngle", angle);
        }

        /// <summary>
        /// Asks server for players
        /// </summary>
        public void GetPlayer()
        {
            _connection.InvokeAsync("GetPlayers");
        }

        /// <summary>
        /// Ask server for connected users
        /// </summary>
        public void GetConnected()
        {
            _connection.InvokeAsync("GetConnected");
        }

        public void GetCrate()
        {
            _connection.InvokeAsync("GetCrate");
        }
    }
}
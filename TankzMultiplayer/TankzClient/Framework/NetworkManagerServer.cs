﻿using System;
using Microsoft.AspNetCore.SignalR.Client;

namespace TankzClient.Framework
{
    /// <summary>
    /// Server-side methods
    /// </summary>
    partial class NetworkManager
    {
        public void JoinLobby(int lobbyNr)
        {
            _connection.InvokeAsync("JoinLobby", lobbyNr);
        }
        public void Register(string name, string password)
        {
            _connection.InvokeAsync("Register", name, password);
        }
        public void Login(string name, string password)
        {
            _connection.InvokeAsync("Login", name, password);
        }

        public void ChangeReadyState()
        {
            _connection.InvokeAsync("ChangeReadyState",CurrentLobby);
        }

        public void Shoot(float angle, float power)
        {
            Console.WriteLine("Tank Shoot " + angle);
            _connection.InvokeAsync("Shoot", angle, power, CurrentLobby);
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
            _connection.InvokeAsync("SetPos", newpos.x, newpos.y, CurrentLobby);
        }
        public void SavePos(Vector2 newpos)
        {
            _connection.InvokeAsync("SavePos", newpos.x, newpos.y, CurrentLobby);
        }
        /// <summary>
        /// Send tank angle
        /// </summary>
        public void SetAngle(float angle)
        {
            _connection.InvokeAsync("SetAngle", angle,CurrentLobby);
        }

        /// <summary>
        /// Asks server for players
        /// </summary>
        public void GetConnectedPlayerList()
        {
            _connection.InvokeAsync("GetConnected", CurrentLobby);
        }

        public void GetCrate()
        {
            _connection.InvokeAsync("GetCrate");
        }
    }
}

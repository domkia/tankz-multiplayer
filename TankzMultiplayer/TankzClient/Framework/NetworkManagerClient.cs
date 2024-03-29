﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TankzClient.Game;
using TankzClient.Models;

namespace TankzClient.Framework
{
    partial class NetworkManager
    {
        private Task Connected(string connectionId)
        {
            if(debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient Connected");
            connected = true;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Connected to the server ({connectionId})");
            Console.ResetColor();

            return Task.CompletedTask;
        }

        private Task Disconnected(string connectionId)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient Disconnected");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Disconnected from the server ({connectionId})");
            Console.ResetColor();

            return Task.CompletedTask;
        }
        private Task LoggedIn(string message)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient LoggedIn");
            MyData = JsonConvert.DeserializeObject<Player>(message);
            LoginSuccess?.Invoke(this, message);
            return Task.CompletedTask;
        }
        private Task LoginError(string message)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient LoginError");
            LoginErrorGot?.Invoke(this, message);
            return Task.CompletedTask;
        }
        private Task Registered(string message)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient Registered");
            RegisterSuccess?.Invoke(this, message);
            return Task.CompletedTask;
        }
        private Task RegisterError(string message)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient RegisterError");
            RegisterErrorGot?.Invoke(this, message);

            return Task.CompletedTask;
        }
        private Task LobbyError(string message)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient LobbyError");
            LobbyErrorGot?.Invoke(this, message);

            return Task.CompletedTask;
        }

        private Task PlayerListReceived(string playerList)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient PlayerListReceived");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(playerList);
            Console.ResetColor();

            Players = JsonConvert.DeserializeObject<List<Player>>(playerList);

            return Task.CompletedTask;
        }

        private Task GameStarted(string playersTanksObject)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient GameStarted");

            // Load gameplay scene
            SceneManager.Instance.LoadScene<GameplayScene>();

            // Tell to instantiate tanks
            List<Player> playersTanks = JsonConvert.DeserializeObject<List<Player>>(playersTanksObject);
            OnTankConfigsReceived?.Invoke(playersTanks);

            return Task.CompletedTask;
        }

        private Task ProjectileMove(float x, float y)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient ProjectileMove");
            Vector2 vec = new Vector2(x, y);
            ProjectileMoved?.Invoke(vec);
            return Task.CompletedTask;
        }

        private Task HealthUpdate(int health)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient HealthUpdate");
            HealthUpdated?.Invoke(health);
            return Task.CompletedTask;
        }
        private Task EndGame(string winner)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient EndGame");
            SceneManager.Instance.LoadScene<EndGameScene>();
            GameEnded?.Invoke(winner);
            return Task.CompletedTask;
        }

        private Task ShootingStart(float x, float y)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient ShootingStart");
            Vector2 vec = new Vector2(x, y);
            OnShootStart?.Invoke(vec);
            return Task.CompletedTask;
        }
        private Task ProjectileExplode()
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient ProjectileExpode");
            ProjectileExplosion?.Invoke();
            return Task.CompletedTask;
        }

        private Task MessageReceived(string message)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient MessageReceived");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Message: {message}");
            Console.ResetColor();

            return Task.CompletedTask;
        }

        private Task TurnStarted(string playerId)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient TurnStarted");
            Console.WriteLine("turn started");
            CurrentTurn = playerId;
            OnTurnStarted?.Invoke(null, EventArgs.Empty);

            return Task.CompletedTask;
        }

        private Task TankPosChanged(float x, float y, string playerId)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient TankPosChanged");
            MoveEventArtgs args = new MoveEventArtgs { X = x, Y = y, ConnID = playerId };
            PlayerMoved(this, args);

            return Task.CompletedTask;
        }

        private Task TankAngleChanged(float angle, string playerId)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient TankAngleChanged");
            RotateEventArgs args = new RotateEventArgs { Angle = angle, ConnID = playerId };
            BarrelRotate(this, args);

            return Task.CompletedTask;
        }

        private Task PlayerJoinedLobby(string playerObject, int currentLobby)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient PlayerJoinedLobby");
            Player player = JsonConvert.DeserializeObject<Player>(playerObject);
            Console.WriteLine(player.ConnectionId + " " + Me.ConnectionId);
            LobbyPlayers.Add(player);

            if (player.ConnectionId == Me.ConnectionId)
            {
                CurrentLobby = currentLobby;
                SceneManager.Instance.LoadScene<IngobbyScene>();
            }
            else
            {
                Console.WriteLine(player.Name + " has joined your lobby!");
            }
            return Task.CompletedTask;
        }

        private Task PlayerReadyStateChanged(string playerId, bool state)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient PlayerReadyStateChanged");
            Player player = GetPlayerById(playerId);
            player.ReadyState = state;
            return Task.CompletedTask;
        }

        private Task CrateDestroyed(int id)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient CrateDestroyed");
            OnCrateDestroyed ?.Invoke(id); //klasutukas reiskia nullable
            return Task.CompletedTask;
        }

        private Task CrateSpawned(string crate)
        {
            if (debug)
                Console.WriteLine($"OBSERVER: NetworkManagerClient CrateSpawned");
            Models.Crate c = JsonConvert.DeserializeObject<Models.Crate>(crate);
            OnCrateSpawned?.Invoke(c);
            return Task.CompletedTask;
        }
    }
}

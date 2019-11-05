using Newtonsoft.Json;
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
            connected = true;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Connected to the server ({connectionId})");
            Console.ResetColor();

            return Task.CompletedTask;
        }

        private Task Disconnected(string connectionId)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Disconnected from the server ({connectionId})");
            Console.ResetColor();

            return Task.CompletedTask;
        }
        private Task Registered(string message)
        {
            RegisterSuccess?.Invoke(this, message);

            return Task.CompletedTask;
        }
        private Task RegisterError(string message)
        {
            RegisterErrorGot?.Invoke(this, message);

            return Task.CompletedTask;
        }

        private Task PlayerListReceived(string playerList)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(playerList);
            Console.ResetColor();

            Players = JsonConvert.DeserializeObject<List<Player>>(playerList);

            return Task.CompletedTask;
        }

        private Task GameStarted(string playersTanksObject)
        {
            Console.WriteLine("Game Starting");

            // Load gameplay scene
            SceneManager.Instance.LoadScene<GameplayScene>();

            // Tell to instantiate tanks
            List<Player> playersTanks = JsonConvert.DeserializeObject<List<Player>>(playersTanksObject);
            OnTankConfigsReceived?.Invoke(playersTanks);

            return Task.CompletedTask;
        }

        private Task MessageReceived(string message)
        {
            //Debug, not used anymore
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Message: {message}");
            Console.ResetColor();

            return Task.CompletedTask;
        }

        private Task TurnStarted(string playerId)
        {
            Console.WriteLine("turn started");
            CurrentTurn = playerId;
            OnTurnStarted?.Invoke(null, EventArgs.Empty);

            return Task.CompletedTask;
        }

        private Task TankPosChanged(float x, float y, string playerId)
        {
            MoveEventArtgs args = new MoveEventArtgs { X = x, Y = y, ConnID = playerId };
            PlayerMoved(this, args);

            return Task.CompletedTask;
        }

        private Task TankAngleChanged(float angle, string playerId)
        {
            RotateEventArgs args = new RotateEventArgs { Angle = angle, ConnID = playerId };
            BarrelRotate(this, args);

            return Task.CompletedTask;
        }

        private Task PlayerJoinedLobby(string playerObject)
        {
            Player player = JsonConvert.DeserializeObject<Player>(playerObject);
            Players.Add(player);

            if (player.ConnectionId == Me.ConnectionId)
            {
                SceneManager.Instance.LoadScene<IngobbyScene>();
            }
            return Task.CompletedTask;
        }

        private Task PlayerReadyStateChanged(string playerId, bool state)
        {
            Player player = GetPlayerById(playerId);
            player.ReadyState = state;
            return Task.CompletedTask;
        }
    }
}

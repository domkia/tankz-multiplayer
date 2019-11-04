using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TankzClient.Game;
using TankzClient.Models;

namespace TankzClient.Framework
{
    partial class NetworkManager
    {
        private Task Connected(string connectionId)
        {
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

        private Task PlayerListReceived(string playerList)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(playerList);
            Console.ResetColor();

            Players = JsonConvert.DeserializeObject<Player[]>(playerList);
            OnPlayersGot(EventArgs.Empty);

            return Task.CompletedTask;
        }

        private Task GameStarted(string value)
        {
            Console.WriteLine("Game Starting");
            SceneManager.Instance.LoadScene<GameplayScene>();

            //TODO: ???
            OnPlayerChanged(EventArgs.Empty);

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
            if (playerId == _connection.ConnectionId)
            {
                currentTurn = "YOU";
            }
            else
            {
                currentTurn = playerId;
            }

            //TODO: ???
            OnPlayerChanged(EventArgs.Empty);

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
    }
}

using System;
using System.Drawing;
using TankzClient.Framework;
using TankzClient.Game;

namespace TankzClient.Game
{
    /// <summary>
    /// Projectile factory
    /// </summary>
    public class ProjectileFactory : EntityFactory
    {
        public override Entity Create(EntityCreateArgs args)
        {
            Projectile projectile = null;
            switch (args.type.ToLower())
            {
                case "grenade":
                    projectile = new Grenade(Image.FromFile("../../res/projectile_grenade.png"), new Vector2(0, 0), 50, 200, 50, 2f);
                    break;
            }
            return SceneManager.Instance.CurrentScene.CreateEntity(projectile);
        }
    }
}

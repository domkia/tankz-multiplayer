using TankzClient.Framework;

namespace TankzClient.Game
{
    /// <summary>
    /// Tank customization class lets you
    /// apply tank decorations
    /// </summary>
    public static class TankCustomization
    {
        public static void ApplyCamouflage(this Tank tank, int camo)
        {
            TankChassis chassis = tank.FindChild<TankDecorator>();
            if (chassis == null)
                chassis = tank.FindChild<TankChassis>();
            TankCamoDecorator camoChassis = new TankCamoDecorator(camo, chassis);
            camoChassis.SetParent(tank);
            SceneManager.Instance.CurrentScene.DestroyEntity(chassis);
            SceneManager.Instance.CurrentScene.CreateEntity(camoChassis);
        }

        public static void ApplyAccessory(this Tank tank, int accessory)
        {
            TankChassis chassis = tank.FindChild<TankDecorator>();
            if (chassis == null)
                chassis = tank.FindChild<TankChassis>();
            TankAccessoriesDecorator accessoryChassis = new TankAccessoriesDecorator(accessory, chassis);
            accessoryChassis.SetParent(tank);
            SceneManager.Instance.CurrentScene.DestroyEntity(chassis);
            SceneManager.Instance.CurrentScene.CreateEntity(accessoryChassis);
        }

        public static void ApplySideskirt(this Tank tank, int sideskirt)
        {
            TankChassis chassis = tank.FindChild<TankDecorator>();
            if (chassis == null)
                chassis = tank.FindChild<TankChassis>();
            TankSideskirtDecorator sideskirtChassis = new TankSideskirtDecorator(sideskirt, chassis);
            sideskirtChassis.SetParent(tank);
            SceneManager.Instance.CurrentScene.DestroyEntity(chassis);
            SceneManager.Instance.CurrentScene.CreateEntity(sideskirtChassis);
        }
    }
}

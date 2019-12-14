using System.Drawing;

namespace TankzClient.Framework
{
    /// <summary>
    /// UI Factory
    /// Creates default UI elements
    /// </summary>
    public class UIFactory : EntityFactory
    {
        public override Entity Create(EntityCreateArgs args)
        {
            System.Console.WriteLine($"FACTORY UIFactory: Create()");
            if (args == null ||args.type == null || args.type=="" ||args.type =="\0")
                return null;
            UICreateArgs ui = args as UICreateArgs;
            UIElement element = null;
            switch (ui.type.ToLower())
            {
                case "button":
                    System.Console.WriteLine($"\tCreating UI Button");
                    element = new Button(
                        (int)ui.position.x,
                        (int)ui.position.y,
                        128,
                        32, null, "new button");
                    break;
                case "inputfield":
                    System.Console.WriteLine($"\tCreating UI InputField");
                    element = new InputField(
                        (int)ui.position.x,
                        (int)ui.position.y,
                        120,
                        20);
                    break;
                case "progressbar":
                    System.Console.WriteLine($"\tCreating UI ProgressBar");
                    element = new ProgressBar(
                        new Rectangle((int)ui.position.x, (int)ui.position.y, 64, 8), 
                        Color.LightGreen, 
                        Color.DarkGreen);
                    break;
                default:
                    element = new NullUIElement(
                        new Rectangle((int)ui.position.x, (int)ui.position.y, 100, 100));
                    break;
            }
            return SceneManager.Instance.CurrentScene.CreateEntity(element);
        }
    }

    public class UICreateArgs : EntityCreateArgs
    {
        public Vector2 position;

        public UICreateArgs(string type, Vector2 position)
            : base(type)
        {
            this.position = position;
        }
    }
}

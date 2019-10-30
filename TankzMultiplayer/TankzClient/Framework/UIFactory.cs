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
            UICreateArgs ui = args as UICreateArgs;
            UIElement element = null;
            switch (ui.type.ToLower())
            {
                case "button":
                    element = new Button(
                        (int)ui.position.x,
                        (int)ui.position.y,
                        128,
                        32, null, "new button");
                    break;
                case "inputfield":
                    element = new InputField(
                        (int)ui.position.x,
                        (int)ui.position.y,
                        120,
                        20);
                    break;
                case "progressbar":
                    element = new ProgressBar(
                        new Rectangle((int)ui.position.x, (int)ui.position.y, 64, 8), 
                        Color.LightGreen, 
                        Color.DarkGreen);
                    break;
                default:
                    return null;
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

using System;
using System.Collections.Generic;

namespace TankzClient.Framework
{
    public class SceneManager
    {
        #region Singleton

        private static SceneManager instance = null;
        public static SceneManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SceneManager();
                return instance;
            }
        }

        #endregion

        private Dictionary<Type, Scene> scenes = new Dictionary<Type, Scene>();

        public Scene CurrentScene { get; private set; }

        private Scene AddScene<TScene>() where TScene : Scene, new()
        {
            Scene scene = new TScene();
            scenes.Add(typeof(TScene), scene);
            return scene;
        }

        /// <summary>
        /// Load scene of specific type
        /// </summary>
        public void LoadScene<TScene>() where TScene : Scene, new()
        {
            Type sceneType = typeof(TScene);
            if (scenes.ContainsKey(sceneType))
            {
                Scene sceneToLoad = scenes[sceneType];
                sceneToLoad.Load();
                CurrentScene = sceneToLoad;
            }
            else
            {
                CurrentScene = AddScene<TScene>();
                CurrentScene.Load();
            }
        }
    }
}

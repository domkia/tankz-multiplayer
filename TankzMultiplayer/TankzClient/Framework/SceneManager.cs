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
                //Console.WriteLine($"SINGLETON SceneManager: GetInstance()");
                if (instance == null)
                {
                    Console.WriteLine($"\tSceneManager: Creating new instance");
                    instance = new SceneManager();
                }
                return instance;
            }
        }

        #endregion

        private Dictionary<Type, Scene> scenes = new Dictionary<Type, Scene>();

        public Scene CurrentScene { get; private set; }

        /// <summary>
        /// Try get already loaded scene from the list
        /// </summary>
        /// <typeparam name="TScene">Type of the scene</typeparam>
        private Scene GetScene<TScene>() where TScene : Scene
        {
            Type sceneType = typeof(TScene);
            if (!scenes.ContainsKey(sceneType))
            {
                throw new Exception("Scene you are trying to get does not exist in the scenes list");
            }
            return scenes[sceneType];
        }

        /// <summary>
        /// Load scene of specific type
        /// </summary>
        public void LoadScene<TScene>() where TScene : Scene, new()
        {
            Type sceneType = typeof(TScene);
            if (!scenes.ContainsKey(sceneType))
            {
                scenes.Add(sceneType, new TScene());
            }

            // Unload current scene
            if (scenes.Values.Count > 1 && CurrentScene.GetType() != sceneType)
            {
                CurrentScene.Unload();
                scenes.Remove(CurrentScene.GetType());
            }

            // Reload scene
            CurrentScene = scenes[sceneType];
            CurrentScene.Load();
        }

        /// <summary>
        /// Load new scene without unloading current one
        /// </summary>
        /// <typeparam name="TScene"></typeparam>
        public void LoadSceneAdditively<TScene>() where TScene : Scene, new()
        {
            Type sceneType = typeof(TScene);
            if (!scenes.ContainsKey(sceneType))
            {
                scenes.Add(sceneType, new TScene());
            }

            CurrentScene = scenes[sceneType];
            CurrentScene.Load();
        }

        public bool UnloadScene<TScene>() where TScene : Scene
        {
            if (scenes.ContainsKey(typeof(TScene)))
            {
                if (scenes.Keys.Count > 1)
                {
                    // TODO: call dispose method?
                    scenes.Remove(typeof(TScene));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}

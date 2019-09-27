using System.Collections.Generic;
using System.Drawing;

namespace TankzClient.Framework
{
    /// <summary>
    /// Base scene class
    /// </summary>
    public abstract class Scene
    {
        // Every entity in the scene that needs updating.
        protected List<Entity> entities = new List<Entity>();

        // All renderables in the scene separated into layers.
        // The number parameter defines - Sorting Layer.
        protected SortedDictionary<int, HashSet<IRenderable>> renderLayers = new SortedDictionary<int, HashSet<IRenderable>>();

        /// <summary>
        /// Update all objects in the scene
        /// </summary>
        /// <param name="deltaTime">Time since last frame</param>
        public virtual void Update(float deltaTime)
        {
            foreach (Entity entity in entities)
            {
                entity.Update(deltaTime);
            }
        }

        /// <summary>
        /// Render all renderable objects in the scene
        /// </summary>
        /// <param name="context">Graphics context</param>
        public virtual void Render(Graphics context)
        {
            foreach (int layer in renderLayers.Keys)
            {
                foreach (IRenderable renderable in renderLayers[layer])
                {
                    renderable.Render(context);
                }
            }
        }

        /// <summary>
        /// Create an instance of Entity in the scene.
        /// </summary>
        /// <param name="newEntity">Instance of newly created entity</param>
        /// <returns>The same entity that was created</returns>
        /// TODO: rename to 'AddToScene' ???
        public Entity CreateEntity(Entity newEntity)
        {
            // Put entity into the list
            entities.Add(newEntity);

            // Check whether entity needs rendering
            // and if so, insert it into the render list
            IRenderable renderable = newEntity as IRenderable;
            if (renderable != null)
            {
                int layer = renderable.SortingLayer;
                if (!renderLayers.ContainsKey(layer))
                {
                    renderLayers.Add(layer, new HashSet<IRenderable>());
                }
                renderLayers[layer].Add(renderable);
            }

            return newEntity;
        }
    }
}

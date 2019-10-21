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


        protected HashSet<UIElement> userInterfaceElements = new HashSet<UIElement>();
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
            foreach(UIElement element in userInterfaceElements)
            {
                if (Input.MouseButtonDown)
                {
                    element.Click(Input.MousePosition);
                }
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
                    context.Transform = renderable.OrientationMatrix;
                    renderable.Render(context);
                    context.ResetTransform();
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
            if (newEntity == null)
                throw new System.NullReferenceException();

            // Put entity into the list
            // only if it does not already exist
            if (entities.Contains(newEntity))
                return null;
            entities.Add(newEntity);
            if (newEntity.children.Count > 0)
            {
                foreach (Entity child in newEntity.children)
                {
                    entities.Add(child);
                }
            }

            // TODO: Possible double update bug
            UIElement element = newEntity as UIElement;
            if(element != null)
            {
                userInterfaceElements.Add(element);
            }

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

        /// <summary>
        /// Remove entity from the scene
        /// </summary>
        /// <param name="entity">Entity to destroy</param>
        public bool DestroyEntity(Entity entity)
        {
            if (entity == null)
                return false;

            // Remove entity's renderable from render list
            IRenderable renderable = entity as IRenderable;
            if (renderable != null)
            {
                int layer = renderable.SortingLayer;
                if (renderLayers[layer].Contains(renderable))
                    renderLayers[layer].Remove(renderable);
            }

            // Remove entity from the list
            if (entities.Contains(entity))
            {
                if (entity.children.Count > 0)
                {
                    foreach (Entity child in entity.children)
                    {
                        DestroyEntity(child);
                    }
                }
                return entities.Remove(entity);
            }

            return false;
        }
    }
}

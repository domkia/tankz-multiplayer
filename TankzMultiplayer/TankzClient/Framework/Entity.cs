using System;
using System.Collections.Generic;
using TankzClient.Framework.Components;

namespace TankzClient.Framework
{
    /// <summary>
    /// Base Entity class
    /// </summary>
    public abstract class Entity
    {
        // Components attached to this entity
        private Dictionary<Type, IComponent> components = new Dictionary<Type, IComponent>();

        /// <summary>
        /// Gets specific component from this entity
        /// </summary>
        /// <returns></returns>
        public TComponent GetComponent<TComponent>() where TComponent : class, IComponent
        {
            if (components.ContainsKey(typeof(TComponent)))
            {
                return (TComponent)components[typeof(TComponent)];
            }
            return null;
        }

        /// <summary>
        /// Add initialized component to this entity
        /// </summary>
        public IComponent AddComponent<TComponent>(TComponent component) where TComponent : IComponent, new()
        {
            if (!components.ContainsKey(typeof(TComponent)))
            {
                components.Add(typeof(TComponent), component);
                return component;
            }
            return null;
        }

        /// <summary>
        /// Add new component to this entity
        /// </summary>
        public IComponent AddComponent<TComponent>() where TComponent : IComponent, new()
        {
            return AddComponent<TComponent>(new TComponent());
        }

        /// <summary>
        /// Update each compoent added to this entity
        /// </summary>
        /// <param name="deltaTime">Time since last frame</param>
        public virtual void Update(float deltaTime)
        {
            foreach (IComponent component in components.Values)
                component.Update(deltaTime, this);
        }
    }
}

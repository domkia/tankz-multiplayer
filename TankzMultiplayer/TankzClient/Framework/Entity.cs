using System;
using System.Collections.Generic;

namespace TankzClient.Framework
{
    /// <summary>
    /// Base Entity class
    /// </summary>
    public abstract class Entity
    {
        public Entity parent { get; internal set; }
        public List<Entity> children { get; internal set; }

        public Transform transform { get; protected set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        protected Entity(Entity parent = null)
        {
            transform = new Transform();
            children = new List<Entity>();
            if (parent != null)
            {
                SetParent(parent);
            }
        }

        /// <summary>
        /// Set this entity as a child of given parent
        /// </summary>
        /// <param name="parent"></param>
        private void SetParent(Entity parent)
        {
            if (children.Contains(parent))
            {
                throw new Exception("Can't set child as a parent. Possible loop");
            }

            if (parent != null)
            {
                this.parent = parent;
                parent.children.Add(this);
            }
            else
            {
                this.parent.children.Remove(this);
                this.parent = null;
            }
        }

        /// <summary>
        /// Update this entity
        /// </summary>
        /// <param name="deltaTime">Time since last frame</param>
        public virtual void Update(float deltaTime) { }
    }
}

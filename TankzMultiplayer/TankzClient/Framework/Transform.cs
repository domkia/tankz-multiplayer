using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TankzClient.Framework
{
    public sealed class Transform
    {
        public Vector2 localPosition { get; private set; }

        private Vector2 worldPosition = new Vector2();
        public Vector2 position
        {
            get
            {
                if (local.parent != null)
                {
                    worldPosition = GetParentWorldPos() + this.localPosition;
                }
                return worldPosition;
            }
            private set
            {
                worldPosition = value;
            }
        }

        public float angle { get; private set; }            
        public Vector2 size { get; private set; }

        private Matrix orientationMatrix;
        public Matrix OrientationMatrix
        {
            get
            {
                orientationMatrix.Reset();
                orientationMatrix.Translate(position.x, position.y);
                orientationMatrix.Rotate(angle);
                orientationMatrix.Translate(-position.x, -position.y);
                return orientationMatrix;
            }
        }

        public Rectangle Rect
        {
            get
            {
                return new Rectangle(
                    (int)(position.x - 0.5f * size.x),
                    (int)(position.y - 0.5f * size.y),
                    (int)size.x, 
                    (int)size.y
                );
            }
        }

        private Entity local = null;

        public Transform(Entity entity)
        {
            this.local = entity;
            this.localPosition = new Vector2(0, 0);
            this.position = new Vector2(0, 0);
            this.size = new Vector2(100, 100);
            this.angle = 0.0f;
            this.orientationMatrix = new Matrix();
        }

        public void SetPosition(Vector2 newPosition)
        {
            if (local.parent != null)
            {
                this.localPosition = newPosition;
                this.worldPosition = position;
            }
            else
                this.position = newPosition;
        }

        public void Translate(Vector2 offset)
        {
            if (local.parent != null)
                this.localPosition += offset;
            this.position += offset;
        }

        public void SetAngle(float newAngle)
        {
            this.angle = newAngle;
        }

        public void Rotate(float deltaAngle)
        {
            this.angle += deltaAngle;
        }

        public void SetSize(Vector2 newSize) => this.size = newSize;

        /// <summary>
        /// If this entity happens to be a child of
        /// other entity, calculate the total position
        /// </summary>
        /// <returns>Parent's world position</returns>
        public Vector2 GetParentWorldPos()
        {
            Vector2 newPosition = new Vector2();
            Entity parent = local.parent;
            if (parent == null)
                return worldPosition;
            else
            {
                while (true)
                {
                    if (parent.parent == null)
                    {
                        newPosition += parent.transform.position;
                        break;
                    }
                    else
                    {
                        newPosition += parent.transform.localPosition;
                    }
                    parent = parent.parent;
                }
                return newPosition;
            }
        }
    }
}

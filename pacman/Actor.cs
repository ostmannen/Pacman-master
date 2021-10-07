using System.Runtime.CompilerServices;
using System.Threading;
using System.Drawing;
using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.Linq;

namespace pacman
{
    public class Actor : Entity
    {
        private bool wasAligned;
        protected float speed;
        protected int direction;
        protected bool moving;
        protected Vector2f originalProstion;
        protected float originalSpeed = 100.0f;

        protected Actor() : base("pacman")
        {

        }
        protected void reset()
        {
            wasAligned = false;
            Position = originalProstion;
            speed = originalSpeed;
        }
        public override void Create(Scene scene)
        {
            base.Create(scene);
            originalProstion = Position;
        }
        protected bool IsAligned =>
        (int)MathF.Floor(Position.X) % 18 == 0 &&
        (int)MathF.Floor(Position.Y) % 18 == 0;
        protected bool isFree(Scene scene, int dir)
        {
            Vector2f at = Position + new Vector2f(9, 9);
            at += 18 * ToVector(dir);
            FloatRect rect = new FloatRect(at.X, at.Y, 1, 1);
            return !scene.FindIntersects(rect).Any(e => e.Solid);
        }
        protected static Vector2f ToVector(int dir)
        {
            System.Console.WriteLine();
            switch (dir)
            {
                case 0:
                    return new Vector2f(1, 0);
                case 1:
                    return new Vector2f(0, -1);
                case 2:
                    return new Vector2f(-1, 0);
                case 3:
                    return new Vector2f(0, 1);
            }
            return new Vector2f();
        }
        protected virtual int PickDirection(Scene scene)
        {
            return 0;
        }
        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            if (IsAligned)
            {
                if (!wasAligned)
                {
                    direction = PickDirection(scene);
                }
                if (moving)
                {
                    wasAligned = true;
                }
            }
            else
            {
                wasAligned = false;
            }
            if (!moving) return;
            Position += ToVector(direction) * (speed * deltaTime);
            Position = MathF.Floor(Position.X) switch
            {
                < 0 => new Vector2f(432, Position.Y),
                > 432 => new Vector2f(0, Position.Y),
                _ => Position
            };
        }
    }
}
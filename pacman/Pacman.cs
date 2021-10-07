using System.Runtime.CompilerServices;
using System.Threading;
using System.Drawing;
using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.Linq;
using static SFML.Window.Keyboard.Key;

namespace pacman
{
    public class Pacman : Actor
    {
        public override void Create(Scene scene)
        {
            speed = 100.0f;
            base.Create(scene);
            sprite.TextureRect = new IntRect(0, 0, 18, 18);
            scene.events.LoseHealth += OnLoseHealth;
            
        }
        private void OnLoseHealth(Scene scene, int amount){
            reset();
        }
        protected override int PickDirection(Scene scene)
        {
            int dir = direction;
            if (Keyboard.IsKeyPressed(Right))
            {
                dir = 0; moving = true;;
            }
            else if (Keyboard.IsKeyPressed(Up))
            {
                dir = 1; moving = true;
            }
            else if (Keyboard.IsKeyPressed(Left))
            {
                dir = 2; moving = true;

            }
            else if (Keyboard.IsKeyPressed(Down))
            {
                dir = 3; moving = true;
            }
            if (isFree(scene, dir)) 
            {
                return dir;
            }

            if (!isFree(scene, direction)) moving = false;
            return direction;
        }
        public override void Destroy(Scene scene)
        {
            base.Destroy(scene);
            scene.events.LoseHealth -= OnLoseHealth;
        }
        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            sprite.TextureRect = new IntRect(0,direction * 18, 18, 18);
        }
    }
}

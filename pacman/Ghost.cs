using System.Collections.Generic;
using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace pacman
{
    public class Ghost : Actor
    {
        
        public override void Create(Scene scene){
            direction = -1;
            speed = 100.0f;
            moving = true;
            base.Create(scene);
            sprite.TextureRect = new IntRect(36, 0, 18, 18);
            scene.CandyEaten += OnCandyEaten;
            
        }
         private void OnCandyEaten(Scene scene, int amount){
            reset();
        }
        protected override int PickDirection(Scene scene)
        {
            List<int> validMoves = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                if (i + 2 % 4 == direction) continue;
                if (isFree(scene, i)) validMoves.Add(i);
            }
            int r = new Random().Next(0, validMoves.Count);
            return validMoves[r];
        }
        protected override void CollideWith(Scene scene, Entity entity)
        {
            if (entity is Pacman){
                System.Console.WriteLine("hej");
                scene.PublishLoseHealth(1);
                reset();
            }
        }
        //private float frozenTimer(){

        //}
    }
}

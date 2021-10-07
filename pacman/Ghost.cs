using System.Collections.Generic;
using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace pacman
{
    public class Ghost : Actor
    {
        public float timer = 0;     
        public float spriteTimer = 0;   
        public int spriteXPostion = 0;
        public override void Create(Scene scene){
            direction = -1;
            speed = 100.0f;
            moving = true;
            base.Create(scene);
            sprite.TextureRect = new IntRect(36, 0, 18, 18);
            scene.events.CandyEaten += OnCandyEaten;
        }
         private void OnCandyEaten(Scene scene, int amount){
            timer += 5;
        }
        protected override int PickDirection(Scene scene)
        {
            List<int> validMoves = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                if ((i + 2) % 4 == direction) continue;
                if (isFree(scene, i)) validMoves.Add(i);
            }
            int r = new Random().Next(0, validMoves.Count);
            return validMoves[r];
        }
        protected override void CollideWith(Scene scene, Entity entity)
        {
            if (entity is Pacman && timer == 0){
                scene.events.PublishLoseHealth(1);
                reset();
            }
            else if (entity is Pacman && timer > 0) {
                reset();
            }
        }
        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);  
            spriteTimer += deltaTime;
            if (spriteTimer <= 0.25) {
                spriteXPostion = 54;
            }
            else if (spriteTimer >= 0.25)
            {
                sprite.TextureRect = new IntRect(spriteXPostion, 0, 18, 18);
                spriteXPostion = 36;
                if (spriteTimer >= 0.5){
                    spriteTimer = 0;
                }
            }
            frozenTimer = MathF.Max(frozenTimer - deltaTime, 0.0f);
            if (timer > 0){

                sprite.TextureRect = new IntRect(spriteXPostion, 18, 18, 18);
            }
            else {
                sprite.TextureRect = new IntRect(spriteXPostion, 0, 18, 18);
            }
            
        }
        public float frozenTimer
        {
            get => timer;
            set => timer = value;
        }
    }
}

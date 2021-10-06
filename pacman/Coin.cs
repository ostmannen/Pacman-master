using System.Threading;
using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace pacman
{
    public class Coin : Entity
    {
        public Coin() : base("pacman"){}
        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = new IntRect(36,36,18,18);
        }
        protected override void CollideWith(Scene scene, Entity entity)
        {
            if (entity is Pacman){
                scene.PublishGainScore(100);
                dead = true;
            }
        }
    }
}

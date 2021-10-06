using System.Reflection.Metadata;
using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace pacman
{
    public class Candy : Entity
    {
        public Candy() : base("pacman"){}
        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = new IntRect(54,36,18,18);
        }
        protected override void CollideWith(Scene scene, Entity entity)
        {
            if (entity is Pacman){
                scene.PublishEatenCandy(1);
                dead = true;
            }
        }
    }
}

using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace pacman
{
    public class Entity
    { 
        private string texturename; 
        protected readonly Sprite sprite;
        public bool dead;
        public bool DontDestroyOnload;
        

        protected Entity(string texturename){
            this.texturename = texturename;
            sprite = new Sprite();
        }
        public virtual void Update(Scene scene, float deltaTime){
            foreach (Entity found in scene.FindIntersects(Bounds)){
                CollideWith(scene, found);
            }
        }
        public virtual Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }
        public virtual FloatRect Bounds => sprite.GetGlobalBounds();
        //public readonly bool solid(){}
        public virtual bool Solid => false;
        //vt inte ifall det är rätt, men det fungerar och jag ser inte något rött
        public virtual void Create(Scene scene){
            sprite.Texture = scene.assets.LoadTexure(texturename);
            
        }
        public virtual void render(RenderTarget target){
            target.Draw(sprite);
        }
        protected virtual void CollideWith(Scene scene, Entity entity){

        }
        public virtual void Destroy(Scene scene){

        }
    }
}
using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace pacman
{
    public delegate void ValueChangedEvent(Scene scene, int value);
    
    public sealed class Scene
    {
        public event ValueChangedEvent GainScore;
        public event ValueChangedEvent LoseHealth;
        public event ValueChangedEvent CandyEaten;
        private List<Entity> entities;
        public readonly SceneLoader loader;
        public readonly AssetManager assets;
        private int scoreGained;
        private int lostHealth;
        private int candyCounter;
        public void PublishGainScore(int amount) => scoreGained += amount;
        public void PublishLoseHealth(int amount) => lostHealth -= amount;
        public void PublishEatenCandy(int amount) => candyCounter -= amount;

        public Scene()
        {
            loader = new SceneLoader();
            entities = new List<Entity>();
            assets = new AssetManager();
        }
        public void spawn(Entity entity)
        {
            entities.Add(entity);
            entity.Create(this);
        }
        public void UpdateAll(float deltaTime)
        {
            loader.HandleSceneLoad(this);
            for (int i = 0; i < entities.Count; i++)
            {
                Entity entity = entities[i];
                entity.Update(this, deltaTime);
            }
            for (int i = 0; i < entities.Count;)
            {
                Entity entity = entities[i];
                if (entity.dead){
                    entities.RemoveAt(i);

                }
                else i++;
            }
            if (scoreGained != 0){
                GainScore?.Invoke(this, scoreGained);
                scoreGained = 0;
            }
            if (lostHealth != 0){
                LoseHealth?.Invoke(this, scoreGained);
            }
            if (candyCounter != 0){
                CandyEaten?.Invoke(this, candyCounter);
            }
        }
        public void RenderAll(RenderTarget target)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].render(target);
            }
            
        }
        public bool FindByType<T>(out T found) where T : Entity
        {
            found = default(T);
            foreach (Entity entity in entities)
            {
                if (entity is T typed)
                {
                    found = typed;
                    return true;
                }
            }
            return false;
        }
        public void clear()
        {
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity entity = entities[i];
                entities.RemoveAt(i);
                entity.Destroy(this);
            }
        }
        public IEnumerable<Entity> FindIntersects(FloatRect bounds)
        {
            int lastEntity = entities.Count - 1;
            for (int i = lastEntity; i >= 0; i--)
            {
                Entity entity = entities[i];
                if (entity.dead) continue;
                if (entity.Bounds.Intersects(bounds))
                {
                    yield return entity;
                }
            }
        }
    }
}
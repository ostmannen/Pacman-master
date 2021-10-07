using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace pacman
{

    public sealed class Scene
    {
        
        private List<Entity> entities;
        public readonly SceneLoader loader;
        public readonly EventManager events;
        public readonly AssetManager assets;
        public Scene()
        {
            loader = new SceneLoader();
            entities = new List<Entity>();
            assets = new AssetManager();
            events = new EventManager();

        }
        public void spawn(Entity entity)
        {
            entities.Add(entity);
            entity.Create(this);
        }
        public void UpdateAll(float deltaTime)
        {
            events.Update(this);
            loader.HandleSceneLoad(this);
            for (int i = 0; i < entities.Count; i++)
            {
                Entity entity = entities[i];
                entity.Update(this, deltaTime);
            }
            for (int i = 0; i < entities.Count;)
            {
                Entity entity = entities[i];
                if (entity.dead)
                {
                    entities.RemoveAt(i);
                }
                else i++;
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
                if (!entity.DontDestroyOnload)
                {
                    entities.RemoveAt(i);
                    entity.Destroy(this);
                }
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
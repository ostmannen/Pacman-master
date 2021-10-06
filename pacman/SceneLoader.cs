using System.Text;
using System.IO;
using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace pacman
{
    public class SceneLoader
    {
        private readonly Dictionary<char, Func<Entity>> loaders;
        private string currentScene = "", nextScene = "";

        public SceneLoader()
        {
            loaders = new Dictionary<char, Func<Entity>>(){
                {'#',() => new Wall()},
                {'g',() => new Ghost()},
                {'p',() => new Pacman()},
                {'c',() => new Candy()},
                {'.',() => new Coin()}
            };
            
        }
        private bool Create(char symbol, out Entity created)
        {
            if (loaders.TryGetValue(symbol, out Func<Entity> loader))
            {
                created = loader();
                return true;
            }
            created = null;
            return false;
        }
        public void HandleSceneLoad(Scene scene)
        {
            if (nextScene == "") return;
            scene.clear();
            string file = $"assets/{nextScene}.txt";
            Console.WriteLine($"Loading scene '{file}");
            currentScene = nextScene;
            nextScene = null;
            
            int y = 0;
            int x = 0;
            
            
            foreach (var line in File.ReadLines(file, Encoding.UTF8))
            {
                
                foreach (char c in line)
                {
                    if(Create(c, out Entity entity)){
                        entity.Position = new Vector2f(x * 18, y * 18);
                        scene.spawn(entity);
                    }
                    x++;
                }
                y++;
                x = 0;
            }
            currentScene = nextScene;
            nextScene = "";
            scene.spawn(new Gui());
           
        }
        public void Load(string scene) => nextScene = scene;
        public void Reload() => nextScene = currentScene;
    }
}
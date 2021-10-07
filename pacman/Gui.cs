using System.Runtime.CompilerServices;
using System.Threading;
using System.Drawing;
using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;
using System.Linq;

namespace pacman
{
    public class Gui : Entity
    {
        private Text scoreText;
        private int maxHealth;
        private int currentHealth;
        private int currentScore;
        //public readonly AssetManager assets; 

        public Gui() : base("pacman"){}
        public override void Create(Scene scene)
        {
            base.Create(scene);
            sprite.TextureRect = new IntRect(0,0,18,18);
            scoreText = new Text();
            scoreText.CharacterSize = 50;
            scoreText.Font = new Font("assets/pixel-font.ttf");
            scoreText.DisplayedString = "Score";
            currentHealth= maxHealth;
            scene.GainScore += OnGainScore;
        }
        public override void render(RenderTarget target)
        {
            sprite.Position = new Vector2f(36,396);
            for (int i = 0; i < maxHealth; i++){
                sprite.TextureRect = i < currentHealth
                ? new IntRect(72, 36, 18,18)
                : new IntRect(72,0 ,18,18);
                base.render(target);
                sprite.Position += new Vector2f(18,0);
            }
            scoreText.DisplayedString = $"Score: {currentScore}";
            scoreText.Position = new Vector2f(
                414 - scoreText.GetGlobalBounds().Width, 396
            );
            target.Draw(scoreText);
        }
        private void OnLoseHealth(Scene scene, int amount){
            currentHealth -= amount;
            if (currentHealth <= 0){
                DontDestroyOnload = false;
                scene.loader.Reload();
            }
        }
        private void OnGainScore(Scene scene, int amount){
            currentScore += 100;
            if (!scene.FindByType<Coin>(out _)){
                DontDestroyOnload = true;
                scene.loader.Reload();
            }
        }
    }
}

using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace pacman
{
    public delegate void ValueChangedEvent(Scene scene, int value);
    public class EventManager
    {
        public event ValueChangedEvent GainScore;
        public event ValueChangedEvent LoseHealth;
        public event ValueChangedEvent CandyEaten;
        private int scoreGained;
        private int lostHealth;
        private int candyCounter;
        public void PublishGainScore(int amount) => scoreGained += amount;
        public void PublishLoseHealth(int amount) => lostHealth += amount;
        public void PublishEatenCandy(int amount) => candyCounter -= amount;
        public void Update(Scene scene){
            if (scoreGained != 0)
            {
                GainScore?.Invoke(scene, scoreGained);
                scoreGained = 0;
            }
            if (lostHealth != 0)
            {
                LoseHealth?.Invoke(scene, lostHealth);
                lostHealth = 0;
            }
            if (candyCounter != 0)
            {
                CandyEaten?.Invoke(scene, candyCounter);
                candyCounter = 0;
            }
        }

    }
}

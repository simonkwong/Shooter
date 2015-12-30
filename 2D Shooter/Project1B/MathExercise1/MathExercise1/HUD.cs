using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text;

namespace MathExercise1
{
    class HUD
    {
        public Vector2 scorePosition { get; set; }

        public SpriteFont scoreFont { get; set; }

        public Vector2 healthPosition { get; set; }

        public Vector2 lifePosition { get; set; }

        public int Enemies { get; set; }

        public int Health { get; set; }

        public int Score { get; set; }

        public int Lives { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.DrawString(scoreFont, "Health: " + Health.ToString(), healthPosition, Color.White);
            spriteBatch.DrawString(scoreFont, "Lives: " + Lives.ToString(), lifePosition, Color.White);
            spriteBatch.DrawString(scoreFont, "Enemies: " + Enemies.ToString(), scorePosition - new Vector2(150, 0), Color.White); 
            spriteBatch.DrawString(scoreFont, "Score: " + Score.ToString(), scorePosition, Color.White);                
        }
    }
}

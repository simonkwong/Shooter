using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MathExercise1
{
    public class World
    {
        protected Game1 game;
        public int WorldWidth { get; set; }
        public int WorldHeight { get; set; }

        private int maxWaves = 3;
        private int currentWave = 1;

        public int numEnemies = 15;

        public int enemiesLeft = 0;

        private GameManager gameManager { get; set; }

        public World(Game1 containingGame, int w, int h)
        {
            game = containingGame;
            WorldWidth = w;
            WorldHeight = h;

            numEnemies *= currentWave;

            enemiesLeft = numEnemies;

            gameManager = new GameManager(this, game, numEnemies);

        }
        public void LoadContent(ContentManager content)
        {

            gameManager.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {

            if (currentWave > maxWaves) {
                game.Exit();
            }


            gameManager.Update(gameTime);
        }

        public void Draw(SpriteBatch sb)
        {
            gameManager.Draw(sb);

        }
    }
}

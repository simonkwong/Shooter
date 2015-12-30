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
    class GameManager
    {

        public HUD hud;
        private World mWorld;
        private List<Enemy1> enemies1;
        private List<Enemy2> enemies2;

        private bool activePowerUps = true;

        static Random rndGen = new Random();

        private Player Player1 { get; set; }

        private PowerUpManager puManager;

        private GameOver gameOver;

        public GameManager(World containingWorld, Game1 game, int numEnemies)
        {
            hud = new HUD();
            mWorld = containingWorld;
            enemies1 = new List<Enemy1>();
            enemies2 = new List<Enemy2>();
            Player1 = new Player(mWorld, enemies1, enemies2);

            puManager = new PowerUpManager(mWorld, Player1);

            Generate(numEnemies);

            gameOver = new GameOver(game, hud);
        }

        public void LoadContent(ContentManager content)
        {

            hud.scoreFont = content.Load<SpriteFont>("ScoreFont");
            hud.scorePosition = new Vector2(mWorld.WorldWidth - 200, 20);
            hud.lifePosition = new Vector2(100, 20);
            hud.healthPosition = new Vector2(300, 20);

            Player1.LoadContent(content);

            puManager.LoadContent(content);

            foreach (Enemy1 e in enemies1)
            {
                e.LoadContent(content);
            }
            foreach (Enemy2 e in enemies2)
            {
                e.LoadContent(content);
            }
        }

        public void Generate(int numEnemies)
        {
            int i;
            for (i = 0; i < numEnemies; i++)
            {
                if (i % 4 == 0 && i != 0)
                {
                    enemies2.Add(new Enemy2(mWorld, Player1, i));
                }
                else
                {
                    enemies1.Add(new Enemy1(mWorld, Player1, i));
                }
            }
        }

        public Vector2 RandomizePosition()
        {
            int iX = rndGen.Next((mWorld.WorldWidth / 4) + 300, (mWorld.WorldWidth - 100));
            int iY = rndGen.Next(110, mWorld.WorldHeight - 50);

            return (new Vector2(iX, iY));

        }

        public void Update(GameTime gameTime)
        {
            foreach (Enemy1 e in enemies1)
            {
                e.Update(gameTime);
            }
            foreach (Enemy2 e in enemies2)
            {
                e.Update(gameTime);
            }

            Player1.Update(gameTime);

            if (Player1.Position.X < 50)
            {
                Player1.Position = new Vector2(50, Player1.Position.Y);
            }
            if (Player1.Position.X > (mWorld.WorldWidth / 4))
            {
                Player1.Position = new Vector2((mWorld.WorldWidth / 4), Player1.Position.Y);
            }
            if (Player1.Position.Y < 100)
            {
                Player1.Position = new Vector2(Player1.Position.X, 100);
            }
            if (Player1.Position.Y > (mWorld.WorldHeight - 35))
            {
                Player1.Position = new Vector2(Player1.Position.X, (mWorld.WorldHeight - 35));
            }

            hud.Score = Player1.PlayerProjectiles.score;

            if (Player1.PlayerProjectiles.score % 200 == 0 && Player1.PlayerProjectiles.score != 0 && activePowerUps)
            {

                puManager.CreatePowerUp(RandomizePosition(), new Vector2(-300, 0));
                activePowerUps = false;
            }
            if (Player1.PlayerProjectiles.score % 200 != 0)
            {
                activePowerUps = true;
            }
            else
            {
                activePowerUps = false;
            }

            puManager.Update(gameTime);

            hud.Lives = Player1.lives;
            hud.Health = Player1.health;
            hud.Enemies = mWorld.enemiesLeft;

            if (Player1.lives <= 0 || hud.Enemies <= 0)
            {
                gameOver.Update();
            }

        }
        public void Draw(SpriteBatch sb)
        {

            Player1.Draw(sb);

            hud.Draw(sb);

            puManager.Draw(sb);

            foreach (Enemy1 e in enemies1)
            {
                if (e.alive == true)
                {
                    e.Draw(sb);
                }
            }

            foreach (Enemy2 e in enemies2)
            {
                if (e.alive == true)
                {
                    e.Draw(sb);
                }
            }

            if (Player1.lives <= 0)
            {
                gameOver.Draw(sb);
            }
        }
    }
}

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
    public class PowerUpManager
    {
        private World mWorld;
        private List<PowerUp> puFire;
        private List<PowerUp> puHeal;
        private List<PowerUp> puPower;
        private List<PowerUp> puShield;

        private AnimData FireData;
        private AnimData HealData;
        private AnimData PowerData;
        private AnimData ShieldData;

        private Texture2D FireSprite;
        private Texture2D HealSprite;
        private Texture2D PowerSprite;
        private Texture2D ShieldSprite;

        List<PowerUp> toDeleteFire = null;
        List<PowerUp> toDeleteHeal = null;
        List<PowerUp> toDeletePower = null;
        List<PowerUp> toDeleteShield = null;

        private Player Player1;

        static Random rndGen = new Random();

        public PowerUpManager(World containingWorld, Player player)
        {
            mWorld = containingWorld;
            puFire = new List<PowerUp>();
            puHeal = new List<PowerUp>();
            puPower = new List<PowerUp>();
            puShield = new List<PowerUp>();
            Player1 = player;
        }

        public void LoadContent(ContentManager content)
        {
            FireData = content.Load<AnimData>("PowerUpFire");
            FireSprite = content.Load<Texture2D>(FireData.spriteName);

            HealData = content.Load<AnimData>("PowerUpHeal");
            HealSprite = content.Load<Texture2D>(HealData.spriteName);

            PowerData = content.Load<AnimData>("PowerUpPower");
            PowerSprite = content.Load<Texture2D>(PowerData.spriteName);

            ShieldData = content.Load<AnimData>("PowerUpShield");
            ShieldSprite = content.Load<Texture2D>(ShieldData.spriteName);
        }

        public void CreatePowerUp(Vector2 position, Vector2 velocity)
        {
            int RandomPowerUp = (int)rndGen.Next(0, 3);

            if (RandomPowerUp == 0)
            {
                CreateFirePowerUp(position, velocity);
            }
            if (RandomPowerUp == 1)
            {
                CreateHealPowerUp(position, velocity);
            }
            if (RandomPowerUp == 2)
            {
                CreatePowerPowerUp(position, velocity);
            }
            if (RandomPowerUp == 3)
            {
                CreatePowerPowerUp(position, velocity);
            }
        }


        public void CreateFirePowerUp(Vector2 position, Vector2 velocity)
        {
            puFire.Add(new PowerUp(FireSprite, FireData.animWidth, position, velocity));
        }

        public void CreateHealPowerUp(Vector2 position, Vector2 velocity)
        {
            puHeal.Add(new PowerUp(HealSprite, HealData.animWidth, position, velocity));
        }

        public void CreatePowerPowerUp(Vector2 position, Vector2 velocity)
        {
            puPower.Add(new PowerUp(PowerSprite, PowerData.animWidth, position, velocity));
        }

        public void CreateShieldPowerUp(Vector2 position, Vector2 velocity)
        {
            puShield.Add(new PowerUp(ShieldSprite, ShieldData.animWidth, position, velocity));
        }

        public void Update(GameTime gameTime)
        {

            foreach (PowerUp p in puFire)
            {

                p.Update(gameTime);

                if ((p.BoundingBox).Intersects(Player1.BoundingBox) || (p.Position.X < 0 || p.Position.X > mWorld.WorldWidth || p.Position.Y < 0 || p.Position.Y > mWorld.WorldHeight))
                {
                    if (p.BoundingBox.Intersects(Player1.BoundingBox))
                    {

                        Player1.health -= 100;

                        if (Player1.health <= 0)
                        {
                            Player1.lives -= 1;
                            Player1.health = 100;
                            Player1.Position = new Vector2(100, 75);
                        }
                    }

                    if (toDeleteFire == null)
                    {
                        toDeleteFire = new List<PowerUp>();
                    }
                    toDeleteFire.Add(p);
                }
            }

            foreach (PowerUp p in puHeal)
            {

                p.Update(gameTime);

                if ((p.BoundingBox).Intersects(Player1.BoundingBox) || (p.Position.X < 0 || p.Position.X > mWorld.WorldWidth || p.Position.Y < 0 || p.Position.Y > mWorld.WorldHeight))
                {
                    if (p.BoundingBox.Intersects(Player1.BoundingBox))
                    {
                        Player1.lives++;
                        Player1.health += 50;
                    }

                    if (toDeleteHeal == null)
                    {
                        toDeleteHeal = new List<PowerUp>();
                    }
                    toDeleteHeal.Add(p);
                }
            }

            foreach (PowerUp p in puPower)
            {

                p.Update(gameTime);

                if ((p.BoundingBox).Intersects(Player1.BoundingBox) || (p.Position.X < 0 || p.Position.X > mWorld.WorldWidth || p.Position.Y < 0 || p.Position.Y > mWorld.WorldHeight))
                {
                    if (p.BoundingBox.Intersects(Player1.BoundingBox))
                    {

                        Player1.Max_Speed = 2 * Player1.Max_Speed;
                        Player1.PlayerProjectiles.pBulletData.bulletSpeed = 2 * Player1.PlayerProjectiles.pBulletData.bulletSpeed;

                    }

                    if (toDeletePower == null)
                    {
                        toDeletePower = new List<PowerUp>();
                    }
                    toDeletePower.Add(p);
                }
            }

            foreach (PowerUp p in puShield)
            {

                p.Update(gameTime);

                if ((p.BoundingBox).Intersects(Player1.BoundingBox) || (p.Position.X < 0 || p.Position.X > mWorld.WorldWidth || p.Position.Y < 0 || p.Position.Y > mWorld.WorldHeight))
                {
                    if (p.BoundingBox.Intersects(Player1.BoundingBox))
                    {

                        Player1.health += 100;
                                                
                    }

                    if (toDeleteShield == null)
                    {
                        toDeleteShield = new List<PowerUp>();
                    }
                    toDeleteShield.Add(p);
                }
            }

            if (toDeleteFire != null)
            {
                foreach (PowerUp p in toDeleteFire)
                {
                    puFire.Remove(p);
                }
            }
            if (toDeleteHeal != null)
            {
                foreach (PowerUp p in toDeleteHeal)
                {
                    puHeal.Remove(p);
                }
            }
            if (toDeletePower != null)
            {
                foreach (PowerUp p in toDeletePower)
                {
                    puPower.Remove(p);
                }
            }
            if (toDeleteShield != null)
            {
                foreach (PowerUp p in toDeleteShield)
                {
                    puShield.Remove(p);
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (PowerUp p in puFire)
            {
                p.Draw(sb);
            }
            foreach (PowerUp p in puHeal)
            {
                p.Draw(sb);
            }
            foreach (PowerUp p in puPower)
            {
                p.Draw(sb);
            }
            foreach (PowerUp p in puShield)
            {
                p.Draw(sb);
            }

        }

    }
}

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
    public class PlayerProjectileManager
    {
        private World mWorld;
        private List<PlayerProjectile> pBullets;
        public BulletData pBulletData;
        private Texture2D pBulletSprite;
        
        private List<PlayerProjectile> pSonars;
        public BulletData pSonarData;
        private Texture2D pSonarSprite;

        private List<PlayerProjectile> pMissiles;
        public BulletData pMissileData;
        private Texture2D pMissileSprite;

        private List<PlayerProjectile> pRockets;
        public BulletData pRocketData;
        private Texture2D pRocketSprite;

        public int score = 0;
        private List<Enemy1> enemies1;
        private List<Enemy2> enemies2;
        private Explosion explosion;
        private Texture2D explosionImage;
        private AnimData explosionData;

        List<PlayerProjectile> toDeleteBullet = null;
        List<PlayerProjectile> toDeleteSonar = null;
        List<PlayerProjectile> toDeleteMissile = null;
        List<PlayerProjectile> toDeleteRocket = null;
        List<Enemy1> deadEnemies1 = null;
        List<Enemy2> deadEnemies2 = null;

        public PlayerProjectileManager(World containingWorld, List<Enemy1> es1, List<Enemy2> es2)
        {
            enemies1 = es1;
            enemies2 = es2;
            mWorld = containingWorld;

            pBullets = new List<PlayerProjectile>();
            pSonars = new List<PlayerProjectile>();
            pMissiles = new List<PlayerProjectile>();
            pRockets = new List<PlayerProjectile>();
        }

        public void LoadContent(ContentManager content)
        {

            pBulletData = content.Load<BulletData>("PlayerBullet");
            pBulletSprite = content.Load<Texture2D>(pBulletData.spriteName);

            pSonarData = content.Load<BulletData>("PlayerSonar");
            pSonarSprite = content.Load<Texture2D>(pSonarData.spriteName);

            pMissileData = content.Load<BulletData>("PlayerMissile");
            pMissileSprite = content.Load<Texture2D>(pMissileData.spriteName);

            pRocketData = content.Load<BulletData>("PlayerRocket");
            pRocketSprite = content.Load<Texture2D>(pRocketData.spriteName);

            explosionImage = content.Load<Texture2D>("explosion");

            explosionData = content.Load<AnimData>("ExplosionEffect");

            explosion = null;

        }

        public void fireBullet(Vector2 position, Vector2 velocity)
        {
            pBullets.Add(new PlayerProjectile(pBulletSprite, pBulletData.animWidth, position, velocity));
        }

        public void fireSonar(Vector2 position, Vector2 velocity)
        {
            pSonars.Add(new PlayerProjectile(pSonarSprite, pSonarData.animWidth, position, velocity));
        }

        public void fireMissile(Vector2 position, Vector2 velocity)
        {
            pMissiles.Add(new PlayerProjectile(pMissileSprite, pMissileData.animWidth, position, velocity));
        }

        public void fireRocket(Vector2 position, Vector2 velocity)
        {
            pRockets.Add(new PlayerProjectile(pRocketSprite, pRocketData.animWidth, position, velocity));
        }

        public void CreateExplosion(Vector2 position)
        {
            explosion = new Explosion(explosionImage, explosionData.animWidth, position, Vector2.Zero);
        }

        public void Update(GameTime gameTime)
        {
            foreach (PlayerProjectile p in pBullets)
            {
                p.Update(gameTime);

                foreach (Enemy1 e in enemies1)
                {
                    
                    if ((p.BoundingBox).Intersects(e.BoundingBox) || p.Position.X < 0 || p.Position.X > mWorld.WorldWidth || p.Position.Y < 0 || p.Position.Y > mWorld.WorldHeight)
                    {
                        if (p.BoundingBox.Intersects(e.BoundingBox))
                        {
                            e.life--;

                            score += 10;

                            if (e.life <= 0)
                            {
                                score += 20;
                                mWorld.enemiesLeft--;
                                e.alive = false;
                                if (deadEnemies1 == null)
                                {
                                    deadEnemies1 = new List<Enemy1>();

                                }
                                CreateExplosion(e.Position);

                                deadEnemies1.Add(e);
                            }
                        }

                        if (toDeleteBullet == null)
                        {
                            toDeleteBullet = new List<PlayerProjectile>();
                        }
                        toDeleteBullet.Add(p);
                    }
                }

                foreach (Enemy2 e in enemies2)
                {

                    if ((p.BoundingBox).Intersects(e.BoundingBox) || p.Position.X < 0 || p.Position.X > mWorld.WorldWidth || p.Position.Y < 0 || p.Position.Y > mWorld.WorldHeight)
                    {
                        if (p.BoundingBox.Intersects(e.BoundingBox))
                        {
                            e.life--;

                            score += 20;

                            if (e.life <= 0)
                            {
                                score += 40;
                                mWorld.enemiesLeft--;
                                e.alive = false;
                                if (deadEnemies2 == null)
                                {
                                    deadEnemies2 = new List<Enemy2>();

                                }
                                CreateExplosion(e.Position);

                                deadEnemies2.Add(e);
                            }
                        }

                        if (toDeleteBullet == null)
                        {
                            toDeleteBullet = new List<PlayerProjectile>();
                        }
                        toDeleteBullet.Add(p);
                    }
                }
            }

            foreach (PlayerProjectile p in pSonars)
            {
                p.Update(gameTime);

                foreach (Enemy1 e in enemies1)
                {

                    if ((p.BoundingBox).Intersects(e.BoundingBox) || p.Position.X < 0 || p.Position.X > mWorld.WorldWidth || p.Position.Y < 0 || p.Position.Y > mWorld.WorldHeight)
                    {
                        if (p.BoundingBox.Intersects(e.BoundingBox))
                        {
                            e.Velocity = Vector2.Zero;

                            score += 5;

                            if (e.life <= 0)
                            {
                                score += 10;
                                mWorld.enemiesLeft--;
                                e.alive = false;
                                if (deadEnemies1 == null)
                                {
                                    deadEnemies1 = new List<Enemy1>();

                                }

                                CreateExplosion(e.Position);

                                deadEnemies1.Add(e);
                            }
                        }

                        if (toDeleteSonar == null)
                        {
                            toDeleteSonar = new List<PlayerProjectile>();
                        }
                        toDeleteSonar.Add(p);
                    }
                }
                foreach (Enemy2 e in enemies2)
                {

                    if ((p.BoundingBox).Intersects(e.BoundingBox) || p.Position.X < 0 || p.Position.X > mWorld.WorldWidth || p.Position.Y < 0 || p.Position.Y > mWorld.WorldHeight)
                    {
                        if (p.BoundingBox.Intersects(e.BoundingBox))
                        {

                            e.Velocity = Vector2.Zero;

                            score += 10;

                            if (e.life <= 0)
                            {
                                score += 20;
                                mWorld.enemiesLeft--;
                                e.alive = false;
                                if (deadEnemies2 == null)
                                {
                                    deadEnemies2 = new List<Enemy2>();

                                }
                                CreateExplosion(e.Position);

                                deadEnemies2.Add(e);
                            }
                        }

                        if (toDeleteSonar == null)
                        {
                            toDeleteSonar = new List<PlayerProjectile>();
                        }
                        toDeleteSonar.Add(p);
                    }
                }
            }

            foreach (PlayerProjectile p in pMissiles)
            {
                p.Update(gameTime);

                foreach (Enemy1 e in enemies1)
                {

                    if ((p.BoundingBox).Intersects(e.BoundingBox) || p.Position.X < 0 || p.Position.X > mWorld.WorldWidth || p.Position.Y < 0 || p.Position.Y > mWorld.WorldHeight)
                    {
                        if (p.BoundingBox.Intersects(e.BoundingBox))
                        {
                            e.life = 0;

                            if (e.life <= 0)
                            {
                                score += 30;
                                mWorld.enemiesLeft--;
                                e.alive = false;
                                if (deadEnemies1 == null)
                                {
                                    deadEnemies1 = new List<Enemy1>();

                                }

                                CreateExplosion(e.Position);

                                deadEnemies1.Add(e);
                            }
                        }

                        if (toDeleteMissile == null)
                        {
                            toDeleteMissile = new List<PlayerProjectile>();
                        }
                        toDeleteMissile.Add(p);
                    }
                }
                foreach (Enemy2 e in enemies2)
                {

                    if ((p.BoundingBox).Intersects(e.BoundingBox) || p.Position.X < 0 || p.Position.X > mWorld.WorldWidth || p.Position.Y < 0 || p.Position.Y > mWorld.WorldHeight)
                    {
                        if (p.BoundingBox.Intersects(e.BoundingBox))
                        {
                            e.life = 0;

                            if (e.life <= 0)
                            {
                                score += 60;
                                mWorld.enemiesLeft--;
                                e.alive = false;
                                if (deadEnemies2 == null)
                                {
                                    deadEnemies2 = new List<Enemy2>();

                                }

                                CreateExplosion(e.Position);

                                deadEnemies2.Add(e);
                            }
                        }

                        if (toDeleteMissile == null)
                        {
                            toDeleteMissile = new List<PlayerProjectile>();
                        }
                        toDeleteMissile.Add(p);
                    }
                }
            }

            foreach (PlayerProjectile p in pRockets)
            {
                p.Update(gameTime);

                foreach (Enemy1 e in enemies1)
                {

                    if ((p.BoundingBox).Intersects(e.BoundingBox) || p.Position.X < 0 || p.Position.X > mWorld.WorldWidth || p.Position.Y < 0 || p.Position.Y > mWorld.WorldHeight)
                    {
                        if (p.BoundingBox.Intersects(e.BoundingBox))
                        {
                            e.life -= 2;

                            score += 15;

                            if (e.life <= 0)
                            {
                                score += 45;
                                mWorld.enemiesLeft--;
                                e.alive = false;
                                if (deadEnemies1 == null)
                                {
                                    deadEnemies1 = new List<Enemy1>();

                                }

                                CreateExplosion(e.Position);

                                deadEnemies1.Add(e);
                            }
                        }

                        if (toDeleteRocket == null)
                        {
                            toDeleteRocket = new List<PlayerProjectile>();
                        }
                        toDeleteRocket.Add(p);
                    }
                }
                foreach (Enemy2 e in enemies2)
                {

                    if ((p.BoundingBox).Intersects(e.BoundingBox) || p.Position.X < 0 || p.Position.X > mWorld.WorldWidth || p.Position.Y < 0 || p.Position.Y > mWorld.WorldHeight)
                    {
                        if (p.BoundingBox.Intersects(e.BoundingBox))
                        {
                            e.life -= 2;

                            score += 30;

                            if (e.life <= 0)
                            {
                                score += 45;
                                mWorld.enemiesLeft--;
                                e.alive = false;
                                if (deadEnemies2 == null)
                                {
                                    deadEnemies2 = new List<Enemy2>();

                                }

                                CreateExplosion(e.Position);

                                deadEnemies2.Add(e);
                            }
                        }

                        if (toDeleteRocket == null)
                        {
                            toDeleteRocket = new List<PlayerProjectile>();
                        }
                        toDeleteRocket.Add(p);
                    }
                }
            }

            if (explosion != null)
            {
                explosion.Update(gameTime);
            }

            if (toDeleteBullet != null)
            {
                foreach (PlayerProjectile p in toDeleteBullet)
                {
                    pBullets.Remove(p);
                }
            }

            if (toDeleteSonar != null)
            {
                foreach (PlayerProjectile p in toDeleteSonar)
                {
                    pSonars.Remove(p);
                }
            }

            if (toDeleteMissile != null)
            {
                foreach (PlayerProjectile p in toDeleteMissile)
                {
                    pMissiles.Remove(p);
                }
            }

            if (toDeleteRocket != null)
            {
                foreach (PlayerProjectile p in toDeleteRocket)
                {
                    pRockets.Remove(p);
                }
            }

            if (deadEnemies1 != null)
            {
                foreach (Enemy1 e in deadEnemies1)
                {
                    enemies1.Remove(e);
                }
            }
            if (deadEnemies2 != null)
            {
                foreach (Enemy2 e in deadEnemies2)
                {
                    enemies2.Remove(e);
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {

            foreach (PlayerProjectile p in pBullets)
            {
                p.Draw(sb);
            }

            foreach (PlayerProjectile p in pSonars)
            {
                p.Draw(sb);
            }

            foreach (PlayerProjectile p in pMissiles)
            {
                p.Draw(sb);
            }

            foreach (PlayerProjectile p in pRockets)
            {
                p.Draw(sb);
            }

            if (explosion != null)
            {
                explosion.Draw(sb);
            }

        }
    }
}

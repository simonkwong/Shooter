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
    public class EnemyProjectileManager
    {
        private World mWorld;
        private List<EnemyProjectile> mProjectiles;
        private AnimData[] projectileData;
        private Player Player1;

        public EnemyProjectileManager(World containingWorld, Player player)
        {
            mWorld = containingWorld;
            mProjectiles = new List<EnemyProjectile>();
            Player1 = player;
        }

        public void LoadContent(ContentManager content)
        {
            projectileData = content.Load<AnimData[]>("EnemyBullet");
            foreach (AnimData d in projectileData)
            {
                d.Texture = content.Load<Texture2D>(d.spriteName);
            }

        }


        public void CreateProjectile(Vector2 position, Vector2 velocity)
        {
            mProjectiles.Add(new EnemyProjectile(projectileData[0].Texture, projectileData[0].animWidth, position, velocity));
        }

        public void Update(GameTime gameTime)
        {
            List<EnemyProjectile> toDelete = null;
            foreach (EnemyProjectile p in mProjectiles)
            {

                p.Update(gameTime);

                if ((p.BoundingBox).Intersects(Player1.BoundingBox) || (p.Postition.X < 0 || p.Postition.X > mWorld.WorldWidth || p.Postition.Y < 0 || p.Postition.Y > mWorld.WorldHeight))
                {
                    if (p.BoundingBox.Intersects(Player1.BoundingBox))
                    {
                        Player1.health -= 10;

                        if (Player1.health <= 0)
                        {
                            Player1.lives -= 1;
                            Player1.health = 100;
                            Player1.Position = new Vector2(100, 75);
                        }
                    }

                    if (toDelete == null)
                    {
                        toDelete = new List<EnemyProjectile>();
                    }
                    toDelete.Add(p);
                }
            }
            if (toDelete != null)
            {
                foreach (EnemyProjectile p in toDelete)
                {
                    mProjectiles.Remove(p);
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (EnemyProjectile p in mProjectiles)
            {
                p.Draw(sb);
            }

        }

    }
}

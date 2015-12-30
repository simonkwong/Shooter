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


namespace MathExercise1
{
    public class Enemy1
    {
        private Texture2D image;
        private World mWorld;
        private float shotCooldown;
        private float timeSinceLastShot = 0.0f;

        public bool alive;
        public int life;

        public float Max_Speed;
        public float Max_Speed_Squared;

        public float randomSpeed = 1.0f;

        static Random rndGen = new Random();

        private AnimatedObject enemyObject;
        public Vector2 Velocity, Position;

        public EnemyProjectileManager EnemyProjectiles { get; set; }

        private ShipData mShipdata;

        private Player Player1;
        private int enemyID;

        public Enemy1(World game, Player player, int i)
        {
            mWorld = game;
            Position = RandomizePosition();

            Player1 = player;
            enemyID = i;

            EnemyProjectiles = new EnemyProjectileManager(mWorld, Player1);

            alive = true;
            life = 3;
        }

        public void LoadContent(ContentManager content)
        {
            
            EnemyProjectiles.LoadContent(content);
            mShipdata = content.Load<ShipData>("EnemyShip1");
            image = content.Load<Texture2D>(mShipdata.spriteName);

            Velocity = Vector2.Zero;

            RandomizeMovement();

            enemyObject = new AnimatedObject(image, image.Width, mShipdata.center, false, 0.1f, Position);
            Max_Speed = mShipdata.maxSpeed;
            Max_Speed_Squared = Max_Speed * Max_Speed;
            shotCooldown = mShipdata.shotCooldown;
        }

        public Vector2 RandomizePosition()
        {
            int iX = rndGen.Next((mWorld.WorldWidth / 4) + 300, (mWorld.WorldWidth - 100));
            int iY = rndGen.Next(110, mWorld.WorldHeight - 50);

            return (new Vector2(iX, iY));

        }

        public void RandomizeMovement()
        {
            float iX = rndGen.Next(-50, 50);
            float iY = rndGen.Next(-50, 50);
            Velocity = new Vector2(iX, iY);
        }

        public void RandomizeShotCooldown()
        {
            int randomMinCooldown = (int)((float)rndGen.Next(0, 2));
            int randomMaxCooldown = (int)((float)rndGen.Next(3, 4));
            float randomCooldown = (float)rndGen.Next(randomMinCooldown, (enemyID + 1) * randomMaxCooldown);
            shotCooldown += randomCooldown;

        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)enemyObject.Position.X + 25, (int)enemyObject.Position.Y + 18, image.Width / 2, image.Height / 2 - 5);
            }
        }

        private Vector2 rotate(Vector2 v, float theta)
        {
            return new Vector2((float)(v.X * Math.Cos(theta) + v.Y * Math.Sin(theta)), (float)(v.X * Math.Sin(theta) - v.Y * Math.Cos(theta)));

        }

        public void Update(GameTime gameTime)
        {
            enemyObject.Update(gameTime);

            timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Velocity.LengthSquared() > Max_Speed_Squared)
            {
                Velocity.Normalize();
                Velocity = Vector2.Normalize(Velocity) * Max_Speed;
            }

            enemyObject.Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position = enemyObject.Position;

            if (enemyObject.Position.X > mWorld.WorldWidth - 75 || enemyObject.Position.X < (mWorld.WorldWidth / 4) + 300)
            {
                Velocity.X = -Velocity.X;
            }
            if (enemyObject.Position.Y > mWorld.WorldHeight - 50 || enemyObject.Position.Y < 100)
            {
                Velocity.Y = -Velocity.Y;
            }

            if (timeSinceLastShot > shotCooldown)
            {
                EnemyProjectiles.CreateProjectile(enemyObject.Position + new Vector2(0, 25), rotate(mShipdata.facing, enemyObject.Rotation) * mShipdata.bulletSpeed);
                timeSinceLastShot = 0.0f;
                RandomizeShotCooldown();
                RandomizeMovement();
            }

            EnemyProjectiles.Update(gameTime);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            enemyObject.Draw(spriteBatch);
            EnemyProjectiles.Draw(spriteBatch);
        }
    }
}

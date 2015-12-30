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
    public class Player
    {

        enum gunType
        {
            D1, D2, D3, D4
        }

        private gunType g;

        private Texture2D image;
        private World mWorld;
        
        private float shotCooldownB = 0.0f;
        private float timeSinceLastBShot = 0.0f;

        private float shotCooldownS = 0.0f;
        private float timeSinceLastSShot = 0.0f;

        private float shotCooldownM = 0.0f;
        private float timeSinceLastMShot = 0.0f;

        private float shotCooldownR = 0.0f;
        private float timeSinceLastRShot = 0.0f;

        public float Max_Speed;
        public float Max_Speed_Squared;

        public PlayerProjectileManager PlayerProjectiles { get; set; }

        private AnimatedObject playerObject;
        public Vector2 Velocity { get; set; }

        private ShipData mShipdata;

        private List<Enemy1> enemies1;
        private List<Enemy2> enemies2;

        public int lives = 1;
        public int health = 100;

        public Player(World game, List<Enemy1> es1, List<Enemy2> es2)
        {
            enemies1 = es1;
            enemies2 = es2;
            mWorld = game;

            g = gunType.D1;

            PlayerProjectiles = new PlayerProjectileManager(mWorld, enemies1, enemies2);
            
        }

        public float Rotation
        {
            get
            {
                return playerObject.Rotation;
            }
            set
            {
                playerObject.Rotation = value;
            }
        }
        public Vector2 Position
        {
            get
            { 
                return playerObject.Position; 
            }
            set
            {
                playerObject.Position = value;
            }
        }

        public void LoadContent(ContentManager content)
        {

            PlayerProjectiles.LoadContent(content);
            mShipdata = content.Load<ShipData>("PlayerShip");
            image = content.Load<Texture2D>(mShipdata.spriteName);
            playerObject = new AnimatedObject(image, image.Width, mShipdata.center, false, 0.1f, new Vector2(100, 100));
            Max_Speed = mShipdata.maxSpeed;
            Max_Speed_Squared = Max_Speed * Max_Speed;
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X - 35, (int)Position.Y + 5, image.Width - 40, (image.Height / 2) - 5);
            }
        }

        private Vector2 rotate(Vector2 v, float theta)
        {
            return new Vector2((float) (v.X * Math.Cos(theta) + v.Y*Math.Sin(theta)),(float) (v.X*Math.Sin(theta) - v.Y*Math.Cos(theta)));
        }


        public void Update(GameTime gameTime)
        {

            timeSinceLastBShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastSShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastMShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            timeSinceLastRShot += (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState keyboard = Keyboard.GetState();
            GamePadState controller = GamePad.GetState(PlayerIndex.One);

            if (keyboard.IsKeyDown(Keys.Left) || controller.IsButtonDown(Buttons.DPadLeft))
            {
                playerObject.Position += new Vector2(-5, 0);
            }
            if (keyboard.IsKeyDown(Keys.Right) || controller.IsButtonDown(Buttons.DPadRight))
            {
                playerObject.Position += new Vector2(5, 0);
            }
            if (keyboard.IsKeyDown(Keys.Up) || controller.IsButtonDown(Buttons.DPadUp))
            {
                playerObject.Position += new Vector2(0, -5);
            }
            if (keyboard.IsKeyDown(Keys.Down) || controller.IsButtonDown(Buttons.DPadDown))
            {
                playerObject.Position += new Vector2(0, 5);
            }

            if (keyboard.IsKeyDown(Keys.D1))
                g = gunType.D1;
            if (keyboard.IsKeyDown(Keys.D2))
                g = gunType.D2;
            if (keyboard.IsKeyDown(Keys.D3))
                g = gunType.D3;
            if (keyboard.IsKeyDown(Keys.D4))
                g = gunType.D4;

            if (timeSinceLastBShot > shotCooldownB)
            {
                if ((keyboard.IsKeyDown(Keys.Space) && g == gunType.D1) || controller.IsButtonDown(Buttons.A))
                {
                    shotCooldownB = PlayerProjectiles.pBulletData.shotCooldown;

                    PlayerProjectiles.fireBullet(playerObject.Position + new Vector2(30, 15), rotate(mShipdata.facing, playerObject.Rotation) * PlayerProjectiles.pBulletData.bulletSpeed);

                    timeSinceLastBShot = 0.0f;
                }
            }
            if (timeSinceLastSShot > shotCooldownS)
            {
                if ((keyboard.IsKeyDown(Keys.Space) && g == gunType.D2) || controller.IsButtonDown(Buttons.B))
                {
                    shotCooldownS = PlayerProjectiles.pSonarData.shotCooldown;

                    PlayerProjectiles.fireSonar(playerObject.Position + new Vector2(30, 15), rotate(mShipdata.facing, playerObject.Rotation) * PlayerProjectiles.pSonarData.bulletSpeed);

                    timeSinceLastSShot = 0.0f;
                }
            }
            if (timeSinceLastMShot > shotCooldownM)
            {
                if ((keyboard.IsKeyDown(Keys.Space) && g == gunType.D3) || controller.IsButtonDown(Buttons.X))
                {
                    shotCooldownM = PlayerProjectiles.pMissileData.shotCooldown;

                    PlayerProjectiles.fireMissile(playerObject.Position + new Vector2(30, 15), rotate(mShipdata.facing, playerObject.Rotation) * PlayerProjectiles.pMissileData.bulletSpeed);

                    timeSinceLastMShot = 0.0f;
                }
            }
            if (timeSinceLastRShot > shotCooldownR)
            {
                if ((keyboard.IsKeyDown(Keys.Space) && g == gunType.D4) || controller.IsButtonDown(Buttons.Y))
                {
                    shotCooldownR = PlayerProjectiles.pRocketData.shotCooldown;

                    PlayerProjectiles.fireRocket(playerObject.Position + new Vector2(30, 15), rotate(mShipdata.facing, playerObject.Rotation) * PlayerProjectiles.pRocketData.bulletSpeed);

                    timeSinceLastRShot = 0.0f;
                }
            }

            PlayerProjectiles.Update(gameTime);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            playerObject.Draw(spriteBatch);
            PlayerProjectiles.Draw(spriteBatch);
        }

    }
}

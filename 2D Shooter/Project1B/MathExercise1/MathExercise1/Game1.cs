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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        World mGameWorld;
        SpriteBatch spriteBatch;
        Texture2D backgroundImage;
        private ScrollingBackground scrollingBackground;
        public int Width { get; set; }
        public int Height { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Width = 1920;
            graphics.PreferredBackBufferHeight = Height = 1080;
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {            

            mGameWorld = new World(this, Width, Height);

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundImage = Content.Load<Texture2D>("background");
            scrollingBackground = new ScrollingBackground(Content, "background");
            mGameWorld.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Left) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadLeft))
            {
                scrollingBackground.BackgroundOffset -= 3;
                scrollingBackground.ParallaxOffset -= 8;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadRight))
            {
                scrollingBackground.BackgroundOffset += 3;
                scrollingBackground.ParallaxOffset += 8;
            }

            mGameWorld.Update(gameTime);

            base.Update(gameTime);
        }
       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            scrollingBackground.Draw(spriteBatch);

            mGameWorld.Draw(spriteBatch);
           
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
} 

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
    class GameOver
    {
        public HUD hud;
        private Texture2D texture;
        private Game1 game;
        private KeyboardState lastState;
        private SpriteFont font;
 
        public GameOver(Game1 game, HUD h)   
        {
            hud = h;
            this.game = game;
            texture = game.Content.Load<Texture2D>("GameOver");
            lastState = Keyboard.GetState();
            font = game.Content.Load<SpriteFont>("ScoreFont");
        }
 
        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState controller = GamePad.GetState(PlayerIndex.One);

            if (keyboardState.IsKeyDown(Keys.Escape) || controller.IsButtonDown(Buttons.Start))
            {
                game.Exit();
            }
 
            lastState = keyboardState;
        }
 
        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                spriteBatch.Draw(texture, new Vector2(0f, 0f), Color.White);
            }
            
            spriteBatch.DrawString(font, "Final Score: " + hud.Score.ToString(), new Vector2(game.Width / 2, game.Height / 2), Color.White);
        }
    }
}

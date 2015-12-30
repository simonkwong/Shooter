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
    class PowerUp
    {

        int X, Y;
        public bool active;
        public float timeOfExplosion = 0.0f;
        public AnimatedObject Animation { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Position
        {
            get { return Animation.Position; }
            set { Animation.Position = value; }
        }

        public PowerUp(Texture2D texture, int width, Vector2 initialPosition, Vector2 initialVelocity)
        {
            Animation = new AnimatedObject(texture, width, new Vector2(width / 2, texture.Height / 2), true, 0.05f, initialPosition);
            Velocity = initialVelocity;
        }

        public void Update(GameTime gametime)
        {

            timeOfExplosion += (float)gametime.ElapsedGameTime.TotalSeconds;

            Animation.Update(gametime);
            Animation.Position += Velocity * (float)gametime.ElapsedGameTime.TotalSeconds;
            X = (int)Animation.Position.X;
            Y = (int)Animation.Position.Y;
        }

        public Rectangle BoundingBox
        {
            get { return new Rectangle(X, Y, 30, 30); }
        }

        public void Draw(SpriteBatch sb)
        {
            Animation.Draw(sb);
        }
    }
}

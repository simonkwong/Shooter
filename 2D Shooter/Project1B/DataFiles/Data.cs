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
    public class ShipData
    {
        public String spriteName;
        public Vector2 center;
        public Vector2 facing;
        public float maxSpeed;      // pixels / second
        public float acceleration;  // pixels / second*second 
        public float rotationSpeed; // degrees / second
        public float bulletSpeed; // pixels / second
        public float shotCooldown;  // seconds / shot
        public Vector2[] gunPositions;
    }

    public class BulletData
    {
        public String spriteName;
        public int animWidth;
        public float bulletSpeed;
        public float shotCooldown;
        public Boolean centered;
        [ContentSerializerIgnore]
        public Texture2D Texture { get; set; }
    }

    public class AnimData
    {
        public String spriteName;
        public int animWidth;
        public Boolean centered;
        [ContentSerializerIgnore]
        public Texture2D Texture { get; set; }
    }
}

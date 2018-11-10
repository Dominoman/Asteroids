using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Sprites {
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Asteroids.Sprites.Sprite" />
    public class Ship:Sprite {
        public bool Thrust{ get; set; }
        public Vector2 Heading=>new Vector2((float) Math.Cos(Rotation),(float) Math.Sin(Rotation));

        /// <summary>
        /// Initializes a new instance of the <see cref="Ship"/> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="center">The center.</param>
        public Ship(Texture2D texture, Vector2 center) : base(texture, center,2){
            Wrap = true;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime){
            CurrentFrame = Thrust ? 1 : 0;
            if (Thrust){
                Velocity += Heading * 0.1f;
                SoundManager.PlayThrust(Center);
            } else{
                SoundManager.StopThrust();
            }
            Velocity *= 0.99f;
            base.Update(gameTime);
        }
    }
}

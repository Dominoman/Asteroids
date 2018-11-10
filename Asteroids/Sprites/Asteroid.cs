using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Sprites {
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Asteroids.Sprites.Sprite" />
    public class Asteroid:Sprite {
        /// <summary>
        /// Initializes a new instance of the <see cref="Asteroid" /> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="center">The center.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="rotation">The rotation.</param>
        /// <param name="scale">The scale.</param>
        public Asteroid(Texture2D texture, Vector2 center, Vector2 velocity,float rotation,float scale=1) : base(texture, center){
            Velocity = velocity;
            DRotation = rotation;
            Scale = scale;
            Wrap = true;
        }
    }
}

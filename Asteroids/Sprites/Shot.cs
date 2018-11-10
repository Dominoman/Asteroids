using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Sprites {
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Asteroids.Sprites.Sprite" />
    public class Shot:Sprite{
        private int timeToLive;
        private const int MaxTimeToLive = 80;
        /// <summary>
        /// Initializes a new instance of the <see cref="Shot"/> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="center">The center.</param>
        /// <param name="velocity">The velocity.</param>
        public Shot(Texture2D texture, Vector2 center, Vector2 velocity) : base(texture, center){
            Velocity = velocity;
            Wrap = true;
            timeToLive = MaxTimeToLive;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime){
            if (timeToLive > 0){
                timeToLive--;
                if (timeToLive == 0) IsDead = true;
            }
            base.Update(gameTime);
        }
    }
}

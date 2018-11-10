using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Sprites {
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Asteroids.Sprites.Sprite" />
    public class Explosion:Sprite {
        /// <summary>
        /// Initializes a new instance of the <see cref="Explosion"/> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="center">The center.</param>
        public Explosion(Texture2D texture, Vector2 center) : base(texture, center, 24){}

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime){
            if (CurrentFrame < Frames.Length - 1){
                CurrentFrame++;
            } else IsDead = true;
            base.Update(gameTime);
        }
    }
}

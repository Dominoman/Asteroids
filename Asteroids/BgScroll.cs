using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids {
    /// <summary>
    /// 
    /// </summary>
    public class BgScroll{
        private readonly Texture2D texture;
        private float xOffset;

        public float DxOffset{ get; set; } = 1;
        public bool Flip{ get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BgScroll"/> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        public BgScroll(Texture2D texture){
            this.texture = texture;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime){
            xOffset+=DxOffset;
            if (xOffset > AsteroidsGame.Width) xOffset = 0;
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch){
            var flip = Flip ? SpriteEffects.FlipVertically : SpriteEffects.None;
            spriteBatch.Draw(texture,new Rectangle((int)xOffset,0,AsteroidsGame.Width,AsteroidsGame.Height),texture.Bounds,Color.White,0,Vector2.Zero,flip,0);
            spriteBatch.Draw(texture,new Rectangle((int)xOffset-AsteroidsGame.Width,0,AsteroidsGame.Width,AsteroidsGame.Height),texture.Bounds,Color.White,0,Vector2.Zero,flip,0);
        }
    }
}

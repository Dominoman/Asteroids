using System.Collections.Generic;
using Asteroids.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids {
    /// <summary>
    /// 
    /// </summary>
    public static class ExplosionsManager {
        private static Texture2D _texture;
        private static readonly List<Explosion> _explosions = new List<Explosion>();
       
        /// <summary>
        /// Initializes the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        public static void Initialize(Texture2D texture) {
            _texture = texture;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public static void Update(GameTime gameTime) {
            for(var i = _explosions.Count - 1;i >= 0;i--) {
                _explosions[i].Update(gameTime);
                if(_explosions[i].IsDead) _explosions.RemoveAt(i);
            }
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public static void Draw(SpriteBatch spriteBatch) {
            foreach(var shot in _explosions) {
                shot.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Adds the shot.
        /// </summary>
        /// <param name="center">The center.</param>
        public static void AddExplosion(Vector2 center) {
            _explosions.Add(new Explosion(_texture,center));    
            SoundManager.PlayExplosion(center);        
        }
    }
}

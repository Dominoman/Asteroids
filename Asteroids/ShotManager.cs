using System.Collections.Generic;
using Asteroids.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids {
    /// <summary>
    /// 
    /// </summary>
    public static class ShotManager{
        private static Texture2D _texture;
        private static readonly List<Shot> _shots=new List<Shot>();
        private static int _shotDelay;
        private const int MaxShotDelay = 15;

        public static IEnumerable<Shot> Shots => _shots.ToArray();

        /// <summary>
        /// Initializes the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        public static void Initialize(Texture2D texture){
            _texture = texture;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public static void Update(GameTime gameTime){
            if (_shotDelay > 0) _shotDelay--;
            for (var i = _shots.Count-1; i >= 0; i--){
                _shots[i].Update(gameTime);
                if(_shots[i].IsDead) _shots.RemoveAt(i);
            }
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public static void Draw(SpriteBatch spriteBatch){
            foreach (var shot in _shots){
                shot.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Adds the shot.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="velocity">The velocity.</param>
        public static void AddShot(Vector2 center, Vector2 velocity){
            if (_shotDelay > 0) return;
            _shots.Add(new Shot(_texture,center,velocity));
            _shotDelay = MaxShotDelay;
            SoundManager.PlayMissile(center);
        }
    }
}

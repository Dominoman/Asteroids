using System;
using System.Collections.Generic;
using Asteroids.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids {
    /// <summary>
    /// 
    /// </summary>
    public static class AsteroidsManager {
        private static Texture2D _texture;
        private static readonly List<Asteroid> _asteroids = new List<Asteroid>();
        private static readonly Random _rand=new Random();

        public static IEnumerable<Asteroid> Asteroids => _asteroids.ToArray();
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
            for(var i = _asteroids.Count - 1;i >= 0;i--) {
                _asteroids[i].Update(gameTime);
                if(_asteroids[i].IsDead) _asteroids.RemoveAt(i);
            }
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public static void Draw(SpriteBatch spriteBatch) {
            foreach(var shot in _asteroids) {
                shot.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Adds the asteroids.
        /// </summary>
        /// <param name="astroNum">The astro number.</param>
        public static void AddAsteroids(int astroNum){
            for (var i = 0; i < astroNum; i++){
                AddAsteroid(1);
            }
        }

        /// <summary>
        /// Adds the shot.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <param name="center">The center.</param>
        public static void AddAsteroid(float scale,Vector2? center=null){
            var newCenter = center ?? new Vector2(_rand.Next(AsteroidsGame.Width),_rand.Next(AsteroidsGame.Height));
            var alfa = MathHelper.ToRadians(_rand.Next(360));
            var velocity=new Vector2((float) Math.Cos(alfa),(float) Math.Sin(alfa));
            var rot =(float) _rand.NextDouble() * MathHelper.ToRadians(20) - MathHelper.ToRadians(10);
            _asteroids.Add(new Asteroid(_texture,newCenter,velocity,rot,scale));
        }
    }
}

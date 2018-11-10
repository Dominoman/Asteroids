using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Asteroids {
    /// <summary>
    /// 
    /// </summary>
    public static class SoundManager{
        private static int _width;
        private static SoundEffect _explosion;
        private static SoundEffect _missile;
        private static SoundEffectInstance _thrustInstance;
        private static Song _song;

        /// <summary>
        /// Initializes the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="width">The width.</param>
        public static void Initialize(ContentManager content, int width){
            _width = width;
            _explosion = content.Load<SoundEffect>(@"Sounds\explosion");
            _missile = content.Load<SoundEffect>(@"Sounds\missile");
            var thrust = content.Load<SoundEffect>(@"Sounds\thrust");
            _thrustInstance = thrust.CreateInstance();
            _song = content.Load<Song>(@"Sounds\soundtrack");
            MediaPlayer.Play(_song);
        }

        /// <summary>
        /// Plays the explosion.
        /// </summary>
        /// <param name="position">The position.</param>
        public static void PlayExplosion(Vector2 position){
            var p = position.X / _width * 2 - 1;
            _explosion.Play(1f, 0f, p);
        }

        /// <summary>
        /// Plays the missile.
        /// </summary>
        /// <param name="position">The position.</param>
        public static void PlayMissile(Vector2 position){
            var p = position.X / _width * 2 - 1;
            _missile.Play(1f, 0f, p);
        }

        /// <summary>
        /// Plays the thrust.
        /// </summary>
        /// <param name="position">The position.</param>
        public static void PlayThrust(Vector2 position){
            if (_thrustInstance.State == SoundState.Stopped){
                _thrustInstance.Play();
            }
            var p = position.X / _width * 2 - 1;
            _thrustInstance.Pan = p;
        }

        /// <summary>
        /// Stops the thrust.
        /// </summary>
        public static void StopThrust(){
            _thrustInstance.Stop();
        }
    }
}

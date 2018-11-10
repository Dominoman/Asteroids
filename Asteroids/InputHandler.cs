using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Asteroids {
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.Xna.Framework.GameComponent" />
    public class InputHandler:GameComponent{
        private static KeyboardState _keyboardState;
        private static KeyboardState _oldKeyboardState;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputHandler"/> class.
        /// </summary>
        /// <param name="game">The game.</param>
        public InputHandler(Game game) : base(game){
            _keyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public override void Update(GameTime gameTime){
            _oldKeyboardState = _keyboardState;
            _keyboardState = Keyboard.GetState();
            base.Update(gameTime);
        }

        /// <summary>
        /// Determines whether [is key pressed] [the specified keys].
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns>
        ///   <c>true</c> if [is key pressed] [the specified keys]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsKeyPressed(Keys keys) => _keyboardState.IsKeyDown(keys) & _oldKeyboardState.IsKeyUp(keys);

        /// <summary>
        /// Determines whether [is key releases] [the specified keys].
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns>
        ///   <c>true</c> if [is key releases] [the specified keys]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsKeyReleases(Keys keys) => _keyboardState.IsKeyUp(keys) & _oldKeyboardState.IsKeyDown(keys);

        /// <summary>
        /// Determines whether [is key down] [the specified keys].
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns>
        ///   <c>true</c> if [is key down] [the specified keys]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsKeyDown(Keys keys) => _keyboardState.IsKeyDown(keys);
    }
}

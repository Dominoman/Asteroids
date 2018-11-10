using Asteroids.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class AsteroidsGame:Game{
        private enum GameState { Menu,Play,Death}
        public const int Width = 800;
        public const int Height = 600;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D background;
        private BgScroll bgScroll;
        private BgScroll bgScroll2;
        private Ship ship;
        private int score;
        private SpriteFont pericles14;
        private int respawnCounter;
        private const int MaxRespawnCounter = 200;
        private GameState gameState=GameState.Menu;
        private Texture2D logo;
        private bool textEnable;
        private int textCounter;
        private const int MaxTextCounter = 30;

        public AsteroidsGame() {
            graphics = new GraphicsDeviceManager(this){
                PreferredBackBufferWidth = Width,
                PreferredBackBufferHeight = Height
            };
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            Components.Add(new InputHandler(this));

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>(@"Images\nebula_blue.f2014");
            bgScroll=new BgScroll(Content.Load<Texture2D>(@"Images\debris2_blue")) {DxOffset =0.5f};
            bgScroll2 = new BgScroll(Content.Load<Texture2D>(@"Images\debris2_blue")) {Flip=true};
            ship=new Ship(Content.Load<Texture2D>(@"Images\double_ship"),new Vector2(Width/2,Height/2));
            ShotManager.Initialize(Content.Load<Texture2D>(@"Images\shot2"));
            AsteroidsManager.Initialize(Content.Load<Texture2D>(@"Images\asteroid_blue"));
            AsteroidsManager.AddAsteroids(5);
            pericles14 = Content.Load<SpriteFont>(@"Fonts\Pericles14");
            ExplosionsManager.Initialize(Content.Load<Texture2D>(@"Images\explosion_alpha"));
            SoundManager.Initialize(Content,Width);
            logo = Content.Load<Texture2D>(@"Images\asteroids");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            bgScroll.Update(gameTime);
            bgScroll2.Update(gameTime);

            if (gameState == GameState.Menu){
                textCounter--;
                if (textCounter <= 0){
                    textCounter = MaxRespawnCounter;
                    textEnable = !textEnable;
                }
                if(InputHandler.IsKeyPressed(Keys.Space)) gameState=GameState.Play;
            }else if (gameState == GameState.Play){
                if(ship.IsDead) {
                    respawnCounter--;
                    if(respawnCounter < 0) {
                        ship.Center = new Vector2(Width / 2,Height / 2);
                        ship.Velocity = Vector2.Zero;
                        ship.IsDead = false;
                    }
                } else {
                    if(InputHandler.IsKeyDown(Keys.Left)) ship.Rotation -= 0.05f;
                    if(InputHandler.IsKeyDown(Keys.Right)) ship.Rotation += 0.05f;
                    ship.Thrust = InputHandler.IsKeyDown(Keys.Up);
                    if(InputHandler.IsKeyDown(Keys.LeftControl)) ShotManager.AddShot(ship.Center + ship.Heading * ship.Radius,ship.Heading * 3);
                }
                ship.Update(gameTime);
                CollisionDetection();
                ShotManager.Update(gameTime);
                AsteroidsManager.Update(gameTime);
                ExplosionsManager.Update(gameTime);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(background,Vector2.Zero,Color.White);
            bgScroll.Draw(spriteBatch);

            if (gameState == GameState.Menu){
                spriteBatch.Draw(logo,new Vector2((Width - logo.Width) / 2,100),Color.White);
                if(textEnable) spriteBatch.DrawString(pericles14,"Press Space to continue",new Vector2(280,300),Color.White);
            }
            bgScroll2.Draw(spriteBatch);
            if (gameState == GameState.Play){
                ship.Draw(spriteBatch);
                ShotManager.Draw(spriteBatch);
                AsteroidsManager.Draw(spriteBatch);
                ExplosionsManager.Draw(spriteBatch);
                spriteBatch.DrawString(pericles14,$"Score: {score}",new Vector2(10,10),Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Collisions the detection.
        /// </summary>
        private void CollisionDetection(){
            foreach (var asteroid in AsteroidsManager.Asteroids){
                foreach (var shot in ShotManager.Shots){
                    if (!shot.IsDead & !asteroid.IsDead & shot.IsCircleCollide(asteroid)){
                        shot.IsDead = true;
                        asteroid.IsDead = true;
                        score += 1000;
                        ExplosionsManager.AddExplosion(asteroid.Center);
                        if (asteroid.Scale > 0.5f){
                            var scale = asteroid.Scale == 1 ? 0.75f : 0.5f;
                            AsteroidsManager.AddAsteroid(scale,asteroid.Center);
                            AsteroidsManager.AddAsteroid(scale,asteroid.Center);
                        }
                    }
                }
                if (!ship.IsDead & !asteroid.IsDead & ship.IsCircleCollide(asteroid)){
                    ship.IsDead = true;
                    ExplosionsManager.AddExplosion(ship.Center);
                    respawnCounter = MaxRespawnCounter;
                    SoundManager.StopThrust();
                }
            }
        }
    }
}

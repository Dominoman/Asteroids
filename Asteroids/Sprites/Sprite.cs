using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Sprites {
    /// <summary>
    /// 
    /// </summary>
    public class Sprite{
        protected readonly Texture2D texture;
        protected readonly int FrameWidth;
        protected readonly int FrameHeight;
        protected readonly Rectangle[] Frames;
        private Vector2 center;
        private int currentFrame;
        private float rotation;
        private float dRotation;
        protected bool Wrap;

        public Vector2 Center{
            get{ return center; }
            set{
                if (Wrap){
                    var x = (value.X + AsteroidsGame.Width) % AsteroidsGame.Width;
                    var y = (value.Y + AsteroidsGame.Height) % AsteroidsGame.Height;
                    center=new Vector2(x,y);
                }else center = value;
            }
        }

        public int CurrentFrame{
            get{ return currentFrame; }
            set{ currentFrame = MathHelper.Clamp(value,0,Frames.Length-1); }
        }

        public float Rotation{
            get{ return rotation; }
            set{ rotation = MathHelper.WrapAngle(value); }
        }

        public float DRotation{
            get { return dRotation; }
            set{ dRotation = MathHelper.WrapAngle(value); }
        }
        
        public Vector2 Velocity{ get; set; }=Vector2.Zero;
        public int Radius{ get; protected set; }
        public bool IsDead{ get; set; }
        public float Scale{ get; set; } = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite" /> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="center">The center.</param>
        /// <param name="frameCount">The frame count.</param>
        public Sprite(Texture2D texture, Vector2 center,int frameCount=1){
            this.texture = texture;
            FrameWidth = texture.Width/frameCount;
            FrameHeight = texture.Height;
            this.center = center;
            Frames=new Rectangle[frameCount];
            for (var i = 0; i < frameCount; i++){
                Frames[i]=new Rectangle(i*FrameWidth,0,FrameWidth,FrameHeight);
            }
            Radius = FrameWidth / 2;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void Update(GameTime gameTime){
            Center += Velocity;
            rotation += dRotation;
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public virtual void Draw(SpriteBatch spriteBatch){
            if(!IsDead) spriteBatch.Draw(texture,new Rectangle((int) center.X,(int) center.Y,(int)(FrameWidth*Scale),(int)(FrameHeight*Scale)),Frames[currentFrame],Color.White,rotation,new Vector2(FrameWidth/2,FrameHeight/2), SpriteEffects.None, 0);
        }

        /// <summary>
        /// Determines whether [is circle collide] [the specified other].
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>
        ///   <c>true</c> if [is circle collide] [the specified other]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCircleCollide(Sprite other) => (center - other.center).Length() < Radius + other.Radius;
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace Break_Out
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _whiteRectangle;
        private Texture2D _whitePlatform;
        private double _xRectangle = 0;
        private double _yRectangle = 0;
        private double _xPlatform = 0;
        private const double _yPlatform = 300;
        private double _speedX = 2.5;
        private double _speedY = 2.5;
        private int width = 10;
        private int height = 10;
        private int _platformWidth = 70;
        private int height2 = 15;
        private SoundEffect _soundbeep4;
        private SoundEffect _soundbeep7;
        private SpriteFont _font;
        private int _score = 0;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);

            _whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
            _whitePlatform = new Texture2D(GraphicsDevice, 1, 1);
            _whiteRectangle.SetData(new[] { Color.White });
            _whitePlatform.SetData(new[] { Color.White });
            _soundbeep4 = Content.Load<SoundEffect>("beep4");
            _soundbeep7 = Content.Load<SoundEffect>("beep7");

            _font = Content.Load<SpriteFont>("File");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _xRectangle += _speedX;
            _yRectangle += _speedY;

            if (_xRectangle +width >= _graphics.PreferredBackBufferWidth || _xRectangle < 0) {
                _speedX *= -1;
                _soundbeep7.Play();
            } 
            if (_yRectangle +height >= _graphics.PreferredBackBufferHeight || _yRectangle < 0)
            {
                _speedY *= -1;
                _soundbeep4.Play();
            }
            if (_xRectangle > _xPlatform && 
                _xRectangle < _xPlatform + _platformWidth + height2 && 
                _yRectangle + height + width> _yPlatform && 
                _yRectangle < _yPlatform + height2)
            {
                _speedY *= -1;
                _soundbeep4.Play();
                _score++;
                if (_score % 5 == 0)
                {
                    _platformWidth -= 5;
                }
                if (_yRectangle +height >= _graphics.PreferredBackBufferHeight || _yRectangle < 0)
                {
                    _score = 0;
                }
                
            }
            var mouseState = Mouse.GetState();
            _xPlatform = mouseState.X;
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();


            var rectangle = new Rectangle((int)_xRectangle, (int)_yRectangle, width, height);
            _spriteBatch.Draw(_whiteRectangle, rectangle, Color.Plum);

            var platform = new Rectangle((int)_xPlatform, (int)_yPlatform, _platformWidth, height2);
            _spriteBatch.Draw(_whitePlatform, platform, Color.Black); 

            _spriteBatch.DrawString(_font, "Score:" + _score.ToString(), new Vector2(100, 100), Color.Black);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

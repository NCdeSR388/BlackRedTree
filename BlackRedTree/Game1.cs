using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlackRedTree
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //instancias
        Arbol arbol;//instancia de arbol
        Texture2D[] TexturasNodo;
        SpriteFont Font;
        int RPX = 1200, RPY = 800;
        int delay = 0;
        //coment
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this._graphics.PreferredBackBufferWidth = RPX;//ancho pantalla
            this._graphics.PreferredBackBufferHeight = RPY;//alto
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            arbol = new Arbol();
            TexturasNodo = new Texture2D[3];
            TexturasNodo[0] = Content.Load<Texture2D>("Nodo");
            TexturasNodo[1] = Content.Load<Texture2D>("LeftLine");
            TexturasNodo[2] = Content.Load<Texture2D>("RigthLine");
            Font = Content.Load<SpriteFont>("Font");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState State = Keyboard.GetState();
            Keys[] key = State.GetPressedKeys();
            delay++;
            foreach(Keys key2 in key)
            {
                if(key2.Equals(Keys.Enter))
                {
                    if (delay > 20)
                    {
                    arbol.Insertar();
                        delay = 0;
                    }
                    
                }
            }
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if(arbol.raiz != null)
            {
                arbol.dibujar_arbol(gameTime, TexturasNodo, _spriteBatch, Font);
            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
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
        Texture2D boton;
        Rectangle[] rectBotones;
        int posXboton = 50;
        Rectangle rectCursor;
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
            rectBotones = new Rectangle[8];

            boton = Content.Load<Texture2D>("Boton");
            TexturasNodo[0] = Content.Load<Texture2D>("Nodo");
            TexturasNodo[1] = Content.Load<Texture2D>("LeftLine");
            TexturasNodo[2] = Content.Load<Texture2D>("RigthLine");
            Font = Content.Load<SpriteFont>("Font");

            rectBotones[0] = new Rectangle(20, 40, boton.Width, boton.Height);
            rectBotones[1] = new Rectangle(20, 120, boton.Width, boton.Height);
            rectBotones[2] = new Rectangle(20, 200, boton.Width, boton.Height);
            rectBotones[3] = new Rectangle(20, 280, boton.Width, boton.Height);
            rectBotones[4] = new Rectangle(20, 360, boton.Width, boton.Height);
            rectBotones[5] = new Rectangle(20, 440, boton.Width, boton.Height);
            rectBotones[6] = new Rectangle(20, 520, boton.Width, boton.Height);
            rectBotones[7] = new Rectangle(20, 600, boton.Width, boton.Height);
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

            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            rectCursor = new Rectangle(mouseState.X, mouseState.Y, 1, 1);


            for (int i = 0; i < rectBotones.Length; i++)
            {
                if (rectCursor.Intersects(rectBotones[i]))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        posXboton += posXboton;
                        rectBotones[i] = new Rectangle(posXboton, 40 * i, boton.Width, boton.Height);
                    }
                }
            }
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(boton, rectBotones[0], Color.White);
            _spriteBatch.DrawString(Font, "Insertar", new Vector2(64, 67), Color.White);
            _spriteBatch.Draw(boton, rectBotones[1], Color.White);
            _spriteBatch.DrawString(Font, "Guardar Arbol", new Vector2(35, 147), Color.White);
            _spriteBatch.Draw(boton, rectBotones[2], Color.White);
            _spriteBatch.DrawString(Font, "Limpiar Arbol", new Vector2(40, 227), Color.White);
            _spriteBatch.Draw(boton, rectBotones[3], Color.White);
            _spriteBatch.DrawString(Font, "Cargar Arbol", new Vector2(44, 307), Color.White);
            _spriteBatch.Draw(boton, rectBotones[4], Color.White);
            _spriteBatch.DrawString(Font, "Buscar Clave", new Vector2(43, 387), Color.White);
            _spriteBatch.Draw(boton, rectBotones[5], Color.White);
            _spriteBatch.DrawString(Font, "Inorden", new Vector2(65, 467), Color.White);
            _spriteBatch.Draw(boton, rectBotones[6], Color.White);
            _spriteBatch.DrawString(Font, "Preorden", new Vector2(58, 547), Color.White);
            _spriteBatch.Draw(boton, rectBotones[7], Color.White);
            _spriteBatch.DrawString(Font, "Posorden", new Vector2(58, 627), Color.White);
            _spriteBatch.End();

            if(arbol.raiz != null)
            {
                arbol.dibujar_arbol(gameTime, TexturasNodo, _spriteBatch, Font);
            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
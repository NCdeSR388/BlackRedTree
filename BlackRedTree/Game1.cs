using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BlackRedTree
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //instancias
        Arbol arbol;//instancia de arbol
        Texture2D[] TexturasNodo;//Variable de las texturas de nonos y elaces
        Texture2D boton;//Textura de los botones
        Rectangle[] rectBotones;//Rectangulos para dibujo de botones
        SpriteFont Font;//Spritefont para textos y numeros de claves
        int RPX = 1200, RPY = 800;//Tamalo de la pantalla preferido
        //Entradas para interaccion con botones
        Rectangle rectCursor;//Rectangulo auto posicionado en las coordenadas del puntero
        bool pressed = false;//Bool para guardar estado de el mouse
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
            //Se borra la tabla temporal
            arbol.data.dropTableTemp();
            TexturasNodo = new Texture2D[3];
            rectBotones = new Rectangle[8];
            //Carga de texturas de nodo, enlaces y el spritefont
            boton = Content.Load<Texture2D>("Boton");
            TexturasNodo[0] = Content.Load<Texture2D>("Nodo");
            TexturasNodo[1] = Content.Load<Texture2D>("LeftLine");
            TexturasNodo[2] = Content.Load<Texture2D>("RigthLine");
            Font = Content.Load<SpriteFont>("Font");
            //Declaracion de rectangulos de los botones en pantalla
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
            //Instancia para obtener estado de el mouse
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            rectCursor = new Rectangle(mouseState.X, mouseState.Y, 1, 1);//Se asigna la posicion al rectangulo del puntero
            int select = 0;
            //Se verifica si se intersecta el rectangulo del puntero y un boton
            for (int i = 0; i < rectBotones.Length; i++)
            {
                if (rectCursor.Intersects(rectBotones[i]))
                {
                    //Si se intersecta se guarda el valor de el boton intersectado(Empezando por 1)
                    select = i + 1;
                }
            }
            //Se verifica si el boton izquierdo del mouse no esta presionado
            if (mouseState.LeftButton == ButtonState.Released)
            {
                //Se actualiza la variable de estado
                pressed = false;
            }
            /*Se verifica si se esta presionando el boton izquierdo del mouse 
             * y ADEMAS si no estaba presionado desde el frame anterior
            Esto nos ayuda a evitar que las operaciones a continuacion se realicen mas de una vez 
            por cada seleccion de el usuario
             */
            if (mouseState.LeftButton == ButtonState.Pressed && pressed == false)
            {
                pressed = true;
                /*
                Insertar
                Guardar
                Limpiar
                Cargar
                Buscar
                Inorden
                Preorden
                Posorden
                */
                /*A continuacion se realizan las operaciones logicas necesarias antes de llamar
                 a cada funcion elaborada respectivamente en la Instancia de la Clase nodo y Arbol*/
                switch (select)
                {
                    case 1:
                        arbol.Insertar();
                        break;
                    case 2:
                        int[] list = arbol.data.CargarNodosTemp();
                        if (list != null)
                        {
                            arbol.data.dropTable();
                            foreach (int n in list)
                            {
                                arbol.data.GuardarNodo(n);
                            }
                            System.Windows.Forms.MessageBox.Show("Arbol guardado correctamente", "Atencion");
                        }
                        break;
                    case 3:
                        arbol = new Arbol();
                        arbol.data.dropTableTemp();
                        System.Windows.Forms.MessageBox.Show("Arbol temporal eliminado", "Atencion");
                        break;
                    case 4:
                        arbol = new Arbol();
                        arbol.data.dropTableTemp();
                        arbol.data.CargarNodos(ref arbol);
                        System.Windows.Forms.MessageBox.Show("Ultimo arbol guardado \n cargado correctamente", "Atencion");
                        break;
                    case 5:
                        arbol.Buscar();
                        break;
                    case 6:
                        System.Windows.Forms.MessageBox.Show(arbol.Inorden(arbol.raiz), "Recorrido Innorden") ;
                        break;
                    case 7:
                        System.Windows.Forms.MessageBox.Show(arbol.Preorden(arbol.raiz), "Recorrido Preorden");
                        break;
                    case 8:
                        System.Windows.Forms.MessageBox.Show(arbol.Posorden(arbol.raiz), "Recorrido Posorden");
                        break;
                    default:
                        break;
                }
            }

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //En este apartado solamente llamamos a la funcion dibujar arbol la cual debe llamarse en tiempo real
            //A dicha funcion se le pasa el _spritebatch para dibujar los objetos dentro de ella
            if (arbol.raiz != null)
            {
                arbol.dibujar_arbol(TexturasNodo, _spriteBatch, Font);
            }
            _spriteBatch.Begin();
            //Se dibujan los botones
            foreach (Rectangle _rectBotones in rectBotones)
            {
                _spriteBatch.Draw(boton, _rectBotones, Color.White);
            }
            //Luego se dibujan los strings de los botones
            _spriteBatch.DrawString(Font, "Insertar", new Vector2(64, 67), Color.White);
            _spriteBatch.DrawString(Font, "Guardar Arbol", new Vector2(35, 147), Color.White);
            _spriteBatch.DrawString(Font, "Limpiar Arbol", new Vector2(40, 227), Color.White);
            _spriteBatch.DrawString(Font, "Cargar Arbol", new Vector2(44, 307), Color.White);
            _spriteBatch.DrawString(Font, "Buscar Clave", new Vector2(43, 387), Color.White);
            _spriteBatch.DrawString(Font, "Inorden", new Vector2(65, 467), Color.White);
            _spriteBatch.DrawString(Font, "Preorden", new Vector2(58, 547), Color.White);
            _spriteBatch.DrawString(Font, "Posorden", new Vector2(58, 627), Color.White);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
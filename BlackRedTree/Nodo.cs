using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Windows.Forms;

namespace BlackRedTree
{


    public class Nodo
    {
        Random rand = new Random();
        public int clave;
        public Nodo izquierdo;
        public Nodo derecho;
        public Color color;

        //Variables para caracteristicas graficas del nodo
        public int nodoX, nodoY, diameter;
        public int nivel;
        public int FacrorEquilibrio;
        public int alturaNodo;
        //Variables para ordenamiento del nodo

        public Nodo()
        {
            this.clave = rand.Next(1000);
            color = Color.Red;

        }
        public Nodo(int clave)
        {
            this.clave = clave;
            color = Color.Red;

        }
        public Nodo(int clave, Color color)
        {
            this.clave = clave;
            this.color = color;

        }

        public bool insertar(Nodo new_nodo)
        {
            if (new_nodo.clave > clave)
            {
                if (derecho == null)
                {
                    derecho = new_nodo;
                }
                else
                {
                    derecho.insertar(new_nodo);
                }
                return true;
            }
            else if (new_nodo.clave < clave)
            {
                if (izquierdo == null)
                {
                    izquierdo = new_nodo;
                }
                else
                {
                    izquierdo.insertar(new_nodo);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public void XY_position(int _X_nodo, int _Y_nodo, int distanceY, int distanceX, int _diameter, int alturaArbol)
        {
            nodoX = _X_nodo;
            nodoY = _Y_nodo;
            diameter = _diameter;
            if (izquierdo != null)
            {
                izquierdo.XY_position(_X_nodo - distanceX - (alturaArbol * diameter) / 2, _Y_nodo + distanceY, distanceY, distanceX, _diameter, alturaArbol);
            }
            if (derecho != null)
            {
                derecho.XY_position(_X_nodo + distanceX + (alturaArbol * diameter) / 2, _Y_nodo + distanceY, distanceY, distanceX, _diameter, alturaArbol);
            }

        }
        public void Draw_nodo(GameTime gameTime, Texture2D[] Textures, SpriteBatch _spriteBatch, SpriteFont Font, int distanceX, int distanceY, int alturaArbol)
        {
            
            if(derecho != null)
            {
                _spriteBatch.Begin();
                Rectangle rectLine = new Rectangle(nodoX + (diameter / 2), nodoY + (diameter / 2), distanceX + (alturaArbol * diameter) / 2, distanceY);
                _spriteBatch.Draw(Textures[2], rectLine, Color.Gray);
                _spriteBatch.End();
                derecho.Draw_nodo(gameTime, Textures, _spriteBatch, Font, distanceX, distanceY, alturaArbol);
            }
            if(izquierdo != null)
            {
                _spriteBatch.Begin();
                Rectangle rectLine = new Rectangle(nodoX - (diameter / 2) - (alturaArbol * diameter) / 3, nodoY + (diameter / 2), distanceX + (alturaArbol * diameter) / 2, distanceY);
                _spriteBatch.Draw(Textures[1], rectLine, Color.Gray);
                _spriteBatch.End();
                izquierdo.Draw_nodo(gameTime, Textures, _spriteBatch, Font, distanceX, distanceY, alturaArbol);
            }
            _spriteBatch.Begin();
            Rectangle rect_nodo = new Rectangle(nodoX, nodoY, diameter, diameter);
            _spriteBatch.Draw(Textures[0], rect_nodo, color);
            Vector2 VectorClave = new Vector2(nodoX+(diameter/4), nodoY+(diameter/3)); 
            _spriteBatch.DrawString(Font, clave.ToString(), VectorClave, Color.Black) ;
            _spriteBatch.End();
        }
        public int altura(int altura_actual, int MayorNivel)
        {
            altura_actual++;
            this.alturaNodo = altura_actual;
            if(MayorNivel < altura_actual)
            {
                MayorNivel = altura_actual;
            }
            if(izquierdo != null)
            {
                MayorNivel = izquierdo.altura(altura_actual,MayorNivel);
            }
            if(derecho != null)
            {
                MayorNivel = derecho.altura(altura_actual, MayorNivel);
            }
            return MayorNivel;
        }

        public void CalcularFactorEq()
        {
            FacrorEquilibrio = 0;
            if(izquierdo != null)
            {
                FacrorEquilibrio--;
                if(izquierdo.izquierdo != null || izquierdo.derecho != null)
                {
                    FacrorEquilibrio--;
                }
                izquierdo.CalcularFactorEq();
            }
            if(derecho != null)
            {
                FacrorEquilibrio++;                
                if(derecho.izquierdo != null || derecho.derecho != null)
                {
                    FacrorEquilibrio++;
                }
                derecho.CalcularFactorEq();
            }
            if(FacrorEquilibrio == 2 || FacrorEquilibrio == -2)
            {
                MessageBox.Show(clave.ToString() + ", " + FacrorEquilibrio.ToString());
            }
        }
        public bool RotacionRojoNegro()
        {

            if(izquierdo != null)
            {
                if(izquierdo.color == Color.Red)
                {
                    //Verificamos un posible Caso 1
                    bool dv = false;
                    if (derecho != null)
                    {
                        if (derecho.color == Color.Red)
                        {
                            dv = true;
                            if (izquierdo.izquierdo != null)
                            {
                                if (izquierdo.izquierdo.color == Color.Red)
                                {
                                    //Caso1 Izquierda-Izquierda
                                    color = Color.Red;
                                    izquierdo.color = Color.Gray;
                                    derecho.color = Color.Gray;
                                    return true;
                                }
                            }
                            if (izquierdo.derecho != null)
                            {
                                if (izquierdo.derecho.color == Color.Red)
                                {
                                    //Caso1 Izquierda-Derecha
                                    color = Color.Red;
                                    izquierdo.color = Color.Gray;
                                    derecho.color = Color.Gray;
                                    return true;
                                }
                            }
                        }
                    }
                    if(dv != true)
                    {
                    //Verificamos un posible caso 2

                        if (izquierdo.derecho != null)
                        {
                            if (izquierdo.derecho.color == Color.Red)
                            {
                                MessageBox.Show("Caso 2");
                                //Caso2 Izquierda(Se convierte a un caso 3 de izquierda)
                                Nodo temp = izquierdo.derecho.izquierdo;
                                izquierdo.derecho.izquierdo = izquierdo;
                                izquierdo = izquierdo.derecho;
                                izquierdo.izquierdo.derecho = temp;
                                return true;
                            }
                        }
                    //Verificamos un posible caso 3
                        
                        if(izquierdo.izquierdo != null)
                        {
                            if(izquierdo.izquierdo.color == Color.Red)
                            {
                                MessageBox.Show("Caso 3");
                                Nodo temp = new Nodo(clave, Color.Red);
                                temp.izquierdo = izquierdo.derecho;
                                temp.derecho = derecho;
                                clave = izquierdo.clave;
                                color = Color.Gray;
                                izquierdo = izquierdo.izquierdo;
                                derecho = temp;
                                return true;
                            }
                        }
                    }


                }
            }
            if (derecho != null)
            {
                if (derecho.color == Color.Red)
                {
                    //Verificamos un posible Caso 1
                    bool dv = false;
                    if (izquierdo != null)
                    {
                        if (izquierdo.color == Color.Red)
                        {
                            dv = true;
                            if (derecho.derecho != null)
                            {
                                if (derecho.derecho.color == Color.Red)
                                {
                                    //Caso1 Izquierda-Izquierda
                                    color = Color.Red;
                                    derecho.color = Color.Gray;
                                    izquierdo.color = Color.Gray;
                                    return true;
                                }
                            }
                            if (derecho.izquierdo != null)
                            {
                                if (derecho.izquierdo.color == Color.Red)
                                {
                                    //Caso1 Izquierda-Derecha
                                    color = Color.Red;
                                    derecho.color = Color.Gray;
                                    izquierdo.color = Color.Gray;
                                    return true;
                                }
                            }
                        }
                    }
                    if (dv != true)
                    {
                        //Verificamos un posible caso 2

                        if (derecho.izquierdo != null)
                        {
                            if (derecho.izquierdo.color == Color.Red)
                            {
                                MessageBox.Show("Caso 2");
                                //Caso2 Izquierda(Se convierte a un caso 3 de izquierda)
                                Nodo temp = derecho.izquierdo.derecho;
                                derecho.izquierdo.derecho = derecho;
                                derecho = derecho.izquierdo;
                                derecho.derecho.izquierdo = temp;
                                return true;
                            }
                        }
                        //Verificamos un posible caso 3

                        if (derecho.derecho != null)
                        {
                            if (derecho.derecho.color == Color.Red)
                            {
                                MessageBox.Show("Caso 3");
                                Nodo temp = new Nodo(clave, Color.Red);
                                temp.derecho = derecho.izquierdo;
                                temp.izquierdo = izquierdo;
                                clave = derecho.clave;
                                color = Color.Gray;
                                derecho = derecho.derecho;
                                izquierdo = temp;
                                return true;
                            }
                        }
                    }


                }
            }



            //Caso3 Izquierda

            //Caso por default
            if (alturaNodo == 1)
            {
                color = Color.Gray;
            }
            //Practicamos recursividad y retornamos resultados
            bool d = false;
            if(derecho != null)
            {
                d = derecho.RotacionRojoNegro();
            }
            if(izquierdo != null)
            {
                if(izquierdo.RotacionRojoNegro() == true || d == true)
                {
                    return true;
                }
            }
            return false;
        }
        /*
         no es lo que queria pero devuelve el numero de nodos xd
        public int altura(int altura_actual)
        {
            altura_actual++;
            int a1 = 0 ;
            if(derecho != null)
            {
                a1 = derecho.altura(altura_actual);
            }
            if(a1 > altura_actual)
            {
                altura_actual = a1;
            }
            if(izquierdo != null)
            {
                a1 = izquierdo.altura(altura_actual);
            }
            if (a1 > altura_actual)
            {
                altura_actual = a1;
            }
            return altura_actual;
        }
         */

    }
}
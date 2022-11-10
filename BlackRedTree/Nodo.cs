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
        public int color;

        //Variables para caracteristicas graficas del nodo
        public int nodoX, nodoY, diameter;
        public int nivel;
        public int FacrorEquilibrio;

        public Nodo()
        {
            this.clave = rand.Next(1000);
            color = 0;

        }
        public Nodo(int clave)
        {
            this.clave = clave;
            color = 0;

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
            _spriteBatch.Draw(Textures[0], rect_nodo, Color.Gray);
            Vector2 VectorClave = new Vector2(nodoX+(diameter/4), nodoY+(diameter/3)); 
            _spriteBatch.DrawString(Font, clave.ToString(), VectorClave, Color.Black) ;
            _spriteBatch.End();
        }
        public int altura(int altura_actual, int MayorNivel)
        {
            altura_actual++;
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
        
        public void RotacionesPorEquilibrio()
        {
            if (FacrorEquilibrio == -2)
            {
                if (izquierdo.izquierdo == null)
                {

                    Nodo temp1 = new Nodo(clave);
                    Nodo temp2 = new Nodo(izquierdo.clave);
                    clave = izquierdo.derecho.clave;
                    izquierdo = temp2;
                    derecho = temp1;
                }
                else
                {
                    Nodo temp1 = new Nodo(clave);
                    Nodo temp2 = new Nodo(izquierdo.izquierdo.clave);
                    clave = izquierdo.clave;
                    izquierdo = temp2;
                    derecho = temp1;
                }
            }
            if (FacrorEquilibrio == 2)
            {
                if (derecho.derecho == null)
                {

                    Nodo temp1 = new Nodo(clave);
                    Nodo temp2 = new Nodo(derecho.clave);
                    clave = derecho.izquierdo.clave;
                    derecho = temp2;
                    izquierdo = temp1;
                }
                else
                {
                    Nodo temp1 = new Nodo(clave);
                    Nodo temp2 = new Nodo(derecho.derecho.clave);
                    clave = derecho.clave;
                    derecho = temp2;
                    izquierdo = temp1;
                }
            }
            if (izquierdo != null)
            {
                izquierdo.RotacionesPorEquilibrio();
            }
            if(derecho != null)
            {

                derecho.RotacionesPorEquilibrio();
            }
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
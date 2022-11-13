using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlackRedTree
{
    public class Arbol
    {
        public Data data = new Data();
        public Nodo raiz;
        public string inorden = "Inorden: ";
        public string preorden = "Preorden: ";
        public string posorden = "Posorden: ";
        //parametros graficos para posiciones de nodo
        int distanceX = 40;
        int distanceY = 70;
        int diameter = 50;
        int X_raiz = 600, Y_raiz = 20;
        int alturaArbol;
        double compensacion = 1.8;

        public Arbol()
        {
            raiz = null;
            alturaArbol = 0;
            data.createTable();
        }

        // Insercion de datos
        public void Insertar()
        {
            Nodo new_nodo = new Nodo();
            if (raiz == null)
            {
                raiz = new_nodo;
            }
            else
            {
                raiz.insertar(new_nodo);
            }
            alturaArbol = raiz.altura(0, 0);
            posicion_nodo();
            bool temp = true;
            while (temp)
            {
                temp = raiz.RotacionRojoNegro();
            }
            data.GuardarNodoTemp(new_nodo.clave);
        }
        public void Insertar(int clave)
        {
            Nodo new_nodo = new Nodo(clave);
            if (raiz == null)
            {
                raiz = new_nodo;
            }
            else
            {
                raiz.insertar(new_nodo);
            }
            alturaArbol = raiz.altura(0, 0);
            posicion_nodo();
            bool temp = true;
            while (temp)
            {
                temp = raiz.RotacionRojoNegro();
            }
        }
        //METODO BUSCAR TIPO STRING PARA RETORNAR EL RESULTADO EN UN SPRITEFONT
        public string Buscar(int clave)
        {
            if (raiz != null)
            {
                raiz.buscar(clave, raiz);
            }

            else
            {
                return "Error de busqueda: No se ha creado un arbol rojo/negro";
            }

            return null;
        }
        public string Inorden(Nodo n)
        {
            if (n != null)
            {
                Inorden(n.izquierdo);
                inorden += n.clave + ", ";
                Inorden(n.derecho);
            }
            return inorden;
        }

        public string Preorden(Nodo n)
        {
            if (n != null)
            {
                preorden += n.clave + ", ";
                Preorden(n.izquierdo);
                Preorden(n.derecho);
            }
            return preorden;
        }

        public string Posorden(Nodo n)
        {
            if (n != null)
            {
                Posorden(n.izquierdo);
                Posorden(n.derecho);
                posorden += n.clave + ", ";
            }
            return posorden;
        }
        private void posicion_nodo()
        {
            raiz.nodoX = X_raiz;
            raiz.nodoY = Y_raiz;
            raiz.diameter = diameter;
            if (raiz.izquierdo != null)
            {
                raiz.izquierdo.XY_position(Convert.ToInt32(X_raiz - (distanceX * raiz.izquierdo.AlturaDerecha())*compensacion), Y_raiz + distanceY, distanceY, distanceX, diameter);
            }
            if (raiz.derecho != null)
            {
                raiz.derecho.XY_position(Convert.ToInt32(X_raiz + (distanceX * raiz.derecho.AlturaIzquierda())*compensacion), Y_raiz + distanceY, distanceY, distanceX, diameter);
            }
        }
        public void dibujar_arbol(GameTime gameTime, Texture2D[] Textures, SpriteBatch _spriteBatch, SpriteFont Font)
        {

            posicion_nodo();
            if (raiz.izquierdo != null)
            {
                _spriteBatch.Begin();
                Rectangle rectLine = new Rectangle(X_raiz + (diameter / 2) - Convert.ToInt32(distanceX * raiz.izquierdo.AlturaDerecha() * compensacion), Y_raiz + (diameter / 2), Convert.ToInt32(distanceX * raiz.izquierdo.AlturaDerecha() * compensacion), distanceY);
                _spriteBatch.Draw(Textures[1], rectLine, Color.Gray);
                _spriteBatch.End();
                raiz.izquierdo.Draw_nodo(gameTime, Textures, _spriteBatch, Font, distanceX, distanceY);
            }
            if (raiz.derecho != null)
            {
                _spriteBatch.Begin();
                Rectangle rectLine = new Rectangle(X_raiz + (diameter / 2), Y_raiz + (diameter / 2), Convert.ToInt32(distanceX * raiz.derecho.AlturaIzquierda() * compensacion), distanceY);
                _spriteBatch.Draw(Textures[2], rectLine, Color.Gray);
                _spriteBatch.End();
                raiz.derecho.Draw_nodo(gameTime, Textures, _spriteBatch, Font, distanceX, distanceY);
            }
            _spriteBatch.Begin();
            Rectangle rect_nodo = new Rectangle(X_raiz, Y_raiz, diameter, diameter);
            _spriteBatch.Draw(Textures[0], rect_nodo, raiz.color);
            Vector2 VectorClave = new Vector2(X_raiz + (diameter / 4), Y_raiz + (diameter / 3));
            _spriteBatch.DrawString(Font, raiz.clave.ToString(), VectorClave, Color.Black);
            _spriteBatch.End();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;

namespace BlackRedTree
{
    public class Arbol
    {
        public Data data = new Data();//instancia de objeto que administra las funciones para bases de datos
        public Nodo raiz;
        //Strings para ordenamientos
        public string inorden = "Inorden: ";
        public string preorden = "Preorden: ";
        public string posorden = "Posorden: ";
        //parametros graficos para posiciones de nodo
        int distanceX = 40;//Asignamos la distancia predeterminada para los enlaces en x
        int distanceY = 70;//Distancia para las alturas de los nodos
        int diameter = 50;//Tamalo de los circulos de los nodos
        int X_raiz = 600, Y_raiz = 20;//punto de origen de el nodo padre
        int alturaArbol;//Altura total de el arbol
        double compensacion = 1.8;//Variable para aumento proporcional por compensacion de distancia entre los nodos

        //instancia de arbol
        public Arbol()
        {
            raiz = null;
            alturaArbol = 0;
            data.createTable();
        }

        // Insercion de datos en arbol y en tabla temporal
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
        //Insersion de datos en arbol en caso de que se posean claves ya guardadas anteriormente
        public void Insertar(int clave)
        {
            Nodo new_nodo = new Nodo(clave);//Se instancia objeto nodo con clave previamente guardada
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
        //METODO BUSCAR TIPO STRING PARA MOSTRAR EL RESULTADO EN UN MESSAGEBOX
        public void Buscar()
        {
            int clave;
            //Solicitamos el dato a buscar por medio de la funcion InputBox libreria Visual basic
            object value = Interaction.InputBox("Por favor ingrese el valor a buscar","Buscar", "");
            try
            {
                clave = Convert.ToInt32((string)value);
                if (raiz != null)
                {
                    //Llamamos un metodo recursivo para buscar la clave
                    bool result = raiz.buscar(clave);
                    if (result)
                    {
                        MessageBox.Show("Nodo encontrado");
                    }
                    else
                    {
                        MessageBox.Show("El nodo no ha sido encontrado en el arbol");
                    }
                }
                else
                {
                    MessageBox.Show("Error de busqueda: No se ha creado o cargado un arbol rojo/negro");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Por Favor ingrese un dato valido");
            }

        }
        //Metodo de recorrido innorden
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
        //Metodo de recorrido Preorden
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
        //Metodo de recorrido Posorden
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
        //Funcion para asignar valores de x y y a el nodo padre raiz
        private void posicion_nodo()
        {
            //Asignamos los datos iniciales de origen
            raiz.nodoX = X_raiz;
            raiz.nodoY = Y_raiz;
            raiz.diameter = diameter;
            //Verificamos el estado de los nodos hijos
            if (raiz.izquierdo != null)
            {
                /*
                Se llama a la funcion recursiva dentro de la clase nodo para poder realizar
                la asignacion igual a el nodo raiz.
                El punto actual menos la distancia por el factor que usamos para definir la 
                distancia de el enlace(Altura derecha de el hijo izquierdo) por la distancia en X
                 */
                raiz.izquierdo.XY_position(Convert.ToInt32(X_raiz - (distanceX * raiz.izquierdo.AlturaDerecha())*compensacion), Y_raiz + distanceY, distanceY, distanceX, diameter);
            }
            if (raiz.derecho != null)
            {
                /*
                El punto actual mas la distancia por el factor que usamos para definir la 
                distancia de el enlace(Altura izquierda de el hijo derecho) por la distancia en X
                 */
                raiz.derecho.XY_position(Convert.ToInt32(X_raiz + (distanceX * raiz.derecho.AlturaIzquierda())*compensacion), Y_raiz + distanceY, distanceY, distanceX, diameter);
            }
        }
        /*Funcion para dibujo de el nodo y los enlaces
         Recibimos como parametros un array de texturas para los enlaces y nodos,
        el Spritebatch... para poder empezar a dibujar
        dentro de la funcion y de manera recursiva mas adelante.
        Ademas recibimos un array de SpriteFont para el dibujado de las claves
         */
        public void dibujar_arbol(Texture2D[] Textures, SpriteBatch _spriteBatch, SpriteFont Font)
        {

            posicion_nodo();
            //Aqui dibujamos el enlace izquierdo del nodo raiz
            if (raiz.izquierdo != null)
            {
                _spriteBatch.Begin();
                //Abrimos el metodo dibujar y definimos un recrangulo en el cual dibujaremos el enlace izquierdo
                Rectangle rectLine = new Rectangle(X_raiz + (diameter / 2) - Convert.ToInt32(distanceX * raiz.izquierdo.AlturaDerecha() * compensacion), Y_raiz + (diameter / 2), Convert.ToInt32(distanceX * raiz.izquierdo.AlturaDerecha() * compensacion), distanceY);
                _spriteBatch.Draw(Textures[1], rectLine, Color.Gray);
                _spriteBatch.End();
                //llamamos a la funcion recursiva para el siguiente nodo
                raiz.izquierdo.Draw_nodo(Textures, _spriteBatch, Font, distanceX, distanceY);
            }
            if (raiz.derecho != null)
            {
                //Usamos el mismo proceso para el nodo derecho, pero utilizando la textura 2
                _spriteBatch.Begin();
                Rectangle rectLine = new Rectangle(X_raiz + (diameter / 2), Y_raiz + (diameter / 2), Convert.ToInt32(distanceX * raiz.derecho.AlturaIzquierda() * compensacion), distanceY);
                _spriteBatch.Draw(Textures[2], rectLine, Color.Gray);
                _spriteBatch.End();
                //llamamos a la funcion recursiva para el siguiente nodo derecho
                raiz.derecho.Draw_nodo(Textures, _spriteBatch, Font, distanceX, distanceY);
            }
            //Volvemos a abrir el spritebatch y dibujamos el nodo raiz en el nuevo rectangulo
            _spriteBatch.Begin();
            Rectangle rect_nodo = new Rectangle(X_raiz, Y_raiz, diameter, diameter);
            _spriteBatch.Draw(Textures[0], rect_nodo, raiz.color);
            Vector2 VectorClave = new Vector2(X_raiz + (diameter / 4), Y_raiz + (diameter / 3));
            _spriteBatch.DrawString(Font, raiz.clave.ToString(), VectorClave, Color.Black);
            _spriteBatch.End();
        }
    }
}

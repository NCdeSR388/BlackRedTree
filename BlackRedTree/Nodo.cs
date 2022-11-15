using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlackRedTree
{


    public class Nodo
    {
        Random rand = new Random();//Objeto para generar el numero aleatorio
        public int clave;//Clave de el nodo
        public Nodo izquierdo;//Nodo hijo izquierdo
        public Nodo derecho;//Nodo hijo derecho
        public Color color;//Color actual por default de el nodo

        //Variables para caracteristicas graficas del nodo
        public int nodoX, nodoY, diameter;//Posicion y tamaño de el nodo
        public int alturaNodo;//Altura de el presente nodo
        private double compenzacion = 1.2;//Compenzacion de distancia en x de su padre para el dibujo del nodo
        private bool intermitencia = false;//Variable para activar o desactivar intermitencia al buscar
        private int timeIntermit = 0;//Contador para cambiio de color de intermitencia
        //Variables para ordenamiento del nodo

        //Instancia de insercion normal
        public Nodo()
        {
            this.clave = rand.Next(1000);
            color = Color.DarkRed;

        }
        //Instancia de insercion en un caso de que ya exista con anticipacion o este siendo cargado el nodo
        public Nodo(int clave)
        {
            this.clave = clave;
            color = Color.DarkRed;

        }
        //Instancia de insercion en caso de ser necesario el insertar un nodo con su color(Ordenamientos)
        public Nodo(int clave, Color color)
        {
            this.clave = clave;
            this.color = color;

        }

        //Insersion de nodos recursivamente
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
        //METODO BUSCAR TIPO BOOL PARA RETORNAR EL RESULTADO COMO UN POSITIVO O FALSO
        public bool buscar(int clave)
        {
            bool result = false;
            if (this.clave == clave)
            {
                //Retorna un positivo a la busqueda
                intermitencia = true;
                return true;

            }
            else
            {
                //Sigue buscando
                if (izquierdo != null)
                {
                    result = izquierdo.buscar(clave);
                }
                if (derecho != null && !result)
                {
                    result = derecho.buscar(clave);
                }

            }
            return result;
        }
        //Numero de nodos a la izquierda de el nodo(Usado para distancia en x de nodo derecho)
        public int AlturaIzquierda()
        {
            int contador = 1;
            if (izquierdo != null)
            {
                contador = contador + izquierdo.AlturaIzquierda();
            }
            return contador;
        }
        //Numero de nodos a la derecha de el nodo(Usado para distancia en x de nodo izquierdo)
        public int AlturaDerecha()
        {
            int contador = 1;
            if (derecho != null)
            {
                contador = contador + derecho.AlturaDerecha();
            }
            return contador;
        }
        //Funcion para asignar la posicion en x y Y de el presente nodo para su posterior dibujado
        public void XY_position(int _X_nodo, int _Y_nodo, int distanceY, int distanceX, int _diameter)
        {
            nodoX = _X_nodo;
            nodoY = _Y_nodo;
            diameter = _diameter;
            if (izquierdo != null)
            {
                izquierdo.XY_position(Convert.ToInt32(_X_nodo - (distanceX * izquierdo.AlturaDerecha()) * compenzacion), _Y_nodo + distanceY, distanceY, distanceX, _diameter);
            }
            if (derecho != null)
            {
                derecho.XY_position(Convert.ToInt32(_X_nodo + (distanceX * derecho.AlturaIzquierda()) * compenzacion), _Y_nodo + distanceY, distanceY, distanceX, _diameter);
            }

        }
        //Funcion para dibujar con spritebatch recursivamente
        public void Draw_nodo(Texture2D[] Textures, SpriteBatch _spriteBatch, SpriteFont Font, int distanceX, int distanceY)
        {
            //Dibujo de los enlaces entre los nodos
            if (derecho != null)
            {
                _spriteBatch.Begin();
                Rectangle rectLine = new Rectangle(nodoX + (diameter / 2), nodoY + (diameter / 2), Convert.ToInt32(distanceX * derecho.AlturaIzquierda() * compenzacion), distanceY);
                _spriteBatch.Draw(Textures[2], rectLine, Color.Gray);
                _spriteBatch.End();
                derecho.Draw_nodo(Textures, _spriteBatch, Font, distanceX, distanceY);
            }
            if (izquierdo != null)
            {
                _spriteBatch.Begin();
                Rectangle rectLine = new Rectangle(Convert.ToInt32(nodoX + (diameter / 2) - (distanceX * izquierdo.AlturaDerecha() * compenzacion)), nodoY + (diameter / 2), Convert.ToInt32(distanceX * izquierdo.AlturaDerecha() * compenzacion), distanceY);
                _spriteBatch.Draw(Textures[1], rectLine, Color.Gray);
                _spriteBatch.End();
                izquierdo.Draw_nodo(Textures, _spriteBatch, Font, distanceX, distanceY);
            }
            //Dibujo de el cuerpo de nodo y clave
            _spriteBatch.Begin();
            Rectangle rect_nodo = new Rectangle(nodoX, nodoY, diameter, diameter);
            //Ademas reviso si el nodo actual es resultado de una busqueda y lo dibujo de forma intermitente
            if (intermitencia)
            {
                timeIntermit++;
            }
            if (timeIntermit > 40)
            {
                _spriteBatch.Draw(Textures[0], rect_nodo, Color.Yellow);
            }
            else
            {
                _spriteBatch.Draw(Textures[0], rect_nodo, color);
            }
            if (timeIntermit > 80)
            {
                timeIntermit = 0;
            }
            Vector2 VectorClave = new Vector2(nodoX + (diameter / 4), nodoY + (diameter / 3));
            _spriteBatch.DrawString(Font, clave.ToString(), VectorClave, Color.Black);
            _spriteBatch.End();
        }
        //Revisamos la altura total de el arbol
        public int altura(int altura_actual, int MayorNivel)
        {
            altura_actual++;
            this.alturaNodo = altura_actual;
            if (MayorNivel < altura_actual)
            {
                MayorNivel = altura_actual;
            }
            if (izquierdo != null)
            {
                MayorNivel = izquierdo.altura(altura_actual, MayorNivel);
            }
            if (derecho != null)
            {
                MayorNivel = derecho.altura(altura_actual, MayorNivel);
            }
            return MayorNivel;
        }
        //Rotaciones segun propiedades de arboles Rojo/Negro
        public bool RotacionRojoNegro()
        {
            //En rotaciones de izquierda
            if (izquierdo != null)
            {
                if (izquierdo.color == Color.DarkRed)
                {
                    //Verificamos un posible Caso 1
                    bool dv = false;
                    if (derecho != null)
                    {
                        if (derecho.color == Color.DarkRed)
                        {
                            dv = true;
                            if (izquierdo.izquierdo != null)
                            {
                                if (izquierdo.izquierdo.color == Color.DarkRed)
                                {
                                    //Caso1 Izquierda-Izquierda
                                    color = Color.DarkRed;
                                    izquierdo.color = Color.Gray;
                                    derecho.color = Color.Gray;
                                    return true;
                                }
                            }
                            if (izquierdo.derecho != null)
                            {
                                if (izquierdo.derecho.color == Color.DarkRed)
                                {
                                    //Caso1 Izquierda-Derecha
                                    color = Color.DarkRed;
                                    izquierdo.color = Color.Gray;
                                    derecho.color = Color.Gray;
                                    return true;
                                }
                            }
                        }
                    }
                    if (dv != true)
                    {
                        //Verificamos un posible caso 2

                        if (izquierdo.derecho != null)
                        {
                            if (izquierdo.derecho.color == Color.DarkRed)
                            {
                                //Caso2 Izquierda(Se convierte a un caso 3 de izquierda)
                                Nodo temp = izquierdo.derecho.izquierdo;
                                izquierdo.derecho.izquierdo = izquierdo;
                                izquierdo = izquierdo.derecho;
                                izquierdo.izquierdo.derecho = temp;
                                return true;
                            }
                        }
                        //Verificamos un posible caso 3

                        if (izquierdo.izquierdo != null)
                        {
                            if (izquierdo.izquierdo.color == Color.DarkRed)
                            {
                                Nodo temp = new Nodo(clave, Color.DarkRed);
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
            //En rotaciones de derecha
            if (derecho != null)
            {
                if (derecho.color == Color.DarkRed)
                {
                    //Verificamos un posible Caso 1
                    bool dv = false;
                    if (izquierdo != null)
                    {
                        if (izquierdo.color == Color.DarkRed)
                        {
                            dv = true;
                            if (derecho.derecho != null)
                            {
                                if (derecho.derecho.color == Color.DarkRed)
                                {
                                    //Caso1 Izquierda-Izquierda
                                    color = Color.DarkRed;
                                    derecho.color = Color.Gray;
                                    izquierdo.color = Color.Gray;
                                    return true;
                                }
                            }
                            if (derecho.izquierdo != null)
                            {
                                if (derecho.izquierdo.color == Color.DarkRed)
                                {
                                    //Caso1 Izquierda-Derecha
                                    color = Color.DarkRed;
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
                            if (derecho.izquierdo.color == Color.DarkRed)
                            {
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
                            if (derecho.derecho.color == Color.DarkRed)
                            {
                                Nodo temp = new Nodo(clave, Color.DarkRed);
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
                //Para cumplir con la propiedad de el nodo raiz asignamos siempre el color del nodo si este es de altura 1
                color = Color.Gray;
            }
            //Practicamos recursividad y retornamos resultados
            bool d = false;
            if (derecho != null)
            {
                d = derecho.RotacionRojoNegro();
            }
            if (izquierdo != null)
            {
                if (izquierdo.RotacionRojoNegro() == true || d == true)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
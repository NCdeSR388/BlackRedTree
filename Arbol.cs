using System;

namespace BlackRedTree
{
    public class Arbol
    {

        public Nodo raiz;
        public static Nodo NIL;
        int nivelMax;

        public Arbol()
        {
            raiz = null;
            NIL = new Nodo(0, ArbolColor.negro, 0);
            nivelMax = 0;
        }

        // Insercion de datos
        public void Insertar(int dato)
        {
            if (raiz == null)
            {
                raiz = NIL;
                raiz.padre = NIL;
                raiz.izquierdo = NIL;
                raiz.derecho = NIL;
                raiz.color = ArbolColor.negro;
            }

            Nodo z = new Nodo(dato, ArbolColor.rojo, 0);
            Nodo y = NIL;
            Nodo x = raiz;

            while (x != NIL)
            {
                y = x;
                if (z.dato < x.dato)
                    x = x.izquierdo;
                else
                {
                    x = x.derecho;
                }
            }

            z.padre = y;
            if (y == NIL)
                raiz = z;
            else
            {
                if (z.dato < y.dato)
                    y.izquierdo = z;
                else
                {
                    y.derecho = z;
                }
            }

            z.izquierdo = NIL;
            z.derecho = NIL;
            z.color = ArbolColor.rojo;

            if (z.padre != NIL)
                z.nivel = z.padre.nivel + 1;

            ComprobarNivelMaximo(z.nivel);
            InsertarCorreccion(z);
        }

        public void IncTodoNodos(Nodo p)
        {
            if (p != NIL)
            {
                IncTodoNodos(p.izquierdo);
                p.nivel += 1;
                IncTodoNodos(p.derecho);
            }
        }

        public void DecTodoNodos(Nodo p)
        {
            if (p != NIL)
            {
                DecTodoNodos(p.izquierdo);
                p.nivel -= 1;
                DecTodoNodos(p.derecho);
            }
        }

        // Rotacion Izquierda
        public void IzquierdoRotacion(Nodo x)
        {
            Nodo y = x.derecho;
            x.derecho = y.izquierdo;

            if (y.izquierdo != NIL)
                y.izquierdo.padre = x;

            y.padre = x.padre;

            if (x.padre == NIL)
                raiz = y;
            else
            {
                if (x == x.padre.izquierdo)
                    x.padre.izquierdo = y;
                else x.padre.derecho = y;
            }

            y.izquierdo = x;
            x.padre = y;

            x.nivel += 1;
            y.nivel -= 1;
            DecTodoNodos(y.derecho);
            IncTodoNodos(x.izquierdo);
        }

        // Rotacion Derecha
        public void DerechaRotacion(Nodo y)
        {
            Nodo x = y.izquierdo;
            y.izquierdo = x.derecho;

            if (x.derecho != NIL)
                x.derecho.padre = y;

            x.padre = y.padre;

            if (y.padre == NIL)
                raiz = x;
            else
            {
                if (y == y.padre.derecho)
                    y.padre.derecho = x;
                else y.padre.izquierdo = x;
            }

            x.derecho = y;
            y.padre = x;

            x.nivel -= 1;
            y.nivel += 1;
            DecTodoNodos(x.izquierdo);
            IncTodoNodos(y.derecho);
        }


        void InsertarCorreccion(Nodo z)
        {
            Nodo y = NIL;
            while (z.padre.color == ArbolColor.rojo)
            {
                if (z.padre == z.padre.padre.izquierdo)
                {
                    y = z.padre.padre.derecho;
                    if (y.color == ArbolColor.rojo)
                    {
                        z.padre.color = ArbolColor.negro;
                        y.color = ArbolColor.negro;
                        z.padre.padre.color = ArbolColor.rojo;
                        z = z.padre.padre;
                    }
                    else
                    {
                        if (z == z.padre.derecho)
                        {
                            z = z.padre;
                            IzquierdoRotacion(z);
                        }
                        z.padre.color = ArbolColor.negro;
                        z.padre.padre.color = ArbolColor.rojo;
                        DerechaRotacion(z.padre.padre);
                    }
                }
                else
                {
                    y = z.padre.padre.izquierdo;
                    if (y.color == ArbolColor.rojo)
                    {
                        z.padre.color = ArbolColor.negro;
                        y.color = ArbolColor.negro;
                        z.padre.padre.color = ArbolColor.rojo;
                        z = z.padre.padre;
                    }
                    else
                    {
                        if (z == z.padre.izquierdo)
                        {
                            z = z.padre;
                            DerechaRotacion(z);
                        }
                        z.padre.color = ArbolColor.negro;
                        z.padre.padre.color = ArbolColor.rojo;
                        IzquierdoRotacion(z.padre.padre);
                    }
                }
            }

            raiz.color = ArbolColor.negro;
        }

        private void ComprobarNivelMaximo(int nivelNuevo)
        {
            nivelMax = (nivelNuevo > nivelMax) ? nivelNuevo : nivelMax;
        }

    }
}

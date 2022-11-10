using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;

namespace BlackRedTree
{
    public class Arbol
    {
        public Nodo raiz;
        int nivelMax;
        public string inorden = "Inorden: ";
        public string preorden = "Preorden: ";
        public string posorden = "Posorden: ";

        public Arbol()
        {
            raiz = null;
            nivelMax = 0;
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
            if(n != null)
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
    }
}

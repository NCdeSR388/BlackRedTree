using System;

namespace BlackRedTree
{
    public class Arbol
    {
        public Nodo raiz;
        int nivelMax;

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
    }
}

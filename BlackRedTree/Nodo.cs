using System;
using System.Windows.Forms;

namespace BlackRedTree
{

    public enum ArbolColor
    {
        rojo,
        negro
    };

    public class Nodo
    {
        Random rand = new Random();
        public int clave;
        public Nodo izquierdo;
        public Nodo derecho;
        public int color;
        public int nivel;

        public Nodo()
        {
            this.clave = rand.Next(1000);
            color = 0;

        }

        public bool insertar(Nodo new_nodo)
        {
            if (new_nodo.clave > clave)
            {
                if (derecho == null)
                {
                    derecho = new_nodo;
                    MessageBox.Show(new_nodo.clave.ToString() + "hijo derecho de " + clave.ToString());
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
                    MessageBox.Show(new_nodo.clave.ToString() + "hijo izquierdo de " + clave.ToString());
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


    }
}
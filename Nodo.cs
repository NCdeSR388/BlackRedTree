using System;

namespace BlackRedTree
{

    public enum ArbolColor
    {
        rojo,
        negro
    };

    public class Nodo
    {

        public Nodo izquierdo;
        public Nodo derecho;
        public Nodo padre;
        public int dato;
        public ArbolColor color;
        public int nivel;

        public Nodo(int dato, ArbolColor color, int nivel)
        {
            this.dato = dato;
            this.color = color;
            this.nivel = nivel;
        }

    }
}
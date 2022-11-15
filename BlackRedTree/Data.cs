using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace BlackRedTree
{
    //Clase orientada a procesos SQLite
    public class Data
    {
        static string url = Application.StartupPath + "database.db";//Nombre de la bd
        SqliteConnection conection;//Objeto Coneccion sqlite
        public Data()
        {
            //Instanciacion de la clase
            createDB();
            conection = new SqliteConnection("Data Source=" + url);
        }
        private void createDB()
        {
            //Se crea la bd en caso de no haber sido creada
            if (!File.Exists(url))
            {
                var file = File.Create(url);
                file.Close();
            }
        }
        private void Conectar()
        {
            //Apretura de coneccion para querys
            if (conection.State == ConnectionState.Closed)
            {
                conection.Open();
            }
        }
        //Funcion para crear tabla principal si no esta creada
        public void createTable()
        {
            Conectar();
            string Query = "CREATE TABLE IF NOT EXISTS Nodo" +
                "(idNodo INTEGER," +
                "clave int NOT NULL," +
                "PRIMARY KEY(idNodo));";
            SqliteCommand comando = new SqliteCommand(Query, conection);
            comando.ExecuteNonQuery();
            conection.Close();
        }
        //Funcion para limpiar tabla principal
        public void dropTable()
        {
            Conectar();
            string Query = "DROP TABLE IF EXISTS Nodo;" +
                "CREATE TABLE Nodo" +
                "(idNodo INTEGER," +
                "clave int NOT NULL," +
                "PRIMARY KEY(idNodo));";
            //Creamos el comando con la coneccion y el string de la consulta
            SqliteCommand comando = new SqliteCommand(Query, conection);
            comando.ExecuteNonQuery();//Ejecutamos la consulta
            conection.Close();
        }
        //Borado de tabla temporal
        public void dropTableTemp()
        {
            Conectar();
            string Query = "DROP TABLE IF EXISTS NodoTemp;" +
                "CREATE TABLE NodoTemp" +
                "(idNodo INTEGER," +
                "clave int NOT NULL," +
                "PRIMARY KEY(idNodo));";
            SqliteCommand comando = new SqliteCommand(Query, conection);
            comando.ExecuteNonQuery();
            conection.Close();
        }
        //Guardado de clave de nodo en tabla principal
        public void GuardarNodo(int clave)
        {
            string Query = "INSERT INTO Nodo(clave) VALUES (@clave);";
            Conectar();
            SqliteCommand comando = new SqliteCommand(Query, conection);
            comando.Parameters.AddWithValue("@clave", clave);
            comando.ExecuteNonQuery();
            conection.Close();

        }
        //Guardado de nodo en tabla de arbol temporal
        public void GuardarNodoTemp(int clave)
        {
            Conectar();
            string Query = "INSERT INTO NodoTemp(clave) VALUES (@clave);";
            SqliteCommand comando = new SqliteCommand(Query, conection);
            comando.Parameters.AddWithValue("@clave", clave);
            comando.ExecuteNonQuery();
            conection.Close();

        }
        //Funcion para cargar nodos de ultimo arbol guardado
        public void CargarNodos(ref Arbol arbol)//Se obtiene un objeto arbol por referencia
        {
            Conectar();
            string Query = "SELECT clave FROM Nodo";
            SqliteCommand comando = new SqliteCommand(Query, conection);
            SqliteDataReader Nodos = comando.ExecuteReader();
            if (Nodos.HasRows)
            {
                while (Nodos.Read())
                {
                    //Insersion continua en el objeto arbol obtenido por referencia
                    arbol.Insertar(Convert.ToInt32(Nodos.GetValue(0).ToString()));
                }
            }
            else
            {
                MessageBox.Show("Sin Arboles guardados");
            }
        }
        //Cargamos los nodos de la tabla temporal y los usamos para retornar una lista de claves
        public int[] CargarNodosTemp()
        {
            Conectar();
            string Query = "SELECT clave FROM NodoTemp";
            SqliteCommand comando = new SqliteCommand(Query, conection);
            SqliteDataReader Nodos = comando.ExecuteReader();
            int[] ListaNodos = null;
            int rows = 0;
            if (Nodos.HasRows)
            {
                while (Nodos.Read())
                {
                    rows++;
                }
                ListaNodos = new int[rows];
                rows = 0;
                Nodos.Close();
                Nodos = comando.ExecuteReader();
                while (Nodos.Read())
                {
                    ListaNodos[rows] = (Convert.ToInt32(Nodos.GetValue(0).ToString()));
                    rows++;
                }
            }
            else
            {
                MessageBox.Show("El arbol esta vacio o ya ha sido guardado anteriormente");
            }
            conection.Close();
            return ListaNodos;
        }
    }
}

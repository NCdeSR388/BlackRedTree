using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace BlackRedTree
{
    public class Data
    {
        static string url = Application.StartupPath + "database.db";
        SqliteConnection conection;
        public Data()
        {
            createDB();
            conection = new SqliteConnection("Data Source=" + url);
        }
        private void createDB()
        {
            if (!File.Exists(url))
            {
                var file = File.Create(url);
                file.Close();
            }
        }
        private void Conectar()
        {
            if (conection.State == ConnectionState.Closed)
            {
                conection.Open();
            }
        }
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
        public void dropTable()
        {
            Conectar();
            string Query = "DROP TABLE IF EXISTS Nodo;" +
                "CREATE TABLE Nodo" +
                "(idNodo INTEGER," +
                "clave int NOT NULL," +
                "PRIMARY KEY(idNodo));";
            SqliteCommand comando = new SqliteCommand(Query, conection);
            comando.ExecuteNonQuery();
            conection.Close();
        }
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
        public void GuardarNodo(int clave)
        {
            string Query = "INSERT INTO Nodo(clave) VALUES (@clave);";
            Conectar();
            SqliteCommand comando = new SqliteCommand(Query, conection);
            comando.Parameters.AddWithValue("@clave", clave);
            comando.ExecuteNonQuery();
            conection.Close();

        }
        public void GuardarNodoTemp(int clave)
        {
            Conectar();
            string Query = "INSERT INTO NodoTemp(clave) VALUES (@clave);";
            SqliteCommand comando = new SqliteCommand(Query, conection);
            comando.Parameters.AddWithValue("@clave", clave);
            comando.ExecuteNonQuery();
            conection.Close();

        }
        public void CargarNodos(ref Arbol arbol)
        {
            Conectar();
            string Query = "SELECT clave FROM Nodo";
            SqliteCommand comando = new SqliteCommand(Query, conection);
            SqliteDataReader Nodos = comando.ExecuteReader();
            if (Nodos.HasRows)
            {
                while (Nodos.Read())
                {
                    arbol.Insertar(Convert.ToInt32(Nodos.GetValue(0).ToString()));
                }
            }
            else
            {
                MessageBox.Show("Sin Arboles guardados");
            }
        }
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace Polla.DAL
{
    public class DAL_Base
    {
        //string cn = "Server=dwqtcdb01\\playarea;Database=RoyTEST;Integrated Security=True";
        string cn ;

        public string queryString { get; set; }
        public SqlConnection connection;
        public SqlCommand command;
        public SqlDataReader reader;

        public void SetConnection()
        {
            cn = System.Configuration.ConfigurationManager.ConnectionStrings["PollaConnection"].ConnectionString;
            connection = new SqlConnection(cn);
        }

        public string ExecuteScalar()
        {
            string mensaje = "";
            SetConnection();
            using (connection)
            {
                command = new SqlCommand(queryString, connection);
                connection.Open();
                mensaje = command.ExecuteScalar().ToString();
                connection.Close();
            }
            return mensaje;
        }

        public string ExecuteNonQuery()
        {
            string mensaje = "";
            SetConnection();
            using (connection)
            {
                command = new SqlCommand(queryString, connection);
                connection.Open();
                mensaje = command.ExecuteNonQuery().ToString();
                connection.Close();
            }
            return mensaje;

        }

        public SqlDataReader ExecuteReader()
        {
            SetConnection();
            command = new SqlCommand(queryString, connection);
            connection.Open();
            reader = command.ExecuteReader();
            return reader;
        }

        public void DisposeObjects ()
        {
            if (command != null) command.Dispose();
            if (reader != null) reader.Close();
            if (connection != null && connection.State != ConnectionState.Closed) connection.Close();
        }

    }
}

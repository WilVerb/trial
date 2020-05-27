using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;



namespace WebApplicationOnOracle2
{
    public class DataAccessLayer : IDisposable
    {
        #region Private Members
        private OracleConnection _connection = null;
        
        #endregion
        
        #region constructors
        public DataAccessLayer()
        {
            openConnection("Oracle Connection");
        }
        public DataAccessLayer(string connectionString)
        {
            openConnection(connectionString);
        }

        private void openConnection(string connectionString)
        {
            try
            {
                ConnectionStringSettings connObj = ConfigurationManager.ConnectionStrings[connectionString];
                string connectString = connObj.ConnectionString;
                //_connection = new OracleConnection(connectString);
                _connection = new OracleConnection();
                _connection.ConnectionString = connectString;
                _connection.Open();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString(), e);
            }
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
        #endregion

        #region Test
        public string SelectScalarValue()
        {
            using (OracleCommand cmd = new OracleCommand("select DptNAme from system.tblDepartments where id=1;"))
            {
                Object res = cmd.ExecuteScalar();
                return res.ToString();
            }

        }
        #endregion
    }
}
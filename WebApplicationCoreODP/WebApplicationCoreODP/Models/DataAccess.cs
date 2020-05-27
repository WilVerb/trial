using Oracle.ManagedDataAccess.Client;

namespace WebApplicationCoreODP.Models
{
    public class DataAccess
    {
        private OracleConnection _con;

        public DataAccess()
        {
            string conString = "User Id=c##toto;Password=toto;"
                + "Data Source=localhost:1521/xe;";
            _con = new OracleConnection
            {
                ConnectionString = conString
            };
            _con.Open();
        }
        public string Connect()
        {
            /*
            string conString = "User Id=c##toto;Password=toto;"
                + "Data Source=localhost:1521/xe;";
            OracleConnection con = new OracleConnection();
            con.ConnectionString = conString;
            con.Open();
            */
            OracleCommand cmd = _con.CreateCommand();
            cmd.CommandText = "select DptNAme from system.tblDepartments where id=2";
            OracleDataReader reader = cmd.ExecuteReader();
            string dptName="";
            while (reader.Read())
            {
                dptName = reader.GetString(0);
            }
            return dptName;
        }
        public string Connect(int dptId )
        {
            OracleCommand cmd = _con.CreateCommand();
            cmd.CommandText = string.Format("select DptNAme from system.tblDepartments where id={0}",dptId);
            OracleDataReader reader = cmd.ExecuteReader();
            string dptName = "";
            while (reader.Read())
            {
                dptName = reader.GetString(0);
            }
            return dptName;
        }
        public void Fetch()
        {
            string methodName = "p_claims_get.get_hcfa_inquiry";
            OracleCommand cmd = new OracleCommand
            {
                CommandType = System.Data.CommandType.StoredProcedure,
                CommandText = methodName
            };
            cmd.Parameters.Add("poReturnCode", OracleDbType.Int16,System.Data.ParameterDirection.ReturnValue);

      
        }

        public int FetchEmployees(int deptId)
        {
            string methodName = "system.p_perDept";
            OracleCommand cmd = _con.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = methodName;
            cmd.BindByName = true;
            //cmd.ImplicitRefCursors
            cmd.Parameters.Add("DeptId", OracleDbType.Int16, deptId, System.Data.ParameterDirection.Input);
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            System.Data.DataSet ds = new System.Data.DataSet();
            adapter.Fill(ds);
            int nbEmps = ds.Tables[0].Rows.Count;
            int nbDepts = ds.Tables[1].Rows.Count;
            System.Data.DataTable dt = ds.Tables[0];
            System.Data.DataRow rwItem = dt.Rows[0];
            string empName;
            empName = rwItem["empname"].ToString();

            
            return nbEmps;
            //using (OracleDataReader dr = cmd.ExecuteReader())
            //{
            //    // do some work here
            //}
        }

        public int FetchDataSets()
        {
            OracleCommand cmd = _con.CreateCommand();
            cmd.CommandText = @"SYSTEM.p";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.BindByName = true;
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            System.Data.DataSet ds = new System.Data.DataSet();
            adapter.Fill(ds);
            int nbEmps = ds.Tables[0].Rows.Count;
            int nbDepts=   ds.Tables[1].Rows.Count;
            return nbDepts;
        }

        public void CallFunction(int empId)
        {
            OracleCommand cmd = _con.CreateCommand();
            cmd.CommandText = "SYSTEM.GetEmp";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.BindByName = false;
            OracleParameter empid = cmd.Parameters.Add("empid",
              OracleDbType.Int32, System.Data.ParameterDirection.Input);
            empid.Value = empId;

            // Populate the DataSet
            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            System.Data.DataSet ds = new System.Data.DataSet();
            adapter.Fill(ds);
            int rowcount= ds.Tables[0].Rows.Count;
        }
    }
}

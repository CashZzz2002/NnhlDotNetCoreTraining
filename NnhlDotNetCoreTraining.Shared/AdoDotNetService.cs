using System.Data;
using System.Data.SqlClient;

namespace NnhlDotNetCoreTraining.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public DataTable Query(string query,params SqlParameterModel[] sqlParameters)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            SqlCommand cmd=new SqlCommand(query,con);

            if (sqlParameters is not null) {
                foreach (var sqlParameter in sqlParameters)
                {
                    cmd.Parameters.Add(sqlParameter);
                }
            }
            

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);


            con.Close();

            return dt;
        }
        public int Execute(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);

            if (sqlParameters is not null)
            {
                foreach (var sqlParameter in sqlParameters)
                {
                    cmd.Parameters.Add(sqlParameter);
                }
            }


            var result =cmd.ExecuteNonQuery();

            con.Close();

            return result;
        }
    }
    public class SqlParameterModel
    {
        public string Name { get; set; }
        public Object Value { get; set; }

        public SqlParameterModel() { }
        public SqlParameterModel(string name,object value)
        {
            Name= name;
            Value= value;
        }
    }
}
//SqlCommand cmd = new SqlCommand(query, con);
//DataTable dt = new DataTable();
//SqlDataAdapter adapter = new SqlDataAdapter(cmd);
//adapter.Fill(dt);

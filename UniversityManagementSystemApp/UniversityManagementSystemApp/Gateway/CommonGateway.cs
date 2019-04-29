using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UniversityManagementSystemApp.Gateway
{
    public class CommonGateway
    {
        private string connectionString =
           WebConfigurationManager.ConnectionStrings["UniversityConnectionString"].ConnectionString;

        public SqlConnection Connection { set; get; }
        public SqlCommand Command { get; set; }

        public CommonGateway()
        {
            Connection = new SqlConnection(connectionString);
            Command = new SqlCommand();
            Command.Connection = Connection;
        }
    }
}
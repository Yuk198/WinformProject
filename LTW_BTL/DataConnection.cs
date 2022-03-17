using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTW_BTL
{
    class DataConnection
    {
        string conStr;
        public DataConnection()
        {
            conStr = "Data Source = DESKTOP-2MCS0RA ; Initial Catalog = QLCUAHANG ; Integrated Security = true";
        }
        public SqlConnection getConnect()
        {
            return new SqlConnection(conStr);
        }
    }
}

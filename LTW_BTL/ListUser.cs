using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTW_BTL
{
   public class ListUser
    {
        private List<user> listaccUser;
        private static ListUser intance;
        public List<user> ListaccUser { get => listaccUser; set => listaccUser = value; }
        public static ListUser Intance 
        {
            get
            {
                if (intance == null)
                    intance = new ListUser();
                return intance;
            }
            set => intance = value; 
        }

        private ListUser()
        {
            listaccUser = new List<user>();
            listaccUser.Add(new user("admin", "1234"));
            //listaccUser.Add(new user("NV1", "1234"));
        }

        
    }
}

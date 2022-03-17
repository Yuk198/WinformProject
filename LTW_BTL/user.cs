using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTW_BTL
{
    public class user
    {
        private string userName; //2 trường User, password
        private string passWord;
        public string UserName { get => userName; set => userName = value; }
        public string PassWord { get => passWord; set => passWord = value; }

        public user(string userName , string passWord)
        {
            this.UserName = userName;
            this.PassWord = passWord;
        }
    }
}

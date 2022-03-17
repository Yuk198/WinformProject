using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTW_BTL
{
    class ThucAnBLL
    {
        ThucAnDAL dalThucAn;
        public ThucAnBLL()
        {
            dalThucAn = new ThucAnDAL();
        }
        public DataTable getALLThucAn()
        {
            return dalThucAn.getALLThucAn();
        }
        public bool InsertThucAn(tblThucAn thucan)
        {
            return dalThucAn.InsertThucAn(thucan);
        }
        public bool UpdateThucAn(tblThucAn thucan)
        {
            return dalThucAn.UpdateThucAn(thucan);
        }
        public bool DeleteThucAn(tblThucAn thucan)
        {
            return dalThucAn.DeleteThucAn(thucan);
        }
    }
}

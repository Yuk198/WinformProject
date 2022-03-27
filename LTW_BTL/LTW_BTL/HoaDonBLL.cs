using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTW_BTL
{
    internal class HoaDonBLL
    {

        HoaDonDAL dalHoaDon;
        public HoaDonBLL()
        {
            dalHoaDon = new HoaDonDAL();
        }
        public DataTable getALLHoaDon()
        {
            return dalHoaDon.getALLHoaDon();
        }
        public bool InsertHoaDon(tblHoaDon hoadon)
        {
            return dalHoaDon.InsertHoaDon(hoadon);
        }
        public bool UpdateHoaDon(tblHoaDon hoadon)
        {
            return dalHoaDon.UpdateHoaDon(hoadon);
        }
        public bool DeleteThucAn(tblHoaDon hoadon)
        {
            return dalHoaDon.DeleteHoaDon(hoadon);
        }

    }
}

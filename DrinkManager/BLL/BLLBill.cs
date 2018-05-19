using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.BLL
{
    class BLLBill
    {
        private static BLLBill instance;
        public static BLLBill Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BLLBill();
                }
                return instance;
            }
        }
        public DataTable getBillTable()
        {
            using (var context = new QLTSEntities())
            {
                DataTable table = new DataTable();
                table.Columns.Add("bid");
                table.Columns.Add("customer");
                table.Columns.Add("price");
                table.Columns.Add("staff");
                table.Columns.Add("purchasedate");
                table.Columns.Add("tid");
                (from x in context.BILLS
                 select x).ToList().ForEach(x => table.Rows.Add(x.bid, x.CUSTOMER.name, x.price, x.EMPLOYEE.name, x.purchasedate, x.tid));
                return table;
            }
        }
        public List<DTO.DetailBill> getListItemInBill(int bid)
        {
            using (var context = new QLTSEntities())
            {
                List<DTO.DetailBill> lstDB = new List<DTO.DetailBill>();
                context.DETAIL_BILLS.Where(x=>x.bid == bid).ToList().ForEach(x => lstDB.Add(new DTO.DetailBill(x.ITEM.nameitem, Convert.ToInt16(x.bid),x.amount,x.ITEM.price)));
                return lstDB;
            }
        }
        public DTO.Bill getBill(int bid)
        {
            using (var context = new QLTSEntities())
            {
                var b = context.BILLS.Single(x => x.bid == bid);
                DTO.Bill bill = new DTO.Bill(b.bid,b.CUSTOMER.name,Convert.ToInt64(b.price),b.EMPLOYEE.name, Convert.ToDateTime(b.purchasedate), Convert.ToInt16(b.tid));
                return bill;
            }
        }
        public DataTable findData(String cus, String fromPrice, String toPrice, String staff, String fromDate, String toDate, String table)
        {
            using (var context = new QLTSEntities())
            {
                DataTable tb = new DataTable();
                tb.Columns.Add("bid");
                tb.Columns.Add("customer");
                tb.Columns.Add("price");
                tb.Columns.Add("staff");
                tb.Columns.Add("purchasedate");
                tb.Columns.Add("tid");

                var a = context.BILLS.ToList();
               
                if (cus != "")
                    a = a.Where(x => x.CUSTOMER.name.Equals(cus)).Select(x=>x).ToList();
                if (fromPrice != "")
                    a = a.Where(x => x.price >= Convert.ToInt64(fromPrice)).Select(x => x).ToList();
                if (toPrice != "")
                    a = a.Where(x => x.price <= Convert.ToInt64(toPrice)).Select(x => x).ToList();
                if (staff != "")
                    a = a.Where(x => x.EMPLOYEE.name.Equals(staff)).Select(x => x).ToList();
                if (fromDate != "")
                    a = a.Where(x => x.purchasedate >= Convert.ToDateTime(fromDate)).Select(x => x).ToList();
                if (toDate != "")
                    a = a.Where(x => x.purchasedate <= Convert.ToDateTime(fromDate)).Select(x => x).ToList();
                if (table != "")
                    a = a.Where(x => x.tid == Convert.ToInt16(table)).Select(x => x).ToList();

                a.ForEach(x => tb.Rows.Add(x.bid, x.CUSTOMER.name, x.price, x.EMPLOYEE.name, x.purchasedate, x.tid));


                return tb;
            }
           
        }
    }
}

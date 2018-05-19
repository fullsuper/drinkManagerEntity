using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.BLL
{
    class BLLStatistic
    {
        private static BLLStatistic instance;
        public static BLLStatistic Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BLLStatistic();
                }
                return instance;
            }
        }

        public DataTable getFriendlyCustomerTable()
        {
            using (var context = new QLTSEntities())
            {
                DataTable table = new DataTable();
                table.Columns.Add("cid");
                table.Columns.Add("name");
                table.Columns.Add("time");
                table.Columns.Add("sumprice");
                //context.BILLS.GroupBy(x=>x.cid).Select(x=> new { cid = x.Key.Value, name = )
                context.CUSTOMERS
                    .GroupBy(x => new { x.cid, x.name })
                    .Select(x => new { cid = x.Key.cid, name = x.Key.name, time = x.Sum(y => y.BILLS.Count), sumprice = (context.BILLS.Where(y => y.cid == x.Key.cid).Sum(y => y.price)) })
                    .ToList()
                    .ForEach(x => table.Rows.Add(x.cid, x.name, x.time, x.sumprice));
                return table;
            }
           // return DAL.DALConnect.Instance.ExecuteQueryDataSet("select c.cid,c.name, count(*) time,sum(b.price) sumprice  from customers c join bills b on c.cid = b.cid group by c.cid,c.name", CommandType.Text).Tables[0];
        }
        public DataTable getFriendlyCustomerByTime(int month, int year)
        {
                using (var context = new QLTSEntities())
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("cid");
                    table.Columns.Add("name");
                    table.Columns.Add("time");
                    table.Columns.Add("sumprice");
                    context.BILLS
                        .Where(x => x.purchasedate.Value.Month == month && x.purchasedate.Value.Year == year)
                        .GroupBy(x => new { x.cid, x.CUSTOMER.name })
                        .Select(x => new { cid = x.Key.cid, name = x.Key.name, time = x.Count(), sumprice = x.Sum(y => y.price) })
                        .ToList()
                        .ForEach(x => table.Rows.Add(x.cid, x.name, x.time, x.sumprice));
                    //context.CUSTOMERS.Where(x=> x.cid == context.BILLS.Where(x=>x.purchasedate.Value.Month == month && x.purchasedate.Value.Year == year).).GroupBy(x => new { x.cid, x.name }).Select(x => new { cid = x.Key.cid, name = x.Key.name, time = x.Sum(y => y.BILLS.Count), sumprice = (context.BILLS.Where(y => y.cid == x.Key.cid).Sum(y => y.price)) })
                    //.ToList().ForEach(x => table.Rows.Add(x.cid, x.name, x.time, x.sumprice));
                    return table;
                }
                   }
        public DataTable getImportGood()
        {
            using (var context = new QLTSEntities())
            {
                DataTable table = new DataTable();
                table.Columns.Add("nameitem");
                table.Columns.Add("amount");
                table.Columns.Add("censor");
                table.Columns.Add("tradingdate");
                table.Columns.Add("rate");
                context.DETAIL_SHIPMENTS
                    .Select(x => new { x.ITEM.nameitem,x.amount,x.SHIPMENT.EMPLOYEE.name,x.SHIPMENT.tradingdate,x.SHIPMENT.rate})
                    .ToList()
                    .ForEach(x => table.Rows.Add(x.nameitem, x.amount, x.name, x.tradingdate,x.rate));
                return table;
            }
        }
        public DataTable getImportGoodByTime(int month, int year)
        {
            using (var context = new QLTSEntities())
            {
                DataTable table = new DataTable();
                table.Columns.Add("nameitem");
                table.Columns.Add("amount");
                table.Columns.Add("censor");
                table.Columns.Add("tradingdate");
                table.Columns.Add("rate");
                context.DETAIL_SHIPMENTS
                    .Where(x=>x.SHIPMENT.tradingdate.Value.Month == month && x.SHIPMENT.tradingdate.Value.Year == year)
                    .Select(x => new { x.ITEM.nameitem, x.amount, x.SHIPMENT.EMPLOYEE.name, x.SHIPMENT.tradingdate, x.SHIPMENT.rate })
                    .ToList()
                    .ForEach(x => table.Rows.Add(x.nameitem, x.amount, x.name, x.tradingdate, x.rate));
                return table;
            }
        }
        public long monthlyIncome(int month, int year)
        {
            using (var context = new QLTSEntities())
            {
                return Convert.ToInt64(context.BILLS.Where(x => x.purchasedate.Value.Month == month && x.purchasedate.Value.Year == year).Sum(x => x.price));
            }

        }
    }
}

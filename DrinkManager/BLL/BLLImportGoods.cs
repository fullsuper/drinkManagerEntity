using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.BLL
{
    class BLLImportGoods
    {
        private static BLLImportGoods instance;

        public static BLLImportGoods Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BLLImportGoods();
                }
                return instance;
            }
        }
        public DataTable getItemTable()
        {
            using (var context = new QLTSEntities())
            {
                DataTable table = new DataTable();
                table.Columns.Add("iid");
                table.Columns.Add("nameitem");
                table.Columns.Add("amount");
                context.ITEMS.Select(x => new { x.iid, x.nameitem,x.amount}).ToList().ForEach(x => table.Rows.Add(x.iid, x.nameitem,x.amount));
                return table;
            }
        }
        public List<String> getListSupplier()
        {
            using (var context = new QLTSEntities())
            {
                List<String> lstSup = new List<String>();
                context.SUPPLIERS.ToList().ForEach(x => lstSup.Add(x.name));
                return lstSup;
            }
            //
            //DataRowCollection drc = DAL.DALConnect.Instance.ExecuteQueryDataSet("select name from suppliers", CommandType.Text).Tables[0].Rows;
            //foreach (DataRow dr in drc)
            //{
            //    lstSup.Add(dr["name"].ToString());
            //}
            //
        }
        public List<String> getListCensor()
        {
            using (var context = new QLTSEntities())
            {
                List<String> lstCensor = new List<String>();
                context.EMPLOYEES.Where(x=>x.TYPE_SERVICES.typeservice.Equals("Kiểm Soát")).ToList().ForEach(x => lstCensor.Add(x.name));
                return lstCensor;
            }
           
            //DataRowCollection drc = DAL.DALConnect.Instance.ExecuteQueryDataSet("select name from employees where tserviceid = 2", CommandType.Text).Tables[0].Rows;
            //foreach (DataRow dr in drc)
            //{
            //    lstCensor.Add(dr["name"].ToString());
            //}
            //
        }
        public int getIDSupplier(string name)
        {
            using (var context = new QLTSEntities())
            {
                return context.SUPPLIERS
                    .Where(x => x.name.Equals(name))
                    .FirstOrDefault().sid;
            }
            //DataRow dr = DAL.DALConnect.Instance.ExecuteQueryDataSet("select sid from suppliers where name = N'" + name + "'", CommandType.Text).Tables[0].Rows[0];
            //return Convert.ToInt16(dr["sid"]);
        }
        public int getIDCensor(string name)
        {
            using (var context = new QLTSEntities())
            {
                return context.EMPLOYEES
                    .Single(x => x.name.Equals(name))
                    .eid;
            }
            //DataRow dr = DAL.DALConnect.Instance.ExecuteQueryDataSet("select eid from employees where name = N'" + name + "'", CommandType.Text).Tables[0].Rows[0];
            //return Convert.ToInt16(dr["eid"]);
        }
        public void insertShipment(DTO.Shipment sm)
        {
            using (var context = new QLTSEntities())
            {
                int eid = getIDCensor(sm.Censor);
                int sid = getIDSupplier(sm.Supplier);
                context.SHIPMENTS
                    .Add(new SHIPMENT{ censor= eid, rate =sm.Rate,sid =sid, tradingdate= sm.Tradingdate});
                context.SaveChanges();
            }
            
            //DAL.DALConnect.Instance.MyExecuteNonQuery("insert into shipments(tradingdate,censor,sid,rate) values ('" + sm.Tradingdate + "'," + eid + "," + sid + ",N'" + sm.Rate + "')", CommandType.Text);
        }
        public int getIDLastShipment()
        {
            using (var context = new QLTSEntities())
            {
                return context.SHIPMENTS.Max(x => x.shid);
            }
            //DataRow dr = DAL.DALConnect.Instance.ExecuteQueryDataSet("select max(shid) as shid from shipments ", CommandType.Text).Tables[0].Rows[0];
            //return Convert.ToInt16(dr["shid"]);
        }
        public void insertDetailShipment(DTO.DetailShipment ds)
        {
            using (var context = new QLTSEntities())
            {
                context.DETAIL_SHIPMENTS
                    .Add(new DETAIL_SHIPMENTS { iid=ds.Iid, shid=ds.Shipment,amount = ds.Amount});
                context.SaveChanges();
            }
            //DAL.DALConnect.Instance.MyExecuteNonQuery("insert into detail_shipments(shid,iid,amount) values (" + ds.Shipment + "," + ds.Iid + "," + ds.Amount + ")", CommandType.Text);
        }
        public void updateItem(DTO.DetailShipment ds)
        {
            using (var context = new QLTSEntities())
            {
                context.ITEMS.Single(x => x.iid == ds.Iid).amount += ds.Amount;
                context.SaveChanges();
            }
           // DAL.DALConnect.Instance.MyExecuteNonQuery("update items set amount = amount + " + ds.Amount + " where iid = " + ds.Iid, CommandType.Text);
        }
    }
}

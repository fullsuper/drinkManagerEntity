using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.BLL
{
    class BLLPurchase
    {

        private static BLLPurchase instance;
        private bool done =false;
        public static BLLPurchase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BLLPurchase();
                }
                return instance;
            }
        }

        public bool Done { get => done; set => done = value; }

        public List<DTO.Table> getTableID()
        {
            using (var context = new QLTSEntities())
            {
                List<DTO.Table> lstTableID = new List<DTO.Table>();
                context.TABLES.ToList().ForEach(x => lstTableID.Add(new DTO.Table(x.tid, Convert.ToInt16(x.floor), x.position)));
                return lstTableID;
            }
        }
        public List<DTO.Employee> getStaff()
        {
            using (var context = new QLTSEntities())
            {
                List<DTO.Employee> lstStaff = new List<DTO.Employee>();
                context.EMPLOYEES.Where(x=>x.tserviceid==1).ToList().ForEach(x => lstStaff.Add(new DTO.Employee(x.eid,x.name,Convert.ToDateTime(x.hiredate),x.TYPE_SERVICES.typeservice, Convert.ToInt64(x.phone), Convert.ToInt64(x.salary))));
                return lstStaff;
            }
        }
        public List<DTO.Item> getItemByType(String typeitem)
        {
            using (var context = new QLTSEntities())
            {
                List<DTO.Item> lstItem = new List<DTO.Item>();
                context.ITEMS.Where(x => x.TYPE_ITEMS.typeitem == typeitem).ToList().ForEach(x => lstItem.Add(new DTO.Item(Convert.ToInt16(x.iid),x.TYPE_ITEMS.typeitem,x.nameitem,Convert.ToInt64(x.price),x.amount)));
                return lstItem;
            }
        }
        public List<DTO.TypeItem> getTypeItem()
        {
            using (var context = new QLTSEntities())
            {
                List<DTO.TypeItem> lstTypeItem = new List<DTO.TypeItem>();
                context.TYPE_ITEMS.ToList().ForEach(x => lstTypeItem.Add(new DTO.TypeItem(x.titemid,x.typeitem)));
                return lstTypeItem;
            }
        }
        public int getCustomerID(String name)
        {
            using (var context = new QLTSEntities())
            {
                return context.CUSTOMERS.Single(x => x.name == name).cid;
            }
          }
        private void insertCustomer(String name)
        {
            using (var context = new QLTSEntities())
            {
                context.CUSTOMERS.Add(new CUSTOMER() { name = name });
                context.SaveChanges();
            }
        }
        private int getStaffID(String name)
        {
            using (var context = new QLTSEntities())
            {
                return context.EMPLOYEES.Single(x => x.name == name).eid;
            }
        }
        private int getItemID(String name)
        {
            using (var context = new QLTSEntities())
            {
                return context.ITEMS.Single(x => x.nameitem == name).iid;
            }
        }
        public int getLastBillID()
        {
            using (var context = new QLTSEntities())
            {
                return context.BILLS.Max(x => x.bid);
            }
        }
        public void insertBill(DTO.Bill b)
        {
            using (var context = new QLTSEntities())
            {
                int cid = getCustomerID(b.CustomerName);
                int eid = getStaffID(b.Cashier);
                context.BILLS.Add(new BILL() { cashier = cid,price = b.Price, cid = cid, tid =b.Table,purchasedate = b.Purchasedate});
            }            
        }
        public void insertDetailBill(String item, int bid, int amount)
        {
            using (var context = new QLTSEntities())
            {
                int iid = getItemID(item);
                context.DETAIL_BILLS.Add(new DETAIL_BILLS() { bid = bid,iid = iid, amount = amount });
            }
        }
        public void updateItems(DTO.DetailBill db)
        {
            using (var context = new QLTSEntities())
            {
                context.ITEMS.Where(x => x.nameitem == db.Item).ToList().ForEach(x => x.amount = x.amount - db.Amount);
                context.SaveChanges();
            }
        }

    }
}

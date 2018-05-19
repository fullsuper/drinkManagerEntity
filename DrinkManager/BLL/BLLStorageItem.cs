using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinkManager.DAL;

namespace DrinkManager.BLL
{

    class BLLStorageItem
    {
        private static BLLStorageItem instance;

        public static BLLStorageItem Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BLLStorageItem();
                }
                return instance;
            }
        }

        public DataTable StorageItemTable()
        {
            using (var context = new QLTSEntities())
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("iid");
                dt.Columns.Add("typeitem");
                dt.Columns.Add("nameitem");
                dt.Columns.Add("price");
                dt.Columns.Add("amount");
                context.ITEMS.ToList().ForEach(x => dt.Rows.Add(x.iid,x.TYPE_ITEMS.typeitem,x.nameitem,x.price,x.amount));
                return dt;
            }
        }
        public void InsertNewItem(DTO.Item item)
        {
            using (var context = new QLTSEntities())
            {
                int tid = context.TYPE_ITEMS.Single(x => x.typeitem == item.Typeitem).titemid;
                context.ITEMS.Add(new ITEM() { nameitem = item.Nameitem, amount = item.Amount, price= item.Price, titemid = tid });
                context.SaveChanges();
            }
        }
        public void UpdateItem(DTO.Item item)
        {
            using (var context = new QLTSEntities())
            {
                int tid = context.TYPE_ITEMS.Single(x => x.typeitem == item.Typeitem).titemid;
                var e = context.ITEMS.Single(x => x.iid == item.Id);
                e.nameitem = item.Nameitem;
                e.price = item.Price;
                e.titemid = tid;
                e.amount = item.Amount;
                context.SaveChanges();
            }
        }
        public void DeleteItem(int id)
        {
            using (var context = new QLTSEntities())
            {
                var delItem = context.ITEMS.Single(x => x.iid == id);
                context.ITEMS.Remove(delItem);
                context.SaveChanges();
            }
        }

    }
}

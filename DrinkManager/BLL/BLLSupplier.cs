using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.BLL
{
    class BLLSupplier
    {

        private static BLLSupplier instance;

        private BLLSupplier(){}
        public static BLLSupplier Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BLLSupplier();
                }
                return instance;
            }
        }

        public List<DTO.Supplier> getSupplierEmployee()
        {
            using (var context = new QLTSEntities())
            {
                List<DTO.Supplier> lstSup = new List<DTO.Supplier>();
                context.SUPPLIERS.ToList().ForEach(x=>lstSup.Add(new DTO.Supplier(x.sid,x.name,x.address,Convert.ToInt64(x.phone))));
                return lstSup;
            }
        }
        public void deleteSupplier(int sid)
        {
            using (var context = new QLTSEntities())
            {
                var deleteSup = context.SUPPLIERS.Where(x => x.sid == sid).FirstOrDefault();
                context.SUPPLIERS.Remove(deleteSup);
                context.SaveChanges();
            }
        }
        public void insertSupplier(DTO.Supplier sup)
        {
            using (var context = new QLTSEntities())
            {
                context.SUPPLIERS.Add(new SUPPLIER() { name = sup.Name,address = sup.Address, phone = sup.Phone});
                context.SaveChanges();
            }
        }
        public void customSupplier(DTO.Supplier sup)
        {
            using (var context = new QLTSEntities())
            {
                var e = context.SUPPLIERS.Single(x => x.sid == sup.Id);
                e.name = sup.Name;
                e.phone = sup.Phone;
                e.address = sup.Address;
                context.SaveChanges();
            }
        }

    }
}

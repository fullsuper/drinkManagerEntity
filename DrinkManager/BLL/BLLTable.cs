using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinkManager.DTO;

namespace DrinkManager.BLL
{
    class BLLTable
    {
        private static BLLTable instance;

        private BLLTable()
        {
            getListTable();
        }
        public static BLLTable Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BLLTable();
                }
                return instance;
            }
        }

        public List<DTO.Table> getListTable()
        {
            using (var context = new QLTSEntities())
            {
                List<DTO.Table> lstTable = new List<DTO.Table>();
                context.TABLES.ToList().ForEach(x => lstTable.Add(new DTO.Table(x.tid,Convert.ToInt16(x.floor),x.position)));
                return lstTable;
            }
        }
        public void insertTable(int floor,string position)
        {
            using (var context = new QLTSEntities())
            {
                context.TABLES.Add(new TABLE() { position = position, floor = floor });
                context.SaveChanges();
            }
        }
    }
}

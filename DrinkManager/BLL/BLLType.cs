using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.BLL
{
    class BLLType
    {
        private static BLLType instance;
        public static BLLType Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BLLType();
                }
                return instance;
            }
        }
        public List<DTO.Service> getListService()
        {
            using (var context = new QLTSEntities())
            {
                List<DTO.Service> lstServices = new List<DTO.Service>();
                context.TYPE_SERVICES.ToList().ForEach(x => lstServices.Add(new DTO.Service(x.tserviceid,x.typeservice,x.detail)));
                return lstServices;
            }
           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Linq;

using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DrinkManager.BLL
{
    class BLLEmployee
    {
        private static BLLEmployee instance;
        private static List<TYPE_SERVICES> lstService;
        public static BLLEmployee Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BLLEmployee();
                }
                return instance;
            }
        }
        public static List<TYPE_SERVICES> ListService
        {
            get { return lstService; }
        }
        private BLLEmployee()
        {
            getListService();
        }
       
        private void getListService()
        {
            using (var context = new QLTSEntities())
            {
                lstService = (from b in context.TYPE_SERVICES
                              select b).ToList();
            }
        }
        private int getTypeID(String typeservice)
        {
            using (var context = new QLTSEntities())
            {
                return (context.TYPE_SERVICES.Single(x => x.typeservice == typeservice)).tserviceid;
            }
        }
        public DataTable getTableEmployee()
        {
            using (var context = new QLTSEntities())
            { 
                DataTable table = new DataTable();
                table.Columns.Add("id");
                table.Columns.Add("name");
                table.Columns.Add("hiredate");
                table.Columns.Add("typeservice");
                table.Columns.Add("phone");
                table.Columns.Add("salary");
                (from x in context.EMPLOYEES
                 select x).ToList().ForEach(x=> table.Rows.Add(x.eid, x.name, x.hiredate, x.TYPE_SERVICES.typeservice, x.phone, x.salary));
                return table;
            }
        }

        public void deleteEmployee(int id)
        {
            using (var context = new QLTSEntities())
            {
                var deleteEmp = context.EMPLOYEES.Where(x => x.eid == id).FirstOrDefault();
                context.EMPLOYEES.Remove(deleteEmp);
                context.SaveChanges();
            }
        }
        public void insertNewEmploy(DTO.Employee emp)
        {
            using (var context = new QLTSEntities())
            {
                int tid = getTypeID(emp.Typeservice);
                context.EMPLOYEES.Add(new EMPLOYEE() { name = emp.Name,hiredate=emp.Hiredate,tserviceid=tid,phone=emp.Phone,salary =emp.Salary});
                context.SaveChanges();
            }
        }
        public void updateEmployee(DTO.Employee emp)
        {
            using (var context = new QLTSEntities())
            {
                int tid = getTypeID(emp.Typeservice);
                var e = context.EMPLOYEES.Single(x => x.eid == emp.Id);
                e.hiredate = emp.Hiredate;
                e.name = emp.Name;
                e.salary = emp.Salary;
                e.tserviceid = tid;
                e.phone = emp.Phone;
                context.SaveChanges();
            }
        }
    }
}

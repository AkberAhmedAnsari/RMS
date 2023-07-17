using DAL.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Classes
{
    public class clsCityBAL : City
    {
        /// <summary> 
        /// get All City recordStatus 1 
        /// </summary>   
        /// <returns>All City return</returns>
        public static ObservableCollection<clsCityBAL> GetAllCity()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptCity", new string[] { "@TypeId" }, 1);
            return clsAppObject.DataTableToList<clsCityBAL>(dt);
        }
    }
}

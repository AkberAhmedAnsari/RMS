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
    public class clsCountryBAL : Country
    {
        /// <summary>
        /// get All Country recordStatus 1 
        /// </summary>
        /// <returns>All Country return</returns>
        public static ObservableCollection<clsCountryBAL> GetAllCountry()
        {
            DataTable dt = new DataTable();
            clsAppObject.clsCore.Getdatafromdb(ref dt, "SptCountry", new string[] { "@TypeId" }, 1);
            return clsAppObject.DataTableToList<clsCountryBAL>(dt);
        }
    }
}

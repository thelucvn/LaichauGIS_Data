using Models.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class WardDistrictModel
    {
        LaichauDBContext context = null;
        public WardDistrictModel()
        {
            context = new LaichauDBContext();
        }
        public List<Ward> ListAllWardOfDistrict(int districtID)
        {
            List<Ward> wardList=null;
            wardList=context.Database.SqlQuery<Ward>("sp_ListWardInDistrict @DistrictID",new SqlParameter("@DistrictID",districtID)).ToList();
            return wardList;
        }
    }
}

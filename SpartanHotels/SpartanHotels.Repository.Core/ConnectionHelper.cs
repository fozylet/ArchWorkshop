using System.Configuration;

namespace SpartanHotels.Repository.Core
{
    public class ConnectionHelper
    {
        public static string SpartanHotelsMasterConnectionString
        {

            get { return ConfigurationManager.ConnectionStrings["SpartanHotelsEntities"].ConnectionString; }
        }

        public static string SpartanHotelsSnapshotConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["SpartanHotelsEntities"].ConnectionString; }
        }
    }
}

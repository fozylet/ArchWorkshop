using System;
using System.Configuration;
using System.Data.EntityClient;

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

        private static string ConvertConfigStagingToEntityConnection(string connectionString)
        {
            EntityConnectionStringBuilder ecb = new EntityConnectionStringBuilder();
            ecb.Metadata = "res://*/";
            ecb.Provider = "System.Data.SqlClient";
            ecb.ProviderConnectionString = connectionString;
            return ecb.ConnectionString;
        }
    }
}

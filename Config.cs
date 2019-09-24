using System.Configuration;

namespace EmpDetails
{
    class Config
    {
        public static readonly string ConnectionString;
        static Config()
        {
            ConnectionString =  ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        }
    }
}
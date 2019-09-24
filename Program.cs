using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDetails
{
    public class Program
    {
        public static SqlCommand Sqlcommand;
        private readonly SqlConnection _sqlConnection = new SqlConnection(Config.ConnectionString);
        private readonly UserInteraction _Interaction = new UserInteraction();
        private readonly PerformOperation _Operation = new PerformOperation();
        public static void Main(string[] args)
        {
            Console.WriteLine(" Starts Getting Employee Details");
            Console.WriteLine(" ****** ******* ******** *******");
            Program p = new Program();
            p.CheckDataExist();
            Console.ReadLine();
        }
        internal void CheckDataExist()
        {
            var userDataExist = 0;
            try
            {
                _sqlConnection.Open();
                using (Sqlcommand = _sqlConnection.CreateCommand())
                {
                    Sqlcommand.CommandText = "SELECT COUNT(JobID) FROM Employee";
                    userDataExist = (int)Sqlcommand.ExecuteScalar();
                }
                _sqlConnection.Close();

                if (userDataExist > 0)
                {
                    // CRUD Operation
                    _Operation.Operation();
                }
                else
                {
                    // Insertion Operation
                    _Interaction.CreateOperation();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in selecting jobid from Employee : {0}", ex.Message);
            }
        }
    }
}

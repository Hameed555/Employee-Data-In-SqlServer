using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDetails
{
    public class SqlServer
    {
        private static readonly SqlConnection _sqlConnection = new SqlConnection(Config.ConnectionString);
        public static SqlCommand Sqlcommand;

        // Create (Insert) && Getting (Insert)
        internal int ProcessEmpHistory(string fromDate, string toDate)
        {
            int id = 0;
            try
            {
                _sqlConnection.Open();
                using (Sqlcommand = _sqlConnection.CreateCommand())
                {
                    Sqlcommand.CommandText = "INSERT INTO Employment_History(EmpFrom,EmpTo) Values (@From,@To)";
                    Sqlcommand.Parameters.AddWithValue("@From",fromDate);
                    Sqlcommand.Parameters.AddWithValue("@To", toDate);
                    Sqlcommand.ExecuteNonQuery();

                    Console.WriteLine("\n Inserting Data into Employment_History ");
                    Sqlcommand.CommandText = "SELECT MAX(EmpHistoryID) FROM Employment_History";
                    id = (int)Sqlcommand.ExecuteScalar();
                }
                _sqlConnection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in inserting data into Employment_History : {0}",ex.Message);
            }
            return id;
        }

        internal void ProcessEmployeeData(string jobtitle, string employer, string desc, int empHistoryid)
        {
            try
            {
                _sqlConnection.Open();
                using (Sqlcommand = _sqlConnection.CreateCommand())
                {
                    Sqlcommand.CommandText = "INSERT INTO Employee (Jobtitle,Employer,Description,EmpHistoryID) Values (@JobTitle,@Employer,@Desc,@EmpHistoryID)";
                    Sqlcommand.Parameters.AddWithValue("@JobTitle", jobtitle);
                    Sqlcommand.Parameters.AddWithValue("@Employer", employer);
                    Sqlcommand.Parameters.AddWithValue("@Desc", desc);
                    Sqlcommand.Parameters.AddWithValue("@EmpHistoryID", empHistoryid);
                    Console.WriteLine("\n Inserting Data into Employee Table ");
                    Sqlcommand.ExecuteNonQuery();
                }
                _sqlConnection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in inserting data into Employee : {0}",ex.Message);
            }
        }

        // Update Operation && Delete Operation
        internal int JobIDExist(int jobid)
        {
            int id = 0;
            try
            {
                _sqlConnection.Open();
                Sqlcommand = _sqlConnection.CreateCommand();
                Sqlcommand.CommandText = "SELECT EmpHistoryID From Employee WHERE JobID = @JobID";
                Sqlcommand.Parameters.AddWithValue("@JobID",jobid);
                using (SqlDataReader dr = Sqlcommand.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Console.WriteLine("Data Already Exist...");
                        id = Convert.ToInt32(dr["EmpHistoryID"]);
                    }
                }
                _sqlConnection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in getting EmpHistory From Employee Table : {0}",ex.Message);
            }
            return id;
        }

        //Update Get Data
        internal void UpdateGetData(string fromDate,string toDate,int empHistoryID,string jobtitle,string employer,string desc)
        {
            try
            {
                _sqlConnection.Open();
                using (Sqlcommand = _sqlConnection.CreateCommand())
                {
                    Sqlcommand.CommandText = "UPDATE Employment_History SET EmpFrom = @From, EmpTo = @To WHERE EmpHistoryID = @EmpHistoryID  UPDATE Employee SET Jobtitle = @Jobtitle, Employer = @Employer, Description = @Description WHERE EmpHistoryID = @EmpHistoryID ";
                    Sqlcommand.Parameters.AddWithValue("@EmpHistoryID",empHistoryID);
                    Sqlcommand.Parameters.AddWithValue("@From", fromDate);
                    Sqlcommand.Parameters.AddWithValue("@To", toDate);
                    Sqlcommand.Parameters.AddWithValue("@Jobtitle",jobtitle);
                    Sqlcommand.Parameters.AddWithValue("@Employer", employer);
                    Sqlcommand.Parameters.AddWithValue("@Description", desc);
                    Console.WriteLine("\n Data Updated Successfully...");
                }
                Sqlcommand.ExecuteNonQuery();
                _sqlConnection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in updating Employment_History : {0}",ex.Message);
            }
        }

        //Delete Data
        internal void DeleteData(int emphistoryid)
        {
            try
            {
                _sqlConnection.Open();
                using (Sqlcommand = _sqlConnection.CreateCommand())
                {
                    Sqlcommand.CommandText = "DELETE FROM Employee WHERE EmpHistoryID = @EmpHistoryID; DELETE FROM Employment_History WHERE EmpHistoryID= @EmpHistoryID";
                    Sqlcommand.Parameters.AddWithValue("@EmpHistoryID", emphistoryid);
                    Console.WriteLine("\n Data Deleted Successfully...");
                    Sqlcommand.ExecuteNonQuery();
                }
                _sqlConnection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in deleting data in Employee and Employment_History : {0}",ex.Message);
            }
        }

        // Display Operation  - Single View
        internal void SingleData(int row)
        {
            string header = "{0}{1}{2}{3}{4}{5}";
            try
            {
                _sqlConnection.Open();
                using (Sqlcommand = _sqlConnection.CreateCommand())
                {
                    Sqlcommand.CommandText = "SELECT Employee.JobID,Employee.Jobtitle,Employee.Employer,Employee.Description,Employment_History.EmpFrom,Employment_History.EmpTo FROM Employee INNER JOIN Employment_History ON Employee.EmpHistoryID = Employment_History.EmpHistoryID where JobID = @Row; ";
                    Sqlcommand.Parameters.AddWithValue("@Row", row);
                    using (SqlDataReader reader = Sqlcommand.ExecuteReader())
                    {
                        row++;
                        if ((reader.Read()) && (row < reader.FieldCount))
                        {
                            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------");
                            string str = "";
                            Console.WriteLine(header, "JobID".PadRight(15), "Jobtitle".PadRight(22), "Employer".PadRight(22), "Description".PadRight(26), "EmpFrom".PadRight(27), "EmpTo".PadRight(15));
                            Console.WriteLine();
                            for (int j = 0; j < reader.FieldCount; j++)
                            {
                                Console.Write(reader.GetValue(j) + "{0}", str.PadRight(15));
                            }
                            Console.WriteLine();
                            //i++;

                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------");
                        }
                    }
                }
                _sqlConnection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in getting single record : {0}",ex.Message);
            }
        }

        // Display Operation - Multiple View
        internal void MultiView()
        {
            string header = "{0}{1}{2}{3}{4}{5}";
            _sqlConnection.Open();
            using (Sqlcommand = _sqlConnection.CreateCommand())
            {
                Sqlcommand.CommandText = "SELECT Employee.JobID,Employee.Jobtitle,Employee.Employer,Employee.Description,Employment_History.EmpFrom,Employment_History.EmpTo FROM Employee INNER JOIN Employment_History ON Employee.EmpHistoryID = Employment_History.EmpHistoryID; ";
            }

            using (SqlDataReader reader = Sqlcommand.ExecuteReader())
            {
                int i = 0;
                while ((reader.Read()) && (i < reader.FieldCount))
                {
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------------");
                    string str = "";
                    Console.WriteLine(header, "JobID".PadRight(15), "Jobtitle".PadRight(22), "Employer".PadRight(22), "Description".PadRight(26), "EmpFrom".PadRight(27), "EmpTo".PadRight(15));
                    Console.WriteLine();
                    for (int j = 0; j < reader.FieldCount; j++)
                    {
                        Console.Write(reader.GetValue(j) + "{0}", str.PadRight(15));
                    }
                    Console.WriteLine();
                    i++;

                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------");
                }
            }
            _sqlConnection.Close();
        }
    }
}
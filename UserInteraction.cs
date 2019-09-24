using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDetails
{
    class UserInteraction
    {
        // Insertion Operation (Create)
        internal void CreateOperation()
        {
            try
            {
                bool loopContinueTitle = true;
                string jobtitle = "";
                while (loopContinueTitle)
                {
                    Console.WriteLine("\n Enter the JobTitle");
                    jobtitle = Console.ReadLine();

                    if (jobtitle == string.Empty)
                        Console.WriteLine("\n Sorry!! Put Valid data...");
                    else
                        loopContinueTitle = false; 
                }

                string employer = "";
                bool loopContinueEmployer = true;
                while (loopContinueEmployer)
                {
                    Console.WriteLine("\n Enter the Employer");
                    employer = Console.ReadLine();

                    if (employer == string.Empty)
                        Console.WriteLine("\n Employer Cannot Be Null Or Empty");
                     
                    else
                        loopContinueEmployer = false;
                }

                string desc = "";
                bool loopContinueDesc = true;
                while (loopContinueDesc)
                {
                    Console.WriteLine("\n Enter the Description");
                    desc = Console.ReadLine();

                    if (desc == string.Empty)
                        Console.WriteLine("\n Description Cannot Be Null Or Empty");
                     
                    else
                        loopContinueDesc = false;
                }

                string fromDate = "";
                bool loopContinueFrom = true;
                DateTime dateFrom;
                while (loopContinueFrom)
                {
                    Console.WriteLine("\n Enter From Period");
                    fromDate = Console.ReadLine();

                    if (DateTime.TryParse(fromDate, out dateFrom))
                    {
                        fromDate = string.Format("{0:yyyy-MM-dd}",dateFrom);
                        loopContinueFrom = false;
                    }
                    else
                        Console.WriteLine("\n Sorry!! Please Give An Valid Data...");
                }

                string toDate = "";
                bool loopContinueTo = true;
                DateTime dateTo;
                while (loopContinueTo)
                {
                    Console.WriteLine("\n Enter To Period");
                    toDate = Console.ReadLine();

                    if (DateTime.TryParse(toDate, out dateTo))
                    {
                        toDate= string.Format("{0:yyyy-MM-dd}",dateTo);
                        loopContinueTo = false;
                    }
                    else
                        Console.WriteLine("\n Sorry!! Please Give An Valid Data...");
                }
                _SqlServer.ProcessEmployeeData(jobtitle, employer, desc, _SqlServer.ProcessEmpHistory(fromDate, toDate));
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in inserting data : {0}",ex.Message);
            }
        }

        //Update Operation

        internal void UpdateOperation()
        {
            Console.WriteLine("\nUpdate Operation");
            Console.WriteLine("****** *********");
            try
            {
                int empHistoryID = 0;
                bool loopContinueid = true;
                while (loopContinueid)
                {
                    Console.WriteLine("\n Enter The JobID");
                    string jobidString = Console.ReadLine();
                    int jobid;
                    if (int.TryParse(jobidString, out jobid))
                    {
                        empHistoryID = _SqlServer.JobIDExist(jobid);
                        loopContinueid = false;
                    }
                    else
                        Console.WriteLine("Please Put Valid Data");
                }
                //int empHistoryID = _SqlServer.JobIDExist(jobid);
                if (empHistoryID != 0)
                {
                    bool loopContinue = true;
                    string jobtitle = "";
                    while (loopContinue)
                    {
                        Console.WriteLine("\n Enter the JobTitle");
                        jobtitle = Console.ReadLine();

                        if (jobtitle == string.Empty)
                        {
                            Console.WriteLine("\n JobTitle Cannot Be Null Or Empty");
                            loopContinue = true;
                        }
                        else
                            loopContinue = false;
                    }

                    bool loopContinue1 = true;
                    string employer = "";
                    while (loopContinue1)
                    {
                        Console.WriteLine("\n Enter the Employer");
                        employer = Console.ReadLine();

                        if (employer == string.Empty)
                        {
                            Console.WriteLine("\n Employer Cannot Be Null Or Empty");
                            loopContinue1 = true;
                        }
                        else
                            loopContinue1 = false;
                    }

                    bool loopContinue2 = true;
                    string desc = "";
                    while (loopContinue2)
                    {
                        Console.WriteLine("\n Enter the Description");
                        desc = Console.ReadLine();

                        if (desc == string.Empty)
                        {
                            Console.WriteLine("\n Description Cannot Be Null Or Empty");
                            loopContinue2 = true;
                        }
                        else
                            loopContinue2 = false;
                    }

                    string fromDate = "";
                    bool loopContinueFrom = true;
                    DateTime dateFrom;
                    while (loopContinueFrom)
                    {
                        Console.WriteLine("\n Enter From Period");
                        fromDate = Console.ReadLine();

                        if (DateTime.TryParse(fromDate, out dateFrom))
                        {
                            string.Format("{0:yyyy-mm-dd}", dateFrom);
                            loopContinueFrom = false;
                        }

                        else
                            Console.WriteLine("\n Sorry!! Please Give An Valid Data...");
                    }

                    string toDate = "";
                    bool loopContinueTo = true;
                    DateTime dateTo;
                    while (loopContinueTo)
                    {
                        Console.WriteLine("\n Enter To Period");
                        toDate = Console.ReadLine();

                        if (DateTime.TryParse(toDate, out dateTo))
                        {
                            string.Format("{0:yyyy-mm-dd}", dateTo);
                            loopContinueTo = false;
                        }
                        else
                            Console.WriteLine("\n Sorry!! Please Give An Valid Data...");
                    }
                    _SqlServer.UpdateGetData(fromDate, toDate, empHistoryID, jobtitle, employer, desc);
                }
                else
                    Console.WriteLine("Data Does Not Exist....");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in updating data : {0}",ex.Message);
            }
        }
        // Delete Operation
        internal void DeleteOperation()
        {
            Console.WriteLine("\n Delete Operation");
            Console.WriteLine(" ****** *********");
            try
            {
                int emphistoryid = 0;
                bool loopContinue = true;
                while (loopContinue)
                {
                    Console.WriteLine("\n Enter The JobID :");
                    string jobidString = Console.ReadLine();
                    int jobid;

                    if (int.TryParse(jobidString, out jobid))
                    {
                        emphistoryid = _SqlServer.JobIDExist(jobid);
                        loopContinue = false;
                    }
                    else
                        Console.WriteLine("Please Put Valid Data");
                }

                if (emphistoryid != 0)
                {
                    _SqlServer.DeleteData(emphistoryid);
                }
                else
                    Console.WriteLine("Data Does Not Exist....");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error in Delete Operation : {0}",ex.Message);
            }
        }
        
        private readonly SqlServer _SqlServer = new SqlServer();
        //Display Operation
        internal void DisplayOperation()
        {
            Console.WriteLine("\nDisplay Operation");
            Console.WriteLine("******* *********");
            bool loopContinue = true;
            int data = 0;
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("\n 1. Single Row Data");
            Console.WriteLine(" 2. Multiple Row Data\n");
            Console.WriteLine("---------------------------------------------------------------");

            while (loopContinue)
            {
                Console.WriteLine("\n Enter Your Choice : ");
                string choice = Console.ReadLine();

                if (int.TryParse(choice, out data))
                {
                    switch (data)
                    {
                        case 1:
                            SingleView();
                            loopContinue = false;
                            break;
                        case 2:
                            _SqlServer.MultiView();
                            loopContinue = false;
                            break;
                        default:
                            Console.WriteLine("Sorry! Please Give Valid Data");
                            loopContinue = true;
                            break;
                    }
                }
                else
                    Console.WriteLine("Please Give An Valid Data");
            }
        }

        internal void SingleView()
        {
            bool loopContinue = true;
            int value = 0;
            try
            {
                while (loopContinue)
                {
                    Console.WriteLine("Enter The Data : ");
                    string data1 = Console.ReadLine();

                    if (int.TryParse(data1, out value))
                    {
                        _SqlServer.SingleData(value);
                        loopContinue = false;
                    }

                    else
                        Console.WriteLine("Please Give Valid Data");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
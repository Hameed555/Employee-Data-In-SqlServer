using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace EmpDetails
{
    public class PerformOperation
    {
       private readonly UserInteraction _Interaction = new UserInteraction();
        internal void Operation()
        {
            bool loopContinue = true;

            while (loopContinue)
            {
                Console.WriteLine("\nSelect Your CRUD Operation");
                Console.WriteLine("****** **** **** *********");
                Console.WriteLine("1. Create");
                Console.WriteLine("2. Update");
                Console.WriteLine("3. Delete");
                Console.WriteLine("4. Display");
                Console.WriteLine("5. Exit");

                int operationNumber = 0;

                Console.WriteLine("\n Enter The Operation : ");
                string selectOperation = Console.ReadLine();

                if (int.TryParse(selectOperation, out operationNumber))
                {
                    switch (operationNumber)
                    {
                        case 1:
                            Console.WriteLine("\n Processing The Data...");
                            _Interaction.CreateOperation();
                            loopContinue = true;
                            Console.WriteLine("------------------------------------------------------------------------------------");
                            break;
                        case 2:
                            Console.WriteLine("\n Processing The Data...");
                            _Interaction.UpdateOperation();
                            loopContinue = true;
                            Console.WriteLine("------------------------------------------------------------------------------------");
                            break;
                        case 3:
                            Console.WriteLine("\n Processing The Data...");
                            _Interaction.DeleteOperation();
                            loopContinue = true;
                            Console.WriteLine("------------------------------------------------------------------------------------");
                            break;
                        case 4:
                            Console.WriteLine("\n Processing The Data...");
                            _Interaction.DisplayOperation();
                            loopContinue = true;
                            break;
                        case 5:
                            loopContinue = false;
                            Console.WriteLine("\n Press any key to exit");
                            break;
                        default:
                            Console.WriteLine("Sorry!! Please Give An Valid Data..");
                            loopContinue = true;
                            break;
                    }
                }
                else
                    Console.WriteLine("Sorry!! Please Give An Valid Data..");
            }
        }
    }
}


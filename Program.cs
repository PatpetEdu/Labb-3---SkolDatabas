using Labb_3___Skol_Databas.Models;
using System.Security.Principal;

namespace Labb_3___Skol_Databas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Meny val
            string options = "";
            Console.WriteLine($"\nVälkommen" +
                    "\nGör ditt val nedan"+    
                    "\n==================" +
                    "\n[1] Information om anställda" +
                    "\n[2] Visa alla elever" +
                    "\n[3] Visa alla elever i en specifik klass" +
                    "\n[4] Visa alla betyg som satts den senaste månaden" +
                    "\n[5] Visa en lista med alla kurser och Betyg" +
                    "\n[6] Lägg till nya elever" +
                    "\n[7] Lägg till ny personal" + 
                    "\n[8] Visa kurslista"+
                    "\n[9] Visa löner för olika befattningar"+
                    "\n==================");

            Console.Write("Ditt val: ");
            options = Console.ReadLine();

            //swtich-sats för våra olika val där som kallar på olika metoder
            switch (options)
            {
                case "1":
                    Functionality.GetAllEmployees();
                    break;
                case "2":
                    Functionality.GetStudents();

                    break;
                case "3":
                    Functionality.GetStudentsFromClass();
                   
                    break;
                case "4":
                    Functionality.GetGradesFromLastMonth();
                    break;

                case "5":
                    Functionality.GetGradeAverage();

                    break;
                case "6":
                    Admin.AddStudent();

                    break;
                case "7":
                    Admin.AddEmployee();
                    break;
                case "8":
                    Functionality.ShowCourseList();
                    break;
                case "9":
                    Admin.SalaryOut();
                    break;
            }
        }
    }
            
}

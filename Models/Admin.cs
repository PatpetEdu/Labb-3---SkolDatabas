using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3___Skol_Databas.Models
{
    public class Admin
    {
        private static readonly HogwartsSkolaContext context = new HogwartsSkolaContext();

        //Metod för att lägga till ny stundenter
        public static void AddStudent()
        {
            Console.WriteLine("Vänligen mata in uppgifterna för den elev som du vill lägga till");

            // Tar emot input från användaren
            Console.Write("Förnamn: ");
            string förnamn = Console.ReadLine();

            Console.Write("Efternamn: ");
            string efternamn = Console.ReadLine();

            Console.Write("Personnummer (10 siffror): ");
            string personnummer = Console.ReadLine();

            Console.Write("Kön(Male or Female): ");
            string kön = Console.ReadLine();

            Console.Write("Ålder: ");
            string ageInput = Console.ReadLine();
            //Använder felhantering så programmet in kraschar vid inmatning av ålder
            int ålder;
            bool isValidAge = int.TryParse(ageInput, out ålder);

            while (!isValidAge || ålder <= 0)
            {
                Console.WriteLine("Ogiltigt värde! Vänligen ange din ålder i siffror:");
                ageInput = Console.ReadLine();
                isValidAge = int.TryParse(ageInput, out ålder);
            }
            Console.Write("Adress: ");
            string adress = Console.ReadLine();

            Console.Write("Telefonnummer: ");
            string telefonnummer = Console.ReadLine();


            //Bekräftar inmatningen från användaren
            //konsolen rensas innan uppgifterna som har inmatats skrivs ut
            Console.Clear();

            Console.WriteLine("Du har valt att lägga till ny elev med följande uppgifter:");
            Console.WriteLine();
            Console.WriteLine($"Förnamn: {förnamn}");
            Console.WriteLine($"Efternamn: {efternamn}");
            Console.WriteLine($"Personnummer: {personnummer}");
            Console.WriteLine($"Kön: {kön}");
            Console.WriteLine($"Ålder: {ålder}");
            Console.WriteLine($"Adress: {adress}");
            Console.WriteLine($"Telefonnummer: {telefonnummer}");

            Console.WriteLine("Vill du registrera eleven i databasen?");
            Console.WriteLine("1. Ja");
            Console.WriteLine("2. Nej");

            bool isUserChoiceValid = false;
            while (!isUserChoiceValid)
            {
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        isUserChoiceValid = true;
                        // Skapa en ny Personalinfo med användarens input
                        var newPersonlInfo = new Personalinfo
                        {
                            FirstName = förnamn,
                            LastName = efternamn,
                            SocialSecurityNumber = personnummer,
                            Gender = kön,
                            Age = ålder,
                            Address = adress,
                            PhoneNumer = telefonnummer
                            
                        };

                        context.Personalinfos.Add(newPersonlInfo);

                        context.SaveChanges();

                        var newStudent = new Student
                        {
                            FkpersonId = newPersonlInfo.PersonId,

                        };
                        // Lägger till en ny student i databasen
                        context.Students.Add(newStudent);                   
                        // Sparar ändringarna i databasen
                        context.SaveChanges();
                        Console.WriteLine("Eleven har registrerats i databasen");
                        Console.WriteLine();

                        bool isClassIdVaild = false;
                        while (!isClassIdVaild)
                        {
                            //Frågar användaren vilken klass studenten ska gå i
                            Console.WriteLine("Vänligen välj en klass att tilldela eleven:");

                            //visar  en listan med alla befintliga klasser med deras ID
                            var availableClasses = context.Classes.ToList();

                            foreach (var classItem in availableClasses)
                            {
                                Console.WriteLine($"{classItem.ClassId}. {classItem.ClassName}");
                            }

                            Console.Write("Ange klassens ID: ");
                            string selectedClassIdInput = Console.ReadLine();

                            if (int.TryParse(selectedClassIdInput, out int selectedClassId))
                            {
                                var selectedClass = context.Classes.FirstOrDefault(classItem => classItem.ClassId == selectedClassId);

                                if (selectedClass != null)
                                {
                                    // Lägg till den nya studenten i den valda klassens Students-lista
                                    selectedClass.Students.Add(newStudent);

                                    // Spara ändringarna i databasen
                                    context.SaveChanges();
                                    Console.WriteLine();
                                    Console.WriteLine("Eleven har tilldelats en klass!");
                                    Console.WriteLine();
                                    isClassIdVaild = true; //loopen avslutas
                                }
                                else
                                {
                                    Console.WriteLine("Klassen kunde inte hittas. Eleven har inte tilldelats någon klass.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ogiltigt klass-ID. Försök igen.");
                            }
                        }
                        
                        //****************************************
                        //Användaren får möjlighet att registrera eleven till en kurs
                        Console.WriteLine("Vill du även registrera eleven till en ny kurs?");
                        Console.WriteLine("1. Ja");
                        Console.WriteLine("2. Nej");


                        string registerChoice = Console.ReadLine();

                        if (registerChoice == "1")
                        {
                            bool continueRegistering = true;

                            while (continueRegistering)
                            {
                                Console.WriteLine("Vänligen välj en kurs att tilldela eleven:");
                                // Hämta och visa en lista över tillgängliga kurser från databasen
                                var availableCourses = context.Courses.ToList();

                                foreach (var course in availableCourses)
                                {
                                    Console.WriteLine($"{course.CourseId}. {course.CourseName}");
                                }

                                Console.Write("Ange kursens ID: ");
                                string selectedCourseIdInput = Console.ReadLine();

                                if (int.TryParse(selectedCourseIdInput, out int selectedCourseId))
                                {  
                                    var selectedCourse = context.Courses
                                        .FirstOrDefault(course => course.CourseId == selectedCourseId);

                                    if (selectedCourse == null)
                                    {
                                        Console.WriteLine("Kursen kunde inte hittas. Vill du fortsätta?");
                                        Console.WriteLine("1. Ja");
                                        Console.WriteLine("2. Nej");

                                        string continueChoice = Console.ReadLine();

                                        if (continueChoice == "1")
                                        {
                                            continue; // Fortsätt loopen för att välja en annan kurs
                                        }
                                        else
                                        {
                                            continueRegistering = false; // Avsluta loopen för att registrera fler kurser
                                            break;
                                        }
                                    }

                                    // Lägg till den nya studenten till kursens Students-lista
                                    selectedCourse.Students.Add(newStudent);

                                    // Spara ändringarna i databasen
                                    context.SaveChanges();
                                    Console.WriteLine();
                                    Console.WriteLine("Eleven har tilldelats en kurs!");
                                    Console.WriteLine();

                                    Console.WriteLine("Vill du registrera eleven till en annan kurs?");
                                    Console.WriteLine("1. Ja");
                                    Console.WriteLine("2. Nej");
                               

                                    string anotherCourseChoice = Console.ReadLine();

                                    if (anotherCourseChoice != "1")
                                    {
                                        continueRegistering = false;
                                        selectedCourse.Students.Add(newStudent);
                                        context.SaveChanges();
                                    }
                                    if (choice != "2")
                                    {
                                        Console.WriteLine("Programmet avslutas");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ogiltigt kurs-ID. Försök igen.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ingen kurs har valts. Avslutar processen.");
                        }
                        break;
                    case "2":
                        isUserChoiceValid = true;
                        Console.WriteLine("Inmatningen har inte accepterats. Ingen data har lagts till");
                        break;
                    default:
                        Console.WriteLine("Vänligen ange ett giltigt val");
                        break;
                }
            }
        }
        //Metod för att lägga till nya anställda
        public static void AddEmployee()
        {
            Console.WriteLine("Vänligen mata in uppgifterna för den anställdas som du vill lägga till");

            // Tar emot input från användaren
            Console.Write("Förnamn: ");
            string förnamn = Console.ReadLine();

            Console.Write("Efternamn: ");
            string efternamn = Console.ReadLine();

            Console.Write("Personnummer (10 siffror): ");
            string personnummer = Console.ReadLine();

            Console.Write("Kön(Male or Female): ");
            string kön = Console.ReadLine();

            Console.Write("Ålder: ");
            string ageInput = Console.ReadLine();
            //Använder felhantering så programmet in kraschar vid inmatning av ålder
            int ålder;
            bool isValidAge = int.TryParse(ageInput, out ålder);

            while (!isValidAge || ålder <= 0)
            {
                Console.WriteLine("Ogiltigt värde! Vänligen ange din ålder i siffror:");
                ageInput = Console.ReadLine();
                isValidAge = int.TryParse(ageInput, out ålder);
            }

            Console.Write("Adress: ");
            string adress = Console.ReadLine();

            Console.Write("Telefonnummer: ");
            string telefonnummer = Console.ReadLine();

            //Bekräftar inmatningen från användaren
            //konsolen rensas innan uppgifterna som har inmatats skrivs ut
            Console.Clear();

            Console.WriteLine("Du har valt att lägga till ny anställd med följande uppgifter:");
            Console.WriteLine();
            Console.WriteLine($"Förnamn: {förnamn}");
            Console.WriteLine($"Efternamn: {efternamn}");
            Console.WriteLine($"Personnummer: {personnummer}");
            Console.WriteLine($"Kön: {kön}");
            Console.WriteLine($"Ålder: {ålder}");
            Console.WriteLine($"Adress: {adress}");
            Console.WriteLine($"Telefonnummer: {telefonnummer}");

            Console.WriteLine("Vill du registrera den anställda i databasen?");
            Console.WriteLine();
            Console.WriteLine("1. Ja");
            Console.WriteLine("2. Nej");

            bool isUserChoiceValid = false;
            while (!isUserChoiceValid)
            {
                string choice = Console.ReadLine();
                switch (choice)
                {

                    case "1":
                        isUserChoiceValid = true;
                        // Skapa en ny Personalinfo med användarens input
                        var newPersonlInfo = new Personalinfo
                        {
                            FirstName = förnamn,
                            LastName = efternamn,
                            SocialSecurityNumber = personnummer,
                            Gender = kön,
                            Age = ålder,
                            Address = adress,
                            PhoneNumer = telefonnummer
                        };

                        context.Personalinfos.Add(newPersonlInfo);

                        context.SaveChanges();

                        var newEmployee = new Employee
                        {
                            FkpersonId = newPersonlInfo.PersonId
                        };

                        // Lägger till en ny anställd i databasen
                        context.Employees.Add(newEmployee);

                        // Sparar ändringarna i databasen
                        context.SaveChanges();
                        Console.WriteLine("Anställd har lagts till i databasen!");
                        break;
                    case "2":
                        isUserChoiceValid = true;
                        Console.WriteLine("Inmatningen har inte accepterats. Ingen data har lagts till");
                        break;
                    default:
                        Console.WriteLine("Vänligen ange ett giltigt val");
                        break;
                }
            }
        }
        public static void SalaryOut()
        {
            var listOfPositions = context.Positions.ToList();

            Console.WriteLine("Vänligen välj befattning för att visa löneinformation:");

            for (int i = 0; i < listOfPositions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {listOfPositions[i].PositionName}");
            }

            Console.WriteLine($"{listOfPositions.Count + 1}. Avsluta");

            int userChoice;

            while (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < 1 || userChoice > listOfPositions.Count + 1)
            {
                Console.WriteLine("Ogiltigt val. Försök igen.");
            }

            if (userChoice == listOfPositions.Count + 1)
            {
                return; // Avsluta metoden om användaren väljer att avsluta
            }

            var selectedPosition = listOfPositions[userChoice - 1];
            var selectedEmployees = context.Employees
                .Include(e => e.Fkperson) // Personalinfo
                .Include(e => e.Fkposition)
                .Where(e => e.EmploymentDate <= DateTime.Now && e.FkpositionId == selectedPosition.PositionId)
                .ToList();

            int employeeCount = selectedEmployees.Count;

            if (employeeCount > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"Löneinformation för befattningen {selectedPosition.PositionName}");
                Console.WriteLine($"Antal anställda: {employeeCount}");
                Console.WriteLine();

                // Beräkna total lön för befattningen
                decimal totalSalary = selectedEmployees.Sum(e => decimal.Parse(e.Salary));
                decimal averageSalary = totalSalary / employeeCount;
                Console.WriteLine($"Total lön för befattningen: {totalSalary} kr");                                          
                Console.WriteLine($"Medellön för befattningen: {averageSalary} kr");
                Console.WriteLine();

                // Visa individuell löneinformation för varje anställd
                foreach (var employee in selectedEmployees)
                {
                    Console.WriteLine($"AnställningsId: {employee.EmployeeId}");
                    Console.WriteLine($"Namn: {employee.Fkperson.FirstName} {employee.Fkperson.LastName}");
                    Console.WriteLine($"Befattning: {employee.Fkposition.PositionName}");
                    Console.WriteLine($"Lön: {employee.Salary} kr");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"Ingen personal hittades med den befattning {selectedPosition.PositionName}");
            }
            Console.ReadKey();
        }
        public static void SetGrades()
        {

        }

    }
        
}


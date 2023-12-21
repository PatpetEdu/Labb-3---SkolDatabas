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
                            FkpersonId = newPersonlInfo.PersonId
                        };

                        // Lägger till en ny student i databasen
                        context.Students.Add(newStudent);

                        // Sparar ändringarna i databasen
                        context.SaveChanges();

                        Console.WriteLine("Elev har lagts till i databasen!");
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
    }    
}


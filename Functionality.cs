using Labb_3___Skol_Databas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3___Skol_Databas
{
    public class Functionality
    {
        private static readonly HogwartsSkolaContext _dbContext = new HogwartsSkolaContext();

        public static void GetAllEmployees()
        //Hämtar alla anställda eller en viss befattning
        {
            string val = "";
            var employees = _dbContext.Employees
            .Include(e => e.Fkperson) //Personalinfo
            .Include(e => e.Fkposition)
            .Where(e => e.EmploymentDate <= DateTime.Now)
            .ToList();

            while (true)
            {
                //Vamöjligheter bereonde på vad man vill visa
                Console.WriteLine($"\nVälj om du vill visa alla anställda eller en speciell befattning som tex lärare" +
                 "\nGör ditt val nedan" +
                 "\n==================" +
                 "\n[1] Visa alla anställda på skolan" +
                 "\n[2] Visa anställda efter befattning"+ 
                 "\n[0] Avsluta" +
                 "\n");

                val = Console.ReadLine();
                Console.Clear();

                switch (val)
                {
                    case "1":
                        Console.WriteLine("Lista över all personal på Skolan");
                        Console.WriteLine();
                        foreach (var employee in employees)
                        {
                            Console.WriteLine($"AnställningsId: {employee.EmployeeId}");
                            Console.WriteLine($"Namn: {employee.Fkperson.FirstName} {employee.Fkperson.LastName}");
                            if (employee.FkpositionId != null)
                            {
                                Console.WriteLine($"Befattning: {employee.Fkposition.PositionName}");

                                if (employee.EmploymentDate != null)
                                {
                                    var currentDate = DateTime.Now;
                                    var yearsWorked = currentDate.Year - employee.EmploymentDate.Value.Year;

                                    // Om anställningen inträffade senare under året, minska antalet år med 1
                                    if (currentDate.Month < employee.EmploymentDate.Value.Month || (currentDate.Month == employee.EmploymentDate.Value.Month && currentDate.Day < employee.EmploymentDate.Value.Day))
                                    {
                                        yearsWorked--;
                                    }

                                    Console.WriteLine($"Total anställningstid: {yearsWorked} år");
                                    Console.WriteLine();
                                }
                                else
                                {
                                    Console.WriteLine("Anställningsdatum saknas");
                                }
                            }
                        }
                        break;
                    case "2":
                        var listOfPositions = _dbContext.Positions.Select(p => p.PositionName).ToList();

                        Console.WriteLine("Vänligen ange namet på den befattning som du vill visa!");
                        Console.WriteLine("Tillgängliga befattningar som finns att visa:");

                        foreach (var position in listOfPositions)
                        {
                            Console.WriteLine($" - {position}");
                        }
                        Console.WriteLine();
                        string befattning = Console.ReadLine();
                        var SelectedEmployees = employees
                            .Where(e => e.Fkposition.PositionName.ToLower() == befattning.ToLower())
                            .ToList();

                        int employeeCount = SelectedEmployees.Count;
                        if (employeeCount > 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Lista över personal med befattningen {befattning}");
                            Console.WriteLine($"Antal anställda: {employeeCount}");
                            Console.WriteLine();
                            foreach (var employee in SelectedEmployees)
                            {
                                Console.WriteLine($"AnställningsId: {employee.EmployeeId}");
                                Console.WriteLine($"Namn: {employee.Fkperson.FirstName} {employee.Fkperson.LastName}");
                                Console.WriteLine($"Befattning: {employee.Fkposition.PositionName}");
                                Console.WriteLine();
                            }
                        }
                        else
                        {

                            Console.WriteLine($"Ingen personal hittades med den befattning {befattning}");
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        break;
                }
            }
        }
        //Metod för att hämta alla stunder på skolan
        public static void GetStudents()
        {
            //string val = ""; //ytterligare valfunktioner implementeras senare
            var students = _dbContext.Students
            .Include(s => s.Fkperson) //Personalinfo           
            .ToList();

            Console.WriteLine("Lista över alla elever på skolan");
            Console.WriteLine();
            foreach (var student in students)
            {
                Console.WriteLine($"Namn: {student.Fkperson.FirstName} {student.Fkperson.LastName}");
                Console.WriteLine($"Ålder: {student.Fkperson.Age}");
                Console.WriteLine();
            }
        }
        //Metod för att hämta studenter från en specfik klass
        public static void GetStudentsFromClass()
        {
            //string val = ""; //ytterligare valfunktioner implementeras senare
            var StudentsWithClassInfo = _dbContext.Students
            .Include(s => s.Fkperson)
            .ToList();

            Console.WriteLine("Lista på över alla klasser");
            Console.WriteLine();
            var listOfClasses = _dbContext.Classes.Select(c => new { c.ClassId, c.ClassName }).ToList();

            Console.WriteLine("Tillgängliga klasser att visa");

            foreach (var classInfo in listOfClasses)
            {

                Console.WriteLine($" - {classInfo.ClassName}");

            }
            Console.WriteLine("Vänligen ange namet på den klass som du vill visa!");
            string selectedClass = Console.ReadLine();

            var studentsInSelectedClass = StudentsWithClassInfo
                .Join(listOfClasses,
                student => student.FkclassId,
                classInfo => classInfo.ClassId,
                (student, classInfo) => new { students = student, className = classInfo.ClassName })
                .Where(s => s.className.ToLower() == selectedClass.ToLower())
                .ToList();

            if (studentsInSelectedClass.Any())
            {
                Console.WriteLine($"Lista över studenter i klassen {selectedClass}");
                Console.WriteLine();

                foreach (var studentInfo in studentsInSelectedClass)
                {
                    var student = studentInfo.students;
                    Console.WriteLine($"StudentID: {student.StudentId}");
                    Console.WriteLine($"Namn: {student.Fkperson.FirstName} {student.Fkperson.LastName}");
                    Console.WriteLine($"Klass: {selectedClass}");
                    Console.WriteLine();
                }
            }
            else
            {
              Console.WriteLine($"Inga studenter hittades i klassen {selectedClass}");
            }

        }
        //Metod för att hämta betyg som har satts den senaste månaden
        public static void GetGradesFromLastMonth()
        {
            var studentsGradeInfo = _dbContext.Courselists
           .Include(g => g.Fkstudent.Fkperson)
           .Include(g => g.Fkcourse)
           .Where (g => g.GradeDate >= DateTime.Now.AddMonths(-1))
           .OrderByDescending(g => g.GradeDate)         
           .ToList();

            if (studentsGradeInfo.Any())
            {
                Console.WriteLine($"Här visas alla betyg som har satts den senaste månaden");
                Console.WriteLine();
                foreach (var grade in studentsGradeInfo)
                {                
                    Console.WriteLine($"Namn: {grade.Fkstudent.Fkperson.FirstName} {grade.Fkstudent.Fkperson.LastName}");
                    Console.WriteLine($"Krus; {grade.Fkcourse.CourseName}");
                    Console.WriteLine($"Betyg: {grade.GradeInfo}");
                    Console.WriteLine($"Betygsdatum: {grade.GradeDate}");
                    Console.WriteLine();                  
                }
            }
            else
            {
                Console.WriteLine("Inga satta betyg hittades den senaste månanden");
            }
        }
        //Hämtar ett betygssnitt samt högsta och lägsta betyg
        public static void GetGradeAverage()
        {
            var gradeAverage = _dbContext.Courselists
           .Include(g => g.Fkstudent.Fkperson)
           .Include(g => g.Fkcourse)
           .Where(g => g.GradeInfo != null)
           .GroupBy(g => g.Fkcourse.CourseName)
           .Select(group => new
           {
               CourseName = group.Key,
               HighestGrade = group.Max(g => g.GradeInfo),
               LowestGrade = group.Min(g => g.GradeInfo),
               AverageGrade = group.Average(g => g.GradeInfo)

           })
           .ToList();
           
            if (gradeAverage.Any())
            {
                Console.WriteLine("Visar betygssnitt för alla kurser");
                Console.WriteLine();

                foreach (var grade in gradeAverage)
                {
                    Console.WriteLine("Kurs:  " + grade.CourseName);
                    Console.WriteLine("Högsta Betyg:  " + grade.HighestGrade);
                    Console.WriteLine("Lägsta Betyg:  " + grade.LowestGrade);
                    Console.WriteLine("Snittbetyg:  " + grade.AverageGrade);
                }
            }
            else
            {
                Console.WriteLine("Inga betyg");
            }
       
        }
        public static void ShowCourseList()
        {
            Console.WriteLine("Tillgängliga kurser på skolan:");
            // Hämta och visa en lista över tillgängliga kurser från databasen
            var availableCourses = _dbContext.Courses.ToList();

            foreach (var course in availableCourses)
            {
                Console.WriteLine($"{course.CourseId}. {course.CourseName}");
            }
        }

    }
}

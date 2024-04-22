using System;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using UsingLINQExample.Entities;
using System.Xml.Linq;

namespace UsingLINQExample {
    internal class Program {
        static void Main(string[] args) {

            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            List<Employee> listOfEmployees = new List<Employee>();

            try {
                using (StreamReader sr = File.OpenText(path)) {
                    while (!sr.EndOfStream) {
                        string[] vet = sr.ReadLine().Split(',');
                        string name = vet[0];
                        string email = vet[1];
                        double salary = double.Parse(vet[2], CultureInfo.InvariantCulture);

                        Employee emp = new Employee(name, email, salary);
                        listOfEmployees.Add(emp);
                    }
                }
                Console.Write("Enter salary: ");
                double chosenSalary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                var emails =
                    from e in listOfEmployees
                    where e.Salary > chosenSalary
                    orderby e.Email
                    select e.Email;

                foreach (string email in emails) {
                    Console.WriteLine(email);
                }

                var salaries = listOfEmployees.Where(e => e.Name[0] == 'J').Sum(e => e.Salary);

                Console.WriteLine("Sum of salary of people whose name starts with 'J': " + salaries);
            }
            catch (IOException e) {
                Console.WriteLine("Error!" + e.Message);
            }
        }
    }
}
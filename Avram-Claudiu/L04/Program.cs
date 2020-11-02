using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;

namespace L04
{
    class Program
    {
        private static CloudTable studentsTable;
        private static CloudTableClient tableClient;
        private static TableOperation tableOperation;
        private static TableResult tableResult;
        private static List<StudentEntity> students  = new List<StudentEntity>();
        static void Main(string[] args)
        {
            Task.Run(async () => { await Initialize(); })
                .GetAwaiter()
                .GetResult();
        }
        static async Task Initialize()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;"
            + "AccountName=flaviusciurciundatc2020;"
           +" AccountKey=YSr9Op7+2uMGLFlfN5stXGGCEaFPO9+qgOR37W5JuvHrrxP4F1YqPLrO57ERR7cdOBhvivqEl4GaKgsrA8bgWg==;"
           +" EndpointSuffix=core.windows.net;";

            var account = CloudStorageAccount.Parse(storageConnectionString);
            tableClient = account.CreateCloudTableClient();

            studentsTable = tableClient.GetTableReference("Students");

            await studentsTable.CreateIfNotExistsAsync();
            
            int option = -1;
            do
            {
                System.Console.WriteLine("1.Adaugati un student.");
                System.Console.WriteLine("2.Modificati date student.");
                System.Console.WriteLine("3.Stergeti student.");
                System.Console.WriteLine("4.Afisati studenti.");
                System.Console.WriteLine("5.Exit");
                System.Console.WriteLine("Introduceti optiunea d-voastra:");
                string opt = System.Console.ReadLine();
                option =Int32.Parse(opt);
                switch(option)
                {
                    case 1:
                        await AddNewStudent();
                        break;
                    case 2:
                        await EditStudent();
                        break;
                    case 3:
                        await DeleteStudent();
                        break;
                    case 4:
                        await DisplayStudents();
                        break;
                    case 5:
                        System.Console.WriteLine("Thank you for visit!");
                        break;
                }
            }while(option != 5);
            
        }
        private static async Task<StudentEntity> RetrieveRecordAsync(CloudTable table,string partitionKey,string rowKey)
        {
            tableOperation = TableOperation.Retrieve<StudentEntity>(partitionKey, rowKey);
            tableResult = await table.ExecuteAsync(tableOperation);
            return tableResult.Result as StudentEntity;
        }
        private static async Task AddNewStudent()
        {
            System.Console.WriteLine("Introduceti o universtitate:");
            string university = Console.ReadLine();
            System.Console.WriteLine("Introduceti cnp:");
            string cnp = Console.ReadLine();
            System.Console.WriteLine("Introduceti numele de familie:");
            string firstName = Console.ReadLine();
            System.Console.WriteLine("Introduceti prenumele:");
            string lastName = Console.ReadLine();
            System.Console.WriteLine("Introduceti facultatea:");
            string faculty = Console.ReadLine();
            System.Console.WriteLine("Introduceti anul de studiu:");
            string year = Console.ReadLine();

            StudentEntity stud = await RetrieveRecordAsync(studentsTable, university, cnp);
            if(stud == null)
            {
                var student = new StudentEntity(university,cnp);
                student.FirstName = firstName;
                student.LastName = lastName;
                student.Faculty = faculty;
                student.Year = Convert.ToInt32(year);
                var insertOperation = TableOperation.Insert(student);
                await studentsTable.ExecuteAsync(insertOperation);
                System.Console.WriteLine("Inregistrarea s-a facut cu succes!");
            }
            else
            {
                System.Console.WriteLine("Studentul exista!");
            }
        }
        private static async Task EditStudent()
        {
            System.Console.WriteLine("Introduceti universitatea:");
            string university = Console.ReadLine();
            System.Console.WriteLine("Introduceti cnp:");
            string cnp = Console.ReadLine();
            StudentEntity stud = await RetrieveRecordAsync(studentsTable, university, cnp);
            if(stud != null)
            {
                System.Console.WriteLine("Intregistrarea exista deja!");
                var student = new StudentEntity(university,cnp);
                System.Console.WriteLine("Introduceti numele de familie:");
                string firstName = Console.ReadLine();
                System.Console.WriteLine("Introduceti prenumele:");
                string lastName = Console.ReadLine();
                System.Console.WriteLine("Introduceti facultatea: ");
                string faculty = Console.ReadLine();
                System.Console.WriteLine("Introduceti anul de studiu: ");
                string year = Console.ReadLine();
                student.FirstName = firstName;
                student.LastName = lastName;
                student.Faculty = faculty;
                student.Year = Convert.ToInt32(year);
                student.ETag = "*";
                var updateOperation = TableOperation.Replace(student);
                await studentsTable.ExecuteAsync(updateOperation);
                System.Console.WriteLine("Modificarea s-a facut cu succes");
            }
            else
            {
                System.Console.WriteLine("Inregistrarea nu exista");
            }
        }
        private static async Task DeleteStudent()
        {
            System.Console.WriteLine("Introduceti universitatea:");
            string university = Console.ReadLine();
            System.Console.WriteLine("Introduceti cnp:");
            string cnp = Console.ReadLine();

            StudentEntity stud = await RetrieveRecordAsync(studentsTable, university, cnp);
            if(stud != null)
            {
                var student = new StudentEntity(university,cnp);
                student.ETag = "*";
                var deleteOperation = TableOperation.Delete(student);
                await studentsTable.ExecuteAsync(deleteOperation);
                System.Console.WriteLine("Inregistrarea a fost stearsa!");
            }
            else
            {
                System.Console.WriteLine("Inregistrarea nu exista!");
            }
        }
        private static async Task<List<StudentEntity>> GetAllStudents()
        {
            TableQuery<StudentEntity> tableQuery = new TableQuery<StudentEntity>();
            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<StudentEntity> result = await studentsTable.ExecuteQuerySegmentedAsync(tableQuery,token);
                token = result.ContinuationToken;
                students.AddRange(result.Results);
            }while(token != null);
            return students;
        }
        private static async Task DisplayStudents()
        {
            await GetAllStudents();

            foreach(StudentEntity std in students)
            {
                Console.WriteLine("Facultea : {0}", std.Faculty);
                Console.WriteLine("Nume : {0}", std.FirstName);
                Console.WriteLine("Prenume : {0}", std.LastName);
                Console.WriteLine("Anul de studiu : {0}", std.Year);
                Console.WriteLine("******************************");
            }
            students.Clear();
            
        }
    }
}
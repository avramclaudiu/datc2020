using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace students_api
{
   public class StudentsRepo {

        private static readonly Random rnd = new Random();
        private static readonly string[] Nume = new[]
        {
            "Avram","Popa","Apostol","Dragomir","Abrudan"
        };

        private static readonly string[] Prenume = new[]
        {
            "Claudiu", "Cristian", "Sebastian", "Vlad", "Raul"
        };

        private static readonly string[] Facultate = new[]
        {
            "Harvard", "Oxford", "MIT", "Politehnica", "UMFT"
        };

         private static readonly string[] Specializare = new[]
        {
            "Automatics", "Sport", "Medicine", "Computer Science", "Human Resources"
        };

        public List<Student> myStudents = new List<Student>() 
        {
            new Student(1, Nume[rnd.Next(Nume.Length)], Prenume[rnd.Next(Prenume.Length)],Facultate[rnd.Next(Facultate.Length)], Specializare[rnd.Next(Specializare.Length)]),
            new Student(2, Nume[rnd.Next(Nume.Length)], Prenume[rnd.Next(Prenume.Length)],Facultate[rnd.Next(Facultate.Length)], Specializare[rnd.Next(Specializare.Length)]),
            new Student(3, Nume[rnd.Next(Nume.Length)], Prenume[rnd.Next(Prenume.Length)],Facultate[rnd.Next(Facultate.Length)], Specializare[rnd.Next(Specializare.Length)]),
            new Student(4, Nume[rnd.Next(Nume.Length)], Prenume[rnd.Next(Prenume.Length)],Facultate[rnd.Next(Facultate.Length)], Specializare[rnd.Next(Specializare.Length)]),
            new Student(5, Nume[rnd.Next(Nume.Length)], Prenume[rnd.Next(Prenume.Length)],Facultate[rnd.Next(Facultate.Length)], Specializare[rnd.Next(Specializare.Length)]),

          
        };
    }
}


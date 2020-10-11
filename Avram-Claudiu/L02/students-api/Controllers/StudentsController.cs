using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;



namespace students_api.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    

    public class StudentsController : ControllerBase
    {
        
        StudentsRepo students = new StudentsRepo();
     
        [HttpGet("{numar_matricol}")]

        public Student GetStudents(int numar_matricol)
        {
            foreach (Student itr in students.myStudents) 
            {
                if (itr.numar_matricol == numar_matricol)
                    return itr;
            }

            return null;
        }

        [HttpPut] 

        public Student UpdateStudent([FromBody] Student student)
        {
            foreach (Student itr in students.myStudents) 
            {
                if (itr.numar_matricol == student.numar_matricol) 
                {
                    itr.nume = student.nume;
                    itr.prenume = student.prenume;
                    itr.facultate = student.facultate;
                    itr.specializare = student.specializare;
                    return itr;
                }
            }
            return null;
        }
        [HttpPost]

        public List<Student> InsertStudent([FromBody] Student student)
        {
            students.myStudents.Add(student);
            return students.myStudents;
        }


       
       
    }
}

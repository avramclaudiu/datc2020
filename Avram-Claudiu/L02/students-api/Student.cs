using System;
namespace students_api
{

public class Student
{
    public int numar_matricol{get; set;}
    public string nume{get; set;}
    public string prenume{get; set;}
    public string facultate{get; set;}
    public string specializare{get; set;}
   

    public Student () {}
        public Student (int id_ = -1, string nume_ = "NONE",string prenume_="NONE", string facultate_ = "NONE",string specializare_ ="NONE") {
            numar_matricol = id_;
            nume = nume_;
            prenume=prenume_;
            facultate = facultate_;
            specializare=specializare_;
        }

    
}
}
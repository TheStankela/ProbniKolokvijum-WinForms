using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFProbniKol.Model;

namespace WFProbniKol.Interface
{
    public interface IStudentRepository
    {
        public List<Student> GetStudent(string querry);
        public List<Student> GetStudents();
        public bool AddStudent(Student student);
        public bool UpdateStudent(Student student);
        public bool DeleteStudent(int id);
    }
}

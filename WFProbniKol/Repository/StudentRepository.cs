using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFProbniKol.Data;
using WFProbniKol.Interface;
using WFProbniKol.Model;

namespace WFProbniKol.Repository
{
    internal class StudentRepository : IStudentRepository
    {
        public bool AddStudent(Student student)
        {
            
            var studenti = GetStudents();
            if (studenti.Any(s => s.JMBG == student.JMBG)) 
            {
                MessageBox.Show("Student sa datim JMBG vec postoji.");
                return false;
            }
            else
            {
                var con = new SqlConnection(Connection.ConnectionString);
                try
                {
                    var command = new SqlCommand();
                    command.Connection = con;
                    con.Open();
                    command.CommandText = @"INSERT INTO Student (Ime, Prezime, JMBG) 
                                        VALUES (@Ime, @Prezime, @JMBG)";
                    command.Parameters.AddWithValue("@Ime", student.FirstName);
                    command.Parameters.AddWithValue("@Prezime", student.LastName);
                    command.Parameters.AddWithValue("@JMBG", student.JMBG);
                    command.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Greska pri dodavanju studenta u bazu.");
                }
                finally
                {
                    con.Close();
                }
                return true;
            }
            
            
        }

        public bool DeleteStudent(int id)
        {
            var con = new SqlConnection(Connection.ConnectionString);
            try
            {
                var command = new SqlCommand();
                command.Connection = con;
                con.Open();
                command.CommandText = @"DELETE FROM STUDENT WHERE Id = '" + id + "'";               
                command.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Greska pri brisanja studenta iz baze.");
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }

        public List<Student> GetStudent(string querry)
        {
            var studenti = new List<Student>();
            var con = new SqlConnection(Connection.ConnectionString);
            try
            {
                con.Open();
                var command = new SqlCommand("SELECT * FROM Student WHERE " +
                    "Ime LIKE '%" + querry + "%' " +
                    "OR Prezime LIKE '%" + querry + "%' " +
                    "OR JMBG LIKE '%" + querry + "%'", con);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var s = new Student
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        FirstName = reader["Ime"].ToString(),
                        LastName = reader["Prezime"].ToString(),
                        JMBG = reader["JMBG"].ToString()
                    };
                    studenti.Add(s);
                }
                reader.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Greska prilikom filtiranja studenata u bazi.");

            }
            finally
            {
                con.Close();
            }
            return studenti;
        }

        public List<Student> GetStudents()
        {
            var studenti = new List<Student>();
            var con = new SqlConnection(Connection.ConnectionString);
            try
            {
                con.Open();
                var command = new SqlCommand("SELECT * FROM Student", con);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var s = new Student
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        FirstName = reader["Ime"].ToString(),
                        LastName = reader["Prezime"].ToString(),
                        JMBG = reader["JMBG"].ToString()
                    };
                    studenti.Add(s);
                }
                reader.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Greska prilikom ucitavanja studenata.");
                
            }
            finally
            {
                con.Close();
            }
            return studenti;
            

        }

        public bool UpdateStudent(Student student)
        {
            var con = new SqlConnection(Connection.ConnectionString);
            try
            {
                var command = new SqlCommand();
                command.Connection = con;
                con.Open();
                command.CommandText = @"UPDATE Student SET  Ime = @Ime, 
                                                            Prezime = @Prezime, 
                                                            JMBG = @JMBG WHERE Id = @ID";
                    command.Parameters.AddWithValue("@ID", student.Id);
                    command.Parameters.AddWithValue("@Ime", student.FirstName);
                    command.Parameters.AddWithValue("@Prezime", student.LastName);
                    command.Parameters.AddWithValue("@JMBG", student.JMBG);
                    command.ExecuteNonQuery();
               
               
            }
            catch
            {
                MessageBox.Show("Greska pri menjanju studenta u bazi.");
            }
            finally
            {
                con.Close();
            }
            return true;
        }
    }
}

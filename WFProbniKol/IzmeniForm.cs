using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFProbniKol.Model;
using WFProbniKol.Repository;

namespace WFProbniKol
{
    public partial class IzmeniForm : Form
    {
        
        StudentRepository _studentRepository = new StudentRepository();
        public IzmeniForm()
        {
            InitializeComponent();
        }

        private void btnSacuvaj_Click(object sender, EventArgs e)
        {
            var s = new Student
            {
                Id = Form1.es.Id,
                FirstName = txtIme.Text,
                LastName = txtPrezime.Text,
                JMBG = txtJmbg.Text
            };
            if (!Form1.ValidJMBG(s.JMBG))
            {
                MessageBox.Show("JMBG nije validan!");
            }
            else
            {
                var studenti = _studentRepository.GetStudents();
                if (studenti.Any(st => st.Id == s.Id))
                {

                    _studentRepository.UpdateStudent(s);
                    MessageBox.Show("Student uspesno promenjen.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Student ne postoji.");
                }
            }
            
            
        }

        private void IzmeniForm_Load_1(object sender, EventArgs e)
        {
            txtId.Text = Form1.es.Id.ToString();
            txtIme.Text = Form1.es.FirstName;
            txtPrezime.Text = Form1.es.LastName;
            txtJmbg.Text = Form1.es.JMBG;
        }
    }
}

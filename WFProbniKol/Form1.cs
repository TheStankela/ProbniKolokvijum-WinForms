using WFProbniKol.Interface;
using WFProbniKol.Model;
using WFProbniKol.Repository;

namespace WFProbniKol
{
    public partial class Form1 : Form
    {
        StudentRepository _studentRepository = new StudentRepository();
        public static Student es = new Student();
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            ListViewRefresh();
            ButtonsEnabled(false);
        }
        public void ClearButtons()
        {
            txtId.Clear();
            txtIme.Clear();
            txtPrezime.Clear();
            txtJmbg.Clear();
        }

        public void ButtonsEnabled(bool value)
        {
            txtIme.Enabled = value;
            txtPrezime.Enabled = value;
            txtJmbg.Enabled = value;
        }


        public void btnRefresh_Click(object sender, EventArgs e)
        {
            ListViewRefresh();

        }

        public void ListViewRefresh()
        {
            listView1.Items.Clear();
            var studenti = _studentRepository.GetStudents();
            foreach (var item in studenti)
            {
                ListViewItem l = new ListViewItem(item.Id.ToString());
                l.SubItems.Add(item.FirstName);
                l.SubItems.Add(item.LastName);
                l.SubItems.Add(item.JMBG);

                listView1.Items.Add(l);
            }
        }

        public void btnDodaj_Click(object sender, EventArgs e)
        {
            ButtonsEnabled(true);
            ClearButtons();
        }

        public void btnSacuvaj_Click(object sender, EventArgs e)
        {
            var s = new Student();
            s.FirstName = txtIme.Text;
            s.LastName = txtPrezime.Text;
            s.JMBG = txtJmbg.Text;
            if (!ValidJMBG(s.JMBG))
            {
                MessageBox.Show("JMBG nije validan.");
            }
            else {
                if (!_studentRepository.AddStudent(s))
                {
                    MessageBox.Show("Greska pri unosenju studenta.");
                }
                else
                {
                    MessageBox.Show("Student uspesno dodat u bazu.");
                    ListViewRefresh();
                    ButtonsEnabled(false);
                    ClearButtons();
                }
            }
            
           
            
        }

        public void btnObrisi_Click(object sender, EventArgs e)
        {
            if (txtId.Text == string.Empty)
            {
                MessageBox.Show("Selektujte studenta kog zelite da obrisete.");
            }
            else {
                var studentid = int.Parse(txtId.Text);
                var studenti = _studentRepository.GetStudents();
                if (studenti.Any(s => s.Id == studentid))
                {
                    if (!_studentRepository.DeleteStudent(studentid))
                    {
                        MessageBox.Show("Greska prilikom brisanja studenta.");
                    }
                    MessageBox.Show("Uspesno obrisan student.");
                    ClearButtons();
                    ListViewRefresh();
                }
                else
                {
                    MessageBox.Show("Student ne postoji u bazi.");
                }
                
            }
        }

        public void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                txtId.Text = item.SubItems[0].Text;
                txtIme.Text = item.SubItems[1].Text;
                txtPrezime.Text = item.SubItems[2].Text;
                txtJmbg.Text = item.SubItems[3].Text;

            }
            else
            {
                txtId.Text = string.Empty;
                txtIme.Text = string.Empty;
                txtPrezime.Text = string.Empty;
                txtJmbg.Text = string.Empty;
            }
        }

        public void btnPretrazi_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                MessageBox.Show("Unesite Ime, Prezime ili JMBG studenta za filtriranje.");
            }
            else
            {
                var querry = txtSearch.Text;
                List<Student> studenti = _studentRepository.GetStudent(querry);
                listView1.Items.Clear();
                foreach (var item in studenti)
                {
                    ListViewItem l = new ListViewItem(item.Id.ToString());
                    l.SubItems.Add(item.FirstName);
                    l.SubItems.Add(item.LastName);
                    l.SubItems.Add(item.JMBG);

                    listView1.Items.Add(l);
                }

            }

        }

        public void btnIzmeni_Click(object sender, EventArgs e)
        {
            var izmeniForma = new IzmeniForm();
            es.Id = int.Parse(txtId.Text);
            es.FirstName = txtIme.Text;
            es.LastName = txtPrezime.Text;
            es.JMBG = txtJmbg.Text;
            izmeniForma.Show();

        }

        public static bool ValidJMBG(string jmbg)
        {
            if (jmbg.Length != 13) 
            { 
                return false; 
            }
            
            return true;
           
        }
    }
}
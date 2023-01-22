using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFProbniKol.Data
{
    public static class Connection
    {
        public static string ConnectionString { get; set; } = "Data Source=.;Initial Catalog=Studenti;Integrated Security=True;";
    }
}

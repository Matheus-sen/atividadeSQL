using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Conexao_SQL
{
    public partial class CadastrarUsuário: Form
    {
        MySqlConnection Conexao;

        public string data_source = "datasource=LocalHost;username=root;password=;database=Atividade_Conexao";

        public CadastrarUsuário()
        {
            InitializeComponent();
        }
    }
}

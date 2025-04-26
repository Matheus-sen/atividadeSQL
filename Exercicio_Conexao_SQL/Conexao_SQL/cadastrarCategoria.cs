using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Conexao_SQL
{
    public partial class cadastrarCategoria: Form
    {

        MySqlConnection Conexao;

        public string data_source = "datasource=LocalHost;username=root;password=;database=Atividade_Conexao";

        public List<string> dadosCategoria;
        public int ?id_produto_selecionado = null;
        public string nome_categoria = "";

        private int IdCategoria = 1; // Variável para armazenar o próximo ID
        public cadastrarCategoria()
        {
            InitializeComponent();
            SelectCategoria();
        }

        private void Limpar()
        {
            txtNomeCategoria.Clear();

        }

        public void Erro(string mensagem)
        {
            MessageBox.Show(mensagem, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void Sucesso(string mensagem)
        {
            MessageBox.Show(mensagem, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void SelectCategoria()
        {

            string Categoria = txtNomeCategoria.Text.Trim().ToUpper();

            Conexao = new MySqlConnection(data_source);

            string sql = "SELECT nome_categoria FORM categoria";

            Conexao.Open();

            MySqlCommand vercmd = new MySqlCommand(sql, Conexao);
            vercmd.Parameters.AddWithValue("@categoria", Categoria);

            MySqlDataReader reader = vercmd.ExecuteReader();

            while (reader.Read())
            {
                string[]row =
            }


            dadosCategoria = new List<string> {  };
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {

            Conexao = new MySqlConnection(data_source);
            Conexao.Open();
            MySqlCommand catcmd = new MySqlCommand();
            catcmd.Connection = Conexao;

            

            if (string.IsNullOrEmpty(Categoria))
            {
                Erro("Campo Categoria não pode estar Vazia!");
                return;
            }
            else if(dadosCategoria.Contains(Categoria, StringComparer.OrdinalIgnoreCase))
            {
                Erro("Categoria já existente!");
                return;
            }
            else
            {
                catcmd.Parameters.Clear(); // limpa os parâmetros antigos
                catcmd.CommandText =
                    "INSERT INTO categoria " +
                    "(nome_categoria) " +
                    "VALUES " +
                    "(@nome_categoria)";

                catcmd.Parameters.AddWithValue("@nome_categoria", Categoria);

                catcmd.ExecuteNonQuery();
                Sucesso("Categoria Cadastrada com sucesso!");
                IdCategoria++; // Incrementa o próximo ID
                txtIdCategoria.Text = IdCategoria.ToString(); // Atualiza o campo de ID

            }

            // foreach para pegar o id da categoria selecionada
            // verificar se o id já existe
            // tratar caso o id exista

            Limpar();
        }


        private void cadastrarCategoria_Load(object sender, EventArgs e)
        {
            txtIdCategoria.Text = IdCategoria.ToString(); // Inicializa o campo de ID
            txtIdCategoria.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

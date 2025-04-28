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

        public string data_source = "datasource=LocalHost;username=root;password=55333891;database=Atividade_Conexao";

        public List<string> dadosCategoria = new List<string>(); // usado para inicializar a lista aqui
        public int ?id_produto_selecionado = null;
        public string nome_categoria = "";
        private int IdCategoria = 1; // Variável para armazenar o próximo ID

        public cadastrarCategoria()
        {
            InitializeComponent();
            VerificarCategoria();
            CarregarProximoIdDoBanco(); //Chamada para a lógica de buscar o ID diretamente aqui 
        }

        private void CarregarProximoIdDoBanco()
        {
            Conexao = new MySqlConnection(data_source);
            try
            {
                Conexao.Open();
                string selectIdCatBanco = "SELECT MAX(id_categoria) FROM categoria";
                MySqlCommand selectbanc = new MySqlCommand(selectIdCatBanco, Conexao);
                object resultadoMax = selectbanc.ExecuteScalar();

                if (resultadoMax != DBNull.Value && resultadoMax != null)
                {
                    IdCategoria = Convert.ToInt32(resultadoMax) + 1;
                }
                else
                {
                    IdCategoria = 1;
                }    

            }
            catch (MySqlException ex)
            {
                Erro($"Erro ao carregar o próximo ID: {ex.Message}");
            }
            finally
            {
                Conexao.Close();
            }
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

        public void VerificarCategoria()
        {

           // string Categoria = txtNomeCategoria.Text.Trim().ToUpper();

            Conexao = new MySqlConnection(data_source);

            try
            {
                Conexao.Open();
                string select = "SELECT nome_categoria FROM categoria";
                MySqlCommand vercmd = new MySqlCommand(select, Conexao);
                MySqlDataReader reader = vercmd.ExecuteReader();
                dadosCategoria.Clear(); // Limpa a lista antes de carregar novamente
                while (reader.Read())
                {
                    dadosCategoria.Add(reader.GetString(0).Trim().ToUpper()); // Adiciona a categoria à lista já no formato desejado
                }
            }
            catch (MySqlException ex)
            {
                Erro($"Erro ao carregar categorias: {ex.Message}");
            }
            finally
            {
                Conexao.Close();
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {  
            string newCategoria = txtNomeCategoria.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(txtNomeCategoria.Text))
            {
                Erro("Campo Categoria não pode estar Vazia!");
                return;
            }
            else if(dadosCategoria.Contains(txtNomeCategoria.Text, StringComparer.OrdinalIgnoreCase))
            {
                Erro("Categoria já existente!");
                return;
            }
            else
            {
                Conexao = new MySqlConnection(data_source);

                try
                {
                    Conexao.Open();
                    string inserir = "INSERT INTO categoria " +
                                     "(nome_categoria) " +
                                     "VALUES " +
                                     "(@nome_categoria)";

                    MySqlCommand catcmd = new MySqlCommand(inserir, Conexao);
                    catcmd.Parameters.AddWithValue("@nome_categoria", txtNomeCategoria.Text);
                    catcmd.ExecuteNonQuery();
                    Sucesso("Categoria Cadastrada com sucesso!");
                    IdCategoria++; // Incrementa o próximo ID
                    txtIdCategoria.Text = IdCategoria.ToString(); // Atualiza o campo de ID
                    VerificarCategoria();
                    Limpar();
                }
                catch (MySqlException ex)
                {
                    Erro($"Erro ao cadastrar categoria: {ex.Message}");
                }
                finally
                {
                    Conexao.Close();
                }

            }

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

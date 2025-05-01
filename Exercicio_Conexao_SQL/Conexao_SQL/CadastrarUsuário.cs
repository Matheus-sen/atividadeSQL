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

        public int IdUsuario = 1;
        public string data_source = "datasource=LocalHost;username=root;password=;database=Atividade_Conexao";
        

        public CadastrarUsuário()
        {
            InitializeComponent();
            CarregarProximoIdBancoUsuario();
        }

        public void Erro(string mensagem) 
        {
            MessageBox.Show(mensagem, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void Sucesso(string mensagem) 
        {
            MessageBox.Show(mensagem, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void CarregarProximoIdBancoUsuario()
        {
            Conexao = new MySqlConnection(data_source);
            try  
            {
                Conexao.Open(); 
                string selectIdUsuBanco = "SELECT MAX(id_usuario) FROM login"; 
                MySqlCommand selectbanc = new MySqlCommand(selectIdUsuBanco, Conexao);
                object resultadoMax = selectbanc.ExecuteScalar();

                if (resultadoMax != DBNull.Value && resultadoMax != null)
                {
                    IdUsuario = Convert.ToInt32(resultadoMax) + 1;
                }
                else
                {
                    IdUsuario = 1;
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

        private void CadastrarUsuário_Load(object sender, EventArgs e)
        {
            txtIdUsuarioCadastrado.Text = IdUsuario.ToString();
            txtIdUsuarioCadastrado.Enabled = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

namespace Conexao_SQL
{
    public partial class Login : Form
    {
        MySqlConnection Conexao;

        public string data_source = "datasource=LocalHost;username=root;password=;database=Atividade_Conexao";

        public Login()
        {
            InitializeComponent();
            UsuarioPadrao();

        }

        private void txtUser_Enter(object sender, EventArgs e)
        {

            if (txtUser.Text == "User/Login...")
            {
                txtUser.Text = "";
                txtUser.ForeColor = Color.Black;
            }

        }

        private void txtUser_Leave(object sender, EventArgs e)
        {

            if (txtUser.Text == "")
            {
                txtUser.Text = "User/Login...";
                txtUser.ForeColor = Color.Gray;
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {

            txtUser.Text = "User/Login...";
            txtUser.ForeColor = Color.Gray;

            txtSenha.Text = "Senha...";
            txtSenha.ForeColor = Color.Gray;

        }

        private void txtSenha_Enter(object sender, EventArgs e)
        {
            if (txtSenha.Text == "Senha...")
            {
                txtSenha.Text = "";
                txtSenha.ForeColor = Color.Black;
            }
        }

        private void txtSenha_Leave(object sender, EventArgs e)
        {
            if (txtSenha.Text == "")
            {
                txtSenha.Text = "Senha...";
                txtSenha.ForeColor = Color.Gray;
                //txtSenha.PasswordChar = '*';
            }
        }

        private void UsuarioPadrao()
        {
            Conexao = new MySqlConnection(data_source);

            try
            {
                Conexao.Open();
                string inserirUsuarioPadrao = "INSERT INTO login " +
                                              "(user, senha, tipo_usuario)" +
                                              "VALUES" +
                                              "(admin, admin, Administrador)";
                MySqlCommand catcmd = new MySqlCommand(inserirUsuarioPadrao, Conexao);

                if()





                /*
                 Conexao.Open();
                    //variável do tipo string que armazena o comando do tipo SQL para ser usado depois
                    string inserir = "INSERT INTO categoria " +
                                     "(nome_categoria) " +
                                     "VALUES " +
                                     "(@nome_categoria)";

                    MySqlCommand catcmd = new MySqlCommand(inserir, Conexao);
                    catcmd.Parameters.AddWithValue("@nome_categoria", txtNomeCategoria.Text); // atribui o valor do meu parametro, no caso o que o usuário digitou.
                    catcmd.ExecuteNonQuery();
                    Sucesso("Categoria Cadastrada com sucesso!");
                    IdCategoria++; // Incrementa o próximo ID
                    txtIdCategoria.Text = IdCategoria.ToString(); // Atualiza o campo de ID
                    ArmazenarCategoria();
                    Limpar();
                 */


            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erro : {ex.Message}");
            }
            finally
            {
                Conexao.Close();
            }

        }

    }
}

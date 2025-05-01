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
        public int IdUsuario = 1;
        MySqlConnection Conexao;

        public string data_source = "datasource=LocalHost;username=root;password=;database=Atividade_Conexao";

        public Login()
        {
            InitializeComponent();
            UsuarioPadrao();
            CarregarProximoIdBancoUsuario();

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
                txtSenha.PasswordChar = '*';

            }
        }

        private void txtSenha_Leave(object sender, EventArgs e)
        {
            if (txtSenha.Text == "")
            {
                txtSenha.Text = "Senha...";
                txtSenha.ForeColor = Color.Gray;
            }
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
                MessageBox.Show($"Erro ao criar usuário padrão: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }
        }

        //1º
        private void UsuarioPadrao()
        {
            Conexao = new MySqlConnection(data_source);

            try
            {
                Conexao.Open();
                string verificarAdmin = "SELECT COUNT(*) FROM login WHERE user = 'admin'";
                MySqlCommand verificarCmd = new MySqlCommand(verificarAdmin, Conexao);
                int count = Convert.ToInt32(verificarCmd.ExecuteScalar());


                if (count == 0)
                {
                    string inserirUsuarioPadrao = "INSERT INTO login (user, senha, tipo_usuario) VALUES ('admin', 'admin', 'Administrador')";
                    MySqlCommand insertcmd = new MySqlCommand(inserirUsuarioPadrao, Conexao);
                    insertcmd.ExecuteNonQuery();
                    MessageBox.Show("Usuário administrador padrão criado com sucesso!", "Primeiro Acesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erro ao criar usuário padrão: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            string usuarioDigitado = txtUser.Text.Trim().ToUpper();
            string senhaDigitada = txtSenha.Text;

            if (string.IsNullOrEmpty(usuarioDigitado) || usuarioDigitado == "User/Login...")
            {
                MessageBox.Show("Campo 'User/Login...' precisa estar preenchido","Aviso",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUser.Focus();
            }

            if (string.IsNullOrEmpty(senhaDigitada) || senhaDigitada == "Senha...")
            {
                MessageBox.Show("Campo 'Senha...' precisa estar preenchido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha.Focus();
            }

            Conexao = new MySqlConnection(data_source);

            try
            {
                Conexao.Open();

                string selectBanco = "SELECT tipo_usuario FROM login WHERE user = @user AND senha = @senha";
                MySqlCommand cmdTipoU = new MySqlCommand(selectBanco, Conexao);
                cmdTipoU.Parameters.AddWithValue("@user", usuarioDigitado);
                cmdTipoU.Parameters.AddWithValue("@senha", senhaDigitada);

                string tipoUsuario = (string)cmdTipoU.ExecuteScalar();

                if (tipoUsuario == "Administrador")
                {
                    Menu menuForm = new Menu();
                    menuForm.ShowDialog(); 
                }
                else if (tipoUsuario == "Simples")
                {
                    Visualizar visualizarForm = new Visualizar();
                    visualizarForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Usuário ou senha incorretos.", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUser.Focus();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erro ao conectar ou consultar o banco de dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }
        }
    }

}


//1° - Precisa fazer o cadastro do usuário padrão e verificar se ele já existe;

//2º - Precisa fazer o método para quando iniciar o form outra vez, ele logar com ou o mesmo admin ou com o novo usuário e tipo cadastrado, fazendo com o que inicie o form visualizar caso seja simples e menu em diante caso seja administrador;
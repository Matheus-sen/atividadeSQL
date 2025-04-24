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

namespace Conexao_SQL
{
    public partial class cadastrarCategoria: Form
    {
        private int IdCategoria = 1; // Variável para armazenar o próximo ID
        public cadastrarCategoria()
        {
            InitializeComponent();
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

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNomeCategoria.Text))
            {
                Erro("Campo Categoria não pode estar Vazia!");
                return;
            }
            else
            {
                Sucesso("Categoria Cadastrada com sucesso!");
                IdCategoria++; // Incrementa o próximo ID
                txtIdCategoria.Text = IdCategoria.ToString(); // Atualiza o campo de ID
             
            }

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

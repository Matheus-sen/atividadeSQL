using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conexao_SQL
{
    public partial class cadastrarProduto: Form
    {
        private int IdProduto = 1;
        public cadastrarProduto()
        {
            InitializeComponent();
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
            if (string.IsNullOrEmpty(txtNomeProduto.Text) || string.IsNullOrEmpty(cbxCategoriaProduto.Text))
            {
                Erro("Não pode conter campos vazios!");
                return;
            }
            else
            {
                Sucesso("Produto Cadastrado com sucesso!");
                IdProduto++; 
                txtIdProduto.Text = IdProduto.ToString(); 

            }
        }

        private void cadastrarProduto_Load(object sender, EventArgs e)
        {
            txtIdProduto.Text = IdProduto.ToString();
            txtIdProduto.Enabled = false;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

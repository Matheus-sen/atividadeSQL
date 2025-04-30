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
    public partial class Menu: Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void cadastrarCategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cadastrarCategoria form = new cadastrarCategoria();
            form.ShowDialog();
        }

        private void cadastrarProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cadastrarProduto form = new cadastrarProduto();
            form.ShowDialog();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Visualizar form = new Visualizar();
            form.ShowDialog();
        }

        private void sairToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cadastrarUsuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadastrarUsuário form = new CadastrarUsuário();
            form.ShowDialog();
        }
    }
}

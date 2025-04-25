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
    public partial class Visualizar: Form
    {
        public Visualizar()
        {
            InitializeComponent();
            //exibe as linhas das colunas e linhas
            lstVisualizar.View = View.Details;
            //
            lstVisualizar.LabelEdit = true;
            //mexe na ordem das colunas
            lstVisualizar.AllowColumnReorder = true;
            //selecionar linha completa
            lstVisualizar.FullRowSelect = true;
            //
            lstVisualizar.GridLines = true;
        }
    }
}

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
    public partial class Visualizar: Form
    {

        public string data_source = "datasource=LocalHost;username=root;password=;database=Atividade_Conexao";
        MySqlConnection Conexao;
        public Visualizar()
        {
            InitializeComponent();
            lstVisualizar.View = View.Details;//exibe as linhas das colunas e linhas
            lstVisualizar.LabelEdit = true;       
            lstVisualizar.AllowColumnReorder = true; //mexe na ordem das colunas
            lstVisualizar.FullRowSelect = true; //selecionar linha completa
            lstVisualizar.GridLines = true;


            lstVisualizar.Columns.Add("ID Produto", 90, HorizontalAlignment.Left);
            lstVisualizar.Columns.Add("Produto", 400, HorizontalAlignment.Left);
            lstVisualizar.Columns.Add("Categoria", 300, HorizontalAlignment.Left);
            carregar_contatos();
        }

        private void carregar_contatos()
        {
            try
            {

                // Criar a conexão com o MySQL
                Conexao = new MySqlConnection(data_source);

                string info = "SELECT p.id_produto, p.nome_produto, c.nome_categoria " +
                              "FROM produto p " +
                              "INNER JOIN categoria c ON p.id_categoria = c.id_categoria";

                Conexao.Open();

                // Buscar as informações
                MySqlCommand buscar = new MySqlCommand(info, Conexao);

                // armazena as informacoes que temos na busca para mostrar na tela
                MySqlDataReader reader = buscar.ExecuteReader();

                // como iremos mostrar os dados na tela para o usuário
                lstVisualizar.Items.Clear();

                while (reader.Read())
                {
                    string[] row =
                    {
                        // obtendo as informações do banco de dados (vetor de strings)
                        reader.GetInt32(0).ToString(), // id produto
                        reader.GetString(1),           // nome produto
                        reader.GetString(2),           // categoria produto
                    };

                    var linha_list_view = new ListViewItem(row);
                    lstVisualizar.Items.Add(linha_list_view);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.Close();
            }
        }

    }
}

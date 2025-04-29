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

        public List<string> dadosCategoria = new List<string>(); // usado para inicializar a lista aqui que receberá as categorias
        //public int ?id_produto_selecionado = null; EXCLUIR
        //public string nome_categoria = ""; EXCLUIR
        private int IdCategoria = 1; // Variável para armazenar o próximo ID

        public cadastrarCategoria()
        {
            InitializeComponent();
            ArmazenarCategoria();
            CarregarProximoIdDoBanco(); //Chamada para a lógica de buscar o ID diretamente aqui ao inicializar o form
        }

        // Método responsável por verificar e trazer ultimo id cadastrado(Maior id dentre os existentes)
        private void CarregarProximoIdDoBanco()
        {
            Conexao = new MySqlConnection(data_source);
            try //bloco de código para exceções 
            {
                Conexao.Open(); // Abre a conexão 
                string selectIdCatBanco = "SELECT MAX(id_categoria) FROM categoria"; // faz a verificação direta no banco e tras o número Maior MAX do ID 
                MySqlCommand selectbanc = new MySqlCommand(selectIdCatBanco, Conexao); // cria um objeto com o nome selectbanc, que recebe o número maior do ID pela string e a conexão data_source, comando para ser usado no banco SQL
                object resultadoMax = selectbanc.ExecuteScalar(); //object indica uma variável genérica(recebe qualquer tipo de dado), recebendo o valor selectbanc = selectIdBanco = (MAX, ID).


                // condição if (se), verifica se o valor da variável resultadoMax não é um valor nulo no Banco (DBNull.Value) e com o operador lógico && (and/e), verifica se o valor da variável não é nulo no C#.
                if (resultadoMax != DBNull.Value && resultadoMax != null)
                {
                    //se não for nulo, o id recebe o valor do object do banco convertido para inteiro (INT) e acrescenta + 1 para ter o ID correspondente.
                    IdCategoria = Convert.ToInt32(resultadoMax) + 1;
                }
                // caso seja nulo, o id categoria recebe 1, ou seja, o primeiro cadastro.
                else
                {
                    IdCategoria = 1;
                }    

            }
            // caso ocorra um exceção, ele será executado:
            catch (MySqlException ex)
            {
                Erro($"Erro ao carregar o próximo ID: {ex.Message}");
            }
            //
            finally
            {
                Conexao.Close();
            }
        }

        //Método criado para limpar o formulário sempre que chamado.
        private void Limpar()
        {
            txtNomeCategoria.Clear();

        }

        //Método criado para evitar repetição caso tenha algum erro ou sucesso no código e precise usar
        public void Erro(string mensagem) //Método do erro
        {
            MessageBox.Show(mensagem, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void Sucesso(string mensagem) // Método do Sucesso
        {
            MessageBox.Show(mensagem, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public void ArmazenarCategoria()
        {

            Conexao = new MySqlConnection(data_source);

            try
            {
                Conexao.Open(); // abre a conexão
                string select = "SELECT nome_categoria FROM categoria"; // cria uma variavel do tipo string que recebe informação do banco (nome_categoria)
                MySqlCommand vercmd = new MySqlCommand(select, Conexao); //cria um objeto com o nome vercmd, que recebe o construtor com o SELECT da string select.
                MySqlDataReader reader = vercmd.ExecuteReader(); // declara a variavel chamada reader, que recebe a minha variável vercmd. o objeto MySqlDataReader me permite que eu acesse cada linha da resposta da consulta.
                dadosCategoria.Clear(); // Limpa a lista antes de carregar novamente
                while (reader.Read()) //loop while enquanto for verdadeiro, o método avança o leitor (Read) para a próxima linha, e se caso conter informação, ela retorna true, caso contrário retorna false.
                {
                    dadosCategoria.Add(reader.GetString(0).Trim().ToUpper()); // Adiciona a categoria à lista já no formato desejado, retornando o valor no indice específico, nesse caso minha coluna 0 (nome_categoria), cada reader.Read é uma linha, se há informação, ele continua e guarda essa minha categoria em dadosCategoria.
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

        //Ação do botão Cadastrar:
        private void btnCadastrar_Click(object sender, EventArgs e)
        {  
            //variável que armazena meu valor de txtNomeCategoria.Text no form
            string Categoria = txtNomeCategoria.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(Categoria)) // verifica se minha sring está nula
            {
                // mensagem caso a condição a cima seja verdadeiro
                Erro("Campo Categoria não pode estar Vazia!");
                return;
            }
            //senão, se: compara minha categoria digitada com as existentes, caso não seja igual, pula a condição, caso contrário executa:
            else if(dadosCategoria.Contains(Categoria, StringComparer.OrdinalIgnoreCase))
            {
                //Mensagem caso a condição for verdadeira.
                Erro("Categoria já existente!");
                return;
            }
            // se nenhuma das condições a cima for verdadeira:
            else
            {
                Conexao = new MySqlConnection(data_source);

                try
                {
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
            txtIdCategoria.Enabled = false; // deixa o campo do ID desabilitado par alteração
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close(); // fecha o form/volta
        }
    }
}


/*
 Eficiência: O ExecuteScalar() é mais eficiente para consultas que retornam um único valor porque ele otimiza a recuperação de dados para trazer apenas essa informação. Ele não carrega um conjunto de resultados completo com várias linhas e colunas na memória, como outros métodos de execução de comandos (por exemplo, ExecuteReader).

Simplicidade: Ele simplifica o código necessário para obter o valor desejado. Se usássemos outros métodos, como ExecuteReader(), teríamos que:

Abrir um MySqlDataReader.
Verificar se o DataReader possui alguma linha (reader.HasRows).
Ler a primeira linha (reader.Read()).
Acessar a primeira coluna dessa linha (reader.GetValue(0) ou reader["NomeDaColuna"]).
Converter o valor para o tipo desejado.
Fechar o DataReader.
O ExecuteScalar() faz tudo isso de forma concisa em uma única chamada de método.
 */
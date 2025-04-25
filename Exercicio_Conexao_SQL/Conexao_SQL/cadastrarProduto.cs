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







/*
 Com certeza! Vamos analisar o código do formulário `cadastrarProduto` linha por linha para entender a função de cada parte:

```csharp
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
```

* **`using System;`**: Importa o namespace `System`, que contém classes base e tipos de dados fundamentais (como `EventArgs`).
* **`using System.Collections.Generic;`**: Importa o namespace `System.Collections.Generic`, que fornece interfaces e classes que definem coleções genéricas (como `List<T>`, `KeyValuePair<TKey, TValue>`).
* **`using System.ComponentModel;`**: Importa o namespace `System.ComponentModel`, que fornece interfaces para implementar o comportamento de tempo de design e tempo de execução de componentes e controles.
* **`using System.Data;`**: Importa o namespace `System.Data`, que fornece classes para acessar e gerenciar dados de várias fontes de dados.
* **`using System.Drawing;`**: Importa o namespace `System.Drawing`, que fornece funcionalidades básicas de desenho do GDI+.
* **`using System.Linq;`**: Importa o namespace `System.Linq`, que adiciona métodos de extensão para trabalhar com coleções de dados (LINQ - Language Integrated Query).
* **`using System.Text;`**: Importa o namespace `System.Text`, que fornece classes para codificação, decodificação, conversão e manipulação de texto.
* **`using System.Threading.Tasks;`**: Importa o namespace `System.Threading.Tasks`, que fornece suporte para operações assíncronas.
* **`using System.Windows.Forms;`**: Importa o namespace `System.Windows.Forms`, que contém classes para criar interfaces de usuário baseadas no Windows Forms.
* **`using MySql.Data.MySqlClient;`**: Importa o namespace `MySql.Data.MySqlClient`, que fornece as classes necessárias para conectar e interagir com bancos de dados MySQL.

```csharp
namespace Conexao_SQL
{
    public partial class cadastrarProduto: Form
    {
```

* **`namespace Conexao_SQL`**: Declara um namespace chamado `Conexao_SQL`. Namespaces ajudam a organizar o código e evitar conflitos de nomes entre diferentes partes do programa ou bibliotecas.
* **`public partial class cadastrarProduto: Form`**: Declara uma classe pública parcial chamada `cadastrarProduto`. A palavra-chave `partial` indica que a definição dessa classe pode estar dividida em vários arquivos (geralmente, o código gerado pelo designer do formulário fica em outro arquivo). A classe herda de `Form`, o que significa que ela representa uma janela ou formulário na sua aplicação Windows Forms.

```csharp
        private int IdProduto = 1;
        public string data_source = "datasource=LocalHost;username=root;password=;database=Atividade_Conexao";
        MySqlConnection Conexao;
```

* **`private int IdProduto = 1;`**: Declara uma variável privada do tipo inteiro chamada `IdProduto` e a inicializa com o valor 1. Pelo nome, parece que seria usada para controlar um ID de produto, mas no código do botão `Cadastrar`, ela é simplesmente incrementada a cada cadastro, sem verificar duplicidade no banco. Em um cenário real, o ID do produto geralmente é gerenciado pelo banco de dados.
* **`public string data_source = "datasource=LocalHost;username=root;password=;database=Atividade_Conexao";`**: Declara uma variável pública do tipo string chamada `data_source`. Essa string contém as informações necessárias para conectar ao seu banco de dados MySQL:
    * `datasource=LocalHost`: Especifica o servidor MySQL (neste caso, rodando localmente).
    * `username=root`: Especifica o nome de usuário para conectar ao banco.
    * `password=`: Especifica a senha do usuário (neste caso, está vazia). **Em aplicações reais, evite armazenar senhas diretamente no código.**
    * `database=Atividade_Conexao`: Especifica o nome do banco de dados a ser utilizado.
* **`MySqlConnection Conexao;`**: Declara uma variável de instância do tipo `MySqlConnection` chamada `Conexao`. Essa variável será usada para representar a conexão com o banco de dados MySQL.

```csharp
        public cadastrarProduto()
        {
            InitializeComponent();
            CarregarCategorias(); // Chama o método para carregar as categorias ao abrir o form
        }
```

* **`public cadastrarProduto()`**: Este é o construtor da classe `cadastrarProduto`. Ele é chamado quando uma nova instância do formulário é criada.
* **`InitializeComponent();`**: Este método é gerado automaticamente pelo designer do Windows Forms e é responsável por inicializar os componentes visuais do formulário (botões, caixas de texto, `ComboBox`, etc.) com base no que você definiu no ambiente de design.
* **`CarregarCategorias();`**: Chama o método `CarregarCategorias()` (definido mais abaixo) para buscar as categorias do banco de dados e preencher o `ComboBox` (`cbxCategoriaProduto`) assim que o formulário é aberto.

```csharp
        public void Erro(string mensagem)
        {
            MessageBox.Show(mensagem, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
```

* **`public void Erro(string mensagem)`**: Declara um método público chamado `Erro` que recebe uma string `mensagem` como argumento.
* **`MessageBox.Show(mensagem, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);`**: Exibe uma caixa de mensagem para o usuário com a `mensagem` especificada. O título da caixa de mensagem é "Erro", possui um botão "OK" e exibe um ícone de erro. Este método é usado para informar o usuário sobre algum problema ou erro que ocorreu.

```csharp
        public void Sucesso(string mensagem)
        {
            MessageBox.Show(mensagem, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
```

* **`public void Sucesso(string mensagem)`**: Declara um método público chamado `Sucesso` que recebe uma string `mensagem` como argumento.
* **`MessageBox.Show(mensagem, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);`**: Exibe uma caixa de mensagem para o usuário com a `mensagem` especificada. O título da caixa de mensagem é "Info", possui um botão "OK" e exibe um ícone de informação. Este método é usado para informar o usuário sobre uma operação bem-sucedida.

```csharp
        private void CarregarCategorias()
        {
            Conexao = new MySqlConnection(data_source);
            try
            {
                Conexao.Open();
                string query = "SELECT id_categoria, nome_categoria FROM categoria";
                MySqlCommand comando = new MySqlCommand(query, Conexao);
                MySqlDataReader leitor = comando.ExecuteReader();

                if (leitor.HasRows)
                {
                    List<KeyValuePair<int, string>> categorias = new List<KeyValuePair<int, string>>();
                    while (leitor.Read())
                    {
                        categorias.Add(new KeyValuePair<int, string>(leitor.GetInt32("id_categoria"), leitor.GetString("nome_categoria")));
                    }

                    cbxCategoriaProduto.DataSource = categorias;
                    cbxCategoriaProduto.DisplayMember = "Value"; // Exibe o nome da categoria
                    cbxCategoriaProduto.ValueMember = "Key";   // Armazena o ID da categoria
                }
                else
                {
                    cbxCategoriaProduto.Items.Add("Nenhuma categoria cadastrada");
                    cbxCategoriaProduto.Enabled = false; // Desabilita se não houver categorias
                }
            }
            catch (MySqlException ex)
            {
                Erro($"Erro ao carregar categorias: {ex.Message}");
            }
            finally
            {
                if (Conexao != null && Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
            }
        }
```

* **`private void CarregarCategorias()`**: Declara um método privado chamado `CarregarCategorias`. Este método é responsável por buscar as categorias do banco de dados e popular o `ComboBox`.
* **`Conexao = new MySqlConnection(data_source);`**: Cria uma nova instância da classe `MySqlConnection` usando a string de conexão definida em `data_source`.
* **`try { ... } catch (MySqlException ex) { ... } finally { ... }`**: Este é um bloco de tratamento de exceções. O código dentro do bloco `try` é executado. Se ocorrer uma exceção do tipo `MySqlException` (erros relacionados ao MySQL), o código dentro do bloco `catch` é executado. O código dentro do bloco `finally` é sempre executado, independentemente de ter ocorrido ou não uma exceção.
* **`Conexao.Open();`**: Abre a conexão com o banco de dados MySQL.
* **`string query = "SELECT id_categoria, nome_categoria FROM categoria";`**: Define uma string `query` contendo o comando SQL `SELECT` para buscar as colunas `id_categoria` e `nome_categoria` da tabela `categoria`.
* **`MySqlCommand comando = new MySqlCommand(query, Conexao);`**: Cria um novo objeto `MySqlCommand` associado à `query` SQL e à `Conexao` aberta. Este objeto será usado para executar o comando no banco de dados.
* **`MySqlDataReader leitor = comando.ExecuteReader();`**: Executa o comando SQL `SELECT` e retorna um `MySqlDataReader`. Este objeto permite ler os resultados da consulta linha por linha.
* **`if (leitor.HasRows)`**: Verifica se o `DataReader` possui alguma linha de resultado (ou seja, se existem categorias cadastradas no banco).
* **`List<KeyValuePair<int, string>> categorias = new List<KeyValuePair<int, string>>();`**: Cria uma nova lista genérica chamada `categorias`. Cada elemento desta lista será um `KeyValuePair`, onde a chave (`int`) representará o `id_categoria` e o valor (`string`) representará o `nome_categoria`.
* **`while (leitor.Read())`**: Loop que itera sobre cada linha de resultado retornada pelo `DataReader`.
* **`categorias.Add(new KeyValuePair<int, string>(leitor.GetInt32("id_categoria"), leitor.GetString("nome_categoria")));`**: Dentro do loop, para cada linha, um novo `KeyValuePair` é criado com o `id_categoria` (obtido como um inteiro da coluna "id\_categoria") e o `nome_categoria` (obtido como uma string da coluna "nome\_categoria") e adicionado à lista `categorias`.
* **`cbxCategoriaProduto.DataSource = categorias;`**: Define a propriedade `DataSource` do `ComboBox` (`cbxCategoriaProduto`) com a lista de `KeyValuePair` que contém as categorias. Isso associa os dados ao `ComboBox`.
* **`cbxCategoriaProduto.DisplayMember = "Value";`**: Define a propriedade `DisplayMember` do `ComboBox` para `"Value"`. Isso especifica que a parte "Value" de cada `KeyValuePair` (o `nome_categoria`) será o que será exibido na lista do `ComboBox`.
* **`cbxCategoriaProduto.ValueMember = "Key";`**: Define a propriedade `ValueMember` do `ComboBox` para `"Key"`. Isso especifica que a parte "Key" de cada `KeyValuePair` (o `id_categoria`) será o valor real associado a cada item selecionado no `ComboBox`. Você pode acessar esse valor posteriormente usando `cbxCategoriaProduto.SelectedValue`.
* **`else { ... }`**: Bloco executado se não houver nenhuma categoria cadastrada no banco de dados (`leitor.HasRows` é falso).
* **`cbxCategoriaProduto.Items.Add("Nenhuma categoria cadastrada");`**: Adiciona um item informativo ao `ComboBox`.
* **`cbxCategoriaProduto.Enabled = false;`**: Desabilita o `ComboBox` para indicar que não há categorias disponíveis para seleção.
* **`catch (MySqlException ex)`**: Bloco executado se ocorrer uma exceção do tipo `MySqlException` durante a tentativa de conectar ou executar a consulta no banco de dados.
* **`Erro($"Erro ao carregar categorias: {ex.Message}");`**: Chama o método `Erro` para exibir uma mensagem de erro para o usuário, incluindo detalhes da exceção (`ex.Message`).
* **`finally { ... }`**: Bloco sempre executado após o `try` e o `catch`.
* **`if (Conexao != null && Conexao.State == ConnectionState.Open)`**: Verifica se a conexão (`Conexao`) não é nula e se o seu estado é `Open` (ou seja, se estava aberta).
* **`Conexao.Close();`**: Fecha a conexão com o banco de dados, liberando os recursos utilizados. É importante fechar a conexão no bloco `finally` para garantir que ela seja fechada mesmo que ocorra um erro.

```csharp
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNomeProduto.Text) || cbxCategoriaProduto.SelectedItem == null)
            {
                Erro("Não pode conter campos vazios!");
                return;
            }
            else
            {
                Conexao = new MySqlConnection(data_source);
                try
                {
                    Conexao.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = Conexao;
                    cmd.CommandText = "INSERT INTO produto (nome_produto, id_categoria) VALUES (@nome_produto, @id_categoria)";
                    cmd.Parameters.AddWithValue("@nome_produto", txtNomeProduto.Text);
                    cmd.Parameters.AddWithValue("@id_categoria", ((KeyValuePair<int, string>)cbxCategoriaProduto.SelectedItem).Key);
                    cmd.ExecuteNonQuery();
                    Sucesso("Produto Cadastrado com sucesso!");
                    IdProduto++;
                    txtIdProduto.Text = IdProduto.ToString();
                    // Limpar outros campos do formulário aqui, se necessário
                }
                catch (MySqlException ex)
                {
                    Erro($"Erro ao cadastrar produto: {ex.Message}");
                }
                finally
                {
                    if (Conexao != null && Conexao.State == ConnectionState.Open)
                    {
                        Conexao.Close();
                    }
                }
            }
        }
```

* **`private void btnCadastrar_Click(object sender, EventArgs e)`**: Declara o evento que é acionado quando o botão de cadastrar (`btnCadastrar`) é clicado.
* **`if (string.IsNullOrEmpty(txtNomeProduto.Text) || cbxCategoriaProduto.SelectedItem == null)`**: Verifica se o campo de texto do nome do produto (`txtNomeProduto`) está vazio ou se nenhum item foi selecionado no `ComboBox` de categorias (`cbxCategoriaProduto.SelectedItem` é nulo).
* **`Erro("Não pode conter campos vazios!");`**: Se a condição acima for verdadeira (algum campo obrigatório está vazio), chama o método `Erro` para exibir uma mensagem de erro.
* **`return;`**: Sai do método `btnCadastrar_Click` sem executar o restante do código.
* **`else { ... }`**: Bloco executado se todos os campos obrigatórios estiverem preenchidos.
* **`Conexao = new MySqlConnection(data_source);`**: Cria uma nova instância de `MySqlConnection`.
* **`try { ... } catch (MySqlException ex) { ... } finally { ... }`**: Bloco de tratamento de exceções para a operação de inserção no banco de dados.
* **`Conexao.Open();`**: Abre a conexão com o banco de dados.
* **`MySqlCommand cmd = new MySqlCommand();`**: Cria um novo objeto `MySqlCommand`.
* **`cmd.Connection = Conexao;`**: Associa o comando à conexão aberta.
* **`cmd.CommandText = "INSERT INTO produto (nome_produto, id_categoria) VALUES (@nome_produto, @id_categoria)";`**: Define a string do comando SQL `INSERT` para inserir um novo produto na tabela `produto`. Utiliza parâmetros (`@nome_produto`, `@id_categoria`) para evitar injeção de SQL e tornar o código mais seguro.
* **`cmd.Parameters.AddWithValue("@nome_produto", txtNomeProduto.Text);`**: Adiciona um parâmetro chamado `@nome_produto` ao comando e atribui o valor do texto digitado no `txtNomeProduto`.
* **`cmd.Parameters.AddWithValue("@id_categoria", ((KeyValuePair<int, string>)cbxCategoriaProduto.SelectedItem).Key);`**: Adiciona um parâmetro chamado `@id_categoria` ao comando e atribui o valor da chave (`Key`) do item selecionado no `ComboBox`. Como o `DataSource` do `ComboBox` foi definido como uma lista de `KeyValuePair<int, string>`, `cbxCategoriaProduto.SelectedItem` retorna o `KeyValuePair` selecionado, e acessamos a chave (o `id_categoria`) através de `.Key`.
* **`cmd.ExecuteNonQuery();`**: Executa o comando `INSERT` no banco de dados. Este método retorna o número de linhas afetadas pela operação (geralmente 1 para uma inserção bem-sucedida).
* **`Sucesso("Produto Cadastrado com sucesso!");`**: Chama o método `Sucesso` para exibir uma mensagem de confirmação.
* **`IdProduto++;`**: Incrementa a variável `IdProduto`. Como mencionado antes, em
 
 */

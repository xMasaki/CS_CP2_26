using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace CP2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Variável para conexão com banco de dados
        /// </summary>
        string conexao = "Server=localhost;Database=Escola;Uid=root;Pwd=fiap1234;";

        public MainWindow()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Cadastra aluno no banco de dados
        /// </summary>
        //INSERT
        private void Inserir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Verifica se dado cadastrado tem nome e idade
                if (textoNome.Text == "" || textoIdade.Text == "")
                {
                    MessageBox.Show("Coloque Nome e Idade para cadastrar aluno");
                    return;
                }

                // Conecta ao banco de dados
                MySqlConnection conn = new MySqlConnection(conexao);
                // Abre conexão
                conn.Open();

                // Comando SQL
                string sql = "INSERT INTO Alunos (Nome, Idade) VALUES (@Nome, @Idade)";

                // Cria comando
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                // Adiciona parâmetros no comando
                cmd.Parameters.AddWithValue("@Nome", textoNome.Text);
                cmd.Parameters.AddWithValue("@Idade", textoIdade.Text);

                // Executa comando
                cmd.ExecuteNonQuery();

                MessageBox.Show("Aluno inserido");

                // Fecha conexão
                conn.Close();

                LimparCampo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Lista tabela Alunos
        /// </summary>
        //SELECT
        private void Listar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                MySqlConnection conn = new MySqlConnection(conexao);
                conn.Open();

                string sql = "SELECT * FROM Alunos";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                System.Data.DataTable tabela = new System.Data.DataTable();

                adapter.Fill(tabela);

                // Verifica se tem dados no banco de dados
                if (tabela.Rows.Count == 0)
                {
                    MessageBox.Show("Nenhum aluno cadastrado");
                }
                else
                {
                    listaAlunos.ItemsSource = tabela.DefaultView;

                    MessageBox.Show("Tabela listada");
                }

                conn.Close();

                LimparCampo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza dados de um aluno específico
        /// </summary>
        //UPDATE
        private void Atualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(conexao);
                conn.Open();

                string sql = "UPDATE Alunos SET Nome=@Nome, Idade=@Idade WHERE Id=@Id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Id", textoId.Text);
                cmd.Parameters.AddWithValue("@Nome", textoNome.Text);
                cmd.Parameters.AddWithValue("@Idade", textoIdade.Text);

                int cont = cmd.ExecuteNonQuery();

                if (cont == 0)
                {
                    MessageBox.Show("Aluno não encontrado");
                }
                else
                {
                    MessageBox.Show("Aluno atualizado");
                }
                conn.Close();

                LimparCampo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Remove dados de um aluno específico
        /// </summary>
        //DELETE
        private void Remover_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(conexao);
                conn.Open();

                string sql = "DELETE FROM Alunos WHERE Id=@Id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Id", textoId.Text);

                int cont = cmd.ExecuteNonQuery();

                if (cont == 0)
                {
                    MessageBox.Show("Aluno já não existente");
                }
                else
                {
                    MessageBox.Show("Aluno removido");
                }

                conn.Close();

                LimparCampo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Sai da aplicação
        /// </summary>
        //SAIR
        private void Sair_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Lista dados de um aluno específico
        /// </summary>
        //BUSCAR
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                MySqlConnection conn = new MySqlConnection(conexao);
                conn.Open();

                string sql = "SELECT * FROM Alunos WHERE Id=@Id";

                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Id", textoBuscar.Text);

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                System.Data.DataTable tabela = new System.Data.DataTable();

                adapter.Fill(tabela);

                if (tabela.Rows.Count == 0)
                {
                    MessageBox.Show("Aluno não encontrado");
                }
                else
                { 
                    listaAlunos.ItemsSource = tabela.DefaultView;

                    MessageBox.Show("Tabela Atualizada");
                }
                conn.Close();

                LimparCampo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Limpa os campos de entrada
        /// </summary>
        //LIMPAR
        private void LimparCampo()
        {
            textoId.Text = "";
            textoNome.Text = "";
            textoIdade.Text = "";
            textoBuscar.Text = "";
        }
    }
}
using System.Data;
using System.Data.SqlClient;
namespace miniprojeto

{

    public partial class frmminiprojeto : Form
    {
        string stringConexao = "" +
        "Data Source=localhost;" +
        "Initial Catalog=n8_miniprojeto;" +
        "User ID=sa;" +
        "Password=123456";

        private void testarconexao()
        {
            SqlConnection conn = new SqlConnection(stringConexao);

            try
            {
                conn.Open();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
                Application.Exit();
                
            }
        }
            
        public frmminiprojeto()
        {
            InitializeComponent();
        }

        private void btocadastro_Click(object sender, EventArgs e)
        {
            string sql = "insert into usuario " +
                "(" +
                    "nome_usuario," +
                    "login_usuario," +
                    "senha_usuario," +
                    "cpf_usuario," +
                    "obs_usuario," +
                    "status_usuario" +
                ")" +
                 "Values" +
                 "(" +
                        "'" +txtnome.Text+ "'," +
                        "'"+txtlogin.Text + "'," +
                        "'"+txtsenha.Text + "'," +
                        "'" + txtmcpf.Text + "'," +
                        "'" +txtobs.Text + "'," +
                        "'"+cbostatus.Text + "'" +
                 ")";
            SqlConnection conn = new SqlConnection(stringConexao);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Cadastro realizado com sucesso");

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private void frmminiprojeto_Load(object sender, EventArgs e)
        {
            testarconexao();
        }

        private void btoSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btoLimpar_Click(object sender, EventArgs e)
        {
            txtcodigo.Text = "";
            txtlogin.Text = "";
            txtmcpf.Text = "";
            txtnome.Text = "";
            txtsenha.Text = "";
            cbostatus.Text = "";
            txtobs.Text = "";
            txtnome.Focus();
        }

        private void btoexcluir_Click(object sender, EventArgs e)
        {
            string sql = "delete usuario where id_usuario = " + txtcodigo.Text;

            SqlConnection conn = new SqlConnection(stringConexao);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Dados excluidos com sucesso");
                    btoLimpar.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btoAlterar_Click(object sender, EventArgs e)
        {
            string sql = "update usuario set " +
               "nome_usuario = '" + txtnome.Text + "'," +
               "login_usuario = '" + txtlogin.Text + "'," +
               "senha_usuario = '" + txtsenha.Text + "'," +
               "cpf_usuario = '" + txtmcpf.Text + "'," +
               "obs_usuario = '" + txtobs.Text + "'," +
               "status_usuario = '" + cbostatus.Text + "' " +
               "where id_usuario = " + txtcodigo.Text;

            SqlConnection conn = new SqlConnection(stringConexao);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Dados foram alterados");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btoCadastrar2_Click(object sender, EventArgs e)
        {
            string sql = "insert into usuario " +
                "(" +
                    "nome_usuario," +
                    "login_usuario," +
                    "senha_usuario," +
                    "cpf_usuario," +
                    "obs_usuario," +
                    "status_usuario" +
                ")values(" +
                    "'" + txtnome.Text + "'," +
                    "'" + txtlogin.Text + "'," +
                    "'" + txtsenha.Text + "'," +
                    "'" + txtmcpf.Text + "'," +
                    "'" + txtobs.Text + "'," +
                    "'" + cbostatus.Text + "'" +
                ")select SCOPE_IDENTITY()";

            SqlConnection conn = new SqlConnection(stringConexao);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader leitura;
            conn.Open();

            try
            {
                leitura = cmd.ExecuteReader();
                if (leitura.Read())
                {
                    MessageBox.Show("Dados cadastrados com sucesso, ID Gerado - " + leitura[0].ToString());
                    btoLimpar.PerformClick();
                    txtcodigo.Text = leitura[0].ToString();
                    btopesquisar.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btopesquisar_Click(object sender, EventArgs e)
        {
            string sql = "select * from usuario where id_usuario = " + txtcodigo.Text;

            SqlConnection conn = new SqlConnection(stringConexao);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader leitura;
            conn.Open();

            try
            {
                leitura = cmd.ExecuteReader();
                if (leitura.Read())
                {
                    txtcodigo.Text = leitura[0].ToString();
                    txtnome.Text = leitura[1].ToString();
                    txtlogin.Text = leitura[2].ToString();
                    txtsenha.Text = leitura[3].ToString();
                    txtmcpf.Text = leitura[4].ToString();
                    txtobs.Text = leitura[5].ToString();
                    cbostatus.Text = leitura[6].ToString();
                }
                else
                {
                    MessageBox.Show("Erro, insira um usuário que exista!");
                    btoLimpar.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
    

}
using System;
using System.IO;
using System.Windows.Forms;

using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Collections.Generic;
using System.Data;

namespace QueryRunner
{
    public partial class MainForm : Form
    {
        private readonly string _fileName;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string fileName) : this()
        {
            labelFileName.Text = _fileName = fileName.Trim();
        }

        private string GetConnectionString(bool useDb = true)
        {
            var server = textBoxServer.Text;

            if (string.IsNullOrWhiteSpace(server))
                throw new ArgumentException("Не указано имя сервера.");

            var trusted = checkBoxTrusted.Checked;
            
            var login = textBoxLogin.Text;

            if (!trusted && string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Не указано имя пользователя.");

            var password = textBoxPassword.Text;

            if (!trusted && string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Не указан пароль.");

            var database = comboBoxDatabase.SelectedValue?.ToString();

            if (useDb && string.IsNullOrWhiteSpace(database))
                throw new ArgumentException("Не указана база данных.");

            return BuildConnectionString (
                server,
                trusted,
                login,
                password,
                database
            );
        }

        private string BuildConnectionString(string server, bool trusted, string login, string password, string db)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = db
            };

            if (trusted)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.UserID = login;
                builder.Password = password;
            }

            return builder.ConnectionString;
        }

        private void ExecuteSql()
        {
            string script = File.ReadAllText(_fileName);

            var sqlConnection = new SqlConnection(GetConnectionString());
            var serverConnection = new ServerConnection(sqlConnection);
            var server = new Server(serverConnection);

            var result = server.ConnectionContext.ExecuteNonQuery(script);

            MessageBox.Show($"Запрос выполнен. Затронуто { result } строк.");
        }

        private void checkBoxTrusted_CheckedChanged(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;

            if (cb == null)
                return;

            textBoxPassword.Enabled = textBoxLogin.Enabled = !cb.Checked;
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            try
            {
                ExecuteSql();
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var options = Properties.Settings.Default;

            textBoxServer.Text = options.Server;
            checkBoxTrusted.Checked = options.Trusted;
            textBoxLogin.Text = options.Login;
            textBoxPassword.Text = options.Password;
            comboBoxDatabase.DataSource = new List<string> { options.Database };
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            var options = Properties.Settings.Default;

            options.Server = textBoxServer.Text;
            options.Trusted = checkBoxTrusted.Checked;
            options.Login = textBoxLogin.Text;
            options.Password = textBoxPassword.Text;
            options.Database = comboBoxDatabase.SelectedValue?.ToString();

            options.Save();
        }

        private void comboBoxDatabase_DropDown(object sender, EventArgs e)
        {
            comboBoxDatabase.DataSource = GetDatabaseList();
        }

        public List<string> GetDatabaseList()
        {
            string connectionString = null;

            try
            {
                connectionString = GetConnectionString(useDb: false);
            }
            catch
            {
                return null;
            }

            List<string> list = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT name FROM sys.databases", connection))
                {
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(dr[0].ToString());
                        }
                    }
                }
            }

            return list;
        }
    }
}

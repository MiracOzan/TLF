using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TLF.DataAccess;
using TLF.Entities;

namespace TLF.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public SqlConnection Con { get; } = new SqlConnection(DbContext.ConnectionString);
        
        public Users Users1 { get; } = new Users();

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            LoginButtons(out var successLogins);
        }

        #region Methods
        public bool Logins(string userName, string password)
        {
            try
            {
                Con.Open();
                var command = "Select * From Users Where UserName = @UserName and Password = @Password";
                var Command = new SqlCommand(command, Con);
                Command.Parameters.AddWithValue("@UserName", userName);
                Command.Parameters.AddWithValue("@Password", password);
                var dr = Command.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    Users1.Id = dr.GetInt32(0);
                    Users1.UserName = dr.GetString(1);
                    Users1.Password = dr.GetString(2);

                    return true;
                }
                else
                {
                    MessageBox.Show("Kullanıcı Bulunamadı");
                    Con.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private void LoginButtons(out bool successLogins)
        {
            successLogins = Logins(textBox1.Text, textBox2.Text);
            if (successLogins == true)
            {
                this.Visible = false;
            }
        }
        #endregion
    }
}

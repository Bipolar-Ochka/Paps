using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Paps
{
    public partial class INSERT : Form
    {
        private SqlConnection connection = null;
        private int currentTable;
        private Tables refForm;
        public INSERT(SqlConnection connection, int currentTable, Tables refForm)
        {
            this.connection = connection;
            this.currentTable = currentTable;
            this.refForm = refForm;
            InitializeComponent();
            switch(currentTable)
            {
                case 0:
                    label1.Text = "Код клиента";
                    label2.Text = "Фамилия";
                    label3.Text = "Имя";
                    label4.Text = "Отчество";
                    label5.Text = "Контактные данные";
                    break;
                case 1:
                    label2.Text = "Код владельца";
                    label1.Text = "Гос. номер";
                    label3.Text = "Марка и цвет";
                    label4.Hide();
                    label5.Hide();
                    textBox4.Hide();
                    textBox5.Hide();
                    break;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlCommand InsertCommand = null;
            switch (currentTable)
            {
                case 0:
                    InsertCommand = new SqlCommand("INSERT INTO [Client] (Id,Familiya,Name,Otchestvo,Kontakt)VALUES (@a1,@a2,@a3,@a4,@a5)", connection);
                    InsertCommand.Parameters.AddWithValue("a1", textBox1.Text);
                    InsertCommand.Parameters.AddWithValue("a2", textBox2.Text);
                    InsertCommand.Parameters.AddWithValue("a3", textBox3.Text);
                    InsertCommand.Parameters.AddWithValue("a4", textBox4.Text);
                    InsertCommand.Parameters.AddWithValue("a5", textBox5.Text);
                    break;
                case 1:
                    InsertCommand = new SqlCommand("INSERT INTO [Auto] (Gos_nomer,Id_vlad,Marka_and_Color) VALUES (@a1,@a2,@a3)", connection);
                    InsertCommand.Parameters.AddWithValue("a1", textBox1.Text);
                    InsertCommand.Parameters.AddWithValue("a2", textBox2.Text);
                    InsertCommand.Parameters.AddWithValue("a3", textBox3.Text);
                    break;
            }
            try
            {
                await InsertCommand.ExecuteNonQueryAsync();
                if (refForm != null) { await refForm.show_info(currentTable); }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

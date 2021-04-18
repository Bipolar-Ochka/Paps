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
    public partial class UPDATE : Form
    {
        private SqlConnection connection = null;
        private int currentTable;
        string id;
        Tables refForm;
        public UPDATE(SqlConnection connection, int currentTable, string id, Tables refForm)
        {
            this.connection = connection;
            this.currentTable = currentTable;
            this.refForm = refForm;
            this.id = id;
            InitializeComponent();
            switch (currentTable)
            {
                case 0:
                    label1.Text = "Код клиента";
                    label2.Text = "Фамилия";
                    label3.Text = "Имя";
                    label4.Text = "Отчество";
                    label5.Text = "Контактные данные";
                    break;
                case 1:                    
                    label1.Text = "Гос. номер";
                    label2.Text = "Код владельца";
                    label3.Text = "Марка и цвет";
                    label4.Hide();
                    label5.Hide();
                    textBox4.Hide();
                    textBox5.Hide();
                    break;
            }
        }

        private async void UPDATE_Load(object sender, EventArgs e)
        {
            SqlCommand GetInfoCommand = null;
            switch (currentTable)
            {
                case 0:
                    GetInfoCommand = new SqlCommand("SELECT [Id],[Familiya],[Name],[Otchestvo],[Kontakt] FROM [Client] WHERE [Id] = @Id", connection);
                    GetInfoCommand.Parameters.AddWithValue("Id", Convert.ToInt32(id));
                    break;
                case 1:
                    GetInfoCommand = new SqlCommand("SELECT [Gos_nomer],[Id_vlad],[Marka_and_Color] FROM [Auto] WHERE [Gos_nomer] = @Id", connection);
                    GetInfoCommand.Parameters.AddWithValue("Id", id);
                    break;
            }
            SqlDataReader dataReader = null;
            try
            {
                dataReader = await GetInfoCommand.ExecuteReaderAsync();
                while (await dataReader.ReadAsync())
                {
                    textBox1.Text = Convert.ToString(dataReader[0]);
                    textBox2.Text = Convert.ToString(dataReader[1]);
                    textBox3.Text = Convert.ToString(dataReader[2]);
                    textBox4.Text = Convert.ToString(dataReader[3]);
                    if (dataReader.FieldCount == 5)
                    {
                        textBox5.Text = Convert.ToString(dataReader[4]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed)
                    dataReader.Close();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlCommand update = null;
            switch (currentTable)
            {
                case 0:
                    update = new SqlCommand("UPDATE [Client] SET [Id] = @a1 ,[Familiya] = @a2,[Name] = @a3,[Otchestvo] = @a4,[Kontakt] = @a5 WHERE [Id] = @Id", connection);
                    update.Parameters.AddWithValue("a1", Convert.ToInt32(textBox1.Text));
                    update.Parameters.AddWithValue("a2", textBox2.Text);
                    update.Parameters.AddWithValue("a3", textBox3.Text);
                    update.Parameters.AddWithValue("a4", textBox4.Text);
                    update.Parameters.AddWithValue("a5", textBox5.Text);
                    update.Parameters.AddWithValue("Id", id);
                    break;
                case 1:
                    update = new SqlCommand("UPDATE [Auto] SET [Gos_nomer] = @a1,[Id_vlad] = @a2,[Marka_and_Color] = @a3 WHERE [Gos_nomer] = @Id", connection);
                    update.Parameters.AddWithValue("Id", id);
                    update.Parameters.AddWithValue("a1", textBox1.Text);
                    update.Parameters.AddWithValue("a2", Convert.ToInt32(textBox2.Text));
                    update.Parameters.AddWithValue("a3", textBox3.Text);
                    break;

            }
            try
            {
                await update.ExecuteNonQueryAsync();
                await refForm.show_info(currentTable);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

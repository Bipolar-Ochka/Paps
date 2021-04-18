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
    public partial class Tables : Form
    {
        private SqlConnection connection = null;
        private int currentTable;
        private Form1 refForm;
        INSERT insert;
        public Tables(SqlConnection connection, int currentTable, Form1 refForm)
        {
            this.connection = connection;
            this.currentTable = currentTable;
            this.refForm = refForm;
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (insert == null || insert.IsDisposed)
            {
                this.insert = new INSERT(connection, currentTable, this);
                insert.Show();
            }
        }

        public async Task show_info(int currentTable)
        {
            listView1.Clear();
            SqlDataReader sqlReader = null;
            SqlCommand command = null;
            switch (currentTable)
            {
                case 0:
                    listView1.Columns.Add("Код клиента");
                    listView1.Columns.Add("Фамилия");
                    listView1.Columns.Add("Имя");
                    listView1.Columns.Add("Отчество");
                    listView1.Columns.Add("Контактные данные");
                    toolStrip1.Items[0].Visible = true;
                    toolStrip1.Items[1].Visible = true;
                    toolStrip1.Items[2].Visible = true;
                    command = new SqlCommand("SELECT * FROM [dbo].[Client]", connection);
                    break;
                case 1:
                    listView1.Columns.Add("Гос. номер");
                    listView1.Columns.Add("Код владельца");                    
                    listView1.Columns.Add("Марка");
                    toolStrip1.Items[0].Visible = true;
                    toolStrip1.Items[1].Visible = true;
                    toolStrip1.Items[2].Visible = true;
                    command = new SqlCommand("SELECT * FROM [dbo].[Auto]", connection);
                    break;
                case 2:
                    listView1.Columns.Add("Код договора");
                    listView1.Columns.Add("Код места");
                    listView1.Columns.Add("Начало парковки");
                    listView1.Columns.Add("Конец парковки");
                    listView1.Columns.Add("Код авто");
                    listView1.Columns.Add("Код оператора");
                    listView1.Columns.Add("Флаг оплаты");
                    toolStrip1.Items[0].Visible = false;
                    toolStrip1.Items[1].Visible = false;
                    toolStrip1.Items[2].Visible = false;
                    command = new SqlCommand("SELECT * FROM [dbo].[Dogovors]", connection);
                    break;
            }
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    List<string> strmas = new List<string>();
                    for (int i = 0; i < sqlReader.FieldCount; i++)
                    {
                        strmas.Add(Convert.ToString(sqlReader[i]));
                    }
                    ListViewItem item = new ListViewItem(strmas.ToArray<string>());
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed) sqlReader.Close();
            }
        }

        private async void Tables_Load(object sender, EventArgs e)
        {
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            await show_info(currentTable);
        }

        private async void but_texbox_TextChanged(object sender, EventArgs e)
        {
            SqlCommand command = null;
            SqlDataReader reader = null;
            listView1.Clear();
            switch (currentTable)
            {
                case 0:
                    listView1.Columns.Add("Код клиента");
                    listView1.Columns.Add("Фамилия");
                    listView1.Columns.Add("Имя");
                    listView1.Columns.Add("Отчество");
                    listView1.Columns.Add("Контактные данные");
                    listView1.Columns.Add("Код автомобиля");
                    command = new SqlCommand("SELECT * FROM Client WHERE [Id] LIKE  @Search OR [Familiya] LIKE  @Search OR [Name] LIKE  @Search OR [Otchestvo] LIKE  @Search OR [Kontakt] LIKE  @Search OR [Id_avto] LIKE  @Search", connection);
                    break;
                case 1:
                    listView1.Columns.Add("Код владельца");
                    listView1.Columns.Add("Гос. номер");
                    listView1.Columns.Add("Марка и цвет");
                    command = new SqlCommand("SELECT * FROM Auto WHERE [Id_vlad] LIKE @Search OR [Gos_nomer] LIKE @Search OR [Marka and Color] LIKE @Search", connection);
                    break;
                case 2:
                    listView1.Columns.Add("Код договора");
                    listView1.Columns.Add("Код клиента");
                    listView1.Columns.Add("Код места");
                    listView1.Columns.Add("Начало парковки");
                    listView1.Columns.Add("Конец парковки");
                    listView1.Columns.Add("Код авто");
                    listView1.Columns.Add("Код оператора");
                    listView1.Columns.Add("Флаг оплаты");
                    command = new SqlCommand("SELECT * FROM Dogovors WHERE [Id] LIKE  @Search OR [Cod_client] LIKE  @Search OR [Cod_place] LIKE  @Search OR [Time_start] LIKE  @Search OR [Time_end] LIKE  @Search OR [Cod_auto] LIKE  @Search OR [Cod_operator] LIKE  @Search OR [Oplata] LIKE  @Search", connection);
                    break;
            }
            command.Parameters.AddWithValue("Search", "%" + but_texbox.Text + "%");
            try
            {
                reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    List<string> strmas = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        strmas.Add(Convert.ToString(reader[i]));
                    }
                    ListViewItem item = new ListViewItem(strmas.ToArray<string>());
                    listView1.Items.Add(item);
                }
                reader.Close();
                but_texbox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void but_redact_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                UPDATE update = new UPDATE(connection, currentTable, listView1.SelectedItems[0].SubItems[0].Text, this);
                update.Show();
            }
            else
            {
                MessageBox.Show("Ни одна строка не выделена", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

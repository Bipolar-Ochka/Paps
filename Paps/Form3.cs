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
    public partial class Form3 : Form
    {
        private SqlConnection connection = null;
        private Form1 refForm;
        private DateTime curtime = DateTime.Now;
        private int cod_oper;
        INSERT insert,insert2;
        public Form3(SqlConnection connection, Form1 refForm, int oper_cod)
        {
            this.connection = connection;
            this.refForm = refForm;
            cod_oper = oper_cod;
            InitializeComponent();
        }
        public async Task show_places()
        {
            listView1.Clear();
            listView1.Columns.Add("Код места").Width = 0;
            listView1.Columns.Add("Зона");
            listView1.Columns.Add("Номер места");
            SqlDataReader sqlReader = null;
            SqlCommand command = null;
            command = new SqlCommand("SELECT Id, Zone, Number FROM [dbo].[Table] WHERE Placed IS NULL", connection);
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

        private async void Form3_Load(object sender, EventArgs e)
        {
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            await show_places();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string gos_n = textBox1.Text;
            SqlDataReader sqlReader = null;
            int a = (checkBox1.Checked) ? 1 : 0;
            SqlCommand comd = new SqlCommand("SELECT * FROM [dbo].[Auto] WHERE Gos_nomer = @a1",connection);
            if ((listView1.SelectedItems.Count > 0) && !(string.IsNullOrWhiteSpace(gos_n))&& !(Convert.ToDateTime(dateTimePicker1.Text) <= curtime))
            {
                comd.Parameters.AddWithValue("a1", gos_n);
                sqlReader = await comd.ExecuteReaderAsync();
                if(sqlReader.HasRows)
                {
                    sqlReader.Close();
                    SqlCommand InsertCommand = new SqlCommand("INSERT INTO [Dogovors] (Cod_place,Time_start,Time_end,Cod_auto,Cod_operator,Oplata) VALUES (@a1,@a2,@a3,@a4,@a5,@a6)", connection);
                    InsertCommand.Parameters.AddWithValue("a1", Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text));
                    InsertCommand.Parameters.AddWithValue("a2", curtime);
                    InsertCommand.Parameters.AddWithValue("a3", Convert.ToDateTime(dateTimePicker1.Value.Date + dateTimePicker2.Value.TimeOfDay));
                    InsertCommand.Parameters.AddWithValue("a4", gos_n);
                    InsertCommand.Parameters.AddWithValue("a5", cod_oper);
                    InsertCommand.Parameters.AddWithValue("a6", a);
                    try
                    {
                        await InsertCommand.ExecuteNonQueryAsync();
                        //SqlCommand update = new SqlCommand("UPDATE [Table] SET Placed = 1 WHERE Id = @id",connection);
                        //update.Parameters.AddWithValue("id", Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text));
                        //await update.ExecuteNonQueryAsync();
                        await refForm.show_table();
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
                else
                {
                    var result = MessageBox.Show("Данного номера в базе нет. Зарегестрировать?","Регистрация",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        var result2 = MessageBox.Show("Водитель уже в базе?", "Регистрация", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if(result2 == DialogResult.No)
                        {
                            if (insert == null || insert.IsDisposed)
                            {
                                this.insert = new INSERT(connection, 0, null);
                                insert.Show();
                            }
                        }
                        if (insert2 == null || insert2.IsDisposed)
                        {
                            this.insert2 = new INSERT(connection, 1, null);
                            insert2.Show();
                        }
                        else
                        {
                            if (insert2 == null || insert2.IsDisposed)
                            {
                                this.insert2 = new INSERT(connection, 1, null);
                                insert2.Show();
                            }
                        }

                    }
                    else { MessageBox.Show("Сначала внестите новые данные в базу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

                }
                
            }
            else
            {
                MessageBox.Show("Ошибка ввода", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

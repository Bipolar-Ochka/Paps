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
// entity framework
namespace Paps
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnect;
        List<char> zones = new List<char>() { 'A','B', 'C', 'D' };
        Tables form2;
        Form3 form3;
        int operator_cod = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Изобретено укропами и взято с боем, трудно представить больное воображение того, кто это создал", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string connectString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Projects\sharp\Paps\Paps\Database1.mdf;Integrated Security=True";
            sqlConnect = new SqlConnection(connectString);
            await sqlConnect.OpenAsync();
            listView1.GridLines = false;
            listView1.View = View.Details;
            
            SqlCommand delete_old = new SqlCommand("DELETE FROM [dbo].[Dogovors] WHERE [Time_end] < GETDATE()",sqlConnect);
            await delete_old.ExecuteNonQueryAsync();
            await show_table();
        }

        public async Task show_table()
        {
            listView1.Clear();
            SqlDataReader dataReader = null;
            SqlCommand command = null;
            listView1.Columns.Add("Зона А").Width = 156;
            listView1.Columns.Add("Зона B").Width = 156;
            listView1.Columns.Add("Зона C").Width = 156;
            listView1.Columns.Add("Зона D").Width = 156;
            try
            {
                for(int i=0;i< zones.Count; i++)
                {
                    command = new SqlCommand("SELECT * FROM [dbo].[Table] WHERE [Zone] = @mark", sqlConnect);
                    command.Parameters.AddWithValue("mark", zones[i]);
                    dataReader = await command.ExecuteReaderAsync();
                    int count = 0;
                    while (await dataReader.ReadAsync())
                    {
                        List<string> strmas = new List<string>();
                        strmas.Add(Convert.ToString(dataReader[1]) + Convert.ToString(dataReader[2]));
                        bool zanyato = (string.IsNullOrEmpty(Convert.ToString(dataReader[3]))) ? true : false;
                        //MessageBox.Show(Convert.ToString(dataReader[3]));
                        if (i == 0)
                        {
                            ListViewItem item = new ListViewItem(strmas.ToArray<string>());
                            item.SubItems.Add("");
                            item.SubItems.Add("");
                            item.SubItems.Add("");
                            item.SubItems[0].BackColor = (zanyato) ? Color.Lime : Color.Pink;
                            item.UseItemStyleForSubItems = false;
                            listView1.Items.Add(item);
                            
                        } else
                        {
                            listView1.Items[count].SubItems[i].Text = strmas[0];
                            listView1.Items[count].SubItems[i].BackColor = (zanyato) ? Color.Lime : Color.Pink;
                        }
                        count++;
                    }
                    dataReader.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed) dataReader.Close();
            }
        }

        private void клиентскаяБазаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form2 == null || form2.IsDisposed)
            {
                this.form2 = new Tables(sqlConnect, 0, this);
                form2.Show();
            }
        }

        private void базаАвтомобилейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form2 == null || form2.IsDisposed)
            {
                this.form2 = new Tables(sqlConnect, 1, this);
                form2.Show();
            }
        }

        private void договорыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form2 == null || form2.IsDisposed)
            {
                this.form2 = new Tables(sqlConnect, 2, this);
                form2.Show();
            }
        }

        private void button_parking_Click(object sender, EventArgs e)
        {
            if (form3 == null || form3.IsDisposed)
            {
                form3 = new Form3(sqlConnect, this, operator_cod);
                form3.Show();
            }
        }
    }
}



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace TSPP
{
    public partial class Form1 : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source = BD.mdb;";
        private OleDbConnection myConnection;

        public Form1()
        {
            InitializeComponent();

            myConnection = new OleDbConnection(connectString);
            myConnection.Open();

            //this.BackgroundImage = imageList1.Images[0] ;
            panel1.BackColor = Color.FromArgb(69, 162, 158);
            menuStrip1.BackColor = Color.FromArgb(69, 162, 158);
            pictureBox5.BackColor = Color.FromArgb(69, 162, 158);
            
            this.BackColor = Color.FromArgb(26, 26, 29);
            //tabPage1.BackColor = Color.FromArgb(31, 40, 51);
            // tabPage2.BackColor = Color.FromArgb(31, 40, 51);
            // tabPage3.BackColor = Color.FromArgb(31, 40, 51);
            // tabPage4.BackColor = Color.FromArgb(31, 40, 51);

            textBox1.Text = "Ім'я";
            textBox1.ForeColor = Color.Gray;
            textBox2.Text = "Адреса";
            textBox2.ForeColor = Color.Gray;
            textBox3.Text = "Наявна кількість продукції";
            textBox3.ForeColor = Color.Gray;
            textBox4.Text = "Вартість одиниці товару";
            textBox4.ForeColor = Color.Gray;

            textBox5.Text = "Ім'я";
            textBox5.ForeColor = Color.Gray;
            textBox6.Text = "Адреса";
            textBox6.ForeColor = Color.Gray;
            textBox7.Text = "Замовлення";
            textBox7.ForeColor = Color.Gray;

            timer1.Start();

        }

        Interface side_panel = new Interface();
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseEnter(pictureBox1, label3);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseLeave(pictureBox1, label3);
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseEnter(pictureBox2, label2);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseLeave(pictureBox2, label2);
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseEnter(pictureBox3, label4);
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseLeave(pictureBox3, label4);
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseEnter(pictureBox4, label5);
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseLeave(pictureBox4, label5);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

            tabControl1.SelectedTab = tabPage4;
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.Gray;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.FromArgb(69, 162, 158);
        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseEnter(pictureBox6, label6);
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseLeave(pictureBox6, label6);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

            tabControl1.SelectedTab = tabPage5;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = "" ;
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                textBox1.Text = "Ім'я";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox2.ForeColor = Color.Black;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Адреса";//подсказка
                textBox2.ForeColor = Color.Gray;
            }
        }

        DataTable dt_provider;
        OleDbDataAdapter adapter_provider;
        private void pictureBox13_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = true;
            textBox1.Focus();

            OleDbCommand selectProvider = new OleDbCommand();
            selectProvider.Connection = myConnection;
            selectProvider.CommandText = "SELECT * FROM Постачальники";
            selectProvider.CommandType = CommandType.Text;
            adapter_provider = new OleDbDataAdapter(selectProvider);

            dt_provider = new DataTable();
            try
            {
                adapter_provider.Fill(dt_provider);
            }
            catch (OleDbException exc)
            {
                dt_provider = null;
                MessageBox.Show(exc.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                selectProvider.Connection.Close();
            }

            dt_provider.PrimaryKey = new DataColumn[]
            {
                dt_provider.Columns["Адреса"]
            };

            // dataGridView1.DataSource = dt_provider;
        }

        static int index_provider = 0;

        List <Provider> provider = new List<Provider> ();

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox1.Text == "Ім'я" || textBox2.Text == "Адреса" || textBox3.Text == "Наявна кількість продукці" || textBox4.Text == "Вартість одиниці товару")
            {
                MessageBox.Show("Будь ласка, заповніть усі поля");
            }

            else
            {
                try
                {
                   // string name = textBox1.Text;
                  //  string address = textBox2.Text;
                  //  int supply = Convert.ToInt32(textBox3.Text);
                   // double cost = Convert.ToDouble(textBox4.Text);

                   /* provider.Add(new Provider(name, address, supply, cost));*/
                    row.CreateCells(dataGridView1);
                    
                    row.Cells[0].Value = textBox1.Text;
                    row.Cells[1].Value = textBox2.Text;
                    row.Cells[2].Value = Convert.ToInt32(textBox3.Text);
                    row.Cells[3].Value = Convert.ToDouble(textBox4.Text); 

                    dataGridView1.Rows.Add(row);

                    row.CreateCells(dataGridView2);
                    row.Cells[0].Value = textBox5.Text;
                    row.Cells[1].Value = textBox6.Text;
                    row.Cells[2].Value = Convert.ToInt32(textBox7.Text);
                    dataGridView2.Rows.Add(row);

                    provider.Add(new Provider(dataGridView1.Rows[index_provider].Cells[0].Value.ToString(), dataGridView1.Rows[index_provider].Cells[1].Value.ToString(), Convert.ToInt32(dataGridView1.Rows[index_provider].Cells[2].Value), Convert.ToDouble(dataGridView1.Rows[index_provider].Cells[3].Value)));
                    
                    index_provider++;
                    textBox1.Text = "Ім'я";
                    textBox1.ForeColor = Color.Gray;
                    textBox2.Text = "Адреса";
                    textBox2.ForeColor = Color.Gray;
                    textBox3.Text = "Наявна кількість продукції";
                    textBox3.ForeColor = Color.Gray;
                    textBox4.Text = "Вартість одиниці товару";
                    textBox4.ForeColor = Color.Gray;

                   
                    DataRow datarow = dt_provider.NewRow();
                    datarow[0] = row.Cells[0].Value;
                    datarow[1] = row.Cells[1].Value;
                    datarow[2] = row.Cells[2].Value;
                    datarow[3] = row.Cells[3].Value;

                    dt_provider.Rows.Add(datarow);

                    if (dt_provider.GetChanges() == null)
                        return;

                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter_provider)
                    { QuotePrefix = "[", QuoteSuffix = "]" };

                    commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;
                    adapter_provider.Update(dt_provider);


                    flowLayoutPanel1.Visible = false;


                }

                catch (System.FormatException)
                {
                    MessageBox.Show("Введено не правильний формат даних! ");

                }
 
                 //listBox1.Items.Add(provider[index_provider].name +" "+ provider[index_provider].address +" "+ provider[index_provider].supply +" "+ provider[index_provider].cost);

            }
        }

        DataTable dt_client;
        OleDbDataAdapter adapter_client;
        private void pictureBox14_Click(object sender, EventArgs e)
        {
            flowLayoutPanel2.Visible = true;
            textBox5.Focus();

            OleDbCommand selectClient = new OleDbCommand();
            selectClient.Connection = myConnection;
            selectClient.CommandText = "SELECT * FROM Замовники";
            selectClient.CommandType = CommandType.Text;
            adapter_client = new OleDbDataAdapter(selectClient);

            dt_client = new DataTable();
            try
            {
                adapter_client.Fill(dt_client);
            }
            catch (OleDbException exc)
            {
                dt_client = null;
                MessageBox.Show(exc.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                selectClient.Connection.Close();
            }

            dt_client.PrimaryKey = new DataColumn[]
            {
                dt_client.Columns["Адреса"]
            };
        }

        static int index_client = 0;
        List<Client> client = new List<Client>();

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();

            if (textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox5.Text == "Ім'я" || textBox6.Text == "Адреса" || textBox7.Text == "Замовлення")
            {
                MessageBox.Show("Будь ласка, заповніть усі поля");
            }

            else
            {
                try
                {
                   // string name = textBox5.Text;
                    //string address = textBox6.Text;
                    //int order = Convert.ToInt32(textBox7.Text);

                    /*
                    client.Add(new Client(name, address, order));
                    row.CreateCells(dataGridView2);
                    row.Cells[0].Value = client[index_client].name;
                    row.Cells[1].Value = client[index_client].address;
                    row.Cells[2].Value = client[index_client].order;
                    dataGridView2.Rows.Add(row);
                    index_client++;
                    */

                    row.CreateCells(dataGridView2);
                    row.Cells[0].Value = textBox5.Text;
                    row.Cells[1].Value = textBox6.Text;
                    row.Cells[2].Value = Convert.ToInt32(textBox7.Text);
                    dataGridView2.Rows.Add(row);
                    client.Add(new Client(dataGridView2.Rows[index_client].Cells[0].Value.ToString(), dataGridView2.Rows[index_client].Cells[1].Value.ToString(), Convert.ToInt32(dataGridView2.Rows[index_client].Cells[2].Value)));
                    index_client++;




                    textBox5.Text = "Ім'я";
                    textBox5.ForeColor = Color.Gray;
                    textBox6.Text = "Адреса";
                    textBox6.ForeColor = Color.Gray;
                    textBox7.Text = "Кількість замовленного товару";
                    textBox7.ForeColor = Color.Gray;

                    DataRow datarow = dt_client.NewRow();
                    datarow[0] = row.Cells[0].Value;
                    datarow[1] = row.Cells[1].Value;
                    datarow[2] = row.Cells[2].Value;

                    dt_client.Rows.Add(datarow);

                    if (dt_client.GetChanges() == null)
                        return;

                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter_client)
                    { QuotePrefix = "[", QuoteSuffix = "]" };

                    commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;
                    adapter_client.Update(dt_client);

                    flowLayoutPanel2.Visible = false;
                }

                catch (System.FormatException)
                {
                    MessageBox.Show("Введено не правильний формат даних! ");

                }
            }
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
           

            try
            {
                if (client.Count != 0 && provider.Count != 0)
                {
                    for (int i = 0; i < client.Count; i++)
                    {
                        DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                        column.HeaderText = Convert.ToString(client[i].address);
                        dataGridView3.Columns.Add(column);
                    }

                    for (int i = 0; i < provider.Count; i++)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView3);
                        row.Cells[0].Value = provider[i].address;
                        dataGridView3.Rows.Add(row);
                    }

                    panel4.Visible = true;
                    pictureBox32.Visible = true;
                    label13.Text = "Оновити таблицю";
                   pictureBox15.Visible = false;
                }

                else
                {
                    MessageBox.Show("Спочатку додайте постачальників та замовників");
                }

            }
            catch
            {

            }

           

        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox3.ForeColor = Color.Black;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Наявна кількіть продукції";//подсказка
                textBox3.ForeColor = Color.Gray;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Вартість одиниці товарy";//подсказка
                textBox4.ForeColor = Color.Gray;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox4.ForeColor = Color.Black;
        }

        private void pictureBox13_MouseEnter(object sender, EventArgs e)
        {
            pictureBox13.Size = new Size(pictureBox13.Size.Width+2, pictureBox13.Size.Height+2);
        }

        private void pictureBox13_MouseLeave(object sender, EventArgs e)
        {

            pictureBox13.Size = new Size(pictureBox13.Size.Width - 2, pictureBox13.Size.Height - 2);
        }

        private void pictureBox22_MouseEnter(object sender, EventArgs e)
        {
            pictureBox22.Size = new Size(pictureBox22.Size.Width + 2, pictureBox22.Size.Height + 2);
            label16.Location = new Point(label16.Location.X - 4, label16.Location.Y);
            label16.ForeColor = Color.Red;
        }

        private void pictureBox22_MouseLeave(object sender, EventArgs e)
        {
            pictureBox22.Size = new Size(pictureBox22.Size.Width - 2, pictureBox22.Size.Height - 2);
            label16.Location = new Point(label16.Location.X + 4, label16.Location.Y);
            label16.ForeColor = Color.DarkGray;
        }

       

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            textBox4.Text = "Вартість одиниці товарy";
            textBox4.ForeColor = Color.Gray;
            flowLayoutPanel1.Visible = false;

        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            try
            {
                // dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                // int index = dataGridView1.CurrentRow.Index;
                int ind = dataGridView1.SelectedCells[0].RowIndex;
                dataGridView1.Rows.RemoveAt(ind);
                    provider.RemoveAt(ind);

                dt_provider.Rows[ind].Delete();
                adapter_provider.Update(dt_provider);
                
            }
            catch 
            {
                MessageBox.Show("Видалення пустого рядка не можливо!");
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage5)
            {
                pictureBox24.Visible = true;
            }
            else
            {
                pictureBox24.Visible = false;
            }

            if (tabControl1.SelectedTab == tabPage1)
            {
                pictureBox25.Visible = true;
            }
            else
            {
                pictureBox25.Visible = false;
            }

            if (tabControl1.SelectedTab == tabPage2)
            {
                pictureBox26.Visible = true;
            }
            else
            {
                pictureBox26.Visible = false;
            }

            if (tabControl1.SelectedTab == tabPage3)
            {
                pictureBox27.Visible = true;
            }
            else
            {
                pictureBox27.Visible = false;
            }

            if (tabControl1.SelectedTab == tabPage4)
            {
                pictureBox28.Visible = true;
            }
            else
            {
                pictureBox28.Visible = false;
            }

            if(dataGridView1.RowCount == 0)
            {
                panel2.Visible = false;
            }
            else
            {
                panel2.Visible = true;
            }

            if (dataGridView2.RowCount == 0)
            {
                panel3.Visible = false;
            }
            else
            {
                panel3.Visible = true;
            }




        }

       

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 39 )
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 39)
            {
                textBox3.Focus();
            }

            if (e.KeyValue == 37)
            {
                textBox1.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 39)
            {
                textBox4.Focus();
            }

            if (e.KeyValue == 37)
            {
                textBox2.Focus();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 37)
            {
                textBox3.Focus();
            }

            if(e.KeyCode == Keys.Enter)
            {
                pictureBox16_Click(sender, e);
            }
        }

        private void pictureBox29_Click(object sender, EventArgs e)
        {
            flowLayoutPanel2.Visible = false;
        }

        private void pictureBox30_Click(object sender, EventArgs e)
        {
            try
            {
                int ind = dataGridView2.SelectedCells[0].RowIndex;
                dataGridView2.Rows.RemoveAt(ind);
                client.RemoveAt(ind);

                dt_client.Rows[ind].Delete();
                adapter_client.Update(dt_client);
            }
            catch
            {
                MessageBox.Show("Видалення пустого рядка не можливо!");
            }
        }


        private void textBox5_Enter(object sender, EventArgs e)
        {
            textBox5.Text = "";
            textBox5.ForeColor = Color.Black;
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "Ім'я";//подсказка
                textBox5.ForeColor = Color.Gray;
            }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            textBox6.Text = "";
            textBox6.ForeColor = Color.Black;
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox6.Text = "Адреса";//подсказка
                textBox6.ForeColor = Color.Gray;
            }
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            textBox7.Text = "";
            textBox7.ForeColor = Color.Black;
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                textBox7.Text = "Замовлення";
                textBox7.ForeColor = Color.Gray;
            }
        }

        private void pictureBox30_MouseEnter(object sender, EventArgs e)
        {
            pictureBox30.Size = new Size(pictureBox30.Size.Width + 2, pictureBox30.Size.Height + 2);
            label15.Location = new Point(label15.Location.X - 4, label15.Location.Y);
            label15.ForeColor = Color.Red;
        }

        private void pictureBox30_MouseLeave(object sender, EventArgs e)
        {
            pictureBox30.Size = new Size(pictureBox30.Size.Width - 2, pictureBox30.Size.Height - 2);
            label15.Location = new Point(label15.Location.X + 4, label15.Location.Y);
            label15.ForeColor = Color.DarkGray;
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 39)
            {
                textBox6.Focus();
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 39)
            {
                textBox7.Focus();
            }

            if (e.KeyValue == 37)
            {
                textBox5.Focus();
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
                if (e.KeyValue == 37)
                {
                    textBox6.Focus();
                }

            if (e.KeyCode == Keys.Enter)
            {
                pictureBox17_Click(sender, e);
            }
        }

        private void pictureBox18_MouseEnter(object sender, EventArgs e)
        {
            pictureBox18.Size = new Size(pictureBox18.Size.Width + 2, pictureBox18.Size.Height + 2);
            label17.Location = new Point(label17.Location.X - 4, label17.Location.Y);
            label17.ForeColor = Color.Red;
        }

        private void pictureBox18_MouseLeave(object sender, EventArgs e)
        {
            pictureBox18.Size = new Size(pictureBox18.Size.Width - 2, pictureBox18.Size.Height - 2);
            label17.Location = new Point(label17.Location.X + 4, label17.Location.Y);
            label17.ForeColor = Color.DarkGray;
        }

        private void pictureBox31_MouseEnter(object sender, EventArgs e)
        {
            pictureBox31.Size = new Size(pictureBox31.Size.Width + 2, pictureBox31.Size.Height + 2);
            label19.Location = new Point(label19.Location.X - 4, label19.Location.Y);
            label19.ForeColor = Color.Red;
        }

        private void pictureBox31_MouseLeave(object sender, EventArgs e)
        {
            pictureBox31.Size = new Size(pictureBox31.Size.Width - 2, pictureBox31.Size.Height - 2);
            label19.Location = new Point(label19.Location.X + 4, label19.Location.Y);
            label19.ForeColor = Color.DarkGray;
        }

        private void pictureBox18_Click_1(object sender, EventArgs e)
        {
            for(int i = 0; i < provider.Count; i++)
            {
                provider[i].name = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                provider[i].address = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value);
                provider[i].supply = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
                provider[i].cost = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < client.Count; i++)
            {
                client[i].name = Convert.ToString(dataGridView2.Rows[i].Cells[0].Value);
                client[i].address = Convert.ToString(dataGridView2.Rows[i].Cells[1].Value);
                client[i].order = Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value);
            }
        }

        private void pictureBox34_MouseEnter(object sender, EventArgs e)
        {
            pictureBox34.Size = new Size(pictureBox34.Size.Width + 2, pictureBox34.Size.Height + 2);
            label20.Location = new Point(label20.Location.X - 4, label20.Location.Y);
            label20.ForeColor = Color.Red;
        }

        private void pictureBox34_MouseLeave(object sender, EventArgs e)
        {
            pictureBox34.Size = new Size(pictureBox34.Size.Width - 2, pictureBox34.Size.Height - 2);
            label20.Location = new Point(label20.Location.X + 4, label20.Location.Y);
            label20.ForeColor = Color.DarkGray;
        }

        private void pictureBox33_MouseEnter(object sender, EventArgs e)
        {
            pictureBox33.Size = new Size(pictureBox33.Size.Width + 2, pictureBox33.Size.Height + 2);
            label18.Location = new Point(label18.Location.X - 4, label18.Location.Y);
            label18.ForeColor = Color.Red;
        }

        private void pictureBox33_MouseLeave(object sender, EventArgs e)
        {
            pictureBox33.Size = new Size(pictureBox33.Size.Width - 2, pictureBox33.Size.Height - 2);
            label18.Location = new Point(label18.Location.X + 4, label18.Location.Y);
            label18.ForeColor = Color.DarkGray;
        }

        static int index_expenses = 0;
        List<Expenses> expenses = new List<Expenses>();
        private void pictureBox33_Click(object sender, EventArgs e)
        {
            try
            {
                expenses.Add(new Expenses());
                expenses[index_expenses].fuel_spending = new double[dataGridView3.RowCount, dataGridView3.ColumnCount - 1];

                for (int i = 0; i < dataGridView3.RowCount; i++)
                {
                    for (int j = 1; j < dataGridView3.ColumnCount; j++)
                    {
                        expenses[index_expenses].fuel_spending[i, j - 1] = Convert.ToDouble(dataGridView3[j, i].Value);
                    }
                }
                expenses[index_expenses].product_costs = new double[dataGridView3.RowCount];
                for(int i = 0; i< dataGridView3.RowCount; i++)
                {
                    expenses[index_expenses].product_costs[i] = provider[i].cost;
                }

            }
            catch(System.FormatException)
            {
                MessageBox.Show("Введено не правильний тип даних");
            }


            label21.Text = expenses[index_expenses].fuel_spending.GetLength(0).ToString() + expenses[index_expenses].fuel_spending.GetLength(1).ToString();

            expenses[index_expenses].Count();

            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                for (int j = 0; j < dataGridView3.ColumnCount-1; j++)
                {
                    textBox8.Text = textBox8.Text +" "+expenses[index_expenses].general_expenses[i, j].ToString();
                }
            }
        }

        private void dataGridView3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pictureBox33.Focus();
            }
        }

        private void pictureBox32_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < dataGridView3.ColumnCount; i++)
            {
                dataGridView3.Columns.RemoveAt(i);
            }

            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                dataGridView3.Rows.RemoveAt(i);
            }
        }

        static int index_plan = 0;
        List<OptimalPlan> plan = new List<OptimalPlan>();
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (client.Count != 0 && provider.Count != 0 && expenses.Count != 0)
                {
                    int[] stock = new int[dataGridView3.RowCount];
                    
                    for ( int i=0; i<stock.Length; i++ )
                    {
                        stock[i] = provider[i].supply;
                    }
                    int[] orders = new int[dataGridView3.ColumnCount - 1];
                    for (int i = 0; i < orders.Length; i++)
                    {
                        orders[i] = client[i].order;
                    }

                    plan.Add( new OptimalPlan(expenses[index_expenses].Count(), stock, orders));
                    plan[index_plan].Start();
                }
                else
                {
                    MessageBox.Show("Спочатку введіть дані про постачальників, замовників та витрати!");
                }
            }

            catch { }

            double [,] optplan = new double [dataGridView3.RowCount, dataGridView3.ColumnCount - 1];
            Array.Copy(plan[index_plan].CalculateGrades(), optplan, optplan.Length);
            
            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                for (int j = 0; j < dataGridView3.ColumnCount - 1; j++)
                {
                    textBox9.Text = textBox9.Text + " " + optplan[i,j].ToString();
                }
            }


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }

        private void pictureBox36_Click(object sender, EventArgs e)
        {
      //  string query = "CREATE TABLE Vendors" + "(id UniqueIdentifier CONSTRAINT PKeyid PRIMARY KEY," + "vendorName NVARCHAR(30),  vendorEmail NVARCHAR(30),  productCount INT, isActive bit)";

            string commText = "ALTER TABLE Замовники ADD COLUMN newField CHAR(10) NULL";
            try
            {
                OleDbCommand comm = new OleDbCommand(commText, myConnection);
                // myConnection.Open();
                comm.ExecuteNonQuery();
            }
            catch { }


        }

        private void pictureBox37_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dt_provider.Rows.Count; i++)
                {
                  for (int j = 0; j < dt_provider.Columns.Count; j++)
                 {
                      dt_provider.Rows[i][j] = dataGridView1.Rows[i].Cells[j].Value;
                 }
                }
                if (dt_provider.GetChanges() == null)
                    return;

                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter_provider)
                { QuotePrefix = "[", QuoteSuffix = "]" };

                commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;
                adapter_provider.Update(dt_provider);
                //adapter_provider.Fill(dt_provider);

                //dataGridView1.DataSource = dt_provider;
            }
            catch (System.ArgumentException) 
            {
                MessageBox.Show("Невозможно присвоить столбцу Column 'Кількість наявного товару' значение null");
            }

        }

        private void pictureBox38_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView2.ColumnCount; j++)
                    {
                        dt_client.Rows[i][j] = dataGridView2.Rows[i].Cells[j].Value;
                    }
                }
                if (dt_client.GetChanges() == null)
                    return;

                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter_client)
                { QuotePrefix = "[", QuoteSuffix = "]" };

                commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;
                adapter_client.Update(dt_client);
            }
            catch { }
        }

        private void pictureBox31_Click(object sender, EventArgs e)
        {

        }

        OleDbDataAdapter da;
        private void pictureBox39_Click(object sender, EventArgs e)
        {
            string query = "SELECT* FROM Замовники";
            OleDbCommand select = new OleDbCommand();
            select.Connection = myConnection;
            select.CommandText = query;
            select.CommandType = CommandType.Text;
            da = new OleDbDataAdapter(select);

            DataTable dt = new DataTable();

            try
            {
                da.Fill(dt);
            }
            catch (OleDbException exc)
            {
                dt = null;
                MessageBox.Show(exc.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                select.Connection.Close();
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView2);

                row.Cells[0].Value = dt.Rows[i][0];
                row.Cells[1].Value = dt.Rows[i][1];
                row.Cells[2].Value = dt.Rows[i][2];
                
                dataGridView2.Rows.Add(row);

                client.Add(new Client(dataGridView2.Rows[i].Cells[0].Value.ToString(), dataGridView2.Rows[i].Cells[1].Value.ToString(), Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value)));
                index_client++;
            }
        }

        private void pictureBox40_Click(object sender, EventArgs e)
        {
            string query = "SELECT* FROM Постачальники";
            OleDbCommand select = new OleDbCommand();
            select.Connection = myConnection;
            select.CommandText = query;
            select.CommandType = CommandType.Text;
            da = new OleDbDataAdapter(select);

            DataTable dt = new DataTable();

            try
            {
                da.Fill(dt);
            }
            catch (OleDbException exc)
            {
                dt = null;
                MessageBox.Show(exc.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                select.Connection.Close();
            }

            for(int i= 0; i < dt.Rows.Count; i++)
            {
                DataGridViewRow row = new DataGridViewRow ();
                row.CreateCells(dataGridView1);

                row.Cells[0].Value = dt.Rows[i][0];
                row.Cells[1].Value = dt.Rows[i][1];
                row.Cells[2].Value = dt.Rows[i][2];
                row.Cells[3].Value = dt.Rows[i][3];

                dataGridView1.Rows.Add(row);
            }
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }


}

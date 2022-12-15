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
using Excel = Microsoft.Office.Interop.Excel;

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

            panel1.BackColor = Color.FromArgb(69, 162, 158);
            menuStrip1.BackColor = Color.FromArgb(69, 162, 158);
            pictureBox5.BackColor = Color.FromArgb(69, 162, 158);

            this.BackColor = Color.FromArgb(26, 26, 29);

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
            side_panel.pictureBox_MouseEnter(pictureBox1, label3, 10);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseLeave(pictureBox1, label3, 10);
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseEnter(pictureBox2, label2, 10);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseLeave(pictureBox2, label2, 10);
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseEnter(pictureBox3, label4, 10);
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseLeave(pictureBox3, label4, 10);
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseEnter(pictureBox4, label5, 10);
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseLeave(pictureBox4, label5, 10);
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
            side_panel.pictureBox_MouseEnter(pictureBox6, label6, 10);
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            side_panel.pictureBox_MouseLeave(pictureBox6, label6, 10);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

            tabControl1.SelectedTab = tabPage5;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
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

        public void Hiding_the_add_provider_panel()
        {
            textBox1.Text = "Ім'я";
            textBox1.ForeColor = Color.Gray;
            textBox2.Text = "Адреса";
            textBox2.ForeColor = Color.Gray;
            textBox3.Text = "Наявна кількість продукції";
            textBox3.ForeColor = Color.Gray;
            textBox4.Text = "Вартість одиниці товару";
            textBox4.ForeColor = Color.Gray;

            flowLayoutPanel1.Visible = false;
        }

        public void Hidding_the_add_client_panel()
        {
            textBox5.Text = "Ім'я";
            textBox5.ForeColor = Color.Gray;
            textBox6.Text = "Адреса";
            textBox6.ForeColor = Color.Gray;
            textBox7.Text = "Кількість замовленного товару";
            textBox7.ForeColor = Color.Gray;
            flowLayoutPanel2.Visible = false;
        }

        public Tuple<DataTable, OleDbDataAdapter> Creation_datatable(DataTable dt, OleDbDataAdapter adapter, OleDbConnection myConnection, string sqlselect)
        {
            dt = new DataTable();

            OleDbCommand selectProvider = new OleDbCommand();
            selectProvider.Connection = myConnection;
            selectProvider.CommandText = sqlselect;
            selectProvider.CommandType = CommandType.Text;
            adapter = new OleDbDataAdapter(selectProvider);

            try
            {
                adapter.Fill(dt);
            }
            catch (OleDbException exc)
            {
                dt = null;
                MessageBox.Show(exc.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                selectProvider.Connection.Close();
            }

            dt.PrimaryKey = new DataColumn[]
            {
                dt.Columns["Адреса"]
            };

            return Tuple.Create(dt, adapter);

        }

       public void update_datatable_provider()
        {
            for (int i = 0; i < provider.Count; i++)
            {
                provider[i].name = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                provider[i].address = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value);
                provider[i].supply = Convert.ToUInt32(dataGridView1.Rows[i].Cells[2].Value);
                provider[i].cost = Convert.ToDouble(dataGridView1.Rows[i].Cells[3].Value);
            }

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
                adapter_provider.Fill(dt_provider);

        }

        public void update_datatable_client()
        {
            try
            {
                for (int i = 0; i < client.Count; i++)
                {
                    client[i].name = Convert.ToString(dataGridView2.Rows[i].Cells[0].Value);
                    client[i].address = Convert.ToString(dataGridView2.Rows[i].Cells[1].Value);
                    client[i].order = Convert.ToUInt32(dataGridView2.Rows[i].Cells[2].Value);
                }

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        DataTable dt_provider;
        OleDbDataAdapter adapter_provider;
        private void pictureBox13_Click(object sender, EventArgs e) // Кнопка Додати постачальника
        {
            flowLayoutPanel1.Visible = true;
            // textBox1.Focus();

        }

        static int index_provider = 0;
        List<Provider> provider = new List<Provider>();

        private void pictureBox16_Click(object sender, EventArgs e) //Операція додовання постачальника
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
                    row.CreateCells(dataGridView1);

                    row.Cells[0].Value = textBox1.Text;
                    row.Cells[1].Value = textBox2.Text;
                    row.Cells[2].Value = Convert.ToInt32(textBox3.Text);
                    row.Cells[3].Value = Convert.ToDouble(textBox4.Text);

                    dataGridView1.Rows.Add(row);

                    provider.Add(new Provider(dataGridView1.Rows[index_provider].Cells[0].Value.ToString(), dataGridView1.Rows[index_provider].Cells[1].Value.ToString(), Convert.ToUInt32(dataGridView1.Rows[index_provider].Cells[2].Value), Convert.ToDouble(dataGridView1.Rows[index_provider].Cells[3].Value)));
                    index_provider++;

                    DataRow datarow = dt_provider.NewRow();
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        datarow[i] = row.Cells[i].Value;
                    }


                    dt_provider.Rows.Add(datarow);

                    if (dt_provider.GetChanges() == null)
                       return;

                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter_provider)
                    { QuotePrefix = "[", QuoteSuffix = "]" };
                    commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;

                    adapter_provider.Update(dt_provider);
                    Hiding_the_add_provider_panel();
                }

                catch (System.FormatException)
                {
                    MessageBox.Show("Введено не правильний формат даних! ");
                }

                catch (System.Data.ConstraintException)
                {
                    MessageBox.Show("Назва адреси повина бути унікальна!");
                    dataGridView1.Rows.Remove(row);
                    index_provider--;
                    provider.RemoveAt(index_provider);
                }

            }
        }

        DataTable dt_client;
        OleDbDataAdapter adapter_client;
        private void pictureBox14_Click(object sender, EventArgs e) // Кнопка Додати замовника
        {
            flowLayoutPanel2.Visible = true;
            // textBox5.Focus();
        }

        static int index_client = 0;
        List<Client> client = new List<Client>();

        private void pictureBox17_Click(object sender, EventArgs e) //Операція додання клієнту
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
                    row.CreateCells(dataGridView2);
                    row.Cells[0].Value = textBox5.Text;
                    row.Cells[1].Value = textBox6.Text;
                    row.Cells[2].Value = Convert.ToInt32(textBox7.Text);
                    dataGridView2.Rows.Add(row);
                    client.Add(new Client(dataGridView2.Rows[index_client].Cells[0].Value.ToString(), dataGridView2.Rows[index_client].Cells[1].Value.ToString(), Convert.ToUInt32(dataGridView2.Rows[index_client].Cells[2].Value)));
                    index_client++;

                    DataRow datarow = dt_client.NewRow();
                    for (int i = 0; i < dataGridView2.ColumnCount; i++)
                    {
                        datarow[i] = row.Cells[i].Value;
                    }

                    dt_client.Rows.Add(datarow);

                    if (dt_client.GetChanges() == null)
                        return;

                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter_client)
                    { QuotePrefix = "[", QuoteSuffix = "]" };

                    commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;
                    adapter_client.Update(dt_client);
                    Hidding_the_add_client_panel();
                }

                catch (System.FormatException)
                {
                    MessageBox.Show("Введено не правильний формат даних! ");

                }
                catch (System.Data.ConstraintException)
                {
                    MessageBox.Show("Назва адреси повина бути унікальна!");
                    dataGridView2.Rows.Remove(row);
                    index_client--;
                    client.RemoveAt(index_client);

                }
            }
        }

        private void pictureBox15_Click(object sender, EventArgs e) //Створення таблиці для додавання витрат
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
                   // pictureBox32.Visible = true;
                   //label13.Text = "Оновити таблицю";
                   label13.Visible = false;
                    pictureBox15.Visible = false;
                }

                else
                {
                    MessageBox.Show("Спочатку додайте постачальників та замовників");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            pictureBox13.Size = new Size(pictureBox13.Size.Width + 2, pictureBox13.Size.Height + 2);
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

        private void pictureBox22_Click(object sender, EventArgs e) //Видалення Постачальників
        {
            OleDbCommand select = new OleDbCommand();
            select.Connection = myConnection;
            select.CommandType = CommandType.Text;
           
            try
            {
                int ind = dataGridView1.SelectedCells[0].RowIndex;
                
                string sqlselect = "DELETE FROM Постачальники WHERE [Ім'я] = '"+dataGridView1.Rows[ind].Cells[0].Value+"';";
                select.CommandText = sqlselect;
                OleDbDataAdapter adapter = new OleDbDataAdapter(select);
                adapter.Fill(dt_provider);

                dataGridView1.Rows.RemoveAt(ind);
                provider.RemoveAt(ind);
                index_provider--;
                // dt_provider.Rows[ind].Delete();
                //adapter_provider.Update(dt_provider);
                dataGridView3.Rows.Clear();

                for (int i = dataGridView3.Columns.Count-1; i >0; i--)
                {
                    dataGridView3.Columns.RemoveAt(i);
                }
                label13.Visible = true;
                pictureBox15.Visible = true;

            }
            catch(Exception ex)
            {
                MessageBox.Show("Видалення пустого рядка не можливо!");
                MessageBox.Show(ex.Message);
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

            if (dataGridView1.RowCount == 0)
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
            if (e.KeyValue == 39)
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

            if (e.KeyCode == Keys.Enter)
            {
                pictureBox16_Click(sender, e);
            }
        }

        private void pictureBox29_Click(object sender, EventArgs e)
        {
            flowLayoutPanel2.Visible = false;
        }

        private void pictureBox30_Click(object sender, EventArgs e) // Видалення Замовників
        {
            OleDbCommand select = new OleDbCommand();
            select.Connection = myConnection;
            select.CommandType = CommandType.Text;

            try
            {
                int ind = dataGridView2.SelectedCells[0].RowIndex;

                string sqlselect = "DELETE FROM Замовники WHERE [Ім'я] = '" + dataGridView2.Rows[ind].Cells[0].Value + "';";
                select.CommandText = sqlselect;
                OleDbDataAdapter adapter = new OleDbDataAdapter(select);
                adapter.Fill(dt_client);

                dataGridView2.Rows.RemoveAt(ind);
                client.RemoveAt(ind);
                index_client--;

                dataGridView3.Rows.Clear();

                for (int i = dataGridView3.Columns.Count - 1; i > 0; i--)
                {
                    dataGridView3.Columns.RemoveAt(i);
                }

                label13.Visible = true;
                pictureBox15.Visible = true;
                //dt_client.Rows[ind].Delete();
                //adapter_client.Update(dt_client);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        Interface button_page = new Interface();
        private void pictureBox30_MouseEnter(object sender, EventArgs e)
        {
            button_page.pictureBox_MouseEnter_button(pictureBox30, label15, 2, 4);
        }

        private void pictureBox30_MouseLeave(object sender, EventArgs e)
        {
            button_page.pictureBox_MouseLeave_button(pictureBox30, label15, 2, 4);
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

        private void pictureBox34_MouseEnter(object sender, EventArgs e)
        {
            button_page.pictureBox_MouseEnter_button(pictureBox34, label20, 2, 4);
        }

        private void pictureBox34_MouseLeave(object sender, EventArgs e)
        {
            button_page.pictureBox_MouseLeave_button(pictureBox34, label20, 2, 4);
        }

        private void pictureBox33_MouseEnter(object sender, EventArgs e)
        {
            button_page.pictureBox_MouseEnter_button(pictureBox33, label18, 2, 4);
        }

        private void pictureBox33_MouseLeave(object sender, EventArgs e)
        {
            button_page.pictureBox_MouseLeave_button(pictureBox33, label18, 2, 4);
        }


        static int index_expenses = 0;
        List<Expenses> expenses = new List<Expenses>();
        private void pictureBox33_Click(object sender, EventArgs e)
        {
            expenses.Clear();
            try
            {
                double[] product_costs = new double[provider.Count];

                for (int i = 0; i < provider.Count; i++)
                {
                    product_costs[i] = provider[i].cost;
                }

                expenses.Add(new Expenses(product_costs));
                expenses[index_expenses].fuel_spending = new double[provider.Count, client.Count];

                for (int i = 0; i < dataGridView3.RowCount; i++)
                {
                    for (int j = 1; j < dataGridView3.ColumnCount; j++)
                    {
                        expenses[index_expenses].fuel_spending[i, j - 1] = Convert.ToDouble(dataGridView3[j, i].Value);
                    }
                }
                expenses[index_expenses].Save_expenses_to_file(expenses[index_expenses].fuel_spending, "expenses.txt");
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Введено не правильний тип даних");
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

       // static int index_plan = 0;
        //List<OptimalPlan> plan = new List<OptimalPlan>();

        OptimalPlan plan;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (client.Count != 0 && provider.Count != 0 && expenses.Count != 0)
                {
                    uint[] stock = new uint[dataGridView3.RowCount];

                    for (int i = 0; i < stock.Length; i++)
                    {
                        stock[i] = provider[i].supply;
                    }
                    uint[] orders = new uint[dataGridView3.ColumnCount - 1];
                    for (int i = 0; i < orders.Length; i++)
                    {
                        orders[i] = client[i].order;
                    }

                    double[,] general_expenses = new double[dataGridView3.RowCount, dataGridView3.ColumnCount - 1];
                    Array.Copy(expenses[index_expenses].Return_GeneralExpenses(), general_expenses, general_expenses.Length);

                    plan = new OptimalPlan(general_expenses, stock, orders);
                    label19.Visible = true;
                    button3.Visible = true;
                }
                else
                {
                    MessageBox.Show("Спочатку введіть дані про постачальників, замовників та витрати!");
                }
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            myConnection.Close();
        }

        private void pictureBox36_Click(object sender, EventArgs e)
        {
            /*
            OleDbDataAdapter adapter;
            OleDbCommand comm;
            
                for (int i = 0; i < client.Count; i++)
                {
                    string commText = "ALTER TABLE Витрати ADD COLUMN " + "Client_"+(i+1) + " CHAR(50) NULL";
                    comm = new OleDbCommand(commText, myConnection);
                     adapter = new OleDbDataAdapter(comm);
                    adapter.Fill(dt_expenses);
                }
                
                for(int i=0; i< provider.Count; i++)
                {
                    DataRow datarow = dt_expenses.NewRow();
                    for (int j = 0; j < dataGridView3.ColumnCount; j++)
                    {
                        datarow[j] = dataGridView3.Rows[i].Cells[j].Value;
                    }
                    dt_expenses.Rows.Add(datarow);
                }

            if (dt_expenses.GetChanges() == null)
                return;

            OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter_expenses)
            { QuotePrefix = "[", QuoteSuffix = "]" };

            commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;
            adapter_expenses.Update(dt_expenses);
            */
        }

        private void pictureBox37_Click(object sender, EventArgs e) // Зберегти зміни - Постачальники
        {
                update_datatable_provider();
        }

        private void pictureBox38_Click(object sender, EventArgs e) // Зберегти зміни - Замовники
        {
            update_datatable_client();
        }

        DataTable dt_expenses;
        OleDbDataAdapter adapter_expenses;
        private void Form1_Load(object sender, EventArgs e)
        {
            string sql_select = "SELECT * FROM Постачальники";
            (dt_provider, adapter_provider) = Creation_datatable(dt_provider, adapter_provider, myConnection, sql_select);
           
            dataGridView1.RowCount = dt_provider.Rows.Count;

            for ( int i = 0; i < dt_provider.Rows.Count; i++)
            {
                provider.Add(new Provider(Convert.ToString(dt_provider.Rows[i][0]), Convert.ToString(dt_provider.Rows[i][1]), Convert.ToUInt32(dt_provider.Rows[i][2]), Convert.ToDouble(dt_provider.Rows[i][3])));
                index_provider++;

                dataGridView1.Rows[i].Cells[0].Value = Convert.ToString(dt_provider.Rows[i][0]);
                dataGridView1.Rows[i].Cells[1].Value = Convert.ToString(dt_provider.Rows[i][1]);
                dataGridView1.Rows[i].Cells[2].Value = Convert.ToUInt32(dt_provider.Rows[i][2]);
                dataGridView1.Rows[i].Cells[3].Value = Convert.ToDouble(dt_provider.Rows[i][3]);

            }

            string sql_select2 = "SELECT * FROM Замовники";
            (dt_client, adapter_client) = Creation_datatable(dt_client, adapter_client, myConnection, sql_select2);

            dataGridView2.RowCount = dt_client.Rows.Count;

            for (int i = 0; i < dt_client.Rows.Count; i++)
            {
                client.Add(new Client(Convert.ToString(dt_client.Rows[i][0]), Convert.ToString(dt_client.Rows[i][1]), Convert.ToUInt32(dt_client.Rows[i][2])));
                index_client++;

                dataGridView2.Rows[i].Cells[0].Value = Convert.ToString(dt_client.Rows[i][0]);
                dataGridView2.Rows[i].Cells[1].Value = Convert.ToString(dt_client.Rows[i][1]);
                dataGridView2.Rows[i].Cells[2].Value = Convert.ToUInt32(dt_client.Rows[i][2]);
            }

            // string sql_select3 = "SELECT * FROM Витрати";
            //(dt_expenses, adapter_expenses) = Creation_datatable(dt_expenses, adapter_expenses, myConnection, sql_select3);

            


          }

        
        Report report;
        private void pictureBox18_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
            string [] provider_adressess = new string [provider.Count];
            string [] client_adressess = new string [client.Count];

            for( int i = 0; i<provider.Count; i++)
            {
                provider_adressess[i] = provider[i].return_address();
            }

            for( int i = 0; i<client_adressess.Length; i++)
            {
                client_adressess[i] = client[i].return_address();
            }

             report = new Report();
            report.get_data_for_report(plan.return_plan(), provider_adressess, client_adressess);

            panel5.Visible = true;
           // label21.Visible = true;
            label21.Text = "Аналіз оптимального плану:"+ "\n" + report.return_report_text();

            button1.Visible = true;
            textBox8.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox8.Text != "")
                {
                    report.Save_report(textBox8.Text);
                }
                else
                {
                    MessageBox.Show("Введіть ім'я файлу!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("Файл успішно збережено!");
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox18_Click_1(object sender, EventArgs e)
        {
           
        }


        private void button4_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                if (provider.Count != 0 && client.Count != 0)
                {

                    double[] product_costs = new double[provider.Count];

                    for (int i = 0; i < provider.Count; i++)
                    {
                        product_costs[i] = provider[i].cost;
                    }

                    expenses.Add(new Expenses(product_costs));

                    double[,] expenses_general = new double[provider.Count, client.Count];
                    expenses_general = expenses[index_expenses].Recording_expenses_from_a_file(expenses_general);
                    dataGridView3.RowCount = provider.Count;
                    dataGridView3.ColumnCount = client.Count + 1;

                    for (int i = 0; i < provider.Count; i++)
                    {
                        for (int j = 1; j < client.Count + 1; j++)
                        {
                            dataGridView3.Rows[i].Cells[j].Value = expenses_general[i, j - 1];
                        }
                    }

                    pictureBox33_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }

        private void pictureBox34_Click(object sender, EventArgs e)
        {
           dataGridView3.Rows.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
            panel5.Visible = false;
            label19.Visible = false;
            button3.Visible = false;
        }
    }


}

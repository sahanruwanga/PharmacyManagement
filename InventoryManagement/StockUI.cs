﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PharmacyManagement.InventoryManagement
{
    public partial class StockUI : Form
    {
        public StockUI()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
            dateTimePicker1.ResetText();
            numericUpDown1.ResetText();
            textBox5.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String connString = "server = 127.0.0.1; database = pharmacy_management; username = root; password = ;";  //open the database
            MySqlConnection MySqlConn = new MySqlConnection(connString);
            MySqlCommand command = new MySqlCommand("insert into pharmacy_management.stock (product_code, stock_id, vendor, exp_date, entered_date, size, cost_price, unit_price) values ('" + Convert.ToInt32(label5.Text) + "','" + Convert.ToInt32(label7.Text) + "','" + textBox3.Text + "','" + formattedDate(dateTimePicker1.Text) + "','" + formattedDate(DateTime.Today.ToString("MM/dd/yyyy")) + "','" + numericUpDown1.Value + "','" + textBox4.Text + "','" + textBox5.Text + "') ", MySqlConn);
            MySqlDataReader dataReader;
            try
            {
                MySqlConn.Open();
                dataReader = command.ExecuteReader();

                while (dataReader.Read()) { }

                MessageBox.Show("Stock added successfully");
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
            finally
            {
                MySqlConn.Close();
            }
           

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int size = 0;
                label5.Text = row.Cells[0].Value.ToString();
                command = new MySqlCommand("select stock from product where product_code = '" + label5.Text + "'", MySqlConn);
                try
                {
                    MySqlConn.Open();
                    MySqlDataReader datReader = command.ExecuteReader();
                    while (datReader.Read())
                    {
                        size = Convert.ToInt32(datReader.GetString("stock"));
                    }
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
                finally
                {
                    MySqlConn.Close();
                }
                
                command = new MySqlCommand("select count(*) from stock where product_code = '" + label5.Text + "'", MySqlConn);
                MySqlCommand command2 = new MySqlCommand("update product set stock = '" + (numericUpDown1.Value + size) + "' where product_code = '" + Convert.ToInt32(label5.Text) + "'", MySqlConn);

                try
                {
                    MySqlConn.Open();
                    label7.Text = (Convert.ToInt32(command.ExecuteScalar()) + 1).ToString();
                    int numRowsUpdated = command2.ExecuteNonQuery();
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
                finally
                {
                    MySqlConn.Close();
                }
            }

            textBox3.Text = "";
            textBox4.Text = "";
            dateTimePicker1.Text = "";
            numericUpDown1.ResetText();
            textBox5.Text = "";
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private String formattedDate(String date)
        {
            String[] arr = date.Split('/');
            String temp = arr[2];
            arr[2] = arr[1];
            arr[1] = arr[0];
            arr[0] = temp;
            return String.Join("-", arr);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String product_code = "";
            String stock_id = "";
            String vendor = "";
            String exp_date = "";
            String size = "";
            String cost_price = "";
            String unit_price = "";

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                product_code = row.Cells[0].Value.ToString();
                
            }
            foreach (DataGridViewRow row in dataGridView3.SelectedRows)
            {
                stock_id = row.Cells[0].Value.ToString();
                vendor = row.Cells[1].Value.ToString();
                exp_date = row.Cells[2].Value.ToString();
                size = row.Cells[3].Value.ToString();
                cost_price = row.Cells[4].Value.ToString();
                unit_price = row.Cells[5].Value.ToString();
            }
            (new UpdateStock(product_code, stock_id, vendor, exp_date, size, cost_price, unit_price)).Show();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            textBox8.Text = "";
            textBox2.Text = "";
            String connString = "server = 127.0.0.1; database = pharmacy_management; username = root; password = ;";  //open the database
            MySqlConnection MySqlConn = new MySqlConnection(connString);
            MySqlCommand command = new MySqlCommand("select product_code, name, description, category, stock, unit_price from product where product_code like '%" + textBox1.Text + "%' ;", MySqlConn);

            try
            {
                MySqlDataAdapter sqladp = new MySqlDataAdapter();
                sqladp.SelectCommand = command;
                DataTable datatable = new DataTable();
                sqladp.Fill(datatable);
                BindingSource bndsrc = new BindingSource();
                bndsrc.DataSource = datatable;
                dataGridView1.DataSource = bndsrc;
                sqladp.Update(datatable);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            textBox8.Text = "";
            textBox1.Text = "";
            String connString = "server = 127.0.0.1; database = pharmacy_management; username = root; password = ;";  //open the database
            MySqlConnection MySqlConn = new MySqlConnection(connString);
            MySqlCommand command = new MySqlCommand("select product_code, name, description, category, stock, unit_price from product where name like '%" + textBox2.Text + "%' ;", MySqlConn);

            try
            {
                MySqlDataAdapter sqladp = new MySqlDataAdapter();
                sqladp.SelectCommand = command;
                DataTable datatable = new DataTable();
                sqladp.Fill(datatable);
                BindingSource bndsrc = new BindingSource();
                bndsrc.DataSource = datatable;
                dataGridView1.DataSource = bndsrc;
                sqladp.Update(datatable);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void textBox8_TextChanged_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            String connString = "server = 127.0.0.1; database = pharmacy_management; username = root; password = ;";  //open the database
            MySqlConnection MySqlConn = new MySqlConnection(connString);
            MySqlCommand command = new MySqlCommand("select product_code, name, description, category, stock, unit_price from product where category like '%" + textBox8.Text + "%' ;", MySqlConn);

            try
            {
                MySqlDataAdapter sqladp = new MySqlDataAdapter();
                sqladp.SelectCommand = command;
                DataTable datatable = new DataTable();
                sqladp.Fill(datatable);
                BindingSource bndsrc = new BindingSource();
                bndsrc.DataSource = datatable;
                dataGridView1.DataSource = bndsrc;
                sqladp.Update(datatable);
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                label5.Text = row.Cells[0].Value.ToString();
                String connString = "server = 127.0.0.1; database = pharmacy_management; username = root; password = ; Convert Zero Datetime = True;";  //open the database
                MySqlConnection MySqlConn = new MySqlConnection(connString);
                MySqlCommand command = new MySqlCommand("select count(*) from stock where product_code = '" + label5.Text + "'", MySqlConn);

                try
                {
                    MySqlConn.Open();
                    label7.Text = (Convert.ToInt32(command.ExecuteScalar()) + 1).ToString();
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
                finally
                {
                    MySqlConn.Close();
                }
                if (checkBox1.Checked)
                {
                    command = new MySqlCommand("select stock_id, vendor, exp_date, size, cost_price, unit_price, status from stock where product_code = '" + label5.Text + "' and status <> 'returned' and status = 'expired' ;", MySqlConn);
                }
                else
                {
                    command = new MySqlCommand("select stock_id, vendor, exp_date, size, cost_price, unit_price, status from stock where product_code = '" + label5.Text + "' and status <> 'returned' ;", MySqlConn);
                }
                try
                {
                    MySqlDataAdapter sqladp = new MySqlDataAdapter();
                    sqladp.SelectCommand = command;
                    DataTable datatable = new DataTable();
                    sqladp.Fill(datatable);
                    BindingSource bndsrc = new BindingSource();
                    bndsrc.DataSource = datatable;
                    dataGridView3.DataSource = bndsrc;
                    sqladp.Update(datatable);
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                label5.Text = row.Cells[0].Value.ToString();
                String connString = "server = 127.0.0.1; database = pharmacy_management; username = root; password = ; Convert Zero Datetime = True;";  //open the database
                MySqlConnection MySqlConn = new MySqlConnection(connString);
                MySqlCommand command = new MySqlCommand("select count(*) from stock where product_code = '" + label5.Text + "'", MySqlConn);

                try
                {
                    MySqlConn.Open();
                    label7.Text = (Convert.ToInt32(command.ExecuteScalar()) + 1).ToString();
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
                finally
                {
                    MySqlConn.Close();
                }
                if (checkBox1.Checked)
                {
                    command = new MySqlCommand("select stock_id, vendor, exp_date, size, cost_price, unit_price, status from stock where product_code = '" + label5.Text + "' and status <> 'returned' and status = 'expired' ;", MySqlConn);
                }
                else
                {
                    command = new MySqlCommand("select stock_id, vendor, exp_date, size, cost_price, unit_price, status from stock where product_code = '" + label5.Text + "' and status <> 'returned' ;", MySqlConn);
                }
                try
                {
                    MySqlDataAdapter sqladp = new MySqlDataAdapter();
                    sqladp.SelectCommand = command;
                    DataTable datatable = new DataTable();
                    sqladp.Fill(datatable);
                    BindingSource bndsrc = new BindingSource();
                    bndsrc.DataSource = datatable;
                    dataGridView3.DataSource = bndsrc;
                    sqladp.Update(datatable);
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.Message);
                }
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

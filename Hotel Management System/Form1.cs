using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hotel_Management_System
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=hotel;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // Add Button
        private void button1_Click(object sender, EventArgs e)
        {
            string cat = "";

            if (radioButton1.Checked)
            {
                cat = "Veg";
            }
            else
            {
                cat = "Non Veg";
            }

            SqlCommand cmd = new SqlCommand(
                "insert into menu(fname,fcategory,fprice,favailability) values('" +
                textBox2.Text + "','" +
                cat + "'," +
                textBox3.Text + ",'" +
               comboBox1.Text + "')", con);

            con.Open();

            int res = cmd.ExecuteNonQuery();

            if (res == 1)
            {
                MessageBox.Show("Menu Added");
            }
            else
            {
                MessageBox.Show("Failed");
            }

            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update menu set fprice=" + textBox3.Text + " where fid = " + textBox1.Text + "", con);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            if (res == 1)
            {
                MessageBox.Show("Price is updated");
            }
            else
            {
                MessageBox.Show("Failed");
            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("delete from menu where fid=" + textBox1.Text + "", con);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            if (res == 1)
            {
                MessageBox.Show("Menu is removed");
            }
            else
            {
                MessageBox.Show("Failed");
            }
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select fprice from menu where fid = " + textBox1.Text + "", con);
            con.Open();
            object m = cmd.ExecuteScalar();
            if (m != null)
            {
                MessageBox.Show(m.ToString());
            }
            else
            {
                MessageBox.Show("No Item Found");
            }
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("select *from menu", con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select *from menu where fid = " + dataGridView1.CurrentCell.Value + "", con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                textBox1.Text = sdr["fid"].ToString();
                textBox2.Text = sdr["fname"].ToString();
                textBox3.Text = sdr["fprice"].ToString();
                comboBox1.Text = sdr["favailability"].ToString();
                if (sdr["fcategory"].ToString() == "Veg")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
            }
            con.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
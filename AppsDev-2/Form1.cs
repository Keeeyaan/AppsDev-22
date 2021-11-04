using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppsDev_2
{
    public partial class Form1 : Form
    {

        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;

        public Form1()
        {

            InitializeComponent();
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Kean\source\repos\AppsDev-2\AppsDev-2\Database1.mdf;Integrated Security=True");
            cn.Open();
            GetAllStudentRecord();
        }

        private void GetAllStudentRecord()
        {
            cmd = new SqlCommand("Select * from tblStd", cn);
            da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if(stdid.Text != "" || firstname.Text != "" || lastname.Text != "" || quizscore.Text != "" || result.Text != "")
            {
                saveInfo();

            } 
            else
            {
                MessageBox.Show("Please fill out all necesarry fields.");
                stdid.Focus();
            }
            GetAllStudentRecord();
        }

        protected void saveInfo()
        {
            string QUERY = "INSERT INTO tblStd " +
                "(stid, firstname, lastname, quizscore, result) " +
                "VALUES (@stid, @firstname, @lastname, @quizscore, @result)";

            SqlCommand CMD = new SqlCommand(QUERY, cn);
            CMD.Parameters.AddWithValue("@stid", stdid.Text);
            CMD.Parameters.AddWithValue("@firstname", firstname.Text);
            CMD.Parameters.AddWithValue("@lastname", lastname.Text);
            CMD.Parameters.AddWithValue("@quizscore", quizscore.Text);
            CMD.Parameters.AddWithValue("@result", result.Text);
            CMD.ExecuteNonQuery();
        }

        protected void updateInfo()
        {
            string QUERY = "Update tblStd " +
                "Set firstname = @firstname, lastname = @lastname, quizscore = @quizscore, result = @result " +
                "where stid = @stid";

            SqlCommand CMD = new SqlCommand(QUERY, cn);
            CMD.Parameters.AddWithValue("@stid", stdid.Text);
            CMD.Parameters.AddWithValue("@firstname", firstname.Text);
            CMD.Parameters.AddWithValue("@lastname", lastname.Text);
            CMD.Parameters.AddWithValue("@quizscore", quizscore.Text);
            CMD.Parameters.AddWithValue("@result", result.Text);
            CMD.ExecuteNonQuery();
        }

        protected void deleteInfo()
        {
            string QUERY = "Delete from tblStd " +
                "where stid = @stid";

            SqlCommand CMD = new SqlCommand(QUERY, cn);
            CMD.Parameters.AddWithValue("@stid", stdid.Text);
            CMD.ExecuteNonQuery();
        }

        private void btnfind_Click(object sender, EventArgs e)
        {

            if (stdid.Text != "")
            {
                cmd = new SqlCommand("Select * from tblStd where stid =" + stdid.Text, cn);
                da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                btnupdate.Visible = true;
                btndelete.Visible = true;
            } else
            {
                MessageBox.Show("Please enter valid ID");
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            updateInfo();
            GetAllStudentRecord();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            deleteInfo();
            GetAllStudentRecord();
        }
    }
}

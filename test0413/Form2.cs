using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace test0413
{   
    
    
    public partial class Form2 : Form
    {
        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        Class1 clscon = new Class1();
        Form1 f1;

        public Form2(Form1 frm1)
        {
            InitializeComponent();
            cn = new MySqlConnection(clscon.dbconnect());
            f1 = frm1;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
         
        }

        public void Clear()
        {
            txtAddress.Clear();
            txtContact.Clear();
            txtEmpNo.Clear();
            txtFname.Clear();
            txtLname.Clear();
            txtMname.Clear();
            cboGender.Text = string.Empty;
            dtBdate.Value=DateTime.Now;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sdate = dtBdate.Value.ToString("yyyy-MM-dd");
                if ((txtEmpNo.Text == string.Empty) || (txtFname.Text == string.Empty) ||
                    (txtLname.Text == string.Empty) || (txtMname.Text == string.Empty) ||
                    (txtAddress.Text == string.Empty) || (txtContact.Text == string.Empty))
                {
                    MessageBox.Show("Warning: Required empty field!", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cn.Open();
                cm = new MySqlCommand("insert into tblemployee_mk" +
                    "(employeeno, lname, fname, mname, bdate, gender, address, contactno)" +
                    "values('"+txtEmpNo.Text+"','" + txtLname.Text + "','" +txtFname.Text + "','" +txtMname.Text
                    + "','" +sdate + "','" +cboGender.Text + "','" +txtAddress.Text + "','" +txtContact.Text +"')",cn);
                cm.ExecuteNonQuery();          
                cn.Close();
                MessageBox.Show("Record has been successfully saved.", "Employee Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                f1.LoadRecords();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show("Warning: " + ex.Message, "Warning", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string sdate = dtBdate.Value.ToString("yyyy-MM-dd");
                if ((txtEmpNo.Text == string.Empty) || (txtFname.Text == string.Empty) ||
                    (txtLname.Text == string.Empty) || (txtMname.Text == string.Empty) ||
                    (txtAddress.Text == string.Empty) || (txtContact.Text == string.Empty))
                {
                    MessageBox.Show("Warning: Required empty field!", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cn.Open();
                cm = new MySqlCommand("update tblemployee_mk set" +
                    " lname = '" + txtLname.Text + "'," +
                    " fname = '" + txtFname.Text + "'," +
                    " mname = '" + txtMname.Text + "'," +
                    " bdate = '" + sdate + "'," +
                    " gender = '" + cboGender.Text + "'," +
                    " address = '" + txtAddress.Text + "'," +
                    " contactno = '" + txtContact.Text + "'" +
                    " where employeeno like '" + txtEmpNo.Text + "'", cn); 
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Record has been successfully updated.", "Employee Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                f1.LoadRecords();
                this.Dispose();
            }
            catch(Exception ex) 
            {
                cn.Close();
                MessageBox.Show("Warning: " + ex.Message, "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

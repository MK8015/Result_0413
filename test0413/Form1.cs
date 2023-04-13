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
    public partial class Form1 : Form
    {
        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        Class1 clscon = new Class1();
        string _id, _lname, _fname, _mname, _bdate, _gender, _address, _contact;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "colEdit")
            {
                Form2 frm = new Form2(this);
                frm.btnSave.Enabled = false;
                frm.btnUpdate.Enabled = true;
                frm.txtEmpNo.Enabled = false;
                frm.txtEmpNo.Text = _id; 
                frm.txtAddress.Text = _address; 
                frm.txtContact.Text = _contact; 
                frm.txtFname.Text = _fname; 
                frm.txtLname.Text = _lname; 
                frm.txtMname.Text = _mname;
                frm.cboGender.Text = _gender;
                frm.dtBdate.Value = DateTime.Parse(_bdate);
                frm.ShowDialog();
            }else if(colName == "colDelete")
            {
                if(MessageBox.Show("Are you sure you want to delete this record?",
                    "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) 
                    == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new MySqlCommand("delete from tblemployee_mk where employeeno like '" + _id + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully deleted.", "Deleted Record",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRecords();



                }
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            _id = dataGridView1[1, i].Value.ToString();
            _lname = dataGridView1[2, i].Value.ToString();
            _fname = dataGridView1[3, i].Value.ToString();
            _mname = dataGridView1[4, i].Value.ToString();
            _bdate = dataGridView1[5, i].Value.ToString();
            _gender = dataGridView1[6, i].Value.ToString();
            _address = dataGridView1[7, i].Value.ToString();
            _contact = dataGridView1[8, i].Value.ToString();

        }

        public Form1()
        {
            InitializeComponent();
            cn = new MySqlConnection(clscon.dbconnect());
            LoadRecords();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2(this);
            frm.txtEmpNo.Enabled = true;
            frm.btnSave.Enabled = true;
            frm.btnUpdate.Enabled = false ;
            frm.ShowDialog();

        }

        public void LoadRecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new MySqlCommand("select * from tblemployee_mk order by lname,fname,mname", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString()
                    , dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString()
                    , dr[7].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}

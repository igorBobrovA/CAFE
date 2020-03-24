using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
//найти должность
//если её нету, то добавить
namespace restaurant
{
    public partial class ADDpost : Form
    {
        public ADDpost()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(@"DataSource=КАФЕ.db;Version=3;");
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "Select count(*)" +
                              " From ДОЛЖНОСТЬ д" +
                              " WHERE д.наименование = '" + textBox1.Text + "' ";
            string Scalar = cmd.ExecuteScalar().ToString();
            cmd.Reset();
            if (Scalar == "0")
            {
                cmd.CommandText = "INSERT into ДОЛЖНОСТЬ" +
                                    " (наименование)" +
                                    " VALUES('" + textBox1.Text + "')";
                cmd.ExecuteNonQuery();
            }
            con.Close();
            DialogResult res = MessageBox.Show("Вы добавили должность\nДобавить ещё? ", "Добавление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                textBox1.Text = "";
            }
            else this.Close();
        }
    }
}

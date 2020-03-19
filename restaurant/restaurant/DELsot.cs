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

namespace restaurant
{
    public partial class DELsot : Form
    {
        public DELsot()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void DELsot_Load(object sender, EventArgs e)
        {
            textBox2.Text = DateTime.Now.ToLongDateString().ToString();
            SQLiteConnection con = new SQLiteConnection(@"DataSource=КАФЕ.db;Version=3;");
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT фио " +
                              "FROM СОТРУДНИКИ";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetValue(0));
            }
            cmd.Reset();
            con.Close();
        }
    }
}

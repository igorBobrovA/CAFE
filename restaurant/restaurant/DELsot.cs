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
        SQLiteConnection con = new SQLiteConnection(@"DataSource=КАФЕ.db;Version=3;");
        SQLiteCommand cmd = new SQLiteCommand();
        private void button1_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "UPDATE СОТРУДНИКИ " +
                              "SET дата_увольнения = '" + dateTimePicker1.Value.ToShortDateString() + "' " +
                              ", причина_увольнения = '" + richTextBox1.Text + "' " +
                              "WHERE фио = '" + comboBox1.Text + "'";
            cmd.ExecuteNonQuery();
            cmd.Reset();
            con.Close();
            DialogResult res = MessageBox.Show("Вы уволили сотрудника\nУволить ещё кого-то? ", "увольнение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                comboBox1.Text = "";
                richTextBox1.Text = "";
            }
            else this.Close();
        }

        private void DELsot_Load(object sender, EventArgs e)
        {
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT фио " +
                              "FROM СОТРУДНИКИ " +
                              "WHERE дата_увольнения = ''";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetValue(0));
            }
            cmd.Reset();
            con.Close();
            if (comboBox1.Items.Count == 0)
            {
                comboBox1.Text = "Нету сотрудников:(";
            }
        }
    }
}

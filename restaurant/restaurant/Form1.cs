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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SQLiteConnection con = new SQLiteConnection(@"DataSource=КАФЕ.db;Version=3;");
        SQLiteCommand cmd = new SQLiteCommand();

        private void button2_Click(object sender, EventArgs e)
        {
            Form ADDpost = new ADDpost();
            ADDpost.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form ADDsot = new ADDsot();
            ADDsot.Show();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                List<string> list = new List<string>();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "SELECT ID, фио, телефон, (SELECT наименование FROM ДОЛЖНОСТЬ WHERE ID_должности = должность), зарплата, дата_приёма, дата_увольнения, причина_увольнения " +
                                  "FROM СОТРУДНИКИ";
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add();
                    list.Add(reader.GetValue(0) + "," +
                             reader.GetValue(1) + "," +
                             reader.GetValue(2) + "," +
                             reader.GetValue(3) + "," +
                             reader.GetValue(4) + "," +
                             reader.GetValue(5) + "," +
                             reader.GetValue(6) + "," +
                             reader.GetValue(7));
                }
                cmd.Reset();
                con.Close();
                for (int i = 0; i < list.Count; i++)
                {
                    string[] mas = list[i].Split(',');
                    for (int j = 0; j < 8; j++)
                    {
                        dataGridView1[j, i].Value = mas[j];
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form DELsot = new DELsot();
            DELsot.Show();
        }
    }
}

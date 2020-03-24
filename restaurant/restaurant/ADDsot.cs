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
    public partial class ADDsot : Form
    {
        public ADDsot()
        {
            InitializeComponent();
        }

        SQLiteConnection con = new SQLiteConnection(@"DataSource=КАФЕ.db;Version=3;");
        SQLiteCommand cmd = new SQLiteCommand();
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ADDsot_Load(object sender, EventArgs e)
        {
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT наименование " +
                              "FROM ДОЛЖНОСТЬ";
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader.GetValue(0));
            }
            cmd.Reset();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            con.Open();
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && comboBox1.Text != "")
            {
                cmd.CommandText = "SELECT фио FROM СОТРУДНИКИ WHERE фио = '" + textBox1.Text + "'";
                int scalar = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Reset();
                if (scalar == 0)
                {
                    
                    cmd.CommandText = "INSERT into СОТРУДНИКИ(фио, телефон, должность, зарплата, дата_приёма) " +
                                      "VALUES('" + textBox1.Text + "', '" + textBox2.Text + "', " +
                                      "(SELECT ID_должности FROM ДОЛЖНОСТЬ WHERE наименование = '" + comboBox1.Text + "')" +
                                      ", '" + textBox3.Text + "', '" + dateTimePicker1.Value.ToShortDateString() + "')";
                    cmd.ExecuteNonQuery();
                    cmd.Reset();
                    DialogResult res = MessageBox.Show("Вы добавили сотрудника \nДобавить нового?", "УСП.операция", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.No)
                    {
                        con.Close();
                        this.Close();
                    }
                    else
                    {
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        comboBox1.Text = "";
                    }
                }
                
            }
            else
            {
                     MessageBox.Show("Вы не зарегистрировали сотрудника:" +
                    "\n не все поля заполнены", "ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            con.Close();
        }
    }
}

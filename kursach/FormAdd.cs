using kursach;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using SD = System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form9 : Form
    {
        public SD.DataTableCollection tableCollection = null;
        public Form9()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            if (main != null)
            {
                try
                {
                    SD.DataRow nRow = main.aDBDataSet.Tables[0].NewRow();
                    int rc = main.dataGridView2.RowCount + 1;
                    nRow[0] = rc;
                    nRow[1] = textBox1.Text;
                    nRow[2] = textBox3.Text;
                    nRow[3] = textBox4.Text;

                    try { nRow[4] = maskedTextBox1.Text; }
                    catch { MessageBox.Show("Введена неверная дата!", "Ошибка!"); return; };

                    nRow[5] = comboBox3.Text;
                    nRow[6] = textBox2.Text;
                    main.aDBDataSet.Tables[0].Rows.Add(nRow);
                    main.studentiTableAdapter.Update(main.aDBDataSet.Studenti);
                    main.aDBDataSet.Tables[0].AcceptChanges();
                    main.dataGridView2.Refresh();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    maskedTextBox1.Text = "";
                }
                catch { MessageBox.Show("Необходимо заполнить все поля!", "Ошибка!"); };
            }
            main.dataGridView2.ClearSelection();
            main.proof = 0;
            main.studentiTableAdapter.Update(main.aDBDataSet.Studenti);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

}

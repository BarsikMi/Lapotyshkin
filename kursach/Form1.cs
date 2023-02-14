using System;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp1;
using System.Text.RegularExpressions;

namespace kursach
{
    public partial class Form1 : Form
    {
        public int newcol = 1;
        public int proof, col = 0;

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
            dataGridView2.DataError += new DataGridViewDataErrorEventHandler(dataGridView2_DataError);
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            object value1 = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            try
            {
                if (!((DataGridViewComboBoxColumn)dataGridView1.Columns[e.ColumnIndex]).Items.Contains(value1))
                {
                    ((DataGridViewComboBoxColumn)dataGridView1.Columns[e.ColumnIndex]).Items.Add(value1);
                    e.ThrowException = false;
                }
            }
            catch { MessageBox.Show("Неверные данные.", "Ошибка!"); }
        }

        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            object value = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            try
            {
                if (!((DataGridViewComboBoxColumn)dataGridView2.Columns[e.ColumnIndex]).Items.Contains(value))
                {
                    ((DataGridViewComboBoxColumn)dataGridView2.Columns[e.ColumnIndex]).Items.Add(value);
                    e.ThrowException = false;
                }
            }
            catch { MessageBox.Show("Неверные данные.", "Ошибка!"); }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        { this.studentiTableAdapter.Update(this.aDBDataSet.Studenti); }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            radioButton2.Text = "Специальность";
            radioButton3.Text = "Дисциплина";
            radioButton4.Text = "Семестр";
            radioButton5.Text = "Кол-во часов";
            radioButton6.Text = "Отчетность";
            radioButton8.Text = "Группа";
            radioButton3.Visible = true;
            radioButton4.Visible = true;
            radioButton6.Visible = true;
            radioButton8.Visible = false;
            radioButton6.Checked = false;
            radioButton8.Checked = false;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            dataGridView3.Visible = false;
            radioButton2.Text = "Фамилия";
            radioButton3.Text = "Имя";
            radioButton4.Text = "Отчество";
            radioButton5.Text = "Год зачисления";
            radioButton6.Text = "Форма обучения";
            radioButton8.Text = "Группа";
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            radioButton6.Visible = true;
            radioButton8.Visible = true;
            radioButton6.Checked = false;
            radioButton8.Checked = false;
        }
        private void button12_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView3.Visible = true;
            radioButton2.Text = "Семестр";
            radioButton3.Text = "Дисциплина";
            radioButton4.Text = "Оценки";
            radioButton5.Text = "Фамилия студента";
            radioButton3.Visible = true;
            radioButton4.Visible = true;
            radioButton6.Visible = false;
            radioButton8.Visible = false;
            radioButton6.Checked = false;
            radioButton8.Checked = false;
        }

        //Функция фильта
        private void DGVFilter(int col)
        {
            foreach (DataGridView dgv in this.Controls.OfType<DataGridView>())
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    dgv.CurrentCell = null;
                    dgv.Rows[i].Visible = false;
                    for (int c = 1; c < dgv.ColumnCount; c++)
                        if (dgv.Rows[i].Cells[c].Value != null)
                        {
                            if (dgv.Rows[i].Cells[col].Value != null)
                            {
                                if (dgv.Rows[i].Cells[col].Value.ToString().Contains(textBox2.Text))
                                {
                                    dgv.Rows[i].Visible = true;
                                    break;
                                }
                            }
                        }
                }
            }
        }
        //кнопка сохранить
        private void button1_Click(object sender, EventArgs e)
        {
            try 
            {
                studentiTableAdapter.Update(this.aDBDataSet.Studenti);
                MessageBox.Show("Файл успешно сохранен!", "Завершено!");
                
            }
            catch { MessageBox.Show("Исходный файл ничем не отличается от текущего.\nСохранение отменено.", "Информация"); };
        }
        //поиск
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var match = Regex.Match(textBox1.Text, @"\d{2}\.\d{2}\.\d{4}");
            foreach (DataGridView dgv in this.Controls.OfType<DataGridView>())
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    dgv.Rows[i].Selected = false;
                    for (int j = 1; j < dgv.ColumnCount; j++ )
                        if (dgv.Rows[i].Cells[j].Value != null)
                        {
                            if (dgv.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                            {
                                dgv.Rows[i].Selected = true;
                                break;
                            }
                        }
                }
            }
            proof = 0;
            if (textBox1.Text == "")
            {
                dataGridView2.ClearSelection();
            }            
        }
        //выход верхний
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите выйти из программы?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        //о программе
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа создана в рамках курсового проекта\nАвтор: Лапотышкиным А.С. гр. 082", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //выход
        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите выйти из программы?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.studentiTableAdapter.Update(this.aDBDataSet.Studenti);
                Application.Exit();
            }
        }
        //информация ОТРЕДАЧИТЬ
        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа работает с базой данных Access.\n" +
                "Разработано в рамках курсового проекта студентом гр. 082 Лапотышкиным А.С.\n\n\n" +
                "Чтобы переключаться между вкладками нажимайте на соответствующие кнопки.\n\n" +
                "Вы можете изменить данные в таблице два раза нажав на нужное поле, введя данные и нажав клавишу 'Enter'.\n\n" +
                "Результат поиска и фильтрации сбрасывается и применяется автоматически. Чтобы сбросить результат просто сотрите всё, что находится в поле поиска/фильтрации\n\n" +
                "Чтобы воспользоваться быстрым поиском, впишите в левое поле поиска искомую информацию\n\n" +
                "Чтобы воспользоваться фильтром, поставьте флажок в нужное положение и впишите в правое поле поиска искомую информацию\n\n" +
                "Результат поиска и фильтрации сбрасывается автоматически. Чтобы сбросить результат просто сотрите всё, что находится в поле поиска/фильтрации\n\n" +
                "Чтобы сохранить результат работы в программе, нажмите кнопку 'Сохранить' и дождитесь сообщения о конце сохранения.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //подключение БД
        public void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aDBDataSet.UchPlan". При необходимости она может быть перемещена или удалена.
            this.uchPlanTableAdapter.Fill(this.aDBDataSet.UchPlan);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aDBDataSet.Uspevaemost". При необходимости она может быть перемещена или удалена.
            this.uspevaemostTableAdapter.Fill(this.aDBDataSet.Uspevaemost);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aDBDataSet.Studenti". При необходимости она может быть перемещена или удалена.
            this.studentiTableAdapter.Fill(this.aDBDataSet.Studenti);

            dataGridView2.ClearSelection();
            proof = 0;
        }
        //добавить запись
        public void button9_Click(object sender, EventArgs e)
        {
            Form9 af = new Form9{ Owner = this };
            af.Show();
        }
        //статистика
        private void button11_Click(object sender, EventArgs e)
        {
            FormStat af = new FormStat { Owner = this };
            af.Show();
        }
        //удалить запись
        private void button10_Click(object sender, EventArgs e)
        {
            if (proof == 1)
            {
                DialogResult result = MessageBox.Show("Вы действительно хотите удалить данную запись?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        int a = dataGridView2.CurrentRow.Index;
                        dataGridView2.Rows.Remove(dataGridView2.Rows[a]);
                    }
                    catch { MessageBox.Show("Не выделена строка которую нужно удалить", "Ошибка!"); };
                    dataGridView2.ClearSelection();
                    proof = 0;
                }
            }
            else
            {
                MessageBox.Show("Не выделена строка которую нужно удалить!", "Ошибка!");
            }
        }
        //проверка для удаления
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            proof = 1;
        }

        //фильтрация
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var match = Regex.Match(textBox2.Text, @"\d{2}\.\d{2}\.\d{4}");
            if (radioButton2.Checked == true) { col = 1; DGVFilter(col); }
            else if (radioButton3.Checked == true) { col = 2; DGVFilter(col); }
            else if (radioButton4.Checked == true) { col = 3; DGVFilter(col); }
            else if (radioButton5.Checked == true) { col = 4; DGVFilter(col); }
            else if (radioButton6.Checked == true) { col = 5; DGVFilter(col); }
            else if (radioButton8.Checked == true) { col = 6; DGVFilter(col); }

            if (textBox2.Text == "")
            {
                for (int i = 0; i <= dataGridView2.Rows.Count - 1; i++)
                {
                    dataGridView2.CurrentCell = null;
                    dataGridView2.Rows[i].Visible = true;
                }
            }
        }
    }
}

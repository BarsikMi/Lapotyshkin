using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace kursach
{
    public partial class FormStat : Form
    {
        public int cash, free, child, old, vet, all, ogr, other, young, mode, schet, people, act, online, offline = 0;
        private double tcost, tfree, tchild, told, tvet, tall, togr, tother, tyoung = 0;
        string col, coly;

        public FormStat()
        {
            InitializeComponent();

        }

        //Построить график
        private void button3_Click(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;

            cartesianChart1.Series.Clear();
            SeriesCollection series = new SeriesCollection();
            List<string> dates = new List<string>();

            ChartValues<int> values = new ChartValues<int>();
            ChartValues<int> actvalues = new ChartValues<int>();
            ChartValues<double> freevalues = new ChartValues<double>();
            ChartValues<double> costvalues = new ChartValues<double>();
            ChartValues<double> childv = new ChartValues<double>();
            ChartValues<double> oldv = new ChartValues<double>();
            ChartValues<double> vetv = new ChartValues<double>();
            ChartValues<double> allv = new ChartValues<double>();
            ChartValues<double> ogrv = new ChartValues<double>();
            ChartValues<double> otherv = new ChartValues<double>();
            ChartValues<double> youngv = new ChartValues<double>();

            /*foreach (var row in main.accountDataSet.MP)
            {
                values.Add(row.APeople);
                dates.Add(row.ADate.ToShortDateString());
                actvalues.Add(row.AActor);
            }*/

            for (int f = 0; f < main.dataGridView2.RowCount; f++)
            {
                string fc = Convert.ToString(main.dataGridView2.Rows[f].Cells[7].Value);
                if (fc == "Нет") { tfree++; freevalues.Add(tfree); tfree--; }
                if (fc != "Нет") { freevalues.Add(tfree); }
                if (fc == "Да") { tcost++; costvalues.Add(tcost); tcost--; }
                if (fc != "Да") { costvalues.Add(tcost); }
                fc = Convert.ToString(main.dataGridView2.Rows[f].Cells[4].Value);
                if (fc == "Для детей") { tchild++; childv.Add(tchild); tchild--; }
                if (fc != "Для детей") { childv.Add(tchild); }
                if (fc == "Для пожилых людей") { told++; oldv.Add(told); told--; }
                if (fc != "Для пожилых людей") { oldv.Add(told); }
                if (fc == "Для ветеранов") { tvet++; vetv.Add(tvet); tvet--; }
                if (fc != "Для ветеранов") { vetv.Add(tvet); }
                if (fc == "Для всех") { tall++; allv.Add(tall); tall--; }
                if (fc != "Для всех") { allv.Add(tall); }
                if (fc == "Для лиц с огр.возможностями") { togr++; ogrv.Add(togr); togr--; }
                if (fc != "Для лиц с огр.возможностями") { ogrv.Add(togr); }
                if (fc == "Другое") { tother++; otherv.Add(tother); tother--; }
                if (fc != "Другое") { otherv.Add(tother); }
                if (fc == "Для молодежи") { tyoung++; youngv.Add(tyoung); tyoung--; }
                if (fc != "Для молодежи") { youngv.Add(tyoung); }
            };

            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis()
            {
                Title = "Даты",
                Labels = dates,
            });

            LineSeries peopleline = new LineSeries();
            peopleline.Title = "Кол-во посетителей";
            peopleline.Values = values;
            series.Add(peopleline);

            LineSeries actline = new LineSeries();
            actline.Title = "Кол-во участников";
            actline.Values = actvalues;
            series.Add(actline);

            LineSeries freeline = new LineSeries();
            freeline.Title = "Бесплатных мероприятий";
            freeline.Values = freevalues;
            series.Add(freeline);

            LineSeries costline = new LineSeries();
            costline.Title = "Платных мероприятий";
            costline.Values = costvalues;
            series.Add(costline);

            LineSeries childline = new LineSeries();
            childline.Title = "Мероприятий для детей";
            childline.Values = childv;
            series.Add(childline);

            LineSeries oldline = new LineSeries();
            oldline.Title = "Мероприятий для пожилых людей";
            oldline.Values = oldv;
            series.Add(oldline);

            LineSeries vetline = new LineSeries();
            vetline.Title = "Мероприятий для ветеранов";
            vetline.Values = vetv;
            series.Add(vetline);

            LineSeries allline = new LineSeries();
            allline.Title = "Мероприятий для всех";
            allline.Values = allv;
            series.Add(allline);

            LineSeries ogrline = new LineSeries();
            ogrline.Title = "Мероприятий для людей с огр. возможностями";
            ogrline.Values = ogrv;
            series.Add(ogrline);

            LineSeries otherline = new LineSeries();
            otherline.Title = "Других мероприятий";
            otherline.Values = otherv;
            series.Add(otherline);

            LineSeries youngline = new LineSeries();
            youngline.Title = "Мероприятий для молодежи";
            youngline.Values = youngv;
            series.Add(youngline);

            cartesianChart1.Series = series;
            cartesianChart1.LegendLocation = LegendLocation.Bottom;
        }

        //Показать статистику
        private void button2_Click(object sender, EventArgs e)
        {
            schet = 0;
            people = 0;
            act = 0;
            cash = 0;
            free = 0;
            young = 0;
            child = 0;
            old = 0;
            vet = 0;
            all = 0;
            ogr = 0;
            other = 0;
            online = 0;
            offline = 0;
            Form1 main = this.Owner as Form1;
            if (mode == 1)
            {
                for (int i = 0; i < main.dataGridView2.RowCount; i++)
                {
                    if (main.dataGridView2.Rows[i].Cells[6].Value != null)
                    {
                        string col = Convert.ToString(main.dataGridView2.Rows[i].Cells[6].Value);
                        col = col.Remove(0, 3).Split('.')[0];
                        if (col == comboBox2.GetItemText(comboBox2.SelectedItem))
                        {
                            schet++;
                            people += Convert.ToInt32(main.dataGridView2.Rows[i].Cells[2].Value);
                            act += Convert.ToInt32(main.dataGridView2.Rows[i].Cells[9].Value);
                            coly = Convert.ToString(main.dataGridView2.Rows[i].Cells[7].Value);
                            if (coly == "Да") { cash++; }
                            if (coly == "Нет") { free++; }
                            coly = Convert.ToString(main.dataGridView2.Rows[i].Cells[3].Value);
                            if (coly == "Да") { online++; }
                            if (coly == "Нет") { offline++; }
                            coly = Convert.ToString(main.dataGridView2.Rows[i].Cells[4].Value);
                            if (coly == "Для детей") { child++; }
                            if (coly == "Для пожилых людей") { old++; }
                            if (coly == "Для ветеранов") { vet++; }
                            if (coly == "Для всех") { all++; }
                            if (coly == "Для лиц с огр.возможностями") { ogr++; }
                            if (coly == "Другое") { other++; }
                            if (coly == "Для молодежи") { young++; }
                        }  
                    }
                }
            }
            else if (mode == 2)
            {
                for (int i = 0; i < main.dataGridView2.RowCount; i++)
                {
                    if (main.dataGridView2.Rows[i].Cells[5].Value != null)
                    {
                        string col = Convert.ToString(main.dataGridView2.Rows[i].Cells[5].Value);
                        if (col == comboBox2.GetItemText(comboBox3.SelectedItem))
                        {
                            schet++;
                            people += Convert.ToInt32(main.dataGridView2.Rows[i].Cells[2].Value);
                            act += Convert.ToInt32(main.dataGridView2.Rows[i].Cells[9].Value);
                            coly = Convert.ToString(main.dataGridView2.Rows[i].Cells[7].Value);
                            if (coly == "Да") { cash++; }
                            if (coly == "Нет") { free++; }
                            coly = Convert.ToString(main.dataGridView2.Rows[i].Cells[3].Value);
                            if (coly == "Да") { online++; }
                            if (coly == "Нет") { offline++; }
                            coly = Convert.ToString(main.dataGridView2.Rows[i].Cells[4].Value);
                            if (coly == "Для детей") { child++; }
                            if (coly == "Для пожилых людей") { old++; }
                            if (coly == "Для ветеранов") { vet++; }
                            if (coly == "Для всех") { all++; }
                            if (coly == "Для лиц с огр.возможностями") { ogr++; }
                            if (coly == "Другое") { other++; }
                            if (coly == "Для молодежи") { young++; }
                        }
                    }
                }
            }
            else if (mode == 3)
            {
                for (int i = 0; i < main.dataGridView2.RowCount; i++)
                {
                    if (main.dataGridView2.Rows[i].Cells[6].Value != null)
                    {
                        col = Convert.ToString(main.dataGridView2.Rows[i].Cells[6].Value);
                        col = col.Remove(0, 3).Split('.')[0];
                        coly = Convert.ToString(main.dataGridView2.Rows[i].Cells[6].Value);
                        coly = coly.Remove(0, 6);
                        coly = coly.Remove(5);
                        if (coly == "2022 ")
                        {
                            if (comboBox2.GetItemText(comboBox4.SelectedItem) == Convert.ToString(1))
                            {
                                if (col == "01" || col == "02" || col == "03" || col == "04" || col == "05")
                                {
                                    schet++;
                                    people += Convert.ToInt32(main.dataGridView2.Rows[i].Cells[2].Value);
                                    act += Convert.ToInt32(main.dataGridView2.Rows[i].Cells[9].Value);
                                    coly = Convert.ToString(main.dataGridView2.Rows[i].Cells[7].Value);
                                    if (coly == "Да") { cash++; }
                                    if (coly == "Нет") { free++; }
                                    coly = Convert.ToString(main.dataGridView2.Rows[i].Cells[3].Value);
                                    if (coly == "Да") { online++; }
                                    if (coly == "Нет") { offline++; }
                                    coly = Convert.ToString(main.dataGridView2.Rows[i].Cells[4].Value);
                                    if (coly == "Для детей") { child++; }
                                    if (coly == "Для пожилых людей") { old++; }
                                    if (coly == "Для ветеранов") { vet++; }
                                    if (coly == "Для всех") { all++; }
                                    if (coly == "Для лиц с огр.возможностями") { ogr++; }
                                    if (coly == "Другое") { other++; }
                                    if (coly == "Для молодежи") { young++; }
                                }
                            }
                            else if (col == "06" || col == "07" || col == "08" || col == "09" || col == "10" || col == "11" || col == "12")
                            {
                                schet++;
                                people += Convert.ToInt32(main.dataGridView2.Rows[i].Cells[2].Value);
                                act += Convert.ToInt32(main.dataGridView2.Rows[i].Cells[9].Value);
                                coly = Convert.ToString(main.dataGridView2.Rows[i].Cells[7].Value);
                                if (coly == "Да") { cash++; }
                                if (coly == "Нет") { free++; }
                                coly = Convert.ToString(main.dataGridView2.Rows[i].Cells[3].Value);
                                if (coly == "Да") { online++; }
                                if (coly == "Нет") { offline++; }
                                coly = Convert.ToString(main.dataGridView2.Rows[i].Cells[4].Value);
                                if (coly == "Для детей") { child++; }
                                if (coly == "Для пожилых людей") { old++; }
                                if (coly == "Для ветеранов") { vet++; }
                                if (coly == "Для всех") { all++; }
                                if (coly == "Для лиц с огр.возможностями") { ogr++; }
                                if (coly == "Другое") { other++; }
                                if (coly == "Для молодежи") { young++; }
                            }
                        }
                    }
                }
            }
            else if (mode == 4)
            {
                for (int i = 0; i < main.dataGridView2.RowCount; i++)
                {
                    if (main.dataGridView2.Rows[i].Cells[6].Value != null)
                    {
                        col = Convert.ToString(main.dataGridView2.Rows[i].Cells[6].Value);
                        col = col.Remove(0, 3).Split('.')[0];
                        coly = Convert.ToString(main.dataGridView2.Rows[i].Cells[6].Value);
                        coly = coly.Remove(0, 6);
                        coly = coly.Remove(5);
                        if (coly == "2022 ")
                        {
                            schet++;
                            people += Convert.ToInt32(main.dataGridView2.Rows[i].Cells[2].Value);
                            act += Convert.ToInt32(main.dataGridView2.Rows[i].Cells[9].Value);
                            coly = Convert.ToString(main.dataGridView2.Rows[i].Cells[7].Value);
                            if (coly == "Да") { cash++; }
                            if (coly == "Нет") { free++; }
                            coly = Convert.ToString(main.dataGridView2.Rows[i].Cells[3].Value);
                            if (coly == "Да") { online++; }
                            if (coly == "Нет") { offline++; }
                            coly = Convert.ToString(main.dataGridView2.Rows[i].Cells[4].Value);
                            if (coly == "Для детей") { child++; }
                            if (coly == "Для пожилых людей") { old++; }
                            if (coly == "Для ветеранов") { vet++; }
                            if (coly == "Для всех") { all++; }
                            if (coly == "Для лиц с огр.возможностями") { ogr++; }
                            if (coly == "Другое") { other++; }
                            if (coly == "Для молодежи") { young++; }
                        }
                    }
                } 
            }
            label6.Text = "\nВсего мероприятий: " + schet +
            "\nОбщее кол-во зрителй: " + people +
            "\nОбщее кол-во участников: " + act + 
            "\n\nПлатных мероприятий: " + cash +
            "\nБесплатных мероприятий: " + free +
            "\n\nОнлайн мероприятий: " + online +
            "\nОффлайн мероприятий: " + offline +
            "\n\nМероприятий для детей: " + child +
            "\nМероприятий для пожилых людей: " + old +
            "\nМероприятий для ветеранов: " + vet +
            "\nМероприятий для всех: " + all +
            "\nМероприятий для лиц с огр.возможностями: " + ogr +
            "\nДля молодежи: " + young +
            "\nДругих мероприятий: " + other;
        }

        //Закрыть
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.SelectedItem = null;
            comboBox3.SelectedItem = null;
            comboBox4.SelectedItem = null;
            if (comboBox1.GetItemText(comboBox1.SelectedItem) == "За месяц")
            {
                comboBox2.Enabled = true;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                mode = 1;
            }
            else if (comboBox1.GetItemText(comboBox1.SelectedItem) == "По кварталу")
            {
                comboBox2.Enabled = false;
                comboBox3.Enabled = true;
                comboBox4.Enabled = false;
                mode = 2;
            }
            else if (comboBox1.GetItemText(comboBox1.SelectedItem) == "По полугодию")
            {
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                comboBox4.Enabled = true;
                mode = 3;
            }
            else if (comboBox1.GetItemText(comboBox1.SelectedItem) == "За год")
            {
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                mode = 4;
            }
        }
    }
}

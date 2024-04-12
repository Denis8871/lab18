using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_18_OOP_Shostya
{
    public partial class Form1 : Form
    {
        private int[,] array;
        private int rowCount = 0;
        private int columnCount = 0;
        public Form1()
        {
            InitializeComponent();
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            button4.Click += button4_Click;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Отримання введених значень з текстового поля та розділення їх на окремі числа
            string input = textBox1.Text;
            string[] values = input.Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

            // Перевірка наявності введених значень
            if (values.Length == 0)
            {
                MessageBox.Show("Введіть елементи масиву", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Створення масиву та заповнення його введеними значеннями
            double[] array = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                if (!double.TryParse(values[i], out array[i]))
                {
                    MessageBox.Show("Некоректне введення", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // a) Кількість від'ємних елементів масиву
            int negativeCount = array.Count(x => x < 0);
            label1.Text = $"Кількість від'ємних елементів: {negativeCount}";

            // б) Сума модулів елементів масиву, розташованих після мінімального за модулем елементу
            double minAbsValue = array.Where(x => x != array.Min()).Min(Math.Abs);
            double sum = array.Where(x => Math.Abs(x) > minAbsValue).Sum(Math.Abs);
            label2.Text = $"Сума модулів після мінімального за модулем: {sum}";

            // Заміна від'ємних елементів масиву їх квадратами
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < 0)
                {
                    array[i] = Math.Pow(array[i], 2);
                }
            }

            // Впорядкування елементів масиву за зростанням
            Array.Sort(array);

            // Виведення відредагованого масиву у вікно програми
            textBox2.Text = "Відредагований масив:\n" + string.Join(", ", array);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string input = textBox3.Text;
            string[] values = input.Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (columnCount == 0)
            {
                columnCount = values.Length;
            }
            else if (columnCount != values.Length)
            {
                MessageBox.Show("Введіть однакову кількість значень для кожного рядка", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (array == null)
            {
                array = new int[1, columnCount];
            }
            else
            {
                int[,] tempArray = new int[rowCount + 1, columnCount];
                Array.Copy(array, tempArray, array.Length);
                array = tempArray;
            }

            for (int i = 0; i < values.Length; i++)
            {
                if (!int.TryParse(values[i], out array[rowCount, i]))
                {
                    MessageBox.Show("Некоректне введення", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            rowCount++;
            UpdateArrayTextBox();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string column = "";
            for (int i = 0; i < rowCount; i++)
            {
                column += array[i, 1] + Environment.NewLine;
            }
            label5.Text = column;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox5.Text, out int rowIndex) || rowIndex < 1 || rowIndex > rowCount)
            {
                MessageBox.Show($"Введіть коректний номер рядка (від 1 до {rowCount})", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string row = "";
            for (int j = 0; j < columnCount; j++)
            {
                row += array[rowIndex - 1, j] + " ";
            }
            label6.Text = row;
        }

        private void UpdateArrayTextBox()
        {
            string arrayStr = "";
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    arrayStr += array[i, j] + " ";
                }
                arrayStr += Environment.NewLine;
            }
            textBox4.Text = arrayStr;
        }
    }
}	

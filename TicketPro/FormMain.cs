using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicketPro
{
    public class FormMain : Form
    {
        private DataGridView dgv;
        private TextBox txtFrom, txtTo;

        public FormMain()
        {
            Text = "TicketPro -продажа билетов";
            Size = new Size(850, 600);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;

            Controls.Add(new Label
            {
                Text = "Поиск рейсов",
                Font = new Font("Times New Roman", 20, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(320, 15)
            });

            Controls.Add(new Label
            {
                Text = "Откуда:", Font = new Font("Times New Roman", 12),
                Location = new Point(30, 70), AutoSize = true
            });
            txtFrom = new TextBox
            {
                Font = new Font("Times New Roman", 12),
                Location = new Point(110, 67), Width = 150
            };
            Controls.Add(txtFrom);

            Controls.Add(new Label
            {
                Text = "Куда:", Font = new Font("Times New Roman", 12),
                Location = new Point(280, 70), AutoSize = true
            });
            txtTo = new TextBox
            {
                Font = new Font("Times New Roman", 12),
                Location = new Point(340, 67), Width = 150
            };
            Controls.Add(txtTo);

            Button btnFind = new Button
            {
                Text = "Найти", Font = new Font("Times New Roman", 12),
                Location = new Point(520, 65), Size = new Size(120, 30),
                BackColor = Color.LightYellow
            };
            btnFind.Click += BtnFind_Click;
            Controls.Add(btnFind);

            dgv = new DataGridView
            {
                Location = new Point(30, 120),
                Size = new Size(780, 350),
                Font = new Font("Times New Roman", 11),
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgv.Columns.Add("Number", "Рейс");
            dgv.Columns.Add("From", "Откуда");
            dgv.Columns.Add("To", "Куда");
            dgv.Columns.Add("Time", "Время");
            dgv.Columns.Add("Price", "Цена");
            dgv.Columns.Add("Seats", "Мест");
            dgv.CellDoubleClick += Dgv_CellDoubleClick;
            Controls.Add(dgv);

            Button btnHistory = new Button
            {
                Text = "История", Font = new Font("Times New Roman", 12),
                Location = new Point(30, 490), Size = new Size(150, 40),
                BackColor = Color.LightYellow
            };
            btnHistory.Click += (s, e) => ShowHistory();
            Controls.Add(btnHistory);

            Button btnExit = new Button
            {
                Text = "Выйти", Font = new Font("Times New Roman", 12),
                Location = new Point(660, 490), Size = new Size(150, 40),
                BackColor = Color.LightPink
            };
            btnExit.Click += (s, e) => Application.Exit();
            Controls.Add(btnExit);
        }

        private void BtnFind_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            if (string.IsNullOrWhiteSpace(txtFrom.Text) || string.IsNullOrWhiteSpace(txtTo.Text))
            {
                MessageBox.Show("Заполните поля «Откуда» и «Куда»");
                return;
            }
            dgv.Rows.Add("SU-101", txtFrom.Text, txtTo.Text, "10:00", "5000", "25");
            dgv.Rows.Add("SU-205", txtFrom.Text, txtTo.Text, "14:30", "6200", "10");
            dgv.Rows.Add("SU-308", txtFrom.Text, txtTo.Text, "19:00", "4800", "5");
        }

        private void Dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string num = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
            string price = dgv.Rows[e.RowIndex].Cells[4].Value.ToString();

            var result = MessageBox.Show(
                $"Рейс: {num}\nЦена: {price} руб.\n\nЗабронировать и оплатить?",
                "Информация о рейсе",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                MessageBox.Show("Место забронировано!\nОплата прошла успешно.\nБилет отправлен на почту.", "Успех");
        }

        private void ShowHistory()
        {
            string history = "История заказов:\n\n" +
                             "SU-101 | 01.12.2023 | Москва -СПб | 5000 руб. | Оплачен\n" +
                             "SU-205 | 15.11.2023 | Москва -Казань | 6200 руб. | Оплачен\n" +
                             "SU-308 | 10.10.2023 | Москва -Сочи | 7800 руб. | Использован";
            MessageBox.Show(history, "История");
        }
    }
}

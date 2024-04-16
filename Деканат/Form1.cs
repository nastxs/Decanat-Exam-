using Npgsql;

using System;

using System.Collections.Generic;

using System.Data;

using System.Windows.Forms;


namespace Деканат
{
    public partial class Form1 : Form
    {
        //Соединение с базой данных
        NpgsqlConnection conn = new NpgsqlConnection("Server=localhost; User ID=postgres; Password=postgres; Port=5432; Database=Dekanat");

        public Form1()
        {
            InitializeComponent();


            //Заполнение Datagrid расписания 
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost; User ID=postgres; Password=postgres; Port=5432; Database=Dekanat");
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select * from myTimetable4";
            NpgsqlDataReader dr = comm.ExecuteReader();
            if(dr.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                
            }
            comm.Dispose();
            conn.Close();



            
           //Заполнение Datagrid преподавателей
            conn.Open();
            NpgsqlCommand cos = new NpgsqlCommand();
            cos.Connection = conn;
            cos.CommandType = CommandType.Text;
            cos.CommandText = "select * from myTeacher1";
            NpgsqlDataReader de = cos.ExecuteReader();
            if (de.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(de);
                dataGridView3.DataSource = dt;
                
            }
            cos.Dispose();
            conn.Close();

            //Заполнение Datagrid студентов
            conn.Open();
            NpgsqlCommand coe = new NpgsqlCommand();
            coe.Connection = conn;
            coe.CommandType = CommandType.Text;
            coe.CommandText = "select * from mystudent1";
            NpgsqlDataReader dw = coe.ExecuteReader();
            if (dw.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dw);
                dataGridView4.DataSource = dt;
                
            }
            coe.Dispose();
            conn.Close();


            
            //Заполнение комбобокса на вкладке расписания именами преподавателей со связкой к данным
            conn.Open();
            String sCommand1 = "select * from Teacher";
            using (NpgsqlCommand cmd = new NpgsqlCommand(sCommand1, conn))
            {
                cmd.CommandType = CommandType.Text;
                DataTable table = new DataTable();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(table);
                comboBox4.DisplayMember = "ID";
                comboBox4.ValueMember = "FIO";
                comboBox4.DataSource = table;

            }
            conn.Close();

            //Заполнение комбобокса на вкладке расписания названиями дисциплин со связкой к данным
            conn.Open();
            String sCommand2 = "select * from Discipline";
            using (NpgsqlCommand cmd = new NpgsqlCommand(sCommand2, conn))
            {
                cmd.CommandType = CommandType.Text;
                DataTable table = new DataTable();
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(table);
                comboBox5.DisplayMember = "ID";
                comboBox5.ValueMember = "name";
                comboBox5.DataSource = table;

            }
            conn.Close();


           
            // Заполнение космбобокса на вкладке расписание типом занятий
            comboBox2.Items.Add("Практика");
            comboBox2.Items.Add("Лекция");

            // Заполнение комбобокса на вкладке расписание днями недели
            comboBox3.Items.Add("Понедельник");
            comboBox3.Items.Add("Вторник");
            comboBox3.Items.Add("Среда");
            comboBox3.Items.Add("Четверг");
            comboBox3.Items.Add("Пятница");
            comboBox3.Items.Add("Суббота");
            comboBox3.Items.Add("Воскресенье");

            // Заполнение комбобокса на вкладке расписание названиями групп
            /*comboBox7.Items.Add("ПБ-201");
            comboBox7.Items.Add("ПБ-202с");
            comboBox7.Items.Add("бГРАД-183");
            comboBox7.Items.Add("ПБ-205");
            comboBox7.Items.Add("ПБ-201с");
            comboBox7.Items.Add("бГРАД-182");*/


        }
        
        public class Group
        {
            
            public string name { get; set; }
            public int ID { get; set; }
            public override string ToString()
            {
                return string.Format("name", name);
            }

        }

        public class Teacher
        {

            public string fio { get; set; }
            public int ID { get; set; }
            public override string ToString()
            {
                return string.Format("FIO", fio);
            }

        }

        public class Discipline
        {

            public string name { get; set; }
            public int ID { get; set; }
            public override string ToString()
            {
                return string.Format("FIO", name);
            }

        }

        public class Specialist
        {

            public string position { get; set; }
            public int ID { get; set; }
            public override string ToString()
            {
                return string.Format("Position", position);
            }

        }


        


        private void Form1_Load(object sender, EventArgs e)
        {

            //Заполнение комбобокса на вкладке расписание названиями групп со связкой к данным
            conn.Open();
            NpgsqlCommand Command2 = new NpgsqlCommand();
            Command2.Connection = conn;
            Command2.CommandText = $"Select ID, name from Groups";
            
            List<Group> group = new List<Group>();
            {
                NpgsqlDataReader dr = Command2.ExecuteReader();
                while (dr.Read())
                {
                    Group gr = new Group
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        name = dr["name"].ToString()
                    };
                    group.Add(gr);
                }
            }
            
            
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "ID";
            comboBox1.DataSource = group;
            conn.Close();


            //Заполнение комбобокса на вкдадке студенты названиями групп со связкой к данным
            conn.Open();
            NpgsqlCommand Command22 = new NpgsqlCommand();
            Command22.Connection = conn;
            Command22.CommandText = $"Select ID, name from Groups";

            List<Group> groups = new List<Group>();
            {
                NpgsqlDataReader dr = Command22.ExecuteReader();
                while (dr.Read())
                {
                    Group gr = new Group
                    {
                        ID = int.Parse(dr["ID"].ToString()),
                        name = dr["name"].ToString()
                    };
                    group.Add(gr);
                }
            }


            comboBox7.DisplayMember = "name";
            comboBox7.ValueMember = "ID";
            comboBox7.DataSource = group;
            conn.Close();



            //Заполнение комбобокса на вкладке расписания именами преподвателей со связкой к данным
            conn.Open();
            NpgsqlCommand Command3 = new NpgsqlCommand();
            Command3.Connection = conn;
            Command3.CommandText = $"Select ID, fio from Teacher";

            List<Teacher> teacher = new List<Teacher>();
            {
                NpgsqlDataReader de = Command3.ExecuteReader();
                while (de.Read())
                {
                    Teacher th = new Teacher
                    {
                        ID = int.Parse(de["ID"].ToString()),
                        fio = de["FIO"].ToString()
                    };
                    teacher.Add(th);
                }
            }


            comboBox4.DisplayMember = "fio";
            comboBox4.ValueMember = "ID";
            comboBox4.DataSource = teacher;
            conn.Close();

            //Заполнение комбобокса на вкладке расписание названиями дисциплин со связкой к данным
            conn.Open();
            NpgsqlCommand Command4 = new NpgsqlCommand();
            Command4.Connection = conn;
            Command4.CommandText = $"Select ID, name from Discipline";

            List<Discipline> discipline = new List<Discipline>();
            {
                NpgsqlDataReader dp = Command4.ExecuteReader();
                while (dp.Read())
                {
                    Discipline di = new Discipline
                    {
                        ID = int.Parse(dp["ID"].ToString()),
                        name = dp["name"].ToString()
                    };
                    discipline.Add(di);
                }
            }


            comboBox5.DisplayMember = "name";
            comboBox5.ValueMember = "ID";
            comboBox5.DataSource = discipline;
            conn.Close();


            //Преподаватели
            //Заполнение комбобокса на вкладке преподаватели названиями должностей со связкой к данным 
            conn.Open();
            NpgsqlCommand Command5 = new NpgsqlCommand();
            Command5.Connection = conn;
            Command5.CommandText = $"Select ID, Position from Specialist";

            List<Specialist> position = new List<Specialist>();
            {
                NpgsqlDataReader po = Command5.ExecuteReader();
                while (po.Read())
                {
                    Specialist dq = new Specialist
                    {
                        ID = int.Parse(po["ID"].ToString()),
                        position = po["Position"].ToString()
                    };
                    position.Add(dq);
                }
            }


            comboBox6.DisplayMember = "position";
            comboBox6.ValueMember = "ID";
            comboBox6.DataSource = position;
            conn.Close();



        }


        

       
         
        //Добавление информации в таблицу расписание с проверкой текстбоксов
private void button1_Click(object sender, EventArgs e)
        {
            if (textBox9.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox7.Text.Trim() == "" || textBox8.Text.Trim() == "")
            {
                MessageBox.Show("Вы не ввели все необходимые данные!!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            else
            {

            }
            try
            {
               
                conn.Open();
                var com = new NpgsqlCommand(
                    $"Insert Into Timetable (ID, ID_Groups, ID_Teacher, ID_Discipline, NAME, Kind_Of_Couples, Day_Of_Week, Steam_Time, Audience_Number) Values({int.Parse(textBox9.Text)}, {comboBox1.SelectedValue}, {comboBox4.SelectedValue}, {comboBox5.SelectedValue}, N'{textBox4.Text}', N'{comboBox2.Text}', N'{comboBox3.Text}', '{DateTime.Parse(textBox7.Text).ToString("yyyy-MM-dd HH:mm:ss")}', {int.Parse(textBox8.Text)})", conn);
                com.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Информация добавлена");
                textBox9.Clear();


                textBox4.Clear();
                textBox7.Clear();
                textBox8.Clear();

                button3_Click(null, null);

                
            }
            catch (Exception ex) { }

            
        }

        
        //Редактирование данных в таблице расписание
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                int ID = int.Parse(textBox9.Text);
                var Groups_ID = comboBox1.SelectedValue;
                var Teacher_ID = comboBox4.SelectedValue;
                var Discipline_ID = comboBox5.SelectedValue;
                string name = textBox4.Text;
                string kind = comboBox2.Text;
                string day = comboBox3.Text;
                string time = DateTime.Parse(textBox7.Text).ToString("yyyy-MM-dd HH:mm:ss");
                int aud = int.Parse(textBox8.Text);

                string Query = "UPDATE timetable SET ID_Groups = '" + Groups_ID + "', ID_Teacher = '" + Teacher_ID + "', ID_Discipline = '" + Discipline_ID + "', NAME = '" + name + "', Kind_Of_Couples = '" + kind + "', Day_Of_Week = '" + day + "', Steam_Time = '" + time + "', Audience_Number = '" + aud + "' WHERE ID = '" + ID + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Информация обновлена");
                textBox9.Clear();

                textBox4.Clear();
                textBox7.Clear();
                textBox8.Clear();
            }
            catch { }
           


        }

        //Удаление данных из таблицы расписание
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox9.Text.Trim() == "" )
            {
                MessageBox.Show("Вы не ввели все необходимые данные!!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            else
            {

            }
            conn.Open();
                int ID = int.Parse(textBox9.Text);
                string Query = "DELETE from Timetable where ID= " + ID;
                NpgsqlCommand cmd = new NpgsqlCommand(Query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Информация удалена");

                textBox9.Clear();
                button3_Click(null, null);
           
        }

       //Кнопка обновить на вкладке расписание
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                conn.Open();

                string Query = "select * from myTimetable4";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, conn);


                NpgsqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;


                }


                dataGridView1.Refresh();


                conn.Close();
            }
            catch { }
            
        }

        //Поисковая система на вкладке расписание
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                var searchData = textBox5.Text;

                string Query = "Select * from myTimetable4 where Группа like '%" + searchData + "%' or Преподаватель like '%" + searchData + "%' or Дисциплина like '%" + searchData + "%' or Тема like '%" + searchData + "%' or Занятие like '%" + searchData + "%'  or День like '%" + searchData + "%' or Аудитория like '%" + searchData + "%'";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, conn);
                cmd.ExecuteNonQuery();


                NpgsqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;


                }


                dataGridView1.Refresh();
                conn.Close();
            }
            catch { }
            

        }

       
        //Кнопка автозаполнения полей по номеру ID на вкладке расписание
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox9.Text.Trim() == "")
            {
                MessageBox.Show("Вы не ввели все необходимые данные!!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            else
                try
                {
                    conn.Open();
                    int ID = int.Parse(textBox9.Text);
                    string Query = "select * from myTimetable4 where №= " + ID;
                    NpgsqlCommand cmd = new NpgsqlCommand(Query, conn);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        comboBox1.Text = reader["Группа"].ToString();
                        comboBox4.Text = reader["Преподаватель"].ToString();
                        comboBox5.Text = reader["Дисциплина"].ToString();
                        textBox4.Text = reader["Тема"].ToString();

                        comboBox2.Text = reader["Занятие"].ToString();
                        comboBox3.Text = reader["День"].ToString();
                        textBox7.Text = reader["Время"].ToString();
                        textBox8.Text = reader["Аудитория"].ToString();

                    }
                    conn.Close();
                }
                catch { }
            
            
        }


        private void button6_Click(object sender, EventArgs e)
        {
           /* conn.Open();
            string name = comboBox1.Text;
            string Query = "select id from Groups where name = '%" + name + "%'";
            NpgsqlCommand cmd = new NpgsqlCommand(Query, conn);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                comboBox1.Text = reader["ID"].ToString();
            }*/

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        //Преподаватели
        //Добавление данных в таблицу преподаватели с проверкой текстбоксов
        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox6.Text.Trim() == "" || textBox10.Text.Trim() == "")
                {
                    MessageBox.Show("Вы не ввели все необходимые данные!!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
                else
                {

                }
                conn.Open();
                var com = new NpgsqlCommand(
                    $"Insert Into Teacher (ID, ID_Specialist, work_experience, fio, salary, personal_number) Values({int.Parse(textBox1.Text)}, {comboBox6.SelectedValue},  N'{textBox2.Text}',  N'{textBox3.Text}', '{textBox6.Text}', {textBox10.Text})", conn);
                com.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Информация добавлена");
                textBox1.Clear();


                textBox2.Clear();
                textBox3.Clear();
                textBox6.Clear();
                textBox10.Clear();
                button9_Click(null, null);
            }
            catch (Exception ex) { }
        }

        //Кнопка обновить на вкладке преподватели
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                string Query = "select * from myTeacher1";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, conn);


                NpgsqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView3.DataSource = dt;


                }


                dataGridView3.Refresh();


                conn.Close();
            }
            catch { }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Редактирование данных в таблице преподаватели
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                int ID = int.Parse(textBox1.Text);
                var id_specialist = comboBox6.SelectedValue;

                string work = textBox2.Text;
                string fio = textBox3.Text;
                string salary = textBox6.Text;
                string pers = textBox10.Text;


                string Query = "UPDATE teacher SET ID_Specialist = '" + id_specialist + "', work_experience = '" + work + "', fio = '" + fio + "', salary = '" + salary + "', personal_number = '" + pers + "' WHERE ID = '" + ID + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Информация обновлена");
                textBox1.Clear();

                textBox2.Clear();
                textBox3.Clear();
                textBox6.Clear();
                textBox10.Clear();
            }
            catch  { }

        }

        //Удаление данных по номеру ID в таблице преподаватели
        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Вы не ввели все необходимые данные!!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            else
            {

            }
            try
            {
                conn.Open();
                int ID = int.Parse(textBox1.Text);
                string Query = "DELETE from Teacher where ID= " + ID;
                NpgsqlCommand cmd = new NpgsqlCommand(Query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Информация удалена");

                textBox1.Clear();
                button9_Click(null, null);
            }
            catch { }
        }


        // Студенты
        //Добавление данных в таблицу Студенты
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox11.Text.Trim() == "" || textBox12.Text.Trim() == "" || textBox13.Text.Trim() == "")
                {
                    MessageBox.Show("Вы не ввели все необходимые данные!!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
                else
                {

                }
                conn.Open();
                var com = new NpgsqlCommand(
                    $"Insert Into Students (ID, fio, phone_number, groupss, date_of_birth) Values({int.Parse(textBox11.Text)}, N'{textBox12.Text}',  N'{textBox13.Text}',  N'{comboBox7.Text}', '{DateTime.Parse(dateTimePicker1.Text).ToString("yyyy-MM-dd HH:mm:ss")}')", conn);
                com.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Информация добавлена");
                textBox11.Clear();


                textBox12.Clear();
                textBox13.Clear();
                textBox6.Clear();

                button10_Click(null, null);
            }
            catch { }
        }

        //Кнопка обновить на вкладке студенты
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                string Query = "select * from Students";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, conn);


                NpgsqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView4.DataSource = dt;


                }


                dataGridView4.Refresh();


                conn.Close();
            }
            catch { }
        }

        //Редактирование данных в таблице студенты
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                int ID = int.Parse(textBox11.Text);
                string fio = textBox12.Text;
                var group = comboBox7.Text;
                string numb = textBox13.Text;



                string date = DateTime.Parse(dateTimePicker1.Text).ToString("yyyy-MM-dd HH:mm:ss");

                string Query = "UPDATE Students SET fio = '" + fio + "', phone_number = '" + numb + "', groupss = '" + group + "', date_of_birth = '" + date + "' WHERE ID = '" + ID + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Информация обновлена");
                textBox11.Clear();

                textBox12.Clear();
                textBox13.Clear();
            }
            catch { }
            
           
        }

        //Удаление данных по номеру ID на вкладке студенты
        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox11.Text.Trim() == "" )
            {
                MessageBox.Show("Вы не ввели все необходимые данные!!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            else
            {

            }
            try
            {
                conn.Open();
                int ID = int.Parse(textBox11.Text);
                string Query = "DELETE from students where ID= " + ID;
                NpgsqlCommand cmd = new NpgsqlCommand(Query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Информация удалена");

                textBox1.Clear();
                button10_Click(null, null);
            }
            catch { }
        }


        //Преподаватели (Автозаполнение)
        //Кнопка автозаполнения полей по номеру ID на вкладке преподватели
        private void button14_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Вы не ввели все необходимые данные!!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            else
            {

            }
            try
            {
                conn.Open();
                int ID = int.Parse(textBox1.Text);
                string Query = "select * from myTeacher1 where №= " + ID;
                NpgsqlCommand cmd = new NpgsqlCommand(Query, conn);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    comboBox6.Text = reader["Специальность"].ToString();

                    textBox2.Text = reader["Опыт_работы"].ToString();


                    textBox3.Text = reader["ФИО"].ToString();
                    textBox6.Text = reader["Зарплата"].ToString();
                    textBox10.Text = reader["Персональный_номер"].ToString();


                }
                conn.Close();
            }
            catch { }

        }

        //Ограничения ввода
        //Запрет ввода букв в текстбокс ID на вкладке расписание
        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            else
                e.Handled = true;
        }

        //Запрет ввода букв в текстбокс номера аудитории на вкладке расписание
        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            else
                e.Handled = true;
        }

        //Запрет ввода букв в текстбокса ID на вкладке преподаватели
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            else
                e.Handled = true;
        }


        //Запрет ввода букв в текстбокс персонального номера на вкладке преподватели
        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            else
                e.Handled = true;
        }

        //Запрет ввода букв в текстбокс ID на вкладке студенты
        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) return;
            else
                e.Handled = true;
        }

        //Маска ввода для номера телефона на вкладке студенты
        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) ||
            (!string.IsNullOrEmpty(textBox13.Text) && e.KeyChar == '+'))
            {
                return;
            }

            e.Handled = true;
            textBox13.MaxLength = 12;

        }


        //Студенты (Автозаполнение)
        //Кнопка автозаполнения полей по номеру ID на вкладке студенты
        private void button15_Click(object sender, EventArgs e)
        {
            if (textBox11.Text.Trim() == "")
            {
                MessageBox.Show("Вы не ввели все необходимые данные!!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            else
            {

            }
            try
            {
                conn.Open();
                int ID = int.Parse(textBox11.Text);
                string Query = "select * from Students where ID= " + ID;
                NpgsqlCommand cmd = new NpgsqlCommand(Query, conn);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    comboBox7.Text = reader["groupss"].ToString();

                    textBox12.Text = reader["fio"].ToString();


                    textBox13.Text = reader["phone_number"].ToString();
                    dateTimePicker1.Text = reader["date_of_birth"].ToString();
                    

                }
                conn.Close();
            }
            catch { }

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        //Поисковая система на вкладке преподаватели
        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                var searchData = textBox14.Text;

                string Query = "Select * from myTeacher1 where Специальность like '%" + searchData + "%' or Кафедра like '%" + searchData + "%' or Опыт_работы like '%" + searchData + "%' or ФИО like '%" + searchData + "%' ";
                NpgsqlCommand cmd = new NpgsqlCommand(Query, conn);
                cmd.ExecuteNonQuery();


                NpgsqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView3.DataSource = dt;

                    
                }


                dataGridView3.Refresh();
                conn.Close();
            }
            catch { }

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}

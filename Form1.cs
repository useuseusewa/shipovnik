using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace bredkakoito
{
    public partial class Form1 : Form
    {
   
        private string connectionString = "Server= localhost ;Database= apteka ;Uid= batat ;Pwd= Alliseeisred12!;";
        private DataTable productDataTable = new DataTable();
        private int currentRow = -1;
        private int userId = 0; 
        public Form1()
        {
            InitializeComponent();
            // Начальная вкладка
            TabControl.SelectedTab = TabControl.TabPages[1];
             // Загрузка данных о продуктах
            LoadProductData();
            
            groupBox1.Visible = false;
            groupBox4.Visible = false;
            button20.Visible = true;
            button19.Visible = true;
            button28.Visible = false;
           

        }

    private void button4_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[1];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            string name = textBox41.Text;
            string surname = textBox42.Text;
            string email = textBox43.Text;
            string phone = textBox44.Text;
            string password = textBox45.Text;
            string kod = textBox46.Text;
            // Валидация данных
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(kod))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            try
            {
               
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
              
                    connection.Open();

                    string query = "INSERT INTO user (name, surname, email, phone, password, kod) VALUES (@name, @surname, @email, @phone, @password, @kod)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                     
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@surname", surname);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@phone", phone);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@kod", kod);

                      
                        command.ExecuteNonQuery();

                        MessageBox.Show("Регистрация прошла успешно!");

                        textBox41.Text = "";
                        textBox42.Text = "";
                        textBox43.Text = "";
                        textBox44.Text = "";
                        textBox45.Text = "";
                        textBox46.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при регистрации: " + ex.Message);
            }
            TabControl.SelectedTab = TabControl.TabPages[1]; 
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string email = textBox7.Text;
            string password = textBox8.Text;
            string kod = textBox9.Text;
            
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(kod))
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя и пароль.");
                return;
            }
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT id, name, surname, phone, email, password FROM user WHERE email = @email AND password = @password AND kod = @kod";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@kod", kod);
                        MySqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            MessageBox.Show("Авторизация успешна!");
                            textBox7.Text = "";
                            textBox8.Text = "";
                            textBox9.Text = "";
                            userId = Convert.ToInt32(reader["id"]);
                            // Логика перехода на вкладку
                            if ((kod == "QQQ") || (kod == "RRR") || (kod == "UUU"))
                            {
                                TabControl.SelectedTab = TabControl.TabPages[4];
                            }
                            else
                            {
                                TabControl.SelectedTab = TabControl.TabPages[2];//открытие нужной вкладки
                             }
                            LoadUserData(userId);
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин или пароль.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при авторизации: " + ex.Message);
            }
        }
        private void LoadUserData(int userId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT name, surname, phone, email, password FROM user WHERE id = @userId";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@userId", userId);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        textBox16.Text = reader["name"].ToString();
                        textBox15.Text = reader["surname"].ToString();
                        textBox14.Text = reader["phone"].ToString();
                        textBox13.Text = reader["email"].ToString();
                        textBox12.Text = reader["password"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден.");
                    }
                }
                catch (Exception ex)
                {
                // Логируйте ошибку вместо показа пользователю
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        private void LoadUserData1(int userId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Запрос на получение информации о пользователе
                    string query = "SELECT name, surname, phone, email, password, kod FROM user WHERE id = @userId";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@userId", userId);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        textBox29.Text = reader["name"].ToString();
                        textBox30.Text = reader["surname"].ToString();
                        textBox31.Text = reader["phone"].ToString();
                        textBox32.Text = reader["email"].ToString();
                        textBox33.Text = reader["password"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        private void button18_Click(object sender, EventArgs e)
        {
         //можно добавить логику, чтобы не скрывать группы, если они уже скрыты
            TabControl.SelectedTab = TabControl.TabPages[2];
            groupBox1.Visible = false;
            groupBox4.Visible = false;
        }
        private void button17_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[7];
        }
        private void button14_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[7];
        }
        private void button16_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[8];
        }
        private void button15_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[8];
        }
        private void button6_Click(object sender, EventArgs e)
        {
            string name = textBox16.Text;
            string surname = textBox15.Text;
            string phone = textBox14.Text;
            string email = textBox13.Text;
            string password = textBox12.Text;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE user SET name = @name, surname = @surname, phone = @phone, email = @email, password = @password WHERE id = @userId";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@surname", surname);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@userId", userId);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные обновлены!");
                        LoadUserData(userId);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при обновлении данных.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string searchText = textBox10.Text.Trim();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM product WHERE name LIKE '%" + searchText + "%'";
                    // можно закрыть соединение с помощью MySqlDataReader, чтобы избежать возможных проблем
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        textBox23.Text = reader["name"].ToString();
                        textBox22.Text = reader["description"].ToString();
                        textBox24.Text = reader["price"].ToString();
                        textBox25.Text = reader["form"].ToString();
                        textBox26.Text = reader["sale"].ToString();
                        textBox27.Text = reader["availability"].ToString();
                    }
                    else
                    {
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
                TabControl.SelectedTab = TabControl.TabPages[6];//открытие нужной вкладки 
            }
        }
        private void button19_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[2];//открытие нужной вкладки 
        }
        private void LoadProductData()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM product";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable productTable = new DataTable();
                    adapter.Fill(productTable);
                    dataGridView1.DataSource = productTable;
                }
                //можно более детально обрабатывать исключения
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[9];
            if (groupBox4.Visible)
            {
                groupBox4.Visible = false;
            }
            groupBox1.Visible = true;
        }
        private void button13_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[10];
            if (groupBox1.Visible)
            {
                groupBox1.Visible = false;
            }
            groupBox4.Visible = true;
        }
        //удалить пустой метод
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button22_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[5];
            if (groupBox4.Visible)
            {
                groupBox4.Visible = false;
            }
            groupBox1.Visible = true;
        }
        private void button23_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[5];
            if (groupBox1.Visible)
            {
                groupBox1.Visible = false;
            }
            groupBox4.Visible = true;
        }
        //удалить пустой метод
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        //удалить пустой метод
        private void button10_Click(object sender, EventArgs e)
        {

        }
        private void button20_Click(object sender, EventArgs e)
        {
            string productName = textBox23.Text;
            int productId = GetProductIdByName(productName);

            if (productId == -1)
            {
                MessageBox.Show("Товар не найден.");
                return;
            }
            if (!string.IsNullOrEmpty(productName))
            {
                checkedListBox2.Items.Add(productName);
                MessageBox.Show($"{productName} добавлен в корзину.");
            }
            else
            {
                MessageBox.Show("Введите слово для добавления!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Уведомление о пустом вводе
            }
        }
        private int GetProductIdByName(string productName)
        {
        //добавить проверку перед выполнением запрос, что ProductName не является аустой строкой
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id FROM product WHERE name = @productName";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@productName", productName);
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        //удаление пустых методов
        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void textBox9_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string[] allowedValues = { "QQQ", "RRR", "UUU", "-" }; 
            string inputValue = textBox9.Text;
            if (!allowedValues.Contains(inputValue))
            {
                if (Control.ModifierKeys == Keys.Shift ||
                  Control.ModifierKeys == Keys.Control ||
                  Control.ModifierKeys == Keys.Alt)
                {
                    return;
                }
                MessageBox.Show("Ошибка: Неверный код доступа");
            }
        }
        //удаление пустых методов
        private void textBox6_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            string kod = textBox9.Text;
            if (kod == "QQQ" || kod == "RRR" || kod == "UUU")
            {
                button20.Visible = false;
                button19.Visible = false;
                button28.Visible = true;
                if (kod == "QQQ")
                {
                    label58.Text = "г. Новосибирск, ул. Кирова, д. 86";
                }
                else
                {
                    if (kod == "RRR")
                    {
                        label58.Text = "г. Красноярск, ул. Ленина, д. 49";
                    }
                    else
                    {
                        label58.Text = "г. Москва, ул. Чаянова, д. 6";
                    }
                }
            }
            if (kod == "-")
            {
                button20.Visible = true;
                button19.Visible = true;
                button28.Visible = false;
            }
        }
        private void button26_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[11];
        }
        private void button9_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[2];
        }
        private void button8_Click(object sender, EventArgs e)

        {
            TabControl.SelectedTab = TabControl.TabPages[5];
            List<string> selectedItems = checkedListBox2.CheckedItems.Cast<string>().ToList();
            string orderString = string.Join(", ", selectedItems);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand("INSERT INTO ordero ( name) VALUES ( @name)", connection))
                {
                    command.Parameters.AddWithValue("@name", orderString); 
                    try
                    {
                    // можно добавить сообщение об успешном добавлении
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при вставке заказа: " + ex.Message);
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }

            listBox3.Items.Add($"Заказ: {orderString}");
            listBox2.Items.Add($"Заказ: {orderString}");
            checkedListBox2.ClearSelected();
        }
        private void SaveOrderToDatabase(string заказ)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO ordero (order_text) VALUES (@orderText)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@orderText", заказ);
                    int rowsAffected = command.ExecuteNonQuery();
                  if (rowsAffected > 0)
                    {
                        MessageBox.Show("Заказ успешно сохранен.");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при сохранении заказа.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        //удаление пустых методов
        private void groupBox4_Enter(object sender, EventArgs e)
        {
            
        }
        //удаление пустых методов
        private void groupBox1_Enter(object sender, EventArgs e)
        {
           
        
    }

        private void button10_Click_1(object sender, EventArgs e)
        {
            string searchText = textBox28.Text.Trim();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM product WHERE name LIKE '%" + searchText + "%'";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        textBox23.Text = reader["name"].ToString();
                        textBox22.Text = reader["description"].ToString();
                        textBox24.Text = reader["price"].ToString();
                        textBox25.Text = reader["form"].ToString();
                        textBox26.Text = reader["sale"].ToString();
                        textBox27.Text = reader["availability"].ToString();
                    }
                    else
                    {
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }

                TabControl.SelectedTab = TabControl.TabPages[6];
            }
        }
        private void button28_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[4];
        }
        private void button27_Click(object sender, EventArgs e)
        {
            string name = textBox29.Text;
            string surname = textBox30.Text;
            string phone = textBox32.Text;
            string email = textBox31.Text;
            string password = textBox33.Text;
            string kod = textBox34.Text;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE user SET name = @name, surname = @surname, phone = @phone, email = @email, password = @password, kod = @kod WHERE id = @userId";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@surname", surname);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@kod", kod);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Данные обновлены!");
                        LoadUserData1(userId);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при обновлении данных.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        private void textBox16_TextChanged(object sender, EventArgs e)
        {
        }
        private void button11_Click_1(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[3];//открытие нужной вкладки 
        }
        private void button29_Click(object sender, EventArgs e)
        {
            string name = textBox35.Text;
            string description = textBox36.Text;
            decimal price = decimal.Parse(textBox37.Text);
            string form = textBox38.Text;
            string sale = textBox39.Text;
            string availability = textBox40.Text;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO product (name, description, price, form, sale, availability) VALUES (@name, @description, @price, @form, @sale, @availability )";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@form", form);
                    command.Parameters.AddWithValue("@sale", sale);
                    command.Parameters.AddWithValue("@availability", availability);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Товар добавлен!");
                        LoadProductData();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при добавлении товара.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        private void textBox35_TextChanged(object sender, EventArgs e)
        {
        }
        private void button30_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[4];//открытие нужной вкладки 
        }
        private void button25_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int productId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                if (MessageBox.Show("Вы уверены, что хотите удалить этот товар?", "Подтверждение удаления", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DeleteProduct(productId);
                    LoadProductData();
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.");
            }
        }
        private void DeleteProduct(int productId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM product WHERE id = @productId";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@productId", productId);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Товар удален!");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении товара.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
       
        private void textBox46_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string[] allowedValues = { "QQQ", "RRR", "UUU", "-" }; 
            string inputValue = textBox46.Text;
            if (!allowedValues.Contains(inputValue))
            {
                if (Control.ModifierKeys == Keys.Shift ||
                  Control.ModifierKeys == Keys.Control ||
                  Control.ModifierKeys == Keys.Alt)
                {
                    return; 
                }
                MessageBox.Show("Ошибка: Неверный код доступа");
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void button24_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[8];//открытие нужной вкладки 
        }
        private void label58_TextChanged(object sender, EventArgs e)
        {
            string labelText = label58.Text;
            listBox1.Items.Clear();
            if (labelText == "г. Новосибирск, ул. Кирова, д. 86")
            {
                listBox1.Items.Add("Иванов Ф.Р. +79564785643");
                listBox1.Items.Add("Петров Ц.Е. +73452348754");
                listBox1.Items.Add("Сидоров Н.В. +7458457690");
            }
            if (labelText == "г. Красноярск, ул. Ленина, д. 49")
            {
                listBox1.Items.Add("Пирогов У.Ш. +75467459832");
                listBox1.Items.Add("Виноградов А.И. +74563469827");
                listBox1.Items.Add("Шаманова Н.С. +72469832487");
            }
            if (labelText == "г. Москва, ул. Чаянова, д. 6")
            {
                listBox1.Items.Add("Гриценко Я.З. +72457864538");
                listBox1.Items.Add("Цветкова З.М. +77854536712");
                listBox1.Items.Add("Йодный И.М. +79853086451");
            }
            else
            {
                listBox1.Items.Add("");
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            string city = textBox11.Text; 
            string street = textBox17.Text;
            string house = textBox18.Text;
            string entrance = textBox19.Text;
            string floor = textBox20.Text; 
            string flat = textBox21.Text;
            if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(street) || string.IsNullOrEmpty(house) ||
              string.IsNullOrEmpty(entrance) || string.IsNullOrEmpty(floor) || string.IsNullOrEmpty(flat))
            {
                MessageBox.Show("Пожалуйста, заполните все поля доставки.");
                return; 
            }
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand("INSERT INTO delivery (city, street, house, entrance, floor, flat) VALUES (@city, @street, @house, @entrance, @floor, @flat)", connection))
                {
                    command.Parameters.AddWithValue("@city", city);
                    command.Parameters.AddWithValue("@street", street);
                    command.Parameters.AddWithValue("@house", house);
                    command.Parameters.AddWithValue("@entrance", entrance);
                    command.Parameters.AddWithValue("@floor", floor);
                    command.Parameters.AddWithValue("@flat", flat);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Данные доставки добавлены в базу данных.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при добавлении данных доставки: " + ex.Message);
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }
        private void button21_Click(object sender, EventArgs e)
        {
            string address = comboBox1.SelectedItem.ToString();
            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Пожалуйста, выберите адрес и город.");
                return; 
            }
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand("INSERT INTO adpvz (adress) VALUES (@address)", connection))
                {
                    command.Parameters.AddWithValue("@address", address);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Данные адреса добавлены в базу данных.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при добавлении данных адреса: " + ex.Message);
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
        }
        //удаление пустых методов
        private void label29_Click(object sender, EventArgs e)
        {
        }
        private void label30_Click(object sender, EventArgs e)
        {
        }

        private void button11_Click(object sender, EventArgs e)
        {
            TabControl.SelectedTab = TabControl.TabPages[3];//открытие нужной вкладки 
        }
    }
}



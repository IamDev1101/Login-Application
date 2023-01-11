using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Login_Application
{
    public partial class Form3 : Form
    {
        public Form3()
        {


            InitializeComponent();
            this.ShowInTaskbar = true;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
        }
        MySqlConnection sql;
        MySqlCommand cmd;
        bool RegEmailValid = false;
        bool PassWordMatch = false;
        bool UsernameAvailable = true;
        bool IsLogged = false;
        int username;



        private void Form3_Load(object sender, EventArgs e)
        {
        
            sql = new MySqlConnection("Server=127.0.0.1;Uid=root;Pwd=<yourpassword>;");

            try
            {
 
                sql.Open();

                MessageBox.Show("Database connected successfully \n\nState:"+sql.State.ToString());
                String commandStr = "CREATE DATABASE IF NOT EXISTS users";

               cmd = new MySqlCommand(commandStr, sql);

                cmd.ExecuteNonQuery();

                cmd.CommandText = "USE users;";
                cmd.ExecuteNonQuery();


                cmd.CommandText = "CREATE TABLE IF NOT EXISTS data (username varchar(255),email varchar(255),password varchar(255));";
                cmd.ExecuteNonQuery();


            }
            catch(Exception error)
            {
                MessageBox.Show("Database connection faild, Exitng..\n\n"+error.Message);
                Application.Exit();

      
            } 
          
        }
        private void button3_Click(object sender, EventArgs e)
        {
            button4.Visible = true;
            panel2.Visible = false;
            panel6.Visible = true; 
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button4.Visible = true;
            panel2.Visible = false;
            panel1.Visible = true;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (panel1.Visible || panel6.Visible)
            {
                BackButtonfnc();
            }
        }

        private void BackButtonfnc()
        {
            panel1.Visible = false;
            panel6.Visible = false;
            panel2.Visible = true;
            panel4.Visible = false;
            button4.Visible = false;
            textBox9.UseSystemPasswordChar = false;
            textBox2.UseSystemPasswordChar = false;
            textBox4.Text = ""; textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox10.Text = ""; textBox9.Text = "";
            label8.Text = "";
            label1.Text = "";
            label2.Text = "";
            label6.Text = "";
            label25.Text = "";
            label23.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (textBox10.Text == "")
            {
                label25.Text = "Email cannot be empty.";
            }
            else
            {
                label25.Text = "";
            }
            if (textBox9.Text == "")
            {
                label23.Text = "Password cannot be empty.";
            }
            else
            {
                label23.Text = "";
            }

            if(textBox10.Text != "" && textBox9.Text != "")
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT * FROM data WHERE (email=@email and password=@password)"; 
                cmd.Parameters.AddWithValue("@password", textBox9.Text); 
                cmd.Parameters.AddWithValue("@email", textBox10.Text);
                try
                {
                    MySqlDataReader executequery = cmd.ExecuteReader();
                    if (executequery.Read())
                    {
                        MessageBox.Show("Login Successfull.");
                        IsLogged = true;
                        label13.Text = "Username: " + executequery["username"];
                        panel1.Visible = false;
                        panel6.Visible = false;
                        panel2.Visible = false;
                        panel4.Visible = true;
                        button4.Visible = false;

                        label8.Text = "";
                        label1.Text = "";
                        label2.Text = "";
                        label6.Text = "";

                    }
                    else
                    {
                        MessageBox.Show("User not found, Please register.");
                    }
                    executequery.Close();
                }
                catch
                {

                    MessageBox.Show("User not found, Please register.");
                }





            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT COUNT(email) FROM users.data WHERE email like @email";
            cmd.Parameters.AddWithValue("@email", textBox1.Text);
            int email = Convert.ToInt32(cmd.ExecuteScalar());

            if (textBox4.Text == "")
            {
                label8.Text = "Username cannot be empty.";
            }
            else if (username > 0)
            {

            }

            if (textBox1.Text == "")
            {
                label1.Text = "Email cannot be empty.";
            }
            else if (RegEmailValid)
            {

            }

            if (textBox2.Text == "")
            {
                label2.Text = "Password cannot be empty.";
            }
            else if (!PassWordMatch)
            {
                label6.Text = "Passwords are not maching.";
            }
            else
            {
                label2.Text = "";
            }


            if (textBox3.Text == "")
            {
                label6.Text = "Password confirm cannot be empty.";
            }
            else if (!PassWordMatch)
            {
                label6.Text = "Passwords are not maching.";
            }
            else
            {
                label6.Text = "";
            }


            if ((textBox4.Text != "" && textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "") && PassWordMatch && (RegEmailValid && email == 0) && UsernameAvailable)
            {


                {

                    cmd.CommandText = "INSERT INTO data (username, email, password) VALUES ('" + textBox4.Text + "', '" + textBox1.Text + "', '" + textBox2.Text + "')";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registered Successfully");

                    textBox4.Text = ""; textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = "";
                    label8.Text = "";
                    label1.Text = "";
                    label2.Text = "";
                    label6.Text = "";
                }
            }
            else if (email > 0 && textBox1.Text != "" && RegEmailValid)
            {
                MessageBox.Show("Email is has been already registered", "Account found !");
            }


        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox2.Checked)
            {

                textBox3.UseSystemPasswordChar = false;
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {

                textBox3.UseSystemPasswordChar = true;
                textBox2.UseSystemPasswordChar = true;
            }
        }


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                label1.Text = "";
            }
            else if (new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").IsMatch(textBox1.Text)){
                label1.Text = "";
                RegEmailValid = true;

            }
            else
            {
                label1.Text = "Email is not valid";
                RegEmailValid = false;
            }; 
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                label6.Text = ""; 
                label2.Text = "";
        
            }
            else if(textBox3.Text == "")
            {
                label6.Text = ""; 
                label2.Text = "";
            }
            else if (textBox2.Text != textBox3.Text)
            {
                label6.Text = "Passwords are not maching.";
                PassWordMatch = false;
            }else
            {
                label6.Text = ""; 
                PassWordMatch = true;
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                label6.Text = "";

            }
            else if (textBox2.Text == "")
            {
                label6.Text = "";
            }
            else if(textBox2.Text != textBox3.Text )
            {
                label6.Text = "Passwords are not maching.";
                PassWordMatch = false;
            }
            else
            {
                label6.Text = "";
                PassWordMatch = true;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = "SELECT COUNT(username) FROM data WHERE username like @username";
            cmd.Parameters.AddWithValue("@username", textBox4.Text);
           username  = Convert.ToInt32(cmd.ExecuteScalar());

            if(username > 0)
            {
                label8.Text = "Username is already taken."; 
                UsernameAvailable = false;
            }
            else
            {
                label8.Text = ""; 
                UsernameAvailable = true;
            }


        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                textBox9.UseSystemPasswordChar = false;
            }
            else
            {
                textBox9.UseSystemPasswordChar = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (IsLogged)
            {
                BackButtonfnc();
                IsLogged = false;
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}

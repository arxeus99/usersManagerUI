using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UsersManagerUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private const string connectionString = "Server=localhost;Port=3306;Database=biblioteca;Uid=usuari";
        private void findButton_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (findNameTextBox.Text.Trim() == "" & findSurnameTextBox.Text.Trim() == "" & findTownComboBox.Text.Trim() == "")
                {
                    MessageBox.Show("You must introduce some info", "User Manager Error", MessageBoxButtons.OK);
                }
                else
                {
                    var usuaris = new List<User>();
                    MySqlConnection con = new MySqlConnection(connectionString);
                    String sql = "";
                    if (!(findNameTextBox.Text.Trim() == "") & findSurnameTextBox.Text.Trim() == "" & findTownComboBox.Text.Trim() == "")
                    {
                        sql = "SELECT id_usr, dni_usr, nom_usr, llinatge1, pob_usr, email_usr, tipus_usr" +
                            " FROM biblioteca.usuaris WHERE NOM_USR LIKE '" + findNameTextBox.Text + "';";
                    }
                    if ((findNameTextBox.Text.Trim() == "") & !(findSurnameTextBox.Text.Trim() == "") & findTownComboBox.Text.Trim() == "")
                    {
                        sql = "SELECT id_usr, dni_usr, nom_usr, llinatge1, pob_usr, email_usr, tipus_usr" +
                            " FROM biblioteca.usuaris WHERE LLINATGE1 LIKE '" + findSurnameTextBox.Text + "';";
                    }
                    if ((findNameTextBox.Text.Trim() == "") & (findSurnameTextBox.Text.Trim() == "") & !(findTownComboBox.Text.Trim() == ""))
                    {
                        sql = "SELECT id_usr, dni_usr, nom_usr, llinatge1, pob_usr, email_usr, tipus_usr" +
                            " FROM biblioteca.usuaris WHERE POB_USR LIKE '" + findTownComboBox.Text + "';";
                    }
                    if (!(findNameTextBox.Text.Trim() == "") & !(findSurnameTextBox.Text.Trim() == "") & findTownComboBox.Text.Trim() == "")
                    {
                        sql = "SELECT id_usr, dni_usr, nom_usr, llinatge1, pob_usr, email_usr, tipus_usr" +
                            " FROM biblioteca.usuaris WHERE NOM_USR LIKE '" + findNameTextBox.Text +
                            "' AND LLINATGE1 LIKE '"+findSurnameTextBox.Text+"';";
                    }
                    if (!(findNameTextBox.Text.Trim() == "") & (findSurnameTextBox.Text.Trim() == "") & !(findTownComboBox.Text.Trim() == ""))
                    {
                        sql = "SELECT id_usr, dni_usr, nom_usr, llinatge1, pob_usr, email_usr, tipus_usr" +
                            " FROM biblioteca.usuaris WHERE NOM_USR LIKE '" + findNameTextBox.Text +
                            "' AND POB_USR LIKE '" + findTownComboBox.Text + "';";
                    }
                    if ((findNameTextBox.Text.Trim() == "") & !(findSurnameTextBox.Text.Trim() == "") & !(findTownComboBox.Text.Trim() == ""))
                    {
                        sql = "SELECT id_usr, dni_usr, nom_usr, llinatge1, pob_usr, email_usr, tipus_usr" +
                            " FROM biblioteca.usuaris WHERE LLINATGE1 LIKE '" + findSurnameTextBox.Text +
                            "' AND POB_USR LIKE '" + findTownComboBox.Text + "';";
                    }
                    if (!(findNameTextBox.Text.Trim() == "") & !(findSurnameTextBox.Text.Trim() == "") & !(findTownComboBox.Text.Trim() == ""))
                    {
                        sql = "SELECT id_usr, dni_usr, nom_usr, llinatge1, pob_usr, email_usr, tipus_usr" +
                            " FROM biblioteca.usuaris WHERE NOM_USR LIKE '" + findNameTextBox.Text +
                            "' AND LLINATGE1 LIKE '" + findSurnameTextBox.Text + 
                            "' AND POB_USR LIKE '" +findTownComboBox.Text+ "';";
                    }
                    usuaris = con.Query<User>(sql).ToList();
                    var users = new UserInfo(usuaris);
                    DialogResult result = users.ShowDialog(this);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (passwordTextBox.Text.Equals(confirmTextBox.Text))
            {
                try
                {
                    MySqlConnection con = new MySqlConnection(connectionString);
                    string sql = "SELECT MAX(ID_USR) FROM usuaris;";
                    int newId = 0;
                    try
                    {
                        newId = con.Query<int>(sql).FirstOrDefault();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    newId++;
                    sql = "INSERT INTO biblioteca.usuaris (ID_USR, DNI_USR, NOM_USR, LLINATGE1, POB_USR, " +
                        "EMAIL_USR, PASSWORD) VALUES (" + newId + ",'" + NIFTextBox.Text +
                        "','" + nameTextBox.Text + "','" + surnameTextBox.Text + "','" +
                    townComboBox.Text + "','" + mailTextBox.Text + "','" + passwordTextBox.Text +
                    "');";
                    var rowsAffected = con.Execute(sql);
                    if (rowsAffected == 1)
                    {
                        MessageBox.Show("User registered correctly", "User Manager", MessageBoxButtons.OK);
                        NIFTextBox.Text = string.Empty;
                        nameTextBox.Text = string.Empty;
                        surnameTextBox.Text = string.Empty;
                        townComboBox.Text = string.Empty;
                        mailTextBox.Text = string.Empty;
                        passwordTextBox.Text = string.Empty;
                        confirmTextBox.Text = string.Empty;
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
    }
}

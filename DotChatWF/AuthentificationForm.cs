using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;

namespace DotChatWF
{
  public partial class AuthentificationForm : Form
  {
    public MainForm mForm; // Указатель на этот объект ставится в MainForms.cs!!!!
    public AuthentificationForm()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (loginBox.Text != String.Empty && passBox.Text != String.Empty) // Если логин и пароль заполнены
      {
        // Создаём POST запрос
        var authRequest = (HttpWebRequest)WebRequest.Create("http://localhost:5000/api/Auth");
        authRequest.Method = "POST";
        authRequest.ContentType = "application/json";
        RegistrationForm.AuthData userData = new RegistrationForm.AuthData(); // Сериализуем данные с помощью класса AuthData
        userData.login = loginBox.Text;
        userData.password = passBox.Text;
        userData.ip = ipBoxBox.Text;

        // Отправляем сериализованные данные
        using (System.IO.StreamWriter writejson = new System.IO.StreamWriter(authRequest.GetRequestStream()))
        {
          writejson.Write(JsonConvert.SerializeObject(userData));
        }

        // Получаем ответ
        string authResponse = String.Empty;
        using (System.IO.StreamReader readtoken = new System.IO.StreamReader(authRequest.GetResponse().GetResponseStream()))
        {
          authResponse = readtoken.ReadToEnd();
        }
                
        // Если неправильные данные, то выводим MessageBox
        if (authResponse == "-1")
        {
          MessageBox.Show("Wrong login or password");
        }
        else if (authResponse == "-2")
        {
          MessageBox.Show("You are not registered");
        }
        else
        {
          if (mForm.TextBox_username.Text != "You are not logged in")
          {
            MainForm.GettingOffline(mForm.TextBox_username.Text); // Отправляем информацию о том, что юзер разлогинился
          }
          mForm.int_token = Convert.ToInt32(authResponse, 10);
          mForm.TextBox_username.Text = loginBox.Text;
          mForm.Show();
          this.Visible = false;
        }
      }
    }

    private void AuthentificationForm_Load(object sender, EventArgs e)
    {
      
    }

    private void AuthentificationForm_FormClosed(object sender, FormClosedEventArgs e)
    {      
      mForm.Show();
    }
  }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DotChatWF
{
  public partial class MainForm : Form
  {

    // Глобальные переменные     
    public List<x_y> x_y = new List<x_y>();
    AuthentificationForm AuthForm;
    RegistrationForm RegForm;
    public TextBox TextBox_username;
    public int int_token;

    public MainForm()
    {
      InitializeComponent();
    }

    private void updateLoop_Tick(object sender, EventArgs e)
    {      
      List<Message> allMSG = GetAllMessages();            
      if (allMSG.Count > listMessages.Items.Count)
      {
        int index = -1;
        listMessages.Items.Clear();
        foreach(Message m in allMSG)
        {
          ++index;
          listMessages.Items.Add($"{m.timestamp}  [{m.username}] {m.text}\tID: {index + 1}");
        } 
      }
      else if (allMSG.Count < listMessages.Items.Count)
      {
        listMessages.Items.Clear();
        int index = -1;
        foreach(Message m in allMSG)
        {
          ++index;
          listMessages.Items.Add($"{m.timestamp}  [{m.username}] {m.text}\tID: {index + 1}");
        }
      }            
    }

    private void btnSend_Click(object sender, EventArgs e)
    {
      if (int_token == 0)
      {
          MessageBox.Show("Please log in or register");
      }
      else
      {
        if (fieldMessage.Text == String.Empty)
        {
            MessageBox.Show("Empty message field");
        }
        else
        {
          if (fieldUsername.Text == "admin")
          {

            if (fieldMessage.Text.IndexOf("/delete ") != -1 && fieldMessage.Text[0] == '/') // Если сообщения вида /delete ID, то удаляем сообщение с индексом ID
            {
              string[] request = fieldMessage.Text.Split(' ');
              if (request.Length == 2)
              {
                var deleteRequest =
                    (HttpWebRequest) WebRequest.Create("http://localhost:5000/api/Chat/" +
                                                          $"{Convert.ToInt32(request[1])}"); // Реквест на api/Chat + ID
                deleteRequest.Method = "DELETE";
                deleteRequest.GetResponse();
                fieldMessage.Text = String.Empty;
              }
              else // Если в окошке есть что-то помимо команды и ID
              {
                MessageBox.Show("Bad request");
                fieldMessage.Text = String.Empty;
              }

            }
            else // Если это обычное сообщение
            {
              SendMessage(new Message()
              {
                username = fieldUsername.Text,
                text = fieldMessage.Text,
              });
              fieldMessage.Text = String.Empty;
            }
          }
          else
          {
            SendMessage(new Message()
            {
              username = fieldUsername.Text,
              text = fieldMessage.Text,
            });
            fieldMessage.Text = String.Empty;
          }

        }
      }
    }

    // Отправляет сообщение на сервер
    void SendMessage(Message msg)
    {
      WebRequest req = WebRequest.Create("http://localhost:5000/api/chat");
      req.Method = "POST";
      string postData = JsonConvert.SerializeObject(msg);
      //byte[] bytes = Encoding.UTF8.GetBytes(postData);
      req.ContentType = "application/json";
      //req.ContentLength = bytes.Length;
      StreamWriter reqStream = new StreamWriter(req.GetRequestStream());
      reqStream.Write(postData);
      reqStream.Close();
      req.GetResponse();
    }

    List<Message> GetAllMessages() // Спрашиваем список всех сообщений с сервера
    {
      var getRequest = (HttpWebRequest)WebRequest.Create("http://localhost:5000/api/chat");
      using (StreamReader readList = new StreamReader(getRequest.GetResponse().GetResponseStream()))
      {
        return JsonConvert.DeserializeObject<List<Message>>(readList.ReadToEnd());
      }
    }
    private void btnAuth_Click(object sender, EventArgs e)
    {
      AuthForm = new AuthentificationForm();
      AuthForm.mForm = this;      
      AuthForm.Show();
      this.Visible = false;
    }

     private void GetSize(object sender, EventArgs e, string filename = "config.json")
    {
        string json = "";
        using (StreamReader sr = new StreamReader("config.json", System.Text.Encoding.Default))
        {
            json = sr.ReadToEnd();
        }
        var deserializer = new JsonSerializer();
        this.x_y = (List<x_y>)deserializer.Deserialize(new StringReader(json), typeof(List<x_y>));

        this.Size = new Size(this.x_y[0].x, this.x_y[0].y);
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      int_token = 0;
     // this.Size = new Size(680, 450);
      GetSize(sender, e);
      AuthForm = new AuthentificationForm();
      RegForm = new RegistrationForm();
      TextBox_username = fieldUsername;

    }

    private void btnReg_Click(object sender, EventArgs e)
    {
      RegForm = new RegistrationForm();
      RegForm.mForm = this;     
      RegForm.Show();
      this.Visible = false;
    }

    private void listMessages_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void MainForm_VisibleChanged(object sender, EventArgs e)
    {
      if (this.Visible == true && int_token != 0)
      {
        this.updateLoop.Enabled = true;
      }
    }

    private void fieldMessage_TextChanged(object sender, EventArgs e)
    {

    }

    // При закрытии формы отправляем то, что юзер вышел
		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{      
      GettingOffline(TextBox_username.Text);
		}

    // Метод, отправляющий серверу инфу, что мы вышли или разлогинились
    public static void GettingOffline(string _login)
    {
      if (_login != "You are not logged in") 
      {
        var deleteRequest = WebRequest.Create("http://localhost:5000/api/Auth/" + $"{_login}");
        deleteRequest.Method = "DELETE";
        deleteRequest.GetResponse();
      }      
    }

		private void fieldUsername_TextChanged(object sender, EventArgs e)
		{

		}

    private void label2_Click(object sender, EventArgs e)
    {

    }
  }
  public class x_y
  {
    public int x { get; set; }
    public int y { get; set; }

    public x_y()
    {
      x = 0;
      y = 0;
    }

    public x_y(int x, int y)
    {
      this.y = x;
      this.x = y;
    }
  }

  [Serializable]
  public class Message
  {
    public string username = "";
    public string text = "";
    public string timestamp;
  } 
}

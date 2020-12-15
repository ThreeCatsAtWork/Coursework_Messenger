using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Server
{
  [Serializable]
  public class Message
  {
    public string Username { get; set; }
    public string Text { get; set; }
    public string Timestamp { get; set; }
    public Message()
    {
      Username = "Server";
      Text = "Server is running...";
      Timestamp = DateTime.Now.ToString("dd MMM H:mm");
    }

    public Message(string username, string text)
    {
      Username = username;
      Text = text;
      Timestamp = DateTime.Now.ToString("dd MMM H:mm");
    }
  }

  [Serializable]
  public class MessagesClass
  {
    public List<Message> messages = new List<Message>();

    public MessagesClass()
    {
      messages.Clear();
      Message ms = new Message();
      messages.Add(ms);
    }

    public void Add(Message ms)
    {
      ms.Timestamp = DateTime.Now.ToString("dd MMM H:mm");
      messages.Add(ms);
      Console.WriteLine(messages.Count);
    }

    public void Add(string username, string text)
    {
      Message msg = new Message(username, text);
      messages.Add(msg);
      Console.WriteLine(messages.Count);
    }

    public Message Get(int id)
    {
      return messages.ElementAt(id);
    }

    public int GetCountMessages()
    {
      return messages.Count;
    }

    // Сохраняем сообщения перед выходом
    public static void SaveHistoryToFile(string filename = "history.json")
		{      
      var messageinjson = new StringWriter();
      var serializer = new JsonSerializer();
      serializer.Serialize(messageinjson, Program.ms);
      using var jsoninfile = new StreamWriter(filename);
      jsoninfile.Write(messageinjson);
    }

    // Достаём сообщения из файла
    public static void GetHistoryFromFile(string filename = "history.json")
		{
      var serializer = new JsonSerializer();
      using var jsonfromfile = new StreamReader(filename);
      Program.ms = (MessagesClass)serializer.Deserialize(new StringReader(jsonfromfile.ReadToEnd()), typeof(MessagesClass));
    }
  }

  public class Tokens
  {
    public int Token { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    public Tokens()
    {
      Token = -1;
      Login = "none";
      Password = "none";
    }

    public Tokens(int token, string login, string password)
    {
      Token = token;
      Login = login;
      Password = password;
    }
  }

  [Serializable]
  public class SessionsClass
  {
    public List<Tokens> list_tokens = new List<Tokens>();
    
    public SessionsClass()
		{

		}

    public int GenToken()
    {
      Random rand = new Random();
      return rand.Next(10 * 1000 , 100 * 1000 );
    }

    public int Login(AuthData auth_data)
    {
      string login = auth_data.Login;
      string password = auth_data.Password;
      bool login_exist = false;
      int row_num = 0;
      foreach (Tokens item in list_tokens)
      {
        if (item.Login == login)
        {
          login_exist = true;
          if (item.Password == password)
          {
            int token = GenToken();
            Tokens record_token = new Tokens(token, login, password);
            list_tokens[row_num].Token = token;
            //list_tokens.Add(record_token);
            Console.WriteLine($"Аутификация успешно login: {login} password: {password} token: {token}");
            return token;
          }
          else
          {
            return -1;
          }
        }
        row_num++;
      }
      if (!login_exist)
      {        
        return -2;
      }
      return -200;   // ошибка логики
    }

    public int Registration(AuthData auth_data)
    {
      bool login_exist = false;
      foreach (Tokens item in list_tokens)
      {
        if (item.Login == auth_data.Login)
        {
          login_exist = true;
        }
      }
      if (!login_exist) 
      { 
        int token = GenToken();
        Tokens record_token = new Tokens(token, auth_data.Login, auth_data.Password);
        list_tokens.Add(record_token);
        Console.WriteLine($"Регистрация успешно login: {auth_data.Login} password: {auth_data.Password} token: {token}");
        return token;
      }
      return -1;
    }


    public void SaveToFile(string filename = "data_sessions.json")
    {  
      try
      {
        var serializer = new JsonSerializer();

        StringWriter writejson = new StringWriter();

        serializer.Serialize(writejson, list_tokens);

        using StreamWriter sw = new StreamWriter(filename);

        sw.Write(writejson);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }

    }

    public void LoadFromFile(string filename = "data_sessions.json")
    {
      long size = 0;
      if (File.Exists(filename)) 
      { 
        FileInfo file = new FileInfo(filename);
        size = file.Length;
      }
      if (size > 0)
      {
        try
        {
          //Console.WriteLine("Dannie vigruzheni");
          string json = "";
          using (StreamReader sr = new StreamReader(filename, System.Text.Encoding.Default))
          {
            json = sr.ReadToEnd();
          }

          var deserializer = new JsonSerializer();

          list_tokens = (List<Tokens>)deserializer.Deserialize(new StringReader(json), typeof(List<Tokens>));
          for (int i = 0; i < list_tokens.Count; i++)
          {
            list_tokens[i].Token = 0;
          }

          // После того, как записали юзеров из файла, заполняем словарь онлайна
          foreach(Tokens t in list_tokens)
					{
            Program.onlineUsers.Add(t.Login, 0);
					}
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
        }
        // Console.WriteLine($"Загружено записей: {this.list_tokens.Count}");
      }

    }
  }

  public class AuthData
  {
    public string Login { get; set; }
    public string Password { get; set; }
    public string Ip { get; set; }
  }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Server
{
    public class Program
    {
    public static MessagesClass ms;
    public static SessionsClass Sessions;
    public static Dictionary<string, int> onlineUsers = new Dictionary<string, int>(); // Словарь, в котором если у логина значение >0, то юзер с таким логином - онлайн
    public static string ip = "http://localhost:5000";

    public static void Main(string[] args)
        {
            ms = new MessagesClass();  //обработка сообщений
            Sessions = new SessionsClass(); // хранение токенов логинов и паролей            
            //Sessions.addValera();
            Sessions.LoadFromFile();
            MessagesClass.GetHistoryFromFile();
                                                       // Console.WriteLine(Sessions.lit_tokens.Count);           
            CreateHostBuilder(args).Build().Run();            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


    }
}

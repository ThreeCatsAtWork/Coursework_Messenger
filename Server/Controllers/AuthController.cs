using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {

    // POST api/<AuthController>
    [HttpPost]
    public int Post([FromBody] AuthData auth_data)
    {
      // Спрашиваем ip из посланной информации
      if (auth_data.Ip != String.Empty) 
			{
        Program.ip = "http://" + auth_data.Ip;
			}

      int int_token = Program.Sessions.Login(auth_data);

      // Если аутентификация удачна, то прибавляем значение в словаре онлайна
      if (int_token != -1 && int_token != -2)
			{
        Program.onlineUsers[auth_data.Login]++;

        Program.ms.Add(new Message("Server", $"{Program.onlineUsers[auth_data.Login]}"));

        if (Program.onlineUsers[auth_data.Login] == 1)
        {
          Program.ms.Add(new Message("Server", $"{auth_data.Login} is now online"));
        }
      }

      Console.WriteLine(int_token);
      return int_token;
    }    


    // DELETE api/<AuthController>
    [HttpDelete("{_login}")]
    public void GettingOffline(string _login) // Вычитаем единичку из словаря
		{
      if (Program.onlineUsers[_login] > 0) // На всякий случай обрабатываем колличество залогиненных с этого аккаунта одновременно юзеров
      {
        Program.onlineUsers[_login]--;

        // Если это был последний залогиненный юзер
        if (Program.onlineUsers[_login] == 0)
				{
          Program.ms.Add(new Message("Server", $"{_login} is now offline"));
				}
      }
		}    
  }
}

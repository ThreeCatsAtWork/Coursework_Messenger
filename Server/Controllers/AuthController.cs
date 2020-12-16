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
      if (auth_data.ip != String.Empty) 
			{
        Program.ip = "http://" + auth_data.ip;
			}

      int int_token = Program.Sessions.login(auth_data);

      // Если аутентификация удачна, то прибавляем значение в словаре онлайна
      if (int_token != -1 && int_token != -2)
			{
        Program.onlineUsers[auth_data.Login]++;

        Program.ms.Add(new message("Server", $"{Program.onlineUsers[auth_data.login]}"));

        if (Program.onlineUsers[auth_data.Login] == 1)
        {
          Program.ms.Add(new message("Server", $"{auth_data.login} is now online"));
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
          Program.ms.Add(new message("Server", $"{_login} is now offline"));
				}
      }
		}    
  }
}

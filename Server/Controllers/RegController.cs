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
  public class RegController : ControllerBase
  {
    // POST api/<RegController>
    [HttpPost]
    public int Post([FromBody] AuthData auth_data)
    {
      int int_token = Program.Sessions.registration(auth_data);

      if (int_token != -1)
			{
        Console.WriteLine(int_token);
        Program.onlineUsers.Add(auth_data.login, 1);
        Program.ms.Add(new message("Server", $"{auth_data.login} is just registered. Welcome!!!"));       
      }      
      return int_token;
    }
  }
}

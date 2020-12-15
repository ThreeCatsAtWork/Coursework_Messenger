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
      int int_token = Program.Sessions.Registration(auth_data);

      if (int_token != -1)
			{
        Console.WriteLine(int_token);
        Program.onlineUsers.Add(auth_data.Login, 1);
        Program.ms.Add(new Message("Server", $"{auth_data.Login} is just registered. Welcome!!!"));       
      }      
      return int_token;
    }
  }
}

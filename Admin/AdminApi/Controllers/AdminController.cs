using Library;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Server.Contracts.Models;

namespace AdminApi.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IRequestClient<UserRequest> _client;

        public AdminController(IBus bus, IRequestClient<UserRequest> client){
            _bus = bus;
            _client = client;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> Test1(){
            var user = new UserForm(){
                Username = "shanto",
                Role = "student",
                Password = "123",
                ConfirmPassword = "123"
            };
            var url = new Uri("rabbitmq://localhost//register-user");

            var endpoint = await _bus.GetSendEndpoint(url);
            await endpoint.Send(user);

            return Ok("user message sent");
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers([FromQuery] string role)
        {
            var requestData = new UserRequest()
            {
                Role = role
            };

            var request = _client.Create(requestData);
            var response = await request.GetResponse<UserListResponse>();

            return Ok(response);
        }

    }
}

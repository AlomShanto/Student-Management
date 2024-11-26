// using AdminApi;

using Library;
using MassTransit;
using Server.BusinessLogic.ILogics;
using Server.Contracts.DataTransferObjects;

namespace Server
{
    public class RequestResponse : IConsumer<UserRequest>
    {
        private readonly IAccountLogic _accountLogic;

        public RequestResponse(IAccountLogic accountLogic)
        {
            _accountLogic = accountLogic;
        }
        public async Task Consume(ConsumeContext<UserRequest> context)
        {
            var users = await _accountLogic.GetUsers(context.Message.Role);

            var usersToReturn = new UserListResponse
            {
                Users = users.Select(x => new User(x.Username, x.Role)).ToList()
            };

            await context.RespondAsync(usersToReturn);
        }
    }

    /*public class RequestResponse : IConsumer<UserRequest>
    {
        public async Task Consume(ConsumeContext<UserRequest> context)
        {
            var user = new User("Shanto", "Student");
            await context.RespondAsync<User>(user);
        }
    }*/
}

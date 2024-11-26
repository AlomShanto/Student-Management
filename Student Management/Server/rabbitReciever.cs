using MassTransit;
using Server.Contracts.Models;

namespace Server;
public class rabbitReciever : IConsumer<UserForm>
{
    public async Task Consume(ConsumeContext<UserForm> context)
    {
        var product = context.Message;
    }
}
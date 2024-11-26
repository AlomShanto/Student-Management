using Server.Application;
using Server.BusinessLogic.ILogics;
using Server.BusinessLogic.Logics;
using Server.Contracts.DBSettings;
using Server.DatabaseAccess.DataAccess;
using Server.DatabaseAccess.IDataAccess;
using MassTransit;
using Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x => {
   

    x.UsingRabbitMq((context, config) => {
        config.Host(new Uri("rabbitmq://localhost"), h => {
            h.Username("admin");
            h.Password("admin123");
        });

        config.ReceiveEndpoint("get-users", e =>
        {
            e.Consumer<RequestResponse>(context);
        });

        config.ReceiveEndpoint("register-user", e =>
        {
            e.Consumer<rabbitReciever>(context);
        });
    });

    x.AddConsumer<rabbitReciever>();
    x.AddConsumer<RequestResponse>();

});
builder.Services.AddMassTransitHostedService();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddSingleton<IStudentDataAccess, StudentDataAccess>();
builder.Services.AddSingleton<ITeacherDataAccess, TeacherDataAccess>();
builder.Services.AddSingleton<IAccountDataAccess, AccountDataAccess>();
builder.Services.AddSingleton<IAccountLogic, AccountLogic>();
builder.Services.AddSingleton<IStudentsLogic, StudentsLogic>();
builder.Services.AddSingleton<ITeachersLogic, TeachersLogic>();
builder.Services.AddSingleton<ISharedDataAccess, SharedDataAccess>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

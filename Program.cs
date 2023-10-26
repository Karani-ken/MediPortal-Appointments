using MediPortal_Appointment.Data;
using MediPortal_Appointment.Extensions;
using MediPortal_Appointment.Services;
using MediPortal_Appointment.Services.IService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

builder.Services.AddScoped<IAppointmentInterface, AppointmentService>();
builder.Services.AddScoped<IHospitalInterface, HospitalService>();
builder.Services.AddScoped<IDoctorInterface, DoctorService>();

builder.Services.AddHttpClient("Hospital", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrl:HospitalApi"]));
builder.Services.AddHttpClient("Doctor", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrl:AuthApi"]));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(options => options.AddPolicy("policy", build =>
{
    build.AllowAnyOrigin();
    build.AllowAnyMethod();
    build.AllowAnyHeader();

}));
builder.AddSwaggenGenExtension();
builder.AddAppAuthentication();
var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    if (!app.Environment.IsDevelopment())
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AUTH API");
        c.RoutePrefix = string.Empty;
    }
});
app.UseMigration();
app.UseHttpsRedirection();
app.UseCors("policy");
app.UseAuthorization();

app.MapControllers();

app.Run();

using EXE201.BLL.DTOs;
using EXE201.BLL.Interfaces;
using EXE201.BLL.Services;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using EXE201.DAL.Repository;
using MCC.DAL.Repository.Implements;
using MCC.DAL.Repository.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using EXE201.DAL.DTOs.EmailDTOs;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.Data.Edm;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(
    x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IDepositRepository, DepositRepository>();
builder.Services.AddScoped<IVerifyCodeRepository, VerifyCodeRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IRentalOrderRepository, RentalOrderRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IRentalOrderDetailRepository, RentalOrderDetailRepository>();
builder.Services.AddScoped<ICartRepository, CartRepostiory>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IFeedbacksRepository, FeedbackRepository>();
builder.Services.AddScoped<IConversationRepository, ConversationRepository >();
builder.Services.AddScoped<IProductDetailRepository, ProductDetailRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<ISizeRepository, SizeRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();



// Add Services
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IRoleServices, RoleServices>();
builder.Services.AddScoped<IInventoryServices, InventoryServices>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPaymentServices, PaymentServices>();
builder.Services.AddScoped<IDepositServices, DepositServices>();
builder.Services.AddScoped<IForgotPawwordService, ForgotPasswordService>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IRentalOrderServices, RentalOrderServices>();
builder.Services.AddScoped<INotificationServices, NotificationServices>();
builder.Services.AddScoped<IRentalOrderDetailServices, RentalOrderDetailServices>();
builder.Services.AddScoped<ICartServices, CartServices>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IRatingServices, RatingServices>();
builder.Services.AddScoped<IFeedbackServices, FeedbackServices>();
builder.Services.AddScoped<IConversationService, ConversationService>();
builder.Services.AddScoped<IProductDetailServices, ProductDetailServices>();
builder.Services.AddScoped<IColorServices, ColorServices>();
builder.Services.AddScoped<ISizeServices, SizeServices>();
builder.Services.AddScoped<IPaymentServices, PaymentServices>();
builder.Services.AddScoped<IMessageService, MessageService>();
//builder.Services.AddScoped<IPayOSPaymentService, PayOSPaymentService>();
builder.Services.AddScoped<IDashboardServices, DashboardServices>();




// Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "")),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
    };
});

// Add Authorization
builder.Services.AddAuthorization();

//Add EmailSetting
builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSetting"));

//Add services to the container.
builder.Services.AddDbContext<EXE201Context>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Register Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Voguary API", Version = "v1" });

    // Add enum descriptions
    c.MapType<OrderStatus>(() => new OpenApiSchema
    {
        Type = "integer",
        Enum = new List<IOpenApiAny>
        {
            new OpenApiInteger((int)OrderStatus.ChoXacNhan),
            new OpenApiInteger((int)OrderStatus.ChoGiaoHang),
            new OpenApiInteger((int)OrderStatus.DangVanChuyen),
            new OpenApiInteger((int)OrderStatus.DaHoanThanh),
            new OpenApiInteger((int)OrderStatus.DaHuy)
        }
    });

    // Add JWT Authentication
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    };

    c.AddSecurityRequirement(securityRequirement);
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "8081";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

//Get swagger.json following root directory 
app.UseSwagger(options => { options.RouteTemplate = "{documentName}/swagger.json"; });
//Load swagger.json following root directory 
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/v1/swagger.json", "Voguary API V1"); c.RoutePrefix = string.Empty; });

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//};

app.UseCors(x => x.AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod());

// app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

IEdmModel GetEdmModel()
{
    var odataBuilder = new ODataConventionModelBuilder();
    odataBuilder.EntitySet<Payment>("Payments");
    return (IEdmModel)odataBuilder.GetEdmModel();
}

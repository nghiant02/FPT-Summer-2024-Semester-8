using FE;
using Refit;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddRazorPages();

# region AddMore

builder.Services.AddHttpContextAccessor();


builder.Services.AddRefitClient<IApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5167"))
    .AddHttpMessageHandler<AuthHeaderHandler>()
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
        };
    });


builder.Services.AddTransient<AuthHeaderHandler>();
builder.Services.AddSession();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

using KDSB.AppWebBlazor.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<ProductKDSBService>();

builder.Services.AddHttpClient("KDSB20240906API", c =>
{
    // Configura la direcci�n base del cliente HTTP desde la configuraci�n
    c.BaseAddress = new Uri(builder.Configuration["UrlsAPI:CRM"]);
    // Puedes configurar otras opciones del HttpClient aqu� seg�n sea necesario
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

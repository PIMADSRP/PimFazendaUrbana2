using PIMFazendaUrbanaRadzen.Services;
using PIMFazendaUrbanaRadzen.Components;
using Radzen;
using PIMFazendaUrbanaAPI.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Adicionando serviços ao container
builder.Services.AddRazorComponents()
      .AddInteractiveServerComponents()
      .AddHubOptions(options => options.MaximumReceiveMessageSize = 10 * 1024 * 1024); // Aumenta o tamanho máximo da mensagem

builder.Services.AddControllers(); // Adiciona suporte a Controllers
builder.Services.AddRadzenComponents(); // Adiciona serviços Radzen

// Serviço de tema usando cookies
builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "MyApplicationTheme";
    options.Duration = TimeSpan.FromDays(365); // Tempo de duração do cookie de tema
});

builder.Services.AddHttpClient(); // Registra o HttpClient para chamadas HTTP

var apiBaseUrl = Environment.GetEnvironmentVariable("API_BASE_URL") ?? "https://localhost:7079/api";

builder.Services.AddScoped<DialogService>(); // Para DialogService

/*
builder.Services.AddScoped<ApiService<ClienteDTO>>(provider =>
    new ApiService<ClienteDTO>(provider.GetRequiredService<HttpClient>(),
    builder.Configuration["API_BASE_URL"]));
*/

builder.Services.AddScoped(provider =>
    new ApiService<ClienteDTO>(
        provider.GetRequiredService<HttpClient>(),
        $"{apiBaseUrl}/Cliente"
    ));


builder.Services.AddScoped(provider =>
    new ApiService<EnderecoDTO>(
        provider.GetRequiredService<HttpClient>(),
        $"{apiBaseUrl}/enderecos"
    ));

builder.Services.AddScoped(provider =>
    new ApiService<TelefoneDTO>(
        provider.GetRequiredService<HttpClient>(),
        $"{apiBaseUrl}/telefones"
    ));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS
app.MapControllers(); // Mapeia as controllers
app.UseStaticFiles(); // Habilita o uso de arquivos estáticos
app.UseAntiforgery(); // Protege contra CSRF (Cross-Site Request Forgery)

// Mapeia os componentes interativos do Blazor Server
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode(); // Renderização interativa do Blazor Server

app.Run();
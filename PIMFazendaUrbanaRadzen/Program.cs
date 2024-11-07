using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PIMFazendaUrbanaRadzen.Services;
using PIMFazendaUrbanaRadzen.Components;
using Radzen;
using PIMFazendaUrbanaAPI.DTOs;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PIMFazendaUrbanaRadzen.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Registra o HttpClient para os controladores
builder.Services.AddHttpClient();

// Registra os controladores (incluindo CepController)
builder.Services.AddControllers();

// Adiciona outros servi�os necess�rios
builder.Services.AddRadzenComponents(); // Registra os componentes Radzen
builder.Services.AddBlazoredLocalStorage(); // Para armazenamento local (LocalStorage)
builder.Services.AddScoped<DialogService>(); // Servi�o de di�logos do Radzen

// Servi�o de tema utilizando cookies
builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "MyApplicationTheme";
    options.Duration = TimeSpan.FromDays(365); // Tempo de dura��o do cookie de tema
});

// Registra autentica��o e servi�os relacionados
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());

// Configura��o da API (URL base da API)
var apiBaseUrl = Environment.GetEnvironmentVariable("API_BASE_URL") ?? "https://localhost:7079/api";

// Registra o servi�o AuthService
builder.Services.AddScoped<AuthService>(provider =>
    new AuthService(
        provider.GetRequiredService<HttpClient>(),
        $"{apiBaseUrl}/auth/login",
        provider.GetRequiredService<ILocalStorageService>(),
        provider.GetRequiredService<CustomAuthenticationStateProvider>()
    ));

// Registra os servi�os da API para Cliente, Endereco e Telefone
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

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddHubOptions(options => options.MaximumReceiveMessageSize = 10 * 1024 * 1024); // Aumenta o tamanho m�ximo da mensagem

var app = builder.Build();

// Configura��o do pipeline de middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Exce��es em produ��o
    app.UseHsts(); // HSTS (HTTP Strict Transport Security)
}

app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS
app.UseStaticFiles(); // Serve arquivos est�ticos

// Configure o middleware AntiForgery sem argumentos
app.UseAntiforgery();  // Simples, sem par�metros

app.UseAuthentication(); // Adiciona autentica��o (se usar)
app.UseAuthorization(); // Adiciona autoriza��o (se usar)

app.MapControllers(); // Mapeia os controladores da API

// Mapeia os componentes interativos do Blazor Server
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); // Renderiza��o interativa do Blazor Server

// Inicia o servidor
app.Run();

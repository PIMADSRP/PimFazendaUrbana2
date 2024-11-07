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

// Adiciona outros serviços necessários
builder.Services.AddRadzenComponents(); // Registra os componentes Radzen
builder.Services.AddBlazoredLocalStorage(); // Para armazenamento local (LocalStorage)
builder.Services.AddScoped<DialogService>(); // Serviço de diálogos do Radzen

// Serviço de tema utilizando cookies
builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "MyApplicationTheme";
    options.Duration = TimeSpan.FromDays(365); // Tempo de duração do cookie de tema
});

// Registra autenticação e serviços relacionados
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());

// Configuração da API (URL base da API)
var apiBaseUrl = Environment.GetEnvironmentVariable("API_BASE_URL") ?? "https://localhost:7079/api";

// Registra o serviço AuthService
builder.Services.AddScoped<AuthService>(provider =>
    new AuthService(
        provider.GetRequiredService<HttpClient>(),
        $"{apiBaseUrl}/auth/login",
        provider.GetRequiredService<ILocalStorageService>(),
        provider.GetRequiredService<CustomAuthenticationStateProvider>()
    ));

// Registra os serviços da API para Cliente, Endereco e Telefone
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
    .AddHubOptions(options => options.MaximumReceiveMessageSize = 10 * 1024 * 1024); // Aumenta o tamanho máximo da mensagem

var app = builder.Build();

// Configuração do pipeline de middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Exceções em produção
    app.UseHsts(); // HSTS (HTTP Strict Transport Security)
}

app.UseHttpsRedirection(); // Redireciona HTTP para HTTPS
app.UseStaticFiles(); // Serve arquivos estáticos

// Configure o middleware AntiForgery sem argumentos
app.UseAntiforgery();  // Simples, sem parâmetros

app.UseAuthentication(); // Adiciona autenticação (se usar)
app.UseAuthorization(); // Adiciona autorização (se usar)

app.MapControllers(); // Mapeia os controladores da API

// Mapeia os componentes interativos do Blazor Server
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); // Renderização interativa do Blazor Server

// Inicia o servidor
app.Run();

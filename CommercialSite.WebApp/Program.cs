using Blazored.LocalStorage;
using CommercialSite.WebApp.App;
using static CommercialSite.WebApp.Extensions;
using static Constants.AccessPlanitAPIConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<StateManagementService>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<TokenHandler>();
builder.Services.AddScoped<InvoiceService>();
builder.Services.AddScoped<CourseDateService>();
builder.Services.AddScoped<ELearningPackageService>();

builder.Services.AddHttpClient<AccessPlanitAPIClientBase>
    (config => config.BaseAddress = new Uri(BaseAddress))
    .AddHttpMessageHandler<TokenHandler>();

builder.Services.AddHttpClient<AccessPlanitAPIClient>
    (config => config.BaseAddress = new Uri(BaseAddress));

builder.Services.AddHttpClient<AccessPlanitUserCourseDateClient>
    (config => config.BaseAddress = new Uri(BaseAddress));

builder.Services.AddHttpClient<AccessPlanitAPIClientBase>
    (config => config.BaseAddress = new Uri(BaseAddress))
    .AddHttpMessageHandler<TokenHandler>();

builder.Services.AddHttpClient<AccessPlanitAPIBasketClient> 
    (config => config.BaseAddress = new Uri(BaseBasketAddress));

builder.Services.AddHttpClient<AccessPlanitUsersElearningPackageClient>
    (config => config.BaseAddress = new Uri(BaseAddress));

builder.Services.AddHttpClient<AccessPlanitUserCourseDateWebClient>
    (config => config.BaseAddress = new Uri(BaseAddress));

builder.RegisterAccessPlanitAPIModuleClients();

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using WebApiTemplate.Core;
using WebApiTemplate.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<Test>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//web view 
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueCountLimit = int.MaxValue;
    x.ValueLengthLimit = int.MaxValue;
    x.MemoryBufferThreshold = int.MaxValue;
    x.MultipartBodyLengthLimit = long.MaxValue;
    x.MultipartBoundaryLengthLimit = int.MaxValue;
    x.MultipartHeadersLengthLimit = int.MaxValue;
    x.MultipartHeadersCountLimit = int.MaxValue;
});

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    //options.LowercaseQueryStrings = true
});
var app = builder.Build();

// Configure the HTTP request pipeline.

if (bool.Parse(AppSettingsExtensions.GetValueByKey("ShowSwaggerUi")))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//https://aspnetcore.readthedocs.io/en/stable/security/cors.html
app.UseCors(a => a.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetPreflightMaxAge(TimeSpan.FromDays(14)));

var staticFolderPublic = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot");
if (!Directory.Exists(staticFolderPublic)) Directory.CreateDirectory(staticFolderPublic);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(staticFolderPublic),
    ServeUnknownFileTypes = true,
    OnPrepareResponse = ctx =>
    {
        // Cache static files for 30 days
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=2592000");
    }
     ,
    ContentTypeProvider = new FileExtensionAllContentTypeProvider()
});

app.Use(async (context, next) =>
{
    //logger
    Console.WriteLine(context.Request.Scheme);
    Console.WriteLine(context.Request.Method);
    Console.WriteLine(context.Request.Path);
    Console.WriteLine(context.Request.QueryString);
    // Call the next delegate/middleware in the pipeline.
    await next(context);
});

app.Run();

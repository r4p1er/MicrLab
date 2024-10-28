using MicrLab.Abstractions;
using MicrLab.Models;
using MicrLab.Services;

namespace MicrLab;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddRazorPages();
        builder.Services.AddSingleton<TcpClientSettings>(provider => new TcpClientSettings
        {
            TargetHost = builder.Configuration["TcpClient:TargetHost"]!,
            TargetPort = Convert.ToInt32(builder.Configuration["TcpClient:TargetPort"]!)
        });
        builder.Services.AddScoped<ITcpClient, TcpClient>();
        
        var app = builder.Build();
        
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }
        
        app.UseStaticFiles();
        app.UseRouting();
        app.MapRazorPages();

        app.Run();
    }
}
using BlazorRealTime.Components;
using Microsoft.AspNetCore.ResponseCompression;

namespace SignalR.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                      new[] { "application/octet-stream" });
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.UseResponseCompression();
            app.MapHub<ChatHub>("/chathub");
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();


            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapBlazorHub();
            //    endpoints.MapHub<ChatHub>("/ImageHub");
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapHub<ChatHub>("/imageHub");
            //    endpoints.MapBlazorHub();
            //    endpoints.MapFallbackToPage("/_Host");
            //});

            app.Run();
        }
    }
}

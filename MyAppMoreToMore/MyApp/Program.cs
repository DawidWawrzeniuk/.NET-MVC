using Microsoft.EntityFrameworkCore;
using MyAppMoreToMore.Data;

namespace MyAppMoreToMore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();             //dodanie us³ugi kontrolera i views (obs³uga zdarzeñ i wyœwietlanie na stronie)
            builder.Services.AddDbContext<MyAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
            var app = builder.Build();          //stworzenie instancji buildera któr¹ mo¿emy konfigurowaæ

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())           //konfiguracja HTTP pipeline która okreœla jak bêd¹ obs³ugiwane zaptania
            {
                app.UseExceptionHandler("/Home/Error");     //je¿eli aplikacja nie jest w trybie development to przekierowuje do strony error
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();                              //odblokowanie HTTP strict transport security aby wymusiæ po³¹czenie chronione https
            }

            app.UseHttpsRedirection();  //obs³uga zapytañ
            app.UseStaticFiles();       //umo¿liwia obs³ugê plików zdjêæ takich jak zdjecia CSS

            app.UseRouting();           //umo¿liwia aplikacji okreœlanie odpwiednich endpointów na zapytania

            app.UseAuthorization();     //dodaje autoryzacje u¿ytkowników

            app.MapControllerRoute      //konfiguracja standardowego wzoru po³aczenia dla aplikacji MVC  (jak bedziemy wpisywac zapytanie i do czego one bedzie prowadziæ)
            (                           //bedzie to standardowa sciezka do ktorej zostaniemy przekierowani podczas uruchomienia aplikacji 
                name: "default",
                pattern: "{controller=Items}/{action=Index}/{id?}"
            );

            app.Run();                  //uruchomienie aplikacji i nas³uchiwanie na ¿¹dania
        }
    }
}

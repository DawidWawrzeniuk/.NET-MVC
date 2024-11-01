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
            builder.Services.AddControllersWithViews();             //dodanie us�ugi kontrolera i views (obs�uga zdarze� i wy�wietlanie na stronie)
            builder.Services.AddDbContext<MyAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
            var app = builder.Build();          //stworzenie instancji buildera kt�r� mo�emy konfigurowa�

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())           //konfiguracja HTTP pipeline kt�ra okre�la jak b�d� obs�ugiwane zaptania
            {
                app.UseExceptionHandler("/Home/Error");     //je�eli aplikacja nie jest w trybie development to przekierowuje do strony error
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();                              //odblokowanie HTTP strict transport security aby wymusi� po��czenie chronione https
            }

            app.UseHttpsRedirection();  //obs�uga zapyta�
            app.UseStaticFiles();       //umo�liwia obs�ug� plik�w zdj�� takich jak zdjecia CSS

            app.UseRouting();           //umo�liwia aplikacji okre�lanie odpwiednich endpoint�w na zapytania

            app.UseAuthorization();     //dodaje autoryzacje u�ytkownik�w

            app.MapControllerRoute      //konfiguracja standardowego wzoru po�aczenia dla aplikacji MVC  (jak bedziemy wpisywac zapytanie i do czego one bedzie prowadzi�)
            (                           //bedzie to standardowa sciezka do ktorej zostaniemy przekierowani podczas uruchomienia aplikacji 
                name: "default",
                pattern: "{controller=Items}/{action=Index}/{id?}"
            );

            app.Run();                  //uruchomienie aplikacji i nas�uchiwanie na ��dania
        }
    }
}

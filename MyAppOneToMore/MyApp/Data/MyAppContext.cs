using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MyAppOneToMany.Models;

namespace MyAppOneToMany.Data
{
	public class MyAppContext : DbContext
	{
		public MyAppContext(DbContextOptions<MyAppContext> options) : base(options) { }     //Konstruktor przyjmuje DbContextOptions<MyAppContext> options jako argument, co umożliwia konfigurację kontekstu bazy danych, np. określenie typu bazy danych (SQL Server, SQLite, itp.) i parametrów połączenia. base(options) wywołuje konstruktor klasy bazowej DbContext, co pozwala na przekazanie opcji do kontekstu

        protected override void OnModelCreating(ModelBuilder modelBuilder)					//OnModelCreating jest metodą, którą można nadpisać w klasie dziedziczącej po DbContext. Jest wywoływana podczas konfigurowania modelu danych przed tym, jak kontekst zostanie zainicjalizowany. Można w niej ustawić różne aspekty modelu, takie jak relacje między encjami, klucze główne, klucze obce, a także wprowadzać dane początkowe do bazy danych.
        {                                                                                   //Deklaracja metody, która jest chroniona (protected) i nadpisuje (override) metodę bazową OnModelCreating. Przyjmuje jako argument obiekt ModelBuilder, który umożliwia konfigurację modelu danych.
            modelBuilder.Entity<Item>().HasData(                                            //Umożliwia dostęp do konfiguracji encji Item. Dzięki temu można ustawić właściwości tej encji, relacje, ograniczenia itp.
                new Item { Id=4, Name="microphone", Price=40, SerialNumberId=10 }           //Metoda ta służy do dodawania danych początkowych do tabeli związanej z encją Item. W tym przypadku dodawany jest jeden obiekt Item z następującymi właściwościami
                );
            modelBuilder.Entity<SerialNumber>().HasData(
                new SerialNumber { Id = 10, Name = "MIC150", ItemId=4 }
                );
            modelBuilder.Entity<Item>().HasData(                                            
                new Item { Id = 5, Name = "pilot", Price = 160, SerialNumberId = 12 }           
                );
            modelBuilder.Entity<SerialNumber>().HasData(
                new SerialNumber { Id = 12, Name = "PILO69", ItemId = 5 }
                );

			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "Electronics"},
				new Category { Id = 2, Name = "Books"}
				);




			base.OnModelCreating(modelBuilder);                                             //wywołanie base.OnModelCreating(modelBuilder); ma na celu zapewnienie, że klasa bazowa (DbContext) wykona swoją własną logikę konfiguracji modelu danych, zanim zostanie dodana specyficzna logika w Twojej klasie kontekstu
        }








        public DbSet<Item> Items { get; set; }  //DbSet<Item> reprezentuje tabelę w bazie danych, która będzie przechowywać obiekty typu Item. Każdy DbSet<T> w klasie DbContext reprezentuje zbiór (czyli tabelę lub widok) w bazie danych, na którym można wykonywać operacje CRUD (Create, Read, Update, Delete).
												//Items to właściwość, która umożliwia dostęp do tabeli Items i wykonywanie zapytań LINQ lub operacji bezpośrednio na danych w tej tabeli.

		public DbSet<SerialNumber> SerialNumbers { get; set; }


        public DbSet<Category> Categories { get; set; }
	}
}

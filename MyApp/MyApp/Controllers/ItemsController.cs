using Microsoft.AspNetCore.Mvc;     //Umożliwia korzystanie z klas i interfejsów związanych z ASP.NET Core MVC.
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;                 // Pozwala na korzystanie z modeli zdefiniowanych w przestrzeni nazw MyApp.Models 

namespace MyApp.Controllers //Definiuje przestrzeń nazw dla kontrolerów w aplikacji MyApp
{
	public class ItemsController : Controller       //Definiuje klasę ItemsController, która dziedziczy po klasie Controller. Oznacza to, że ItemsController ma dostęp do funkcji i metod kontrolera w ASP.NET Core MVC
	{
		//      public IActionResult Overview()                 //Definiuje metodę akcji o nazwie Overview. Metody akcji odpowiadają na żądania HTTP i zwracają rezultaty (w tym przypadku IActionResult, co oznacza, że może zwrócić różne typy odpowiedzi) 
		//{
		//          var item = new Item()                       //Wewnątrz metody tworzony jest nowy obiekt Item z właściwością Name ustawioną na "keyboard"
		//	{
		//              Name = "keyboard"                       //inicjalizacja wartośći obiektu
		//          };
		//          return View(item);      //Zwraca widok (szablon), przekazując do niego obiekt item. Widok może wykorzystać te dane do renderowania strony
		//}
		//      public ContentResult Overview()
		//      {
		//          return Content("Hello there");      //ContentResult to konkretna implementacja IActionResult, która zwraca odpowiedź HTTP z treścią w postaci tekstu. Możesz zdefiniować typ treści (np. text/plain, text/html) oraz zawartość tekstową, która ma być zwrócona
		//}                                       //Użycie: Jest używane, gdy chcesz zwrócić prosty tekst jako odpowiedź, np. w przypadku API, które nie potrzebuje pełnych widoków


		//public IActionResult Edit(int id)
		//{
		//	return Content($"id= {id}");	
		//}

		//public IActionResult Edit(int Itemid)
		//{
		//	return Content($"id= {Itemid}");
		//}

		private readonly MyAppContext _context;         //_context to prywatne pole w kontrolerze ItemsController. Przechowuje ono instancję MyAppContext, który jest kontekstem bazy danych. Kontekst to "most" między aplikacją a bazą danych, umożliwiający wykonywanie operacji takich jak dodawanie, pobieranie czy usuwanie danych.
		public ItemsController(MyAppContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var item = await _context.Items.Include(s=>s.SerialNumber).ToListAsync();//await _context.Items.ToListAsync(); – tutaj ToListAsync() pobiera wszystkie rekordy z tabeli Items w bazie danych w sposób asynchroniczny, czyli nie blokując wątku (nie czekając bezczynnie na wyniki),		Metoda Include służy do załadowania powiązanych danych. W tym przypadku, gdy pobierasz przedmioty, jednocześnie ładujesz powiązane obiekty SerialNumber. Umożliwia to dostęp do informacji o numerze seryjnym dla każdego przedmiotu bez dodatkowego zapytania do bazy danych.
                                                                                     //ToListAsync(): Asynchroniczna metoda, która pobiera wszystkie elementy z Items i zwraca je jako listę. W tym przypadku wszystkie przedmioty oraz ich powiązane numery seryjne.
            return View(item);														 // Ta linia kodu renderuje widok Index.cshtml (domyślnie przypisaną stronę), przekazując do niego pobrane item jako model danych. Dzięki temu widok ma dostęp do danych z tabeli Items i może je wyświetlić użytkownikowi.
		}																			 //s to instancje kolekcji Items (czyli rekordy w tabeli)


		//--------------------------------------------------------TWORZENIE-----------------------------------------------------------------------
		public IActionResult Create()
		{
			return View();
		}



		[HttpPost]                                                                      //[HttpPost]: To atrybut, który wskazuje, że ta akcja kontrolera ma być wywoływana w odpowiedzi na żądanie HTTP POST. Oznacza to, że ta metoda będzie wywoływana, gdy formularz zostanie przesłany z widoku, który używa metody POST.
		public async Task<IActionResult> Create([Bind("Id, Name, Price")] Item item)    //async: Oznacza, że metoda jest asynchroniczna, co pozwala na nieblokujące operacje, takie jak zapisywanie danych do bazy.
		{                                                                               //Task<IActionResult>: Metoda zwraca obiekt typu IActionResult, co oznacza, że może zwrócić różne typy odpowiedzi, takie jak widok, przekierowanie itp.
																						//[Bind(...)]: Atrybut ten informuje framework, że tylko te właściwości, które są wymienione w atrybucie, powinny być mapowane z danymi przesłanymi z formularza. W tym przypadku Id, Name i Price
																						//Item item: To parametr metody, który jest instancją klasy Item. Framework automatycznie tworzy obiekt item i ustawia jego właściwości na podstawie danych przesłanych z formularza.

			if (ModelState.IsValid)                                                     //ModelState.IsValid: To sprawdzenie, które weryfikuje, czy dane przesłane przez użytkownika są poprawne(czy spełniają zasady walidacji, jeśli takie zostały zdefiniowane). Jeśli dane są poprawne, kod wewnątrz bloku if jest wykonywany.					
			{
				_context.Items.Add(item);
				await _context.SaveChangesAsync();                                      //await to słowo kluczowe używane w metodach oznaczonych jako async. Informuje kompilator, że metoda będzie wykonywana asynchronicznie, co oznacza, że nie zablokuje wątku wykonawczego podczas oczekiwania na zakończenie operacji asynchronicznej.
				return RedirectToAction("Index");
			}
			return View(item);                                                          //Jeśli model nie jest ważny (czyli walidacja nie przeszła), metoda zwraca ten sam widok, przekazując obiekt item z powrotem. Dzięki temu użytkownik może zobaczyć formularz wypełniony danymi, które wprowadził, a także ewentualne komunikaty o błędach walidacji.
		}
		//--------------------------------------------------------EDYCJA--------------------------------------------------------------------------
		public async Task<IActionResult> Edit(int id)
		{
			var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
			return View(item);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Price")] Item item)
		{
			if (ModelState.IsValid)
			{
				_context.Update(item);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(item);
		}
        //--------------------------------------------------------DELETE--------------------------------------------------------------------------

        public async Task<IActionResult> Delete(int id)									/*ta metoda zaczyna dzialac gdy nacisniemy pierwszy raz przycisk delete*/
		{
			var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
			return View(item);
		}
		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)						/*ta metoda zaczyna działać gdy naciśniemy drugi raz na przycisk delete (znajduje sie w formularzu wiec jest to post*/
		{
			var item = await _context.Items.FindAsync(id);
			if(item!=null)
			{
				_context.Items.Remove(item);
				await _context.SaveChangesAsync();
			}
			return RedirectToAction("Index");
		}
		




	}		
}










//action parameters to droga do uzyskania danych wejściowych które mogą pochodzić z różnych źródeł (URL segments, Query strings, Form submissions)
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyAppMoreToMore.Models
{
    public class SerialNumber
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? ItemId { get; set; }   /*Właściwość ItemId jest typu int?, co oznacza, że może być wartością całkowitą lub null. Używa się jej do przechowywania identyfikatora przedmiotu, z którym dany numer seryjny jest związany.Zastosowanie? oznacza, że ta właściwość jest opcjonalna.*/
        [ForeignKey("ItemId")]             /*Adnotacja[ForeignKey("ItemId")] informuje Entity Framework, że ItemId jest kluczem obcym(foreign key), który wskazuje na identyfikator przedmiotu(Item) w innej tabeli.Dzięki temu EF może tworzyć odpowiednie relacje między tabelami w bazie danych.*/
        public Item Item { get; set; }     /*to zmienna ktora przechowuje instancje itemu ktory chcemy przypisac */
    }
}

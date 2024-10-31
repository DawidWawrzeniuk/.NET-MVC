using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAppOneToMany.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;               /*przypisuje null do właściwości, ale operator !(ang. "null-forgiving operator") jest użyty, aby wyciszyć ostrzeżenie kompilatora o możliwości przypisania null do typu referencyjnego, który nie powinien akceptować null.Innymi słowy, kompilator nie będzie generował ostrzeżeń, ponieważ null! oznacza, że programista bierze odpowiedzialność za zapewnienie, że właściwość zostanie później poprawnie zainicjowana.*/
        public double Price { get; set; }
        public int? SerialNumberId { get; set; }              //Właściwość typu całkowitego, która przechowuje identyfikator numeru seryjnego powiązanego z tym przedmiotem.
        public SerialNumber? SerialNumber { get; set; }       //Właściwość typu SerialNumber, która reprezentuje powiązanie z obiektem SerialNumber. Może być używana do dostępu do właściwości SerialNumber dla danego przedmiotu.

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }


    }
}

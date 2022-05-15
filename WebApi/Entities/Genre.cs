using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entites
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Auto Increment
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }=true;
        //public ICollection<Book> Books { get; set; }
    }
}
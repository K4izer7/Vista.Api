using System.ComponentModel.DataAnnotations;
namespace Vista.Api.Data
{
    public class Category
    {
        [Key] // Since the key name (Category code) does not include 
              // "Id" we have to use an annotation( could also
              // specify this using Fluent API) 
        [MaxLength(15)]
        public string CategoryCode { get; set; }

        [MaxLength(30)]
        public string CategoryName { get; set; }
        public List<TrainerCategory>? TrainerCategories { get; set; }
    }
}

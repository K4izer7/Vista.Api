using System.ComponentModel.DataAnnotations;
namespace Vista.Api.Data
{
    public class TrainerCategory
    {
        // Has a composite key made up of the TrainerId and CategoryCode
        [Required]
        public int TrainerId { get; set; }

        [MaxLength(15)]
        public required string CategoryCode { get; set; }
        public Trainer? Trainer { get; set; }
        // See TrainerDBContext.cs for the configuration of the composite key using Fluent API
        public Category? Category { get; set; }
    }
}

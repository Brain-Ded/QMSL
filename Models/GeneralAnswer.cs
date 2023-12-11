using QMSL.Models;
using System.ComponentModel.DataAnnotations;

public class GeneralAnswer
{
    [Key]
    public int Id { get; set; }
    public string Text { get; set; }
    public int? GeneralQuestionId { get; set; }
    //public GeneralQuestion? GeneralQuestion { get; set; } = null!;
   // public EditableQuestion? EditableQuestion { get; set; } = null!;
}
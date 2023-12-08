using QMSL.Models;
using System.ComponentModel.DataAnnotations;

public class Answer
{
    [Key]
    public int Id { get; set; }
    public string Text { get; set; }
    public int GeneralQuestionId { get; set; }
    public GeneralQuestion GeneralQuestion { get; set; } = null!;
    public int EditableQuestionId { get; set; }
    public EditableQuestion EditableQuestion { get; set; } = null!;
}
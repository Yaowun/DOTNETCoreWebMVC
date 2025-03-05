using System.ComponentModel.DataAnnotations;
using DOTNETCoreWebMVC.Models.Enum;

namespace DOTNETCoreWebMVC.Models;

public class TodoModel
{
    public int Id { get; set; }
    [Required]
    [Display(Name = "Title")]
    [StringLength(50)]
    public string Title { get; set; }
    public TodoStatus Status { get; set; }
    public TodoType Type { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTime CreatedAt { get; set; }
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTime ModifiedAt { get; set; }
}
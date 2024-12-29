using System.ComponentModel.DataAnnotations;

namespace FinanceApi.Dtos.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(8, ErrorMessage = "min 8")]
        [MaxLength(280, ErrorMessage = "min 280")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(8, ErrorMessage = "min 8")]
        [MaxLength(280, ErrorMessage = "min 280")]
        public string Content { get; set; } = string.Empty;
    }
}

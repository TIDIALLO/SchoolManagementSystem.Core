
namespace SchoolManagementSystem.Portal.Shared.Request;

public class CreateQuestionRequest
{
    /*    public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Question { get; set; } = string.Empty;*/
    //######################################################################
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Question { get; set; } = string.Empty;
    public string CorrectAnswer { get; set; } = string.Empty;
    public string Choice1 { get; set; } = string.Empty;
    public string Choice2 { get; set; } = string.Empty;
    public string Choice3 { get; set; } = string.Empty;
    public string Choice4 { get; set; } = string.Empty;

}

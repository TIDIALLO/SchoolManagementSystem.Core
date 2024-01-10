namespace SchoolManagementSystem.Portal.Shared.Response;

public class CreateQuestionResponse
{
    /* public Guid Id { get; set; }
     public string? Name { get; set; }
     public string? Email { get; set; }
     public string? Question { get; set; } = string.Empty;*/
    //######################################################################
    // 🙂🙂
    public Guid Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Choice1 { get; set; } = string.Empty;
    public string Choice2 { get; set; } = string.Empty;
    public string Choice3 { get; set; } = string.Empty;
    public string Choice4 { get; set; } = string.Empty;
}

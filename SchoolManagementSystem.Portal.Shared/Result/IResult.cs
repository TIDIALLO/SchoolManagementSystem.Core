namespace SchoolManagementSystem.Portal.Shared.Result;

public interface IResult<out T>
{
   public  bool Successed { get; set; }
    public T Data { get; }

    public List<string> Messages { get; set; } 
}

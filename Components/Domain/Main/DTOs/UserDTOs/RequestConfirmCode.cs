namespace TaskList.Components.Domain.Main.DTOs.UserDTOs
{
    public record RequestConfirmCode(string Email, string Password, string Code)
    {
    }
}

using MovieManagementSystem.Models;

namespace MovieManagementSystem.Interface
{
    public interface ITokenGenerate
    {
        public string GenerateToken(User user);
    }
}

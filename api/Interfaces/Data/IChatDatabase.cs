using api.Models;

namespace api.Interfaces.Data
{
    public interface IChatDatabase
    {
        Task AddStatus(string id, PeopleStatus status, Plataform plataform, string conversation);
        Task UpdateStatus(string id, PeopleStatus status, Plataform plataform, string conversation);
        Task GetConversation(string id);
        Task<int?> GetStatus(string id);
    }
}
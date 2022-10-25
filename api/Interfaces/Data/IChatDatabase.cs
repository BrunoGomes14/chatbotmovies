using api.Models;
using api.Models.Db;

namespace api.Interfaces.Data
{
    public interface IChatDatabase
    {
        Task AddStatus(string id, PeopleStatus status, Plataform plataform);
        Task UpdateStatus(string id, PeopleStatus status);
        Conversation? GetConversation(string id);
        Task InsertResult(string peopleId, string result, int functionId);
    }
}
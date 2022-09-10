using api.Models;

namespace api.Interfaces.Data
{
    public class ChatDatabase : IChatDatabase
    {
        public async Task AddStatus(string id, PeopleStatus status, Plataform plataform, string conversation)
        {
        }

        public async Task GetConversation(string id)
        {
        }

        public async Task<int?> GetStatus(string id)
        {
            return 2;
        }

        public async Task UpdateStatus(string id, PeopleStatus status, Plataform plataform, string conversation)
        {
        }
    }
}
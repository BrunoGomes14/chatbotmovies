using api.Models;
using api.Models.Db;

namespace api.Interfaces.Data
{
    public class ChatMySqlDatabase : IChatDatabase
    {
        private readonly chatbotdbContext db;

        public ChatMySqlDatabase(AppSettings config)
        {
            db = new chatbotdbContext(config);
        }

        public async Task AddStatus(string id, PeopleStatus status, Plataform plataform)
        {
            await db.Conversations.AddAsync(new Conversation()
            {
                PeopleId = id,
                Status = (int)status,
                From = plataform.ToString(),
                LastReceive = DateTime.UtcNow.AddHours(-3)
            });

            await db.SaveChangesAsync();
        }

        public Conversation? GetConversation(string id)
        {
            return db.Conversations.FirstOrDefault(x => x.PeopleId == id
                                                     && x.Status != (int)PeopleStatus.Finished
                                                     && x.Status != (int)PeopleStatus.FinishedForRestart
                                                     && x.Status != (int)PeopleStatus.FinishedByApplication
                                                     && x.Status != (int)PeopleStatus.FinishedByUser);
        }

        public async Task UpdateStatus(string id, PeopleStatus status)
        {
            var model = db.Conversations.FirstOrDefault(x => x.PeopleId == id
                                                          && x.Status != (int)PeopleStatus.Finished
                                                          && x.Status != (int)PeopleStatus.FinishedForRestart
                                                          && x.Status != (int)PeopleStatus.FinishedByApplication)!;

            model.Status = (int)status;
            await db.SaveChangesAsync();
        }
        
        public async Task InsertResult(string peopleId, string result, int functionId)
        {
            await db.ConversationResultHistories.AddAsync(new()
            {
                CreatedAt = DateTime.UtcNow,
                FunctionId = functionId,
                PeopleId = peopleId,
                Result = result
            });

            await db.SaveChangesAsync();
        }
    }
}
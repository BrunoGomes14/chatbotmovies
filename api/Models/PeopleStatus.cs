namespace api.Models
{
    public enum PeopleStatus
    {
        NotFound = -1,
        InitConversation = 1,
        WaitingChoseFunction = 2,
        WaintingWriteMovieToFind = 3,
        WaitingAnswerQuestion = 4,
        Finished = 5
    }
}
namespace api.Models
{
    public enum PeopleStatus
    {
        NotFound = -1,
        Processing = 0,
        InitConversation = 1,
        WaitingChoseFunction = 2,
        WaintingWriteMovieToFind = 3,
        WaitingAnswerQuestion = 4,
        WaitingSendLocation = 5,
        Finished = 6,
        FinishedByApplication = 7,
        WatingDecideAfterFindLocation = 8,
        RestartConversation = 9,
        WaitingChoseFunctionAfterMovie = 10
    }
}
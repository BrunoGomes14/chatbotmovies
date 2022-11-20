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
        FinishedForRestart = 8,
        FinishedByUser = 9,
        RestartConversation = 10,
        WaitingChoseFunctionAfterMovie = 11,
        WaitingConfirmSortMovie = 12
    }
}
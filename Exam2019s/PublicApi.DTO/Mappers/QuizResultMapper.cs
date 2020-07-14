using DAL = ee.itcollege.mavozd.DAL.Base.Mappers;

namespace PublicApi.DTO.Mappers
{
    public class QuizResultMapper : DAL.BaseMapper<Domain.QuizResult, QuizResult>
    {
        public QuizResultView MapForQuizResultView(Domain.QuizResult inObject)
        {
            return new QuizResultView()
            {
                CorrectAnswers = inObject.CorrectAnswers,
                Id = inObject.Id,
                QuizId = inObject.QuizId,
                TotalAnswers = inObject.Quiz!.QuizQuestions!.Count
            };
        }
    }
}
using System.Linq;
using DAL = ee.itcollege.mavozd.DAL.Base.Mappers;

namespace PublicApi.DTO.Mappers
{
    public class QuizMapper : DAL.BaseMapper<Domain.Quiz, Quiz>
    {
        private readonly QuestionMapper _questionMapper = new QuestionMapper();
        public QuizView MapForQuizView(Domain.Quiz inObject)
        {
            return new QuizView()
            {
                CompletelyCorrectAnswers = inObject.QuizResults.Count(e => e.CorrectAnswers == e.Quiz!.QuizQuestions!.Count),
                HowManyTimesDone = inObject.QuizResults!.Count,
                Id = inObject.Id,
                QuizType = inObject.QuizType!.Type,
                QuizTypeId = inObject.QuizTypeId,
                Title = inObject.Title,
                QuizQuestionViews = inObject.QuizQuestions.Select(e => _questionMapper.MapForQuestionView(e)).ToList()
            };
        }
    }
}
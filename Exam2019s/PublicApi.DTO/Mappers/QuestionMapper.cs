using System.Linq;
using DAL = ee.itcollege.mavozd.DAL.Base.Mappers;

namespace PublicApi.DTO.Mappers
{
    public class QuestionMapper : DAL.BaseMapper<Domain.Question, Question>
    {
        private readonly AnswerMapper _answerMapper = new AnswerMapper();
        
        public QuestionView MapForQuestionView(Domain.Question inObject)
        {
            return new QuestionView()
            {
                Content = inObject.Content,
                Id = inObject.Id,
                QuestionAnswers = inObject.QuestionAnswers.Select(e => _answerMapper.Map(e)).ToList(),
                QuizId = inObject.QuizId
            };
        }
    }
}
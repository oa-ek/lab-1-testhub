using Domain.common;
using Domain.events;

namespace Domain.entities
{
    public class StatusSessionQuestion : BaseAuditableEntity
    {
        public int SessionId { get; set; }
        public int QuestionId { get; set; }

        private bool _isCorrect;
        public bool Correct
        {
            get => _isCorrect;
            set
            {
                if (value && !_isCorrect)
                {
                    AddDomainEvent(new CorrectAnswerEnteredEvent(this));
                }

                _isCorrect = value;
            }
        }

        protected internal virtual ICollection<Question> Question { get; set; } = new List<Question>();
        protected internal virtual ICollection<TestSession> TestSession { get; set; } = new List<TestSession>();
    }
}
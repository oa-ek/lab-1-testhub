namespace Domain.entities
{
    public class StatusSessionQuestion : BaseAuditableEntity
    {
        public int SessionId { get; set; }

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

        public int QuestionId { get; set; }
        public virtual Question Questions { get; set; } = new Question();
    
        public virtual TestSession TestSession { get; set; } = new TestSession();
        public virtual ICollection<TestSession> TestSessions { get; set; } = new List<TestSession>();
    }
}
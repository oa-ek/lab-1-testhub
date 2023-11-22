using Domain.common;
using Domain.events;

namespace Domain.entities
{
    public class TestSession : BaseAuditableEntity
    {
        public TestSession()
        {
            _attemptAmount++;
        }

        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public bool IsTraining { get; set; }
        public int? Result { get; set; }

        private int _attemptAmount;
        public int AttemptAmount
        {
            get => _attemptAmount;
            set
            {
                if (value > _attemptAmount)
                {
                    AddDomainEvent(new AttemptAmountChangedEvent(this));
                }

                _attemptAmount = value;
            }
        }

        public int UserId { get; set; }
        public virtual User User { get; set; } = new User();

        public int TestId { get; set; }
        public virtual Test Test { get; set; } = new Test()!;

        protected internal virtual ICollection<StatusSessionQuestion> StatusSessionQuestions { get; set; } = new List<StatusSessionQuestion>();
    }
}
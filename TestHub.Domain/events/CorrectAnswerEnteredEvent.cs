using Domain.common;
using Domain.entities;

namespace Domain.events
{
    public class CorrectAnswerEnteredEvent : BaseEvent
    {
        public CorrectAnswerEnteredEvent(StatusSessionQuestion statusSessionQuestion)
        {
            StatusSessionQuestion = statusSessionQuestion;
        }

        public StatusSessionQuestion StatusSessionQuestion { get; }
    }
}
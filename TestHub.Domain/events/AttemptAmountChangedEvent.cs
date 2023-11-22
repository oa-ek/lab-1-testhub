using Domain.common;
using Domain.entities;

namespace Domain.events
{
    public class AttemptAmountChangedEvent : BaseEvent
    {
        public AttemptAmountChangedEvent(TestSession testSession)
        {
            TestSession = testSession;
        }

        public TestSession TestSession { get; }
    }
}
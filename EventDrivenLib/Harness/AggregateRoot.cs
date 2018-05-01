using System.Collections.Generic;

namespace EventDrivenLib.Harness
{
	public abstract class AggregateRoot
	{
		protected AggregateRoot()
		{
			_pendingEvents = new List<DomainEvent>();
		}

		public void AddEvent(DomainEvent @event)
		{
			_pendingEvents.Add(@event);
		}

		public void YieldPersisted()
		{
			_pendingEvents.Clear();
		}

		public IEnumerable<DomainEvent> PendingEvents => _pendingEvents;

		private readonly List<DomainEvent> _pendingEvents;
	}
}
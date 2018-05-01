using System.Collections.Generic;

namespace EventDrivenLib.Harness
{
	public interface IEventPublisher
	{
		void Publish(IEnumerable<DomainEvent> eventsBatch);

		void Publish(DomainEvent domainEvent);
	}
}
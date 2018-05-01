using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;

namespace EventDrivenLib.Harness
{
	public abstract class AggregateRepository<T> where T: AggregateRoot
	{
		protected AggregateRepository(IEventPublisher publisher, IEventDescriber eventDescriber)
		{
			_publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
			_eventDescriber = eventDescriber ?? throw new ArgumentNullException(nameof(eventDescriber));
		}

		public abstract Option<T> TryLoad(Guid id);

		public void Save(T aggregate)
		{
			if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));

			var events = aggregate.PendingEvents;
			if (!events.Any())
			{
				return;
			}

			var descriptions = events.Select(_eventDescriber.Describe);
			ApplyEvents(descriptions);
			_publisher.Publish(events);
			aggregate.YieldPersisted();
		}

		private void ApplyEvents(IEnumerable<EventDescription> descriptions)
		{
			//todo: implement
			throw new NotImplementedException();
		}

		private readonly IEventPublisher _publisher;
		private readonly IEventDescriber _eventDescriber;
	}
}
using System;
using System.Collections.Generic;
using LanguageExt;

namespace EventDrivenLib.Harness
{
	public class InMemoryEventHub : IEventPublisher
	{
		public void Subscribe<T>(IEventConsumer<T> consumer) where T :DomainEvent
		{
			var eventType = typeof(T);
			var publishEvent = _consumersMap.TryGetValue(eventType).IfNone(() => EmptyPublishEventAction);

			publishEvent += @event =>
			{
				var typedEvent = (T) @event;
				consumer.Consume(typedEvent);
			};
			
			_consumersMap[eventType] = publishEvent;
		}

		public void Publish(IEnumerable<DomainEvent> eventsBatch)
		{
			foreach (var domainEvent in eventsBatch)
			{
				Publish(domainEvent);
			}
		}

		public void Publish(DomainEvent domainEvent)
		{
			var publishEventAction = _consumersMap.TryGetValue(domainEvent.GetType()).IfNone(() => EmptyPublishEventAction);
			publishEventAction(domainEvent);
		}

		private static PublishEvent EmptyPublishEventAction => @event => { };
		private readonly IDictionary<Type, PublishEvent> _consumersMap = new Dictionary<Type, PublishEvent>();
		private delegate void PublishEvent(DomainEvent domainEvent);
	}
}
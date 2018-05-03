using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using LanguageExt;

namespace EventDrivenLib.Harness
{
	public abstract class AggregateRepository<T> where T: AggregateRoot
	{
		protected AggregateRepository(IDbConnection dbConnection, IEventPublisher publisher, IEventDescriber eventDescriber)
		{
			DbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
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
			using (var transaction = DbConnection.BeginTransaction())
			{
				foreach (var eventDescription in descriptions)
				{
					DbConnection.Execute(
						eventDescription.SqlRequest,
						eventDescription.Parameters.FirstOrDefault(),
						transaction);
				}

				transaction.Commit();
			}
		}

		protected readonly IDbConnection DbConnection;

		private readonly IEventPublisher _publisher;
		private readonly IEventDescriber _eventDescriber;
	}
}
using System;

namespace EventDrivenLib.Harness
{
	public abstract class DomainEvent
	{
		protected DomainEvent(DateTimeOffset occuredOn)
		{
			OccuredOn = occuredOn;
		}

		public DateTimeOffset OccuredOn { get; }
	}
}
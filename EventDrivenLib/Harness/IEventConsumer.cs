namespace EventDrivenLib.Harness
{
	public interface IEventConsumer<in T> where T : DomainEvent
	{
		void Consume(T @event);
	}
}
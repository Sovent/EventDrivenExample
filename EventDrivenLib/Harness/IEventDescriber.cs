namespace EventDrivenLib.Harness
{
	public interface IEventDescriber
	{
		EventDescription Describe(DomainEvent domainEvent);
	}
}
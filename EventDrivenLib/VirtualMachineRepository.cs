using System;
using EventDrivenLib.Harness;
using LanguageExt;

namespace EventDrivenLib
{
	public class VirtualMachineRepository : AggregateRepository<VirtualMachine>, IVirtualMachineRepository
	{
		public VirtualMachineRepository(IEventPublisher publisher, IEventDescriber eventDescriber) : base(publisher, eventDescriber)
		{
		}

		public override Option<VirtualMachine> TryLoad(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
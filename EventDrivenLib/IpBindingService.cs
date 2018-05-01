using System;
using System.Net;
using EventDrivenLib.Harness;

namespace EventDrivenLib
{
	public class IpBindingService : IEventConsumer<VirtualMachineReserved>, IEventConsumer<IpAddressBound>
	{
		public IpBindingService(IVirtualMachineRepository virtualMachineRepository)
		{
			_virtualMachineRepository = virtualMachineRepository ??
			                            throw new ArgumentNullException(nameof(virtualMachineRepository));
		}

		public void Consume(VirtualMachineReserved @event)
		{
			if (@event == null) throw new ArgumentNullException(nameof(@event));

			var virtualMachine = _virtualMachineRepository.TryLoad(@event.VirtualMachineId).Head();
			var ipAddressToBind = GetIpAddressSomehow();
			virtualMachine.BindIpAddress(ipAddressToBind);
			_virtualMachineRepository.Save(virtualMachine);
		}

		public void Consume(IpAddressBound @event)
		{
			// отправляем нотификации пользователю, например
		}

		private static IPAddress GetIpAddressSomehow()
		{
			return IPAddress.Any;
		}

		private readonly IVirtualMachineRepository _virtualMachineRepository;
	}
}
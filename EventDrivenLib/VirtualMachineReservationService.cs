using System;

namespace EventDrivenLib
{
	public class VirtualMachineReservationService : IVirtualMachineReservationService
	{
		public VirtualMachineReservationService(IVirtualMachineRepository repository)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		public void ReserveVirtualMachine(Guid id, ReservationInfo reservationInfo)
		{
			if (reservationInfo == null) throw new ArgumentNullException(nameof(reservationInfo));
			
			var virtualMachine = _repository.TryLoad(id).IfNone(() => throw new InvalidOperationException($"VM #{id} not found"));

			virtualMachine.Reserve(reservationInfo);

			_repository.Save(virtualMachine);
		}

		private readonly IVirtualMachineRepository _repository;
	}
}
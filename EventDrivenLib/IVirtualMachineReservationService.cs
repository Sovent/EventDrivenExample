using System;

namespace EventDrivenLib
{
	public interface IVirtualMachineReservationService
	{
		void ReserveVirtualMachine(Guid id, ReservationInfo reservationInfo);
	}
}
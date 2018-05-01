using System;
using EventDrivenLib.Harness;

namespace EventDrivenLib
{
	public class VirtualMachineReserved : DomainEvent
	{
		public VirtualMachineReserved(Guid virtualMachineId, ReservationInfo reservationInfo, DateTimeOffset occuredOn) : base(occuredOn)
		{
			VirtualMachineId = virtualMachineId;
			ReservationInfo = reservationInfo;
		}

		public Guid VirtualMachineId { get; }

		public ReservationInfo ReservationInfo { get; }
	}
}
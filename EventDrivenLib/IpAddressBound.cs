using System;
using System.Net;
using EventDrivenLib.Harness;

namespace EventDrivenLib
{
	public class IpAddressBound : DomainEvent
	{
		public IpAddressBound(
			Guid virtualMachineId,
			ReservationInfo reservationInfo,
			IPAddress ipAddress,
			DateTimeOffset occuredOn) : base(occuredOn)
		{
			VirtualMachineId = virtualMachineId;
			ReservationInfo = reservationInfo ?? throw new ArgumentNullException(nameof(reservationInfo));
			IpAddress = ipAddress ?? throw new ArgumentNullException(nameof(ipAddress));
		}

		public Guid VirtualMachineId { get; }

		public ReservationInfo ReservationInfo { get; }

		public IPAddress IpAddress { get; }
	}
}
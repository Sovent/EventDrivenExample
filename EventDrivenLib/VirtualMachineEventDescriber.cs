using System;
using EventDrivenLib.Harness;

namespace EventDrivenLib
{
	public class VirtualMachineEventDescriber : IEventDescriber
	{
		public EventDescription Describe(DomainEvent domainEvent)
		{
			switch (domainEvent)
			{
				case VirtualMachineReserved @event:
					return new EventDescription(
						"UPDATE VirtualMachines SET ClientId=@clientId, ReservationDateTime=@reservationDateTime WHERE Id=@vmId",
						new
						{
							clientId = @event.ReservationInfo.ClientId,
							reservationDateTime = @event.ReservationInfo.ReservationTime,
							vmId = @event.VirtualMachineId.ToString()
						});
				case IpAddressBound @event:
					return new EventDescription(
						"UPDATE VirtualMachines SET IpAddress=@ipAddress WHERE Id=@vmId",
						new
						{
							ipAddress = @event.IpAddress.ToString(),
							vmId = @event.VirtualMachineId.ToString()
						});
				default:
					throw new InvalidOperationException($"Unknown event {domainEvent.GetType().FullName} to describe");
			}
		}
	}
}
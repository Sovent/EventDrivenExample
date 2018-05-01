using System;
using System.Net;
using EventDrivenLib.Harness;
using LanguageExt;

namespace EventDrivenLib
{
    public class VirtualMachine : AggregateRoot
    {
	    public VirtualMachine(
			Guid id, 
			Option<ReservationInfo> reservationInfo,
			Option<IPAddress> boundIpAddress)
	    {
		    Id = id;
		    ReservationInfo = reservationInfo;
		    BoundIpAddress = boundIpAddress;
	    }

	    public void Reserve(ReservationInfo reservationInfo)
	    {
			ReservationInfo.IfSome(info => throw new InvalidOperationException(
				$"VM #{Id} is already reserved by client #{info.ClientId}"));

		    ReservationInfo = reservationInfo ?? throw new ArgumentNullException(nameof(reservationInfo));

			AddEvent(new VirtualMachineReserved(Id, reservationInfo, DateTimeOffset.UtcNow));
	    }

	    public void BindIpAddress(IPAddress ipAddress)
	    {
		    ReservationInfo.IfNone(() =>
			    throw new InvalidOperationException($"Can't bind ip address to not reserved machine #{Id}"));

		    BoundIpAddress.IfSome(ip =>
			    throw new InvalidOperationException($"Can't bind ip to machine #{Id} because ip is already bound"));

		    BoundIpAddress = ipAddress ?? throw new ArgumentNullException(nameof(ipAddress));

			AddEvent(new IpAddressBound(Id, ReservationInfo.Head(), ipAddress, DateTimeOffset.UtcNow));
	    }

	    public Guid Id { get; }
		
		public Option<ReservationInfo> ReservationInfo { get; private set; }

		public Option<IPAddress> BoundIpAddress { get; private set; }
    }
}

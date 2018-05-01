using System;

namespace EventDrivenLib
{
	public class ReservationInfo
	{
		public ReservationInfo(Guid clientId, DateTimeOffset reservationTime)
		{
			ClientId = clientId;
			ReservationTime = reservationTime;
		}

		public Guid ClientId { get; }

		public DateTimeOffset ReservationTime { get; }
	}
}
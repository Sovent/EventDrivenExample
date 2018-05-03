using System;
using System.Data;
using System.Net;
using Dapper;
using EventDrivenLib.Harness;
using LanguageExt;
using static LanguageExt.Prelude;

namespace EventDrivenLib
{
	public class VirtualMachineRepository : AggregateRepository<VirtualMachine>, IVirtualMachineRepository
	{
		public VirtualMachineRepository(
			IDbConnection dbConnection, 
			IEventPublisher publisher, 
			IEventDescriber eventDescriber) : base(dbConnection, publisher, eventDescriber)
		{
		}

		public override Option<VirtualMachine> TryLoad(Guid id)
		{
			var queryResult = DbConnection.QueryFirstOrDefault(
				"SELECT * FROM VirtualMachines WHERE Id=@id", 
				new {id});
			if (queryResult == null)
			{
				return Option<VirtualMachine>.None;
			}

			var reservationInfo = queryResult.ClientId != null
				? new ReservationInfo(queryResult.ClientId, queryResult.ReservationDateTime)
				: null;
			var ipAddress = queryResult.IpAddress != null 
				? (IPAddress)IPAddress.Parse(queryResult.IpAddress) 
				: default(IPAddress);
			return new VirtualMachine(queryResult.Id, Optional(reservationInfo), Optional(ipAddress));
		}
	}
}
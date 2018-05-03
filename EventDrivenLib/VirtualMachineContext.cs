using System.Data.SqlClient;
using EventDrivenLib.Harness;

namespace EventDrivenLib
{
	public class VirtualMachineContext
	{
		public static VirtualMachineContext Configure(string connectionString)
		{
			var dbConnection = new SqlConnection(connectionString);
			dbConnection.Open();

			var inMemoryEventHub = new InMemoryEventHub();

			var virtualMachineRepository = new VirtualMachineRepository(
				dbConnection,
				inMemoryEventHub,
				new VirtualMachineEventDescriber());

			var ipBindingService = new IpBindingService(virtualMachineRepository);
			inMemoryEventHub.Subscribe<VirtualMachineReserved>(ipBindingService);
			inMemoryEventHub.Subscribe<IpAddressBound>(ipBindingService);

			var reservationService = new VirtualMachineReservationService(virtualMachineRepository);
			return new VirtualMachineContext
			{
				VirtualMachineReservationService = reservationService
			};
		}

		public IVirtualMachineReservationService VirtualMachineReservationService { get; private set; }
	}
}
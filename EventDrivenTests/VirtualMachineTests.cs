using System;
using EventDrivenLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventDrivenTests
{
    [TestClass]
    public class VirtualMachineTests
    {
		[TestInitialize]
	    public void Initialize()
		{
			var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\projects\eventdrivenexample\VirtualMachineDB.mdf;Integrated Security=True;Connect Timeout=30";
			_context = VirtualMachineContext.Configure(connectionString);
		}

        [TestMethod]
        public void ReserveVirtualMachine_Test()
        {
	        var reservationService = _context.VirtualMachineReservationService;
			reservationService.ReserveVirtualMachine(
				Guid.Parse("28014e66-f5f0-49f4-a951-59bf590c7e06"),
				new ReservationInfo(Guid.NewGuid(), DateTimeOffset.UtcNow));
        }

	    private VirtualMachineContext _context;
    }
}

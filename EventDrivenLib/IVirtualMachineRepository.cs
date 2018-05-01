using System;
using LanguageExt;

namespace EventDrivenLib
{
	public interface IVirtualMachineRepository
	{
		Option<VirtualMachine> TryLoad(Guid id);

		void Save(VirtualMachine virtualMachine);
	}
}
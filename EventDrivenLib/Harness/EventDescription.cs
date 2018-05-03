using LanguageExt;

namespace EventDrivenLib.Harness
{
	public class EventDescription
	{
		public EventDescription(string sqlRequest, Option<object> parameters)
		{
			SqlRequest = sqlRequest;
			Parameters = parameters;
		}

		public string SqlRequest { get; }

		public Option<object> Parameters { get; }
	}
}
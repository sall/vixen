using Vixen.Data.Value;

namespace Vixen.Sys.Dispatch
{
	internal interface IAnyIntentStateHandler : IHandler<IIntentState<RGBAValue>>, IHandler<IIntentState<LightingValue>>,
	                                            IHandler<IIntentState<PositionValue>>, IHandler<IIntentState<CommandValue>>
	{
	}
}
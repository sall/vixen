using Vixen.Data.Value;

namespace Vixen.Sys.Dispatch
{
	internal interface IAnyIntentSegmentHandler : IHandler<IIntentSegment<PositionValue>>,
	                                              IHandler<IIntentSegment<RGBAValue>>,
	                                              IHandler<IIntentSegment<CommandValue>>,
	                                              IHandler<IIntentSegment<LightingValue>>
	{
	}
}
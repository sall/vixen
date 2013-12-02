using Vixen;
using Vixen.Data.Combinator._8Bit;
using Vixen.Data.Evaluator;
using Vixen.Data.Policy;
using Vixen.Sys;



namespace VixenModules.Output.ConductorOutput
{
    //  Authored by Charles Strang on 10-29-2013
    //  Additional contributions by Tony Eberle
    //  code can be used and distributed freely
    //  Please give the coders their proper acknowledgement.

    class ConductorModuleDataPolicy : ControllerDataPolicy
 
    {
        protected override IEvaluator GetEvaluator()
        {
            return new _8BitEvaluator();
        }

        protected override ICombinator GetCombinator()
        {
            return new _8BitHighestWinsCombinator();
        }
    }
}

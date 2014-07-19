using System.Linq;
using System.Text;
using Vixen.Data.Policy;
using Vixen.Sys;
using Vixen.Data.Evaluator;
using Vixen.Data.Combinator._8Bit;

namespace VixenModules.Output.Exporter
{
    class ExporterDataPolicy : ControllerDataPolicy
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
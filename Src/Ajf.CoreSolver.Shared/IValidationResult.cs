using Elasticsearch.Net;

namespace Ajf.CoreSolver.Shared
{
    public interface IValidationResult
    {
        bool IsValid { get; set; }
        IValidationItem[] ValidationItems { get; set; }
    }

    public interface IValidationItem
    {
        ValidationLevel Level { get; set; }
        string Comment { get; set; }
    }

    public enum ValidationLevel
    {
        None=0,
        Info=1,
        Warning=2,
        Error=3
    }
}
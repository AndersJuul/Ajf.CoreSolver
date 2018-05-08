namespace Ajf.CoreSolver.Shared
{
    public class ValidationItem : IValidationItem
    {
        public ValidationLevel Level { get; set; }
        public string Comment { get; set; }
    }
}
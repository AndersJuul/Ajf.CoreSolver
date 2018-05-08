namespace Ajf.CoreSolver.Shared
{
    public class ValidationResult : IValidationResult
    {
        public bool IsValid { get; set; }
        public IValidationItem[] ValidationItems { get; set; }
    }
}
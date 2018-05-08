namespace Ajf.CoreSolver.DbModels
{
    public enum CalculationStatusDto
    {
        None,
        Queued=1,
        InProgress = 2,
        DoneAndFailed = 3,
        DoneAndSuccess = 4
    }
}
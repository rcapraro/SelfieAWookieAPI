namespace SelfieAWookie.core.Framework
{
    /// <summary>
    ///     Use it to define class is a repository
    /// </summary>
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
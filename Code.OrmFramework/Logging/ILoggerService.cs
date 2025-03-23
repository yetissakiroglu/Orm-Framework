namespace Code.OrmFramework.Logging
{
    public interface ILoggerService
    {
        void LogInfo(string actionName, object request, object response);
        void LogError(string actionName, object request, Exception ex);
    }
}

namespace HousingOffersAPI.Services.ScriptRelated
{
    public interface IRScriptTasksRunner
    {
        void RunRScript(string scriptPath);
        void UpdateScriptCsvs();
    }
}
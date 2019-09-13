namespace CodeMetricsDBRepositoryContractsLib
{
    public interface ICodeMetricsDbRepository
    {
        void PersistToDatabase(string gitRepo);
        int GetSimianDuplicates(string gitRepo);
        void UpdateSimianDuplicates(string gitRepo,int simianDuplicates);
        int GetTicsErrors(string gitRepo);
        void UpdateTicsErrors(string gitRepo, int ticsErrors);
    }
}
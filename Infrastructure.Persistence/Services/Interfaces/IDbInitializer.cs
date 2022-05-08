namespace Infrastructure.Persistence.Services
{
    public interface IDbInitializer
    {
        void Initialize();
        void SeedStatesandLGAs();
        void SeedAppTrooperHelpers();
    }
}

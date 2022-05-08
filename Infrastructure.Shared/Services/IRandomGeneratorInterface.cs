namespace Infrastructure.Shared.Services
{
    public interface IRandomNumberGeneratorInterface : IAutoDependencyService
    {
        string GenerateRandomNumber(int length, Mode mode = Mode.AlphaNumeric);
    }


}

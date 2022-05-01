namespace Infrastructure.Shared.Services
{
    public interface IRandomNumberGeneratorInterface
    {
        string GenerateRandomNumber(int length, Mode mode = Mode.AlphaNumeric);
    }


}

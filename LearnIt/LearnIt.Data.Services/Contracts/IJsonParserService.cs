using LearnIt.Data.Models;

namespace LearnIt.Data.Services.Contracts
{
    public interface IJsonParserService
    {
        Course Execute(byte[] fileStream);

    }
}

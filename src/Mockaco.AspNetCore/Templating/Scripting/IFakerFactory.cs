using Bogus;

namespace Mockaco.Templating.Scripting
{
    public interface IFakerFactory
    {
        Faker GetDefaultFaker();

        Faker GetFaker(IEnumerable<string> acceptLanguages);     
    }
}
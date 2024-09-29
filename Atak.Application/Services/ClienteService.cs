using Atak.Core.Entities;
using Bogus;

namespace Atak.Application.Services
{
    public class ClienteService
    {
        public List<Cliente> GerarClientes(int quantidade)
        {
            var faker = new Faker<Cliente>()
                .RuleFor(c => c.Nome, f => f.Name.FullName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber())
                .RuleFor(c => c.DataNascimento, f => f.Date.Past(30));

            return faker.Generate(quantidade);
        }
    }
}

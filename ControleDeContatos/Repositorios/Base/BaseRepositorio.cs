namespace ControleDeContatos.Repositorios.Base
{
    public class BaseRepositorio
    {
        public readonly string _urlBase;

        public BaseRepositorio(string sectionName)
        {
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.Development.json")
                                .Build();

            var section = configuration.GetSection(sectionName);
            _urlBase = section.GetSection("URL_Base").Value;
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace Test4e.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "banana", "maca", "melancia", "morango", "uva", "pera", "abacate", "limao", "melao", "coco"
        };

        private static readonly string[] Cores = new[]
{
            "Amarelo", "Vermelho", "Verde", "Rosa", "Roxo", "Marrom"
};

        private readonly ILogger<WeatherForecastController> _logger;

        private string escolheCor(object nome)
        {
           switch (nome.ToString())
           {
            case "banana":
                return "Amarelo";
            case "maca":
                return "Vermelho";
            case "melancia":
                return "Verde";
            case "morango":
                return "Rosa";
            case "uva":
                return "Roxo";
            case "pera":
                return "Verde";
            case "abacate":
                return "Verde";
            case "limao":
                return "Amarelo";
            case "melao":
                return "Amarelo";
            case "coco":
                return "Marrom";
            default:
                return "Desconhecido";
            }
        }

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public IEnumerable<Frutas> Get()
        {
            var frutaSorteada = Summaries[Random.Shared.Next(Summaries.Length)];
            return Enumerable.Range(1, 10).Select(index => new Frutas
            {
                Quantidade = Random.Shared.Next(0, 55),
                Nome = frutaSorteada,
                Cor = escolheCor(frutaSorteada)
            })
            .ToArray();
        }

        [HttpGet("detalhes")]
        public IEnumerable<Frutas> GetDetalhes()
        {
            return Enumerable.Range(1, 3).Select(index => new Frutas
            {
                Quantidade = Random.Shared.Next(0, 30),
                Nome = "Detalhado",
                 Cor = "Transparente"
            })
            .ToArray();
        }
    }
}

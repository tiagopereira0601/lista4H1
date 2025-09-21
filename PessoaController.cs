using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();

        [HttpPost("adicionar")]
        public IActionResult Adicionar([FromBody] Pessoa pessoa)
        {
            if (pessoas.Any(p => p.Cpf == pessoa.Cpf))
                return BadRequest("CPF já cadastrado!");

            pessoas.Add(pessoa);
            return Ok("Pessoa adicionada com sucesso.");
        }

        [HttpPut("atualizar/{cpf}")]
        public IActionResult Atualizar(string cpf, [FromBody] Pessoa pessoaAtualizada)
        {
            var pessoa = pessoas.FirstOrDefault(p => p.Cpf == cpf);
            if (pessoa == null) return NotFound("Pessoa não encontrada.");

            pessoa.Nome = pessoaAtualizada.Nome;
            pessoa.Peso = pessoaAtualizada.Peso;
            pessoa.Altura = pessoaAtualizada.Altura;

            return Ok("Pessoa atualizada com sucesso.");
        }

        [HttpDelete("remover/{cpf}")]
        public IActionResult Remover(string cpf)
        {
            var pessoa = pessoas.FirstOrDefault(p => p.Cpf == cpf);
            if (pessoa == null) return NotFound("Pessoa não encontrada.");

            pessoas.Remove(pessoa);
            return Ok("Pessoa removida com sucesso.");
        }

        [HttpGet("todas")]
        public IActionResult Todas()
        {
            return Ok(pessoas);
        }

        [HttpGet("buscarPorCpf/{cpf}")]
        public IActionResult BuscarPorCpf(string cpf)
        {
            var pessoa = pessoas.FirstOrDefault(p => p.Cpf == cpf);
            if (pessoa == null) return NotFound("Pessoa não encontrada.");
            return Ok(pessoa);
        }

        [HttpGet("imc-bom")]
        public IActionResult ImcBom()
        {
            var lista = pessoas.Where(p => {
                double imc = p.CalcularIMC();
                return imc >= 18 && imc <= 24;
            }).ToList();

            return Ok(lista);
        }

        [HttpGet("buscarPorNome/{nome}")]
        public IActionResult BuscarPorNome(string nome)
        {
            var lista = pessoas
                .Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(lista);
        }
    }
}

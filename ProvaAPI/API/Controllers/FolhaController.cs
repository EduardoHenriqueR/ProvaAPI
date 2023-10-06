using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/folha")]
public class FolhaController : ControllerBase
{
    private readonly AppDataContext _ctx;
    public FolhaController(AppDataContext ctx)
    {
        _ctx = ctx;
    }

    //CADASTRAR FOLHA
    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Folha folha)
    {
        try
        {
            Funcionario? funcionario = 
                _ctx.Funcionarios.Find(folha.FuncionarioId);
            if (funcionario == null)
            {
                return NotFound();
            }
            //Cálculos da folha
            double salariobruto = folha.Valor * folha.Quantidade;
            double impostoRenda = CalcularImpostoRenda(salariobruto);
            double impostoInss = CalcularINSS(salariobruto);
            double ImpostoFgts = CalcularFGTS(salariobruto);
            double salarioLiquido = salariobruto - impostoRenda - ImpostoFgts - impostoInss;
            folha.SalarioBruto = salariobruto;
            folha.ImpostoRenda = impostoRenda;
            folha.ImpostoINSS = impostoInss;
            folha.ImpostoFGTS = ImpostoFgts;
            folha.SalarioLiquido = salarioLiquido;
            folha.Funcionario = funcionario;
            _ctx.Folhas.Add(folha);
            _ctx.SaveChanges();
            return Created("", folha);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    //LISTAR FOLHAS
    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        try
        {
            List<Folha> folhas =
                _ctx.Folhas
                .Include(x => x.Funcionario)
                .ToList();
            return folhas.Count == 0 ? NotFound(): Ok(folhas);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    //BUSCAR POR CPF, MES e ANO
[HttpGet]
[Route("buscar/{cpf}/{mes}/{ano}")]
public IActionResult Buscar(
    [FromRoute] string cpf, 
    [FromRoute] int mes,
    [FromRoute] int ano)
{
    Folha folha = _ctx.Folhas.Include(f => f.Funcionario)
    .FirstOrDefault
    (f => f.Funcionario.Cpf.Equals(cpf) && f.Mes == mes && f.Ano == ano);
    
    if (folha != null)
    {
        var funcionarioFolha = new{
            FuncionarioNome = folha.Funcionario.Nome,
            FuncionarioCpf = folha.Funcionario.Cpf,
        };
        return Ok(folha);
    }
    else
    {
        return NotFound();
    }
}






    private double CalcularImpostoRenda(double salariobruto)
    {
        if(salariobruto <= 1903.98)
        {
            return 0;
        }
            else if(salariobruto <= 2826.65)
            {
            return (salariobruto * 0.075) - 142.8;
            }
                else if (salariobruto <= 3751.05)
                {
                return (salariobruto * 0.15) - 354.8;
                }
                    else if (salariobruto <= 4664.68)
                    {
                    return (salariobruto * 0.225) - 636.13;
                    }
        return (salariobruto * 0.275) - 869.39;
        }

        private double CalcularINSS(double salariobruto)
        {
        if (salariobruto <= 1693.72) 
        {
        return salariobruto * 0.08;
        } 
            else if (salariobruto <= 2822.9) 
            {
            return salariobruto * 0.09;
            } 
                else if (salariobruto <= 5645.8) 
                {
                return salariobruto * 0.11;
                }
        return 621.03;
        }

        private double CalcularFGTS(double salariobruto)
        {
            return salariobruto * 0.08;
        }
    }





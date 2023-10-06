
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/funcionario")]
public class FuncionarioController : ControllerBase
{
    private readonly AppDataContext _ctx;
    public FuncionarioController(AppDataContext ctx)
    {
        _ctx = ctx;
    }


    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar ([FromBody] Funcionario funcionario)
    {
        try
        {
            _ctx.Funcionarios.Add(funcionario);
            _ctx.SaveChanges();
            return Created("", funcionario);
        }
        catch (Exception)
        {
            return NotFound();
            //Erro 404
        }
    }

    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        try
        {
            List<Funcionario> funcionarios = _ctx.Funcionarios.ToList();
            return funcionarios.Count == 0 ? NotFound() : Ok(funcionarios);
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}
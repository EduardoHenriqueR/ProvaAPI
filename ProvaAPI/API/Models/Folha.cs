namespace API.Models
{
    public class Folha
    {
        public int FolhaId { get; set; }
        public int Valor { get; set; }
        public int Quantidade { get; set; }
        public double SalarioBruto { get; set; }
        public double ImpostoRenda { get; set; }
        public double ImpostoINSS { get; set; }
        public double ImpostoFGTS { get; set; }
        public double SalarioLiquido { get; set; }
        public int Mes { get; set; }
        public int Ano { get; set; }
        public Funcionario? Funcionario { get; set; }
        public int FuncionarioId { get; set; }
    }
}
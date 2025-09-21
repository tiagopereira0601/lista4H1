namespace WebApp.Models
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }

        public double CalcularIMC()
        {
            if (Altura <= 0) return 0;
            return Peso / (Altura * Altura);
        }
    }
}

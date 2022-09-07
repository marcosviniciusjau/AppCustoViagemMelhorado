using SQLite;

namespace AppCustoViagemMelhorado.Model
{
    public class Viagem
    {
        public int Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public double Distancia { get; set; }
        public double Consumo { get; set; }
        public double Preco { get; set; }

    }
}

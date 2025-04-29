namespace MRent.Application.DTO
{
    public class RentDTO
    {
        public string? identificador { get; set; }
        public string entregador_id { get; set; }
        public string moto_id { get; set; }
        public double? valor_diaria { get; set; }
        public double? multa { get; set; }
        public double? subtotal { get; set; }
        public double? total { get; set; }
        public int? plano { get; set; }
        public DateTime data_inicio { get; set; }
        public DateTime data_termino { get; set; }
        public DateTime data_previsao_termino { get; set; }
        public DateTime? data_devolucao { get; set; }
    }
}

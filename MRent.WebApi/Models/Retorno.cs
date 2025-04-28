namespace MRent.WebApi.Models
{
    public class Retorno
    {
        public string Mensagem { get; set; }

        public Retorno()
        {

        }

        public Retorno(string mensagem)
        {
            Mensagem = mensagem;
        }
    }
}

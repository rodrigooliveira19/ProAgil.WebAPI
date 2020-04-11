namespace ProAgil.WebAPI.Model
{
    public class Evento
    {
        /*A referencia EventoId ja cria o campo 
        como id no Sqlite*/
        public int EventoId { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }
        public string Lote { get; set; }
        public string ImageURL { get; set; }
    }
}
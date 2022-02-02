namespace ProjectoTEN.Models
{
    public class LinkVideo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Link { get; set; }
        public string Tags { get; set; }
        
        public string Descricao { get; set; }
        public int UtilizadorId {get;set;}
    }
}

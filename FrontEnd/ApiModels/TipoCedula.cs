namespace FrontEnd.ApiModels
{
    public class TipoCedula
    {
        public int IdTipoCedula { get; set; }

        public string? NombreTipoCedula { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    }
}

namespace CompaniaRepuestos.ViewModels
{
    public class InformeDiarioViewModel
    {
        public DateTime Fecha { get; set; }
        public double MontoTotalVentas { get; set; }
        public Dictionary<string, int> RepuestosVendidos { get; set; }
    }
}

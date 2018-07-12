namespace Andrade.Chamados.Web.ViewModels.Grafico
{
    public class ListaGraficoViewModel
    {
        public GraficoQuantidadeViewModel GraficoStatus { get; set; }
        public GraficoQuantidadeViewModel GraficoSetor { get; set; }

        public ListaGraficoViewModel()
        {
            GraficoStatus = new GraficoQuantidadeViewModel();
            GraficoSetor = new GraficoQuantidadeViewModel();
        }
    }
}
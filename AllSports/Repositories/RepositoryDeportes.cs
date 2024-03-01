using AllSports.Data;
using AllSports.Models;

namespace AllSports.Repositories
{
    public class RepositoryDeportes
    {
        private AllSportsContext context;

        public RepositoryDeportes(AllSportsContext context)
        {
            this.context = context;
        }

        public List<Deporte> GetDeportes()
        {
            var consulta = from datos in this.context.Deportes select datos;
            return consulta.ToList();
        }

        //GET Sexo

        //Get NUTRICION
        public List<Nutricion> GetNutricion()
        {
            var consulta = from datos in this.context.Nutricion select datos;
            return consulta.ToList();
        }

        //Get DETALLES DEPORTES
        public List<DetalleDeporte> GetDetalleDeportes()
        {
            var consulta = from datos in this.context.DetalleDeporte select datos;
            return consulta.ToList();
        }
        //Get DetalleDeporte por id
        public List<DetalleDeporte> GetDetalleDeportesById(int IdDeporte)
        {
            var consulta = from datos in this.context.DetalleDeporte
                           where datos.IdDeporte == IdDeporte
                           select datos;
            return consulta.ToList();
        }
    }
}

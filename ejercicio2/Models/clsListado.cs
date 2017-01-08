using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
namespace ejercicio2.Models
{
    public class clsListado
    {
        /// <summary>
        /// Uri de la ruta donde tenemos subido el ejercicio de Api Rest en Azure junto con la Base de Datos
        /// </summary>
        private Uri miUri = new Uri("http://ejercicio2Navidades.azurewebsites.net/api");

        public async Task<ObservableCollection<clsPersona>> getListado()
        {
            ObservableCollection<clsPersona> listado = new ObservableCollection<clsPersona>();

            //Igual que ejemplo de gitHub
            HttpBaseProtocolFilter miFiltro = new HttpBaseProtocolFilter();
            miFiltro.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;
            miFiltro.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;

            HttpClient miCliente = new HttpClient(miFiltro);
            try
            {
                String respuesta = await miCliente.GetStringAsync(miUri);
                miCliente.Dispose();

                /*OJO!! Para importar el JsonConvert hay que hacer click derecho sobre la solucion y seleccionar Administrar paquetes Nuget para la solucion e instalamos el paquete Newtonsoft.Json*/
                listado = JsonConvert.DeserializeObject<ObservableCollection<clsPersona>>(respuesta);
            }
            catch(Exception e)
            {
                throw e;
            }
            return listado;
        }
    }
}

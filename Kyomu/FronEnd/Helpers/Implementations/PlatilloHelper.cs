using FronEnd.ApiModels;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Newtonsoft.Json;

namespace FronEnd.Helpers.Implementations
{
    public class PlatilloHelper : IPlatilloHelper
    {
        IServiceRepository _ServiceRepository;

        PlatilloViewModel Convertir(PlatilloAPI platillo)
        {
            PlatilloViewModel platilloViewModel = new PlatilloViewModel
            {
               IdPlatillo = platillo.IdPlatillo,
               Nombre = platillo.Nombre,
               Descripcion = platillo.Descripcion,
               Precio = platillo.Precio,
               Imagen = platillo.Imagen,
               IdCategoria = platillo.IdCategoria

            };
            return platilloViewModel;
        }

        public PlatilloHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
        }
        public PlatilloViewModel Add(PlatilloViewModel platillo)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/Platillo", platillo);
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return platillo;
        }

        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/Platillo" + id.ToString());
            _ = _ServiceRepository.DeleteResponse("api/Platillo/" + id.ToString());
            if (responseMessage != null)
            {
                var content = responseMessage.Content;
            }
        }

        public PlatilloViewModel GetPlatillo(int? id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Platillo/" + id.ToString());
            PlatilloAPI platillo = new PlatilloAPI();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                platillo = JsonConvert.DeserializeObject<PlatilloAPI>(content);
            }

            PlatilloViewModel resultado = Convertir(platillo);


            return resultado;
        }

        public List<PlatilloViewModel> GetPlatillos()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Platillo");
            List<PlatilloAPI> platillos = new List<PlatilloAPI>();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                platillos = JsonConvert.DeserializeObject<List<PlatilloAPI>>(content);
            }
            List<PlatilloViewModel> lista = new List<PlatilloViewModel>();
            foreach (var platillo in platillos)
            {
                lista.Add(Convertir(platillo));
            }
            return lista;
        }

        public PlatilloViewModel Update(PlatilloViewModel platillo)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.PutResponse("api/Platillo/" + platillo.IdPlatillo.ToString(), platillo);
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                PlatilloAPI updatedPlatillo = JsonConvert.DeserializeObject<PlatilloAPI>(content);
                return Convertir(updatedPlatillo);
            }
            else
            {

                var errorMessage = responseMessage.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Error al actualizar el platillo: " + errorMessage);
                return null;
            }
        }
    }
}

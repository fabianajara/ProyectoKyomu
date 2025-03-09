using FronEnd.ApiModels;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Newtonsoft.Json;

namespace FronEnd.Helpers.Implementations
{
    public class ReseñaHelper : IReseñaHelper
    {
        IServiceRepository _ServiceRepository;

        ReseñaViewModel Convertir(ReseñaAPI reseña)
        {
            ReseñaViewModel reseñaViewModel = new ReseñaViewModel
            {
                  IdReseña = reseña.IdReseña,
                  IdUsuario = reseña.IdUsuario,
                  IdPlatillo = reseña.IdPlatillo,
                  Calificacion = reseña.Calificacion,
                  Comentario = reseña.Comentario

            };
            return reseñaViewModel;
        }

        public ReseñaHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
        }
        public ReseñaViewModel Add(ReseñaViewModel reseña)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/Reseña", reseña);
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return reseña;
        }

        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/Reseña" + id.ToString());
            _ = _ServiceRepository.DeleteResponse("api/Reseña/" + id.ToString());
            if (responseMessage != null)
            {
                var content = responseMessage.Content;
            }
        }

        public ReseñaViewModel GetReseña(int? id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Reseña/" + id.ToString());
            ReseñaAPI reseña = new ReseñaAPI();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                reseña = JsonConvert.DeserializeObject<ReseñaAPI>(content);
            }

            ReseñaViewModel resultado = Convertir(reseña);


            return resultado;
        }

        public List<ReseñaViewModel> GetReseñas()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Reseña");
            List<ReseñaAPI> reseñas = new List<ReseñaAPI>();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                reseñas = JsonConvert.DeserializeObject<List<ReseñaAPI>>(content);
            }
            List<ReseñaViewModel> lista = new List<ReseñaViewModel>();
            foreach (var reseña in reseñas)
            {
                lista.Add(Convertir(reseña));
            }
            return lista;
        }

        public ReseñaViewModel Update(ReseñaViewModel reseña)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.PutResponse("api/Reseña/" + reseña.IdReseña.ToString(), reseña);
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                ReseñaAPI updatedReseña = JsonConvert.DeserializeObject<ReseñaAPI>(content);
                return Convertir(updatedReseña);
            }
            else
            {

                var errorMessage = responseMessage.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Error al actualizar la reseña: " + errorMessage);
                return null;
            }
        }
    }
}

using FronEnd.ApiModels;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Newtonsoft.Json;

namespace FronEnd.Helpers.Implementations
{
    public class RolHelper : IRolHelper
    {
        IServiceRepository _ServiceRepository;

        RolViewModel Convertir(RolAPI rol)
        {
            RolViewModel rolViewModel = new RolViewModel
            {
                IdRol = rol.IdRol,
                NombreRol = rol.NombreRol,
                Descripcion = rol.Descripcion

            };
            return rolViewModel;
        }

        public RolHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
        }

        public RolViewModel Add(RolViewModel rol)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/Rol", rol);
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return rol;
        }

        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/Rol" + id.ToString());
            _ = _ServiceRepository.DeleteResponse("api/Rol/" + id.ToString());
            if (responseMessage != null)
            {
                var content = responseMessage.Content;
            }
        }

        public RolViewModel GetRol(int? id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Rol/" + id.ToString());
            RolAPI rol = new RolAPI();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                rol = JsonConvert.DeserializeObject<RolAPI>(content);
            }

            RolViewModel resultado = Convertir(rol);


            return resultado;
        }

        public List<RolViewModel> GetRoles()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Rol");
            List<RolAPI> roles = new List<RolAPI>();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                roles = JsonConvert.DeserializeObject<List<RolAPI>>(content);
            }
            List<RolViewModel> lista = new List<RolViewModel>();
            foreach (var rol in roles)
            {
                lista.Add(Convertir(rol));
            }
            return lista;
        }

        public RolViewModel Update(RolViewModel rol)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.PutResponse("api/Rol/" + rol.IdRol.ToString(), rol);
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                RolAPI updatedRol = JsonConvert.DeserializeObject<RolAPI>(content);
                return Convertir(updatedRol);
            }
            else
            {

                var errorMessage = responseMessage.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Error al actualizar el rol del usuario: " + errorMessage);
                return null;
            }
        }
    }
}

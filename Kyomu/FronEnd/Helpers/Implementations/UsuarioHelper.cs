using FronEnd.ApiModels;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Newtonsoft.Json;

namespace FronEnd.Helpers.Implementations
{
    public class UsuarioHelper : IUsuarioHelper
    {
        IServiceRepository _ServiceRepository;

        UsuarioViewModel Convertir(UsuarioAPI usuario)
        {
            UsuarioViewModel usuarioViewModel = new UsuarioViewModel
            {
                    IdUsuario = usuario.IdUsuario,
                    IdRol = usuario.IdRol,
                    Nombre = usuario.Nombre,
                    Telefono = usuario.Telefono,
                    CorreoElectronico = usuario.CorreoElectronico,
                    Contraseña = usuario.Contraseña,
                    Direccion = usuario.Direccion,
                    Imagen = usuario.Imagen

            };
            return usuarioViewModel;
        }
        public UsuarioHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
        }

        public UsuarioViewModel Add(UsuarioViewModel usuario)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/Usuario", usuario);
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return usuario;
        }

        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/Usuario" + id.ToString());
            _ = _ServiceRepository.DeleteResponse("api/Usuario/" + id.ToString());
            if (responseMessage != null)
            {
                var content = responseMessage.Content;
            }
        }

        public UsuarioViewModel GetUsuario(int? id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Usuario/" + id.ToString());
            UsuarioAPI usuario = new UsuarioAPI();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                usuario = JsonConvert.DeserializeObject<UsuarioAPI>(content);
            }

            UsuarioViewModel resultado = Convertir(usuario);


            return resultado;
        }

        public List<UsuarioViewModel> GetUsuarios()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Usuario");
            List<UsuarioAPI> usuarios = new List<UsuarioAPI>();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                usuarios = JsonConvert.DeserializeObject<List<UsuarioAPI>>(content);
            }
            List<UsuarioViewModel> lista = new List<UsuarioViewModel>();
            foreach (var usuario in usuarios)
            {
                lista.Add(Convertir(usuario));
            }
            return lista;
        }

        public UsuarioViewModel Update(UsuarioViewModel usuario)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.PutResponse("api/Usuario/" + usuario.IdUsuario.ToString(), usuario);
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                UsuarioAPI updatedUsuario = JsonConvert.DeserializeObject<UsuarioAPI>(content);
                return Convertir(updatedUsuario);
            }
            else
            {

                var errorMessage = responseMessage.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Error al actualizar los datos del usuario: " + errorMessage);
                return null;
            }
        }

        public UsuarioViewModel GetUsuarioByCorreo(string correo)
        {
            HttpResponseMessage response = _ServiceRepository.GetResponse($"api/Usuario/GetUsuarioByCorreo?correo={correo}");
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var usuarioAPI = JsonConvert.DeserializeObject<UsuarioAPI>(content);
                return Convertir(usuarioAPI);
            }
            return null;
        }

        public UsuarioViewModel Login(string correo, string contraseña)
        {
            HttpResponseMessage response = _ServiceRepository.GetResponse($"api/Usuario/Login?correo={correo}&contraseña={contraseña}");
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var usuarioAPI = JsonConvert.DeserializeObject<UsuarioAPI>(content);
                return Convertir(usuarioAPI);
            }
            return null;
        }

    }
}

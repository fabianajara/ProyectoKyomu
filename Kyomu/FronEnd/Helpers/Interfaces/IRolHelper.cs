using FronEnd.Models;

namespace FronEnd.Helpers.Interfaces
{
    public interface IRolHelper
    {
        List<RolViewModel> GetRoles();

        RolViewModel GetRol(int? id);
        RolViewModel Add(RolViewModel rol);
        RolViewModel Update(RolViewModel rol);
        void Delete(int id);
    }
}

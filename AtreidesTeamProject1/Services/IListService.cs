using AtreidesTeamProject1.Models;

namespace AtreidesTeamProject1.Services
{
    public interface IListService
    {
        IEnumerable<Department> GetDepartmentList();

        IEnumerable<Item> GetItemList();

        IEnumerable<AboutUs> GetEmployeeList();

        IEnumerable<Products> GetProductsList();

        IEnumerable<UserRoles> GetRoleList();
    }
}

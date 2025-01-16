using WebApi_2.Models;

namespace WebApi_2.Repositories
{
    public static class EmployeeRepository
    {
        public static List<Employee> GetAllEmployee()
        {
            return Employees;
        }

        

    }
}

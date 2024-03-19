using OjoREGED.BO;
using System.Collections.Generic;

namespace OjoREGED.DAL.Interfaces
{
    public interface IemployeeDAL : Icrud<Employee>
    {
        int AddUser(Employee employee);
        IEnumerable<Employee> GetEmpByID(int id);
        IEnumerable<Employee> GetByID(int id);
        IEnumerable<Employee_Schedule> GetEmployee_Schedules();
        IEnumerable<Employee_Schedule> GetEmployee_SchedulesByID(int id);
        IEnumerable<Employee> GetAllEmployeeData();
        int AddAddressEmp(Employee_Location employee_Location);
        int EmpSchedule(Employee_Schedule employee_Schedule);
        int AddPickup(PickupSP pickup);
        IEnumerable<Employee_OrderPlaced> GetOrderPlacedByID(int id);
        IEnumerable<Employee> Login(string Username, string password);
        IEnumerable<Pickup> BillsByEmployeeID(int id);
    }
}

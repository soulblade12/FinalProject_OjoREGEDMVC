using OjoREGED.BLL.DTOs;
using System.Collections.Generic;

namespace OjoREGED.BLL.Interfaces
{
    public interface IemployeeBLL
    {
        IEnumerable<employeeDTO> EmployeeLogin(EmployeeLogin employee);
        int Insertemployee(CreateEmployeeDTO employee);
        IEnumerable<employeeDTO> GetAllEmployee();
        IEnumerable<EmployeeScheduleDTO> GetAllEmployeeSchedule();
        IEnumerable<employeeDTO> GetEmployeesByID(int id);
        void Update(employeeDTO employee);
        IEnumerable<employeeDTO> GetDataEmployee();
        int AddAddressEmp(EmployeeLocationCreateDTO employee_Location);
        int EmpSchedule(EmployeeCreateSchedule employee_Schedule);
        int AddPickup(EmployeeInsertPickup pickup);
        IEnumerable<Employee_OrderPlacedDTO> GetEmployee_OrderPlacedDTOs(int id);
        IEnumerable<PickupDTO> GetPickups(int id);


        void Delete(int id);
    }
}

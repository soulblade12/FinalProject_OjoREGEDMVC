using OjoREGED.BLL.DTOs;
using OjoREGED.BLL.Interfaces;
using OjoREGED.BO;
using OjoREGED.DAL;
using OjoREGED.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace OjoREGED.BLL
{
    public class EmployeeBLL : IemployeeBLL
    {
        private readonly IemployeeDAL _employeeDAL;
        public EmployeeBLL()
        {
            _employeeDAL = new EmployeeDAL();
        }

        public int AddAddressEmp(EmployeeLocationCreateDTO employee_Location)
        {
            if (string.IsNullOrEmpty(employee_Location.Location_Address))
            {
                throw new ArgumentException("Location Address is required");
            }

            if (string.IsNullOrEmpty(employee_Location.Province))
            {
                throw new ArgumentException("Province is required");
            }
            if (string.IsNullOrEmpty(employee_Location.City))
            {
                throw new ArgumentException("City is required");
            }
            if (string.IsNullOrEmpty(employee_Location.Postal_Code))
            {
                throw new ArgumentException("Postal code is required");
            }

            try
            {
                var address = new Employee_Location
                {
                    Employee_ID = employee_Location.Employee_ID,
                    City = employee_Location.City,
                    Location_Address = employee_Location.Location_Address,
                    Province = employee_Location.Province,
                    Postal_Code = employee_Location.Postal_Code,
                };
                int result = _employeeDAL.AddAddressEmp(address);
                return result;
            }
            catch (System.Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public int AddPickup(EmployeeInsertPickup pickup)
        {
            if (pickup.Employee_ID <= 0)
            {
                throw new ArgumentException("Employee ID is required");
            }

            if (pickup.Tip_Amount < 0)
            {
                throw new ArgumentException("Order ID is required");
            }
            if (pickup.Order_ID < 0)
            {
                throw new ArgumentException("Order ID is required");
            }
            if (pickup.Delivery_Charges < 0)
            {
                throw new ArgumentException("Delivery Charge is required");
            }
            if (string.IsNullOrEmpty(pickup.Delivery_Status))
            {
                throw new ArgumentException("Delivery Status is required");
            }

            try
            {
                var picksUp = new PickupSP
                {
                    Employee_ID = pickup.Employee_ID,
                    Delivery_Status = pickup.Delivery_Status,
                    Delivery_Charges = pickup.Delivery_Charges,
                    Order_ID = pickup.Order_ID,
                    Tip_Amount = pickup.Tip_Amount,
                };
                int result = _employeeDAL.AddPickup(picksUp);
                return result;
            }
            catch (System.Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Employee ID is required");
            }

            try
            {
                _employeeDAL.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<employeeDTO> EmployeeLogin(EmployeeLogin employee)
        {

            List<employeeDTO> Employees = new List<employeeDTO>();

            if (string.IsNullOrEmpty(employee.Username))
            {
                throw new ArgumentException("Username is required");
            }
            if (string.IsNullOrEmpty(employee.Password))
            {
                throw new ArgumentException("Password is required");
            }
            try
            {
                var result = _employeeDAL.Login(employee.Username, Helper.GetHash(employee.Password));
                if (result == null)
                {
                    throw new ArgumentException("Username or Password is wrong");
                }
                foreach (var reads in result)
                {
                    Employees.Add(new employeeDTO
                    {
                        Employee_ID = reads.Employee_ID,
                        First_Name = reads.First_Name,
                        Middle_Name = reads.Middle_Name,
                        Last_Name = reads.Last_Name,
                        Telephone = reads.Telephone,
                        Username = reads.Username,
                        Password = reads.Password,
                        Role_ID = reads.Role_ID,
                    });
                }

                return Employees;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }


        }

        public int EmpSchedule(EmployeeCreateSchedule employee_Schedule)
        {
            if (employee_Schedule.Employee_ID <= 0)
            {
                throw new ArgumentException("Employee ID is required");
            }

            if (employee_Schedule.Date == DateTime.MinValue)
            {
                throw new ArgumentException("Date is required");
            }
            if (employee_Schedule.Max_Order <= 0)
            {
                throw new ArgumentException("Max order limit is required");
            }

            try
            {
                var schedule = new Employee_Schedule
                {
                    Employee_ID = employee_Schedule.Employee_ID,
                    Max_Order = employee_Schedule.Max_Order,
                    Date = employee_Schedule.Date,
                };
                int result = _employeeDAL.EmpSchedule(schedule);
                return result;
            }
            catch (System.Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public IEnumerable<employeeDTO> GetAllEmployee()
        {
            List<employeeDTO> employeeDTOs = new List<employeeDTO>();
            var EmployeeFromDAL = _employeeDAL.GetAll();
            foreach (var employ in EmployeeFromDAL)
            {
                employeeDTOs.Add(new employeeDTO
                {
                    Employee_ID = employ.Employee_ID,
                    First_Name = employ.First_Name,
                    Middle_Name = employ.Middle_Name,
                    Last_Name = employ.Last_Name,
                    Telephone = employ.Telephone,
                    Username = employ.Username
                    //Password = employ.Password

                });

            }

            return employeeDTOs;
        }

        public IEnumerable<EmployeeScheduleDTO> GetAllEmployeeSchedule()
        {
            List<EmployeeScheduleDTO> employeeDTOsch = new List<EmployeeScheduleDTO>();
            var EmployeeFromDAL = _employeeDAL.GetEmployee_Schedules();
            foreach (var employ in EmployeeFromDAL)
            {
                employeeDTOsch.Add(new EmployeeScheduleDTO
                {
                    Employee_ID = employ.Employee_ID,
                    Employee_Schedule_ID = employ.Employee_Schedule_ID,
                    Date = employ.Date,
                    Status = employ.Status,
                    Order_Scheduled = employ.Order_Scheduled,
                    Max_Order = employ.Max_Order,
                });
            }
            return employeeDTOsch;
        }

        public IEnumerable<employeeDTO> GetDataEmployee()
        {
            List<employeeDTO> Employee = new List<employeeDTO>();
            var result = _employeeDAL.GetAllEmployeeData();

            foreach (var reads in result)
            {
                Employee.Add(new employeeDTO
                {
                    Username = reads.Username,
                    First_Name = reads.First_Name,
                    Middle_Name = reads.Middle_Name,
                    Last_Name = reads.Last_Name,
                    Telephone = reads.Telephone,
                    EmployeeLocations = new Employee_Location()
                    {
                        Employee_Loc_ID = reads.EmployeeLocations.Employee_Loc_ID,
                        City = reads.EmployeeLocations.City,
                        Province = reads.EmployeeLocations.Province,
                        Postal_Code = reads.EmployeeLocations.Postal_Code,
                        Location_Address = reads.EmployeeLocations.Location_Address,
                    },
                    EmployeeSchedules = new Employee_Schedule()
                    {
                        Employee_Schedule_ID = reads.EmployeeSchedules.Employee_Schedule_ID,
                        Date = reads.EmployeeSchedules.Date,
                        Max_Order = reads.EmployeeSchedules.Max_Order,
                        Order_Scheduled = reads.EmployeeSchedules.Order_Scheduled,
                        Status = reads.EmployeeSchedules.Status,
                    },

                });
            }

            return Employee;
        }

        public IEnumerable<employeeDTO> GetEmployeesByID(int id)
        {
            List<employeeDTO> employeeID = new List<employeeDTO>();
            var employeeDAL = _employeeDAL.GetEmpByID(id);
            foreach (var employ in employeeDAL)
            {
                employeeID.Add(new employeeDTO
                {
                    Employee_ID = employ.Employee_ID,
                    First_Name = employ.First_Name,
                    Middle_Name = employ.Middle_Name,
                    Last_Name = employ.Last_Name,
                    Telephone = employ.Telephone,
                    Role_ID = employ.Role_ID,
                    EmployeeLocations = new Employee_Location()
                    {
                        Employee_Loc_ID = employ.EmployeeLocations.Employee_Loc_ID,
                        City = employ.EmployeeLocations.City,
                        Province = employ.EmployeeLocations.Province,
                        Postal_Code = employ.EmployeeLocations.Postal_Code,
                        Location_Address = employ.EmployeeLocations.Location_Address,
                    },
                    EmployeeSchedules = new Employee_Schedule()
                    {
                        Employee_Schedule_ID = employ.EmployeeSchedules.Employee_Schedule_ID,
                        Date = employ.EmployeeSchedules.Date,
                        Max_Order = employ.EmployeeSchedules.Max_Order,
                        Order_Scheduled = employ.EmployeeSchedules.Order_Scheduled,
                        Status = employ.EmployeeSchedules.Status,
                    },

                });
            }
            return employeeID;
        }

        public IEnumerable<Employee_OrderPlacedDTO> GetEmployee_OrderPlacedDTOs(int id)
        {
            List<Employee_OrderPlacedDTO> employeeID = new List<Employee_OrderPlacedDTO>();
            var employeeDAL = _employeeDAL.GetOrderPlacedByID(id);
            foreach (var employ in employeeDAL)
            {
                employeeID.Add(new Employee_OrderPlacedDTO
                {
                    Employee_ID = employ.Employee_ID,
                    Employee_Schedule_ID = employ.Employee_Schedule_ID,
                    Date = employ.Date,
                    Max_Order = employ.Max_Order,
                    Order_Scheduled = employ.Order_Scheduled,
                    Status = employ.Status,
                    Status_Description = employ.Status_Description,
                    Size = employ.Size,
                    Weight = employ.Weight,
                    Order_Instruction = employ.Order_Instruction,
                    Order_Img = employ.Order_Img,
                    Customer_ID = employ.Customer_ID,
                    OrderDetail_ID = employ.OrderDetail_ID,
                    Order_ID = employ.Order_ID,
                    Time_Placed = employ.Time_Placed
                });

            }
            return employeeID;
        }

        public IEnumerable<PickupDTO> GetPickups(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Employee ID must be greater than zero.");
            }

            var pickups = _employeeDAL.BillsByEmployeeID(id);
            var pickupDTOs = new List<PickupDTO>();

            foreach (var pickup in pickups)
            {
                var pickupDTO = new PickupDTO
                {
                    Employee_ID = pickup.Employee_ID,
                    Pickup_Time = pickup.Pickup_Time,
                    Pickup_ID = pickup.Pickup_ID,
                    Delivery_Status = pickup.Delivery_Status,
                    Order_ID = pickup.Order_ID,
                    BillsIds = new Bills_ID
                    {
                        bills_ID = pickup.BillsIds.bills_ID,
                        Delivery_Charges = pickup.BillsIds.Delivery_Charges,
                        Tip_Amount = pickup.BillsIds.Tip_Amount,
                        BillDate = pickup.BillsIds.BillDate
                    }
                };

                pickupDTOs.Add(pickupDTO);
            }

            return pickupDTOs;
        }


        public int Insertemployee(CreateEmployeeDTO employee)
        {
            if (string.IsNullOrEmpty(employee.First_Name))
            {
                throw new ArgumentException("FirstName is required");
            }

            if (string.IsNullOrEmpty(employee.Last_Name))
            {
                throw new ArgumentException("Last Name is required");
            }
            if (string.IsNullOrEmpty(employee.Telephone))
            {
                throw new ArgumentException("telp is required");
            }
            if (string.IsNullOrEmpty(employee.Username))
            {
                throw new ArgumentException("telp is required");
            }
            if (employee.Password != employee.RePassword)
            {
                throw new ArgumentException("Password and Re-Password must be same");
            }

            try
            {
                var employees = new Employee
                {
                    Employee_ID = employee.Employee_ID,
                    First_Name = employee.First_Name,
                    Middle_Name = employee.Middle_Name,
                    Last_Name = employee.Last_Name,
                    Telephone = employee.Telephone,
                    Username = employee.Username,
                    Password = Helper.GetHash(employee.Password)
                };
                int result = _employeeDAL.AddUser(employees);
                return result;
            }
            catch (System.Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }


        }

        public void Update(employeeDTO employee)
        {

            try
            {
                var employees = new Employee
                {
                    Employee_ID = employee.Employee_ID,
                    First_Name = employee.First_Name,
                    Middle_Name = employee.Middle_Name,
                    Last_Name = employee.Last_Name,
                    Telephone = employee.Telephone,
                };

                if (string.IsNullOrEmpty(employee.Middle_Name))
                {
                    employees.Middle_Name = null;
                }
                _employeeDAL.Update(employees);


            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}

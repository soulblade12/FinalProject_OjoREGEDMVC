using OjoREGED.BO;
using OjoREGED.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static Dapper.SqlMapper;

namespace OjoREGED.DAL
{
    public class EmployeeDAL : IemployeeDAL
    {
        private T GetValueOrDefault<T>(object value, T defaultValue = default)
        {
            return value != DBNull.Value ? (T)value : defaultValue;
        }

        private string GetConnString()
        {
            return Helper.GetConnectionString();
        }
        public int AddUser(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strsp = "dbo.InsertEmployee";
                var param = new
                {
                    FirstName = employee.First_Name,
                    MiddleName = employee.Middle_Name,
                    LastName = employee.Last_Name,
                    Telephone = employee.Telephone,
                    Username = employee.Username,
                    Password = employee.Password
                };
                try
                {
                    int result = Convert.ToInt32(conn.Execute(strsp, param, commandType: System.Data.CommandType.StoredProcedure));
                    if (result != 1)
                    {
                        throw new ArgumentException("Insert data failed..");
                    }
                    return result;
                }
                catch (SqlException sqlEx)
                {
                    throw new ArgumentException($"{sqlEx.InnerException.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }


            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strSP = @"dbo.DeleteEmployee";
                var param = new { ID = id };

                try
                {
                    int result = conn.Execute(strSP, param);
                    if (result != 1)
                    {
                        throw new Exception("Data tidak berhasil dihapus");
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new ArgumentException($"{sqlEx.InnerException.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Kesalahan: " + ex.Message);
                }


            }
        }

        public IEnumerable<Employee> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strsql = @"SELECT Employee_ID,First_Name,Middle_Name,Last_Name,Telephone,Username,Role_ID,Image FROM Employee";

                List<Employee> Employee = new List<Employee>();
                SqlCommand cmd = new SqlCommand(strsql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var employee = new Employee()
                        {
                            Employee_ID = Convert.ToInt32(dr["Employee_ID"]),
                            First_Name = dr["First_Name"].ToString(),
                            Middle_Name = dr["Middle_Name"].ToString(),
                            Last_Name = dr["Last_Name"].ToString(),
                            Telephone = dr["Telephone"].ToString(),
                            Username = dr["Username"].ToString(),
                            Role_ID = Convert.ToInt32(dr["Role_ID"]),
                            Image = dr["Image"].ToString()
                        };
                        Employee.Add(employee);
                    }
                }
                return Employee;
            }




        }



        public Employee GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(Employee entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Employee entity)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strSp = @"UPDATE Employee 
                         SET First_Name = @First_Name, 
                             Middle_Name = @Middle_Name, 
                             Last_Name = @Last_Name, 
                             Telephone = @Telephone 
                         WHERE Employee_ID = @Employee_ID";

                var param = new
                {
                    Employee_ID = entity.Employee_ID,
                    First_Name = entity.First_Name,
                    Middle_Name = entity.Middle_Name,
                    Last_Name = entity.Last_Name,
                    Telephone = entity.Telephone,


                };
                try
                {
                    int result = conn.Execute(strSp, param, commandType: System.Data.CommandType.Text);
                    if (result != 1)
                    {
                        throw new ArgumentException("Update data failed..");
                    }
                    //return result;

                }
                catch (SqlException sqlEx)
                {
                    throw new ArgumentException($"{sqlEx.InnerException.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }


            }
        }





        public IEnumerable<Employee> GetByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strSql = @"dbo.ReadEmployee";
                //var param = new { EmployeeID = id };
                //var result = conn.Query<Employee>(strSql, param, commandType: System.Data.CommandType.StoredProcedure);
                //return result;

                List<Employee> Employee = new List<Employee>();
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var employee = new Employee()
                        {
                            Employee_ID = Convert.ToInt32(dr["Employee_ID"]),
                            First_Name = dr["First_Name"].ToString(),
                            Middle_Name = dr["Middle_Name"].ToString(),
                            Last_Name = dr["Last_Name"].ToString(),
                            Telephone = dr["Telephone"].ToString(),
                            Username = dr["Username"].ToString(),
                            Password = dr["Password"].ToString(),
                            Role_ID = Convert.ToInt32(dr["Role_ID"]),
                        };
                        Employee.Add(employee);
                    }
                }
                return Employee;
            }
        }

        public IEnumerable<Employee_Schedule> GetEmployee_Schedules()
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strSql = @"select * from Employee_Schedule";

                List<Employee_Schedule> EmployeeSchedule = new List<Employee_Schedule>();
                SqlCommand cmd = new SqlCommand(strSql, conn);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var employee = new Employee_Schedule()
                        {
                            Employee_ID = Convert.ToInt32(dr["Employee_ID"]),
                            Employee_Schedule_ID = Convert.ToInt32(dr["Employee_Schedule_ID"]),
                            Date = Convert.ToDateTime(dr["Date"]),
                            Max_Order = Convert.ToInt32(dr["Max_Order"]),
                            Order_Scheduled = Convert.ToInt32(dr["Order_Scheduled"]),
                            Status = dr["Status"].ToString(),
                        };
                        EmployeeSchedule.Add(employee);
                    }
                }
                return EmployeeSchedule;
            }
        }

        public IEnumerable<Employee> GetAllEmployeeData()
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strSql = @"SELECT 
    emp.Employee_ID , 
    emp.First_Name, 
    emp.Middle_Name, 
    emp.Last_Name, 
    emp.Telephone, 
    emp.Role_ID,
    emp_sched.Employee_Schedule_ID, 
    emp_sched.Status, 
    emp_sched.Date, 
    emp_sched.Max_Order, 
    emp_sched.Order_Scheduled, 
    emp_loc.Employee_Loc_ID, 
    emp_loc.Province, 
    emp_loc.City, 
    emp_loc.Location_Address, 
    emp_loc.Postal_Code
FROM     
    dbo.Employee AS emp 
    LEFT OUTER JOIN dbo.Employee_Schedule AS emp_sched ON emp.Employee_ID = emp_sched.Employee_ID 
    INNER JOIN dbo.Employee_Location AS emp_loc ON emp.Employee_ID = emp_loc.Employee_ID";

                List<Employee> Employee = new List<Employee>();
                SqlCommand cmd = new SqlCommand(strSql, conn);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var employes = new Employee()
                        {
                            Employee_ID = Convert.ToInt32(dr["Employee_ID"]),
                            First_Name = dr["First_Name"].ToString(),
                            Middle_Name = dr["Middle_Name"].ToString(),
                            Last_Name = dr["Last_Name"].ToString(),
                            Telephone = dr["Telephone"].ToString(),
                            Role_ID = Convert.ToInt32(dr["Role_ID"]),
                            EmployeeLocations = new Employee_Location()
                            {
                                Employee_Loc_ID = Convert.ToInt32(dr["Employee_Loc_ID"]),
                                City = dr["City"].ToString(),
                                Province = dr["Province"].ToString(),
                                Postal_Code = dr["Postal_Code"].ToString(),
                                Location_Address = dr["Postal_Code"].ToString(),
                            },
                            EmployeeSchedules = new Employee_Schedule()
                            {
                                Employee_Schedule_ID = Convert.ToInt32(dr["Employee_Schedule_ID"]),
                                Date = Convert.ToDateTime(dr["Date"]),
                                Max_Order = Convert.ToInt32(dr["Max_Order"]),
                                Order_Scheduled = Convert.ToInt32(dr["Order_Scheduled"]),
                                Status = dr["Status"].ToString(),
                            }

                        };

                        Employee.Add(employes);
                    }
                }
                return Employee;
            }
        }

        public IEnumerable<Employee> Login(string Username, string password)
        {

            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strSql = @"SELECT * FROM Employee WHERE Username = @Username and Password = @Password";

                List<Employee> Employes = new List<Employee>();
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@Password", password);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var employee = new Employee()
                        {
                            Employee_ID = Convert.ToInt32(dr["Employee_ID"]),
                            First_Name = dr["First_Name"].ToString(),
                            Middle_Name = dr["Middle_Name"].ToString(),
                            Last_Name = dr["Last_Name"].ToString(),
                            Telephone = dr["Telephone"].ToString(),
                            Username = dr["Username"].ToString(),
                            Password = dr["Password"].ToString(),
                            Role_ID = Convert.ToInt32(dr["Role_ID"])
                        };
                        Employes.Add(employee);
                    }
                }
                return Employes;
            }

        }

        public IEnumerable<Employee> GetEmpByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strSql = @"SELECT emp.Employee_ID ,emp.Role_ID, emp.First_Name, emp.Middle_Name, emp.Last_Name, emp.Telephone, emp_sched.Employee_Schedule_ID, emp_sched.Status, emp_sched.Date, emp_sched.Max_Order, emp_sched.Order_Scheduled, emp_loc.Employee_Loc_ID,     emp_loc.Province, emp_loc.City, emp_loc.Location_Address, emp_loc.Postal_Code
FROM     
    dbo.Employee AS emp 
    LEFT OUTER JOIN dbo.Employee_Schedule AS emp_sched ON emp.Employee_ID = emp_sched.Employee_ID 
    INNER JOIN dbo.Employee_Location AS emp_loc ON emp.Employee_ID = emp_loc.Employee_ID WHERE 
    emp.Employee_ID = @ID;";

                List<Employee> employees = new List<Employee>();
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var employes = new Employee()
                        {
                            Employee_ID = Convert.ToInt32(dr["Employee_ID"]),
                            First_Name = dr["First_Name"].ToString(),
                            Middle_Name = dr["Middle_Name"].ToString(),
                            Last_Name = dr["Last_Name"].ToString(),
                            Telephone = dr["Telephone"].ToString(),
                            Role_ID = Convert.ToInt32(dr["Role_ID"]),
                            EmployeeLocations = new Employee_Location()
                            {
                                Employee_Loc_ID = Convert.ToInt32(dr["Employee_Loc_ID"]),
                                City = dr["City"].ToString(),
                                Province = dr["Province"].ToString(),
                                Postal_Code = dr["Postal_Code"].ToString(),
                                Location_Address = dr["Postal_Code"].ToString(),
                            },
                            EmployeeSchedules = new Employee_Schedule()
                            {
                                Employee_Schedule_ID = GetValueOrDefault(dr["Employee_Schedule_ID"], 0),
                                Date = GetValueOrDefault(dr["Date"], DateTime.MinValue),
                                Max_Order = GetValueOrDefault(dr["Max_Order"], 0),
                                Order_Scheduled = GetValueOrDefault(dr["Order_Scheduled"], 0),
                                Status = dr["Status"].ToString(),
                            }

                        };

                        employees.Add(employes);
                    }
                }
                return employees;
            }
        }

        public IEnumerable<Employee_Schedule> GetEmployee_SchedulesByID(int id)
        {
            throw new NotImplementedException();
        }

        public int AddAddressEmp(Employee_Location employee_Location)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strsp = "dbo.InsertLocationEmployee";
                var param = new
                {
                    City = employee_Location.City,
                    LocationAddress = employee_Location.Location_Address,
                    Province = employee_Location.Province,
                    Postalcode = employee_Location.Postal_Code,
                    EmployeeID = employee_Location.Employee_ID,
                };
                try
                {
                    int result = Convert.ToInt32(conn.Execute(strsp, param, commandType: System.Data.CommandType.StoredProcedure));
                    if (result != 1)
                    {
                        throw new ArgumentException("Insert data failed..");
                    }
                    return result;
                }
                catch (SqlException sqlEx)
                {
                    throw new ArgumentException($"{sqlEx.InnerException.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }


            }
        }

        public int EmpSchedule(Employee_Schedule employee_Schedule)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strsp = "dbo.CreateEmployeeSchedule";
                var param = new
                {
                    EmployeeID = employee_Schedule.Employee_ID,
                    Date = employee_Schedule.Date,
                    MaxOrder = employee_Schedule.Max_Order,
                };
                try
                {
                    int result = Convert.ToInt32(conn.Execute(strsp, param, commandType: System.Data.CommandType.StoredProcedure));
                    if (result != 1)
                    {
                        throw new ArgumentException("Insert data failed..");
                    }
                    return result;
                }
                catch (SqlException sqlEx)
                {
                    throw new ArgumentException($"{sqlEx.InnerException.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }


            }
        }

        public int AddPickup(PickupSP pickup)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strsp = "dbo.MakePickupTransaction";
                var param = new
                {
                    PickedBy = pickup.Employee_ID,
                    DeliveryStatus = pickup.Delivery_Status,
                    DeliveryCharges = pickup.Delivery_Charges,
                    TipAmount = pickup.Tip_Amount,
                    OrderID = pickup.Order_ID,
                };
                try
                {
                    int result = Convert.ToInt32(conn.Execute(strsp, param, commandType: System.Data.CommandType.StoredProcedure));
                    return result;
                }
                catch (SqlException sqlEx)
                {
                    throw new ArgumentException($"{sqlEx.InnerException.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }


            }
        }

        public IEnumerable<Employee_OrderPlaced> GetOrderPlacedByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strSql = @"SELECT 
    OS.Status_Description, 
    OD.Weight, 
    OD.Size, 
    OD.Order_Instruction, 
    OD.Order_Img, 
    OP.Order_ID, 
    OP.Time_Placed, 
    OP.Customer_ID, 
    OP.Employee_Schedule_ID, 
    OD.OrderDetail_ID, 
    ES.Employee_ID,
    ES.Date,
    ES.Max_Order,
    ES.Status,
    ES.Order_Scheduled
FROM     
    dbo.Order_Placed AS OP
    INNER JOIN dbo.Employee_Schedule AS ES ON OP.Employee_Schedule_ID = ES.Employee_Schedule_ID 
    INNER JOIN dbo.Order_Detail AS OD ON OP.Order_ID = OD.Order_ID 
    INNER JOIN dbo.Order_Status AS OS ON OD.Order_Status = OS.Order_Status
WHERE    
    ES.Employee_ID = @EmployeeID;
";

                List<Employee_OrderPlaced> Emp_Order = new List<Employee_OrderPlaced>();
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var employes = new Employee_OrderPlaced()
                        {
                            Employee_ID = Convert.ToInt32(dr["Employee_ID"]),
                            Employee_Schedule_ID = GetValueOrDefault(dr["Employee_Schedule_ID"], 0),
                            Date = GetValueOrDefault(dr["Date"], DateTime.MinValue),
                            Max_Order = GetValueOrDefault(dr["Max_Order"], 0),
                            Order_Scheduled = GetValueOrDefault(dr["Order_Scheduled"], 0),
                            Status = dr["Status"].ToString(),
                            Customer_ID = Convert.ToInt32(dr["Customer_ID"]),
                            Order_ID = Convert.ToInt32(dr["Order_ID"]),
                            Time_Placed = GetValueOrDefault(dr["Time_Placed"], DateTime.MinValue),
                            OrderDetail_ID = Convert.ToInt32(dr["OrderDetail_ID"]),
                            Order_Img = dr["Order_Img"].ToString(),
                            Order_Instruction = dr["Order_Instruction"].ToString(),
                            Weight = Convert.ToInt32(dr["Weight"]),
                            Size = dr["Size"].ToString(),
                            Status_Description = dr["Status_Description"].ToString()

                        };

                        Emp_Order.Add(employes);
                    }
                }
                return Emp_Order;
            }
        }

        public IEnumerable<Pickup> BillsByEmployeeID(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strSql = @"SELECT b.Bills_ID, b.Delivery_Charges, b.Tip_Amount, b.BillDate, 
       p.Employee_ID, p.Pickup_ID, p.Pickup_Time, 
       p.Delivery_Status, p.Order_ID
FROM dbo.Bills_ID AS b
INNER JOIN dbo.Pickup AS p ON b.Pickup_ID = p.Pickup_ID
WHERE p.Employee_ID = @EmployeeID;
";

                List<Pickup> PickupEmp = new List<Pickup>();
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@EmployeeID", id);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var pickup = new Pickup()
                        {
                            Employee_ID = Convert.ToInt32(dr["Employee_ID"]),
                            Pickup_Time = Convert.ToDateTime(dr["Pickup_Time"]),
                            Pickup_ID = Convert.ToInt32(dr["Pickup_ID"]),
                            Delivery_Status = dr["Delivery_Status"].ToString(),
                            Order_ID = Convert.ToInt32(dr["Order_ID"]),
                            BillsIds = new Bills_ID()
                            {
                                bills_ID = Convert.ToInt32(dr["bills_ID"]),
                                Delivery_Charges = Convert.ToDecimal(dr["Delivery_Charges"]),
                                Tip_Amount = Convert.ToDecimal(dr["Tip_Amount"]),
                                BillDate = Convert.ToDateTime(dr["BillDate"]),
                            },

                        };

                        PickupEmp.Add(pickup);
                    }
                }
                return PickupEmp;
            }
        }
    }
}






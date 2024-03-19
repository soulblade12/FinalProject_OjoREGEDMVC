using Dapper;
using OjoREGED.BO;
using OjoREGED.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OjoREGED.DAL
{
    public class CustomerDAL : Icustomer
    {
        private string GetConnString()
        {
            return Helper.GetConnectionString();
        }
        public int addCustomer(Customer datacustomer)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strsp = "dbo.InsertCustomer";
                var param = new
                {
                    FirstName = datacustomer.First_Name,
                    MiddleName = datacustomer.Middle_Name,
                    LastName = datacustomer.Last_Name,
                    Telephone = datacustomer.Telephone,
                    EmailAddress = datacustomer.Email_Address,
                    Username = datacustomer.Username,
                    Password = datacustomer.Password
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
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetcustomerByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strSql = @"SELECT C.Customer_ID, C.First_Name, C.Middle_Name, C.Last_Name, C.Telephone, C.Subscription_ID, C.Email_Address, C.Username, C.Image, C.Role_ID,
                  C.Password, Adr.Address_ID, Adr.Province, Adr.City, Adr.Street_Address, Adr.Postal_Code, adr.Modified_Date, Sl.Name, Sl.Price
                  FROM Customer as C LEFT OUTER JOIN
                  Address as adr ON C.Customer_ID = Adr.Customer_ID LEFT OUTER JOIN
                  Subcriptions_Level as Sl ON C.Subscription_ID = Sl.Subscription_ID LEFT OUTER JOIN
				  Role as Rl ON C.Role_ID = Rl.Role_ID WHERE C.Customer_ID = @ID";

                List<Customer> Customer = new List<Customer>();
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var customer = new Customer()
                        {
                            Customer_ID = Convert.ToInt32(dr["Customer_ID"]),
                            Subscription_ID = Convert.ToInt32(dr["Subscription_ID"]),
                            First_Name = dr["First_Name"].ToString(),
                            Middle_Name = dr["Middle_Name"].ToString(),
                            Last_Name = dr["Last_Name"].ToString(),
                            Telephone = dr["Telephone"].ToString(),
                            Username = dr["Username"].ToString(),
                            Password = dr["Password"].ToString(),
                            Email_Address = dr["Email_Address"].ToString(),
                            Image = dr["Image"].ToString(),
                            SubcriptionsLevel = new Subcriptions_Level()
                            {
                                Subscription_ID = Convert.ToInt32(dr["Subscription_ID"]),
                                Name = dr["Name"].ToString(),
                                Price = Convert.ToDecimal(dr["Price"]),
                            },
                            Addresses = new Address()
                            {
                                Address_ID = dr["Address_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Address_ID"]),
                                City = dr["City"] == DBNull.Value ? null : dr["City"].ToString(),
                                Province = dr["Province"] == DBNull.Value ? null : dr["Province"].ToString(),
                                Postal_Code = dr["Postal_Code"] == DBNull.Value ? null : dr["Postal_Code"].ToString(),
                                Street_Address = dr["Street_Address"] == DBNull.Value ? null : dr["Street_Address"].ToString(),
                                Customer_ID = dr["Customer_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Customer_ID"]),
                            },


                        };

                        Customer.Add(customer);
                    }
                }
                return Customer;
            }
        }

        public void Insert(Customer entity)
        {

        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Login(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strSql = @"SELECT * FROM Customer WHERE Username = @Username and Password = @Password";

                List<Customer> Customer = new List<Customer>();
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var customer = new Customer()
                        {
                            Customer_ID = Convert.ToInt32(dr["Customer_ID"]),
                            Subscription_ID = Convert.ToInt32(dr["Subscription_ID"]),
                            First_Name = dr["First_Name"].ToString(),
                            Middle_Name = dr["Middle_Name"].ToString(),
                            Last_Name = dr["Last_Name"].ToString(),
                            Telephone = dr["Telephone"].ToString(),
                            Username = dr["Username"].ToString(),
                            Password = dr["Password"].ToString(),
                            Email_Address = dr["Email_Address"].ToString(),
                            Role_ID = Convert.ToInt32(dr["Role_ID"]),
                            Image = dr["Image"].ToString(),
                        };
                        Customer.Add(customer);
                    }
                }
                return Customer;
            }
        }

        public int AddAddress(Address addresscustomer)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strsp = "dbo.InsertAddress";
                var param = new
                {
                    City = addresscustomer.City,
                    StreetAddress = addresscustomer.Street_Address,
                    Province = addresscustomer.Province,
                    Postalcode = addresscustomer.Postal_Code,
                    CustomerID = addresscustomer.Customer_ID,
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

        public void AddBooking(OrderPlacedSP orderplaced)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnString()))
                {
                    var strSP = "dbo.BookPickUp";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(strSP, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerID", orderplaced.Customer_ID);
                    cmd.Parameters.AddWithValue("@Weight", orderplaced.Weight);
                    cmd.Parameters.AddWithValue("@Size", orderplaced.Size);
                    cmd.Parameters.AddWithValue("@Order_Instruction", orderplaced.Order_Instruction);
                    cmd.Parameters.AddWithValue("@Employee_Schedule", orderplaced.Employee_Schedule_ID);
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                // Handle the exception according to your requirements
                throw new Exception("An error occurred while adding order placed.", ex);
            }
        }

        public IEnumerable<Order_Placed> GetOrderByCustomer(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strSql = @"SELECT op.Order_ID,op.Customer_ID, op.Time_Placed, op.Employee_Schedule_ID, od.OrderDetail_ID,od.Weight, od.Size, od.Order_Instruction, od.Order_Status, od.Order_Img
                FROM dbo.Order_Placed AS op INNER JOIN dbo.Order_Detail AS od ON op.Order_ID = od.Order_ID where op.Customer_ID = @ID";

                List<Order_Placed> Orderplace = new List<Order_Placed>();
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var order_place = new Order_Placed()
                        {
                            Customer_ID = Convert.ToInt32(dr["Customer_ID"]),
                            Order_ID = Convert.ToInt32(dr["Order_ID"]),
                            Time_Placed = Convert.ToDateTime(dr["Time_Placed"]),
                            Employee_Schedule_ID = Convert.ToInt32(dr["Employee_Schedule_ID"]),
                            OrderDetails = new Order_Detail()
                            {
                                OrderDetail_ID = Convert.ToInt32(dr["OrderDetail_ID"]),
                                Weight = Convert.ToInt32(dr["Weight"]),
                                Size = dr["Size"].ToString(),
                                Order_Instruction = dr["Order_Instruction"].ToString(),
                                Order_Status = Convert.ToInt32(dr["Order_Status"]),
                                Order_Img = dr["Order_Img"].ToString(),
                            }

                        };

                        Orderplace.Add(order_place);
                    }
                }
                return Orderplace;
            }
        }

        public IEnumerable<Address> GetAddressByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strSql = @"select * from Address where Customer_ID = @ID";

                List<Address> addresses = new List<Address>();
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var address = new Address()
                        {
                            Address_ID = dr["Address_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Address_ID"]),
                            City = dr["City"] == DBNull.Value ? null : dr["City"].ToString(),
                            Province = dr["Province"] == DBNull.Value ? null : dr["Province"].ToString(),
                            Postal_Code = dr["Postal_Code"] == DBNull.Value ? null : dr["Postal_Code"].ToString(),
                            Street_Address = dr["Street_Address"] == DBNull.Value ? null : dr["Street_Address"].ToString(),
                            Customer_ID = dr["Customer_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Customer_ID"]),
                        };


                        addresses.Add(address);
                    }
                }
                return addresses;
            }
        }
    }
}

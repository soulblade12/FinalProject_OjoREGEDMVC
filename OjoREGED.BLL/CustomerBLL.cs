using OjoREGED.BLL.DTOs;
using OjoREGED.BLL.Interfaces;
using OjoREGED.BO;
using OjoREGED.DAL;
using OjoREGED.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace OjoREGED.BLL
{
    public class CustomerBLL : ICustomerBLL
    {
        private readonly Icustomer _customer;
        public CustomerBLL()
        {
            _customer = new CustomerDAL();
        }

        public int AddAddress(AddressesDTO addresscustomer)
        {
            if (string.IsNullOrEmpty(addresscustomer.Street_Address))
            {
                throw new ArgumentException("Street Address is required");
            }

            if (string.IsNullOrEmpty(addresscustomer.Province))
            {
                throw new ArgumentException("Province is required");
            }
            if (string.IsNullOrEmpty(addresscustomer.City))
            {
                throw new ArgumentException("City is required");
            }
            if (string.IsNullOrEmpty(addresscustomer.Postal_Code))
            {
                throw new ArgumentException("Postal code is required");
            }

            try
            {
                var address = new Address
                {
                    Customer_ID = addresscustomer.Customer_ID,
                    City = addresscustomer.City,
                    Street_Address = addresscustomer.Street_Address,
                    Province = addresscustomer.Province,
                    Postal_Code = addresscustomer.Postal_Code,
                };
                int result = _customer.AddAddress(address);
                return result;
            }
            catch (System.Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public void AddBooking(InsertBookingSP insertbook)
        {
            if (string.IsNullOrEmpty(insertbook.Order_Instruction))
            {
                throw new ArgumentException("order instruction is required");
            }
            if (string.IsNullOrEmpty(insertbook.Size))
            {
                throw new ArgumentException("Size is required");
            }
            if (insertbook.Customer_ID <= 0)
            {
                throw new ArgumentException("Customer ID is required");
            }
            if (insertbook.Employee_Schedule_ID <= 0)
            {
                throw new ArgumentException("Customer ID is required");
            }
            if (insertbook.Weight <= 0)
            {
                throw new ArgumentException("Customer ID is required");
            }
            try
            {
                var newBook = new OrderPlacedSP
                {
                    Customer_ID = insertbook.Customer_ID,
                    Order_Instruction = insertbook.Order_Instruction,
                    Employee_Schedule_ID = insertbook.Employee_Schedule_ID,
                    Size = insertbook.Size,
                    Weight = insertbook.Weight,
                };
                _customer.AddBooking(newBook);

            }
            catch (System.Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public IEnumerable<AddressesDTO> AddressGetByID(int id)
        {
            List<AddressesDTO> addresses = new List<AddressesDTO>();
            var result = _customer.GetAddressByID(id);

            foreach (var reads in result)
            {
                addresses.Add(new AddressesDTO
                {
                    City = reads.City,
                    Province = reads.Province,
                    Postal_Code = reads.Postal_Code,
                    Street_Address = reads.Street_Address,
                    Customer_ID = Convert.ToInt32(reads.Customer_ID),
                });
            }

            return addresses;
        }

        public IEnumerable<OrderPlacedDTO> custGetOrderByCustomeromergetbyid(int id)
        {
            List<OrderPlacedDTO> order_placed = new List<OrderPlacedDTO>();
            var result = _customer.GetOrderByCustomer(id);

            foreach (var reads in result)
            {
                order_placed.Add(new OrderPlacedDTO
                {
                    Customer_ID = reads.Customer_ID,
                    Order_ID = reads.Order_ID,
                    Time_Placed = reads.Time_Placed,
                    Employee_Schedule_ID = reads.Employee_Schedule_ID,
                    OrderDetails = new Order_Detail()
                    {
                        OrderDetail_ID = reads.OrderDetails.OrderDetail_ID,
                        Weight = reads.OrderDetails.OrderDetail_ID,
                        Size = reads.OrderDetails.Size,
                        Order_Instruction = reads.OrderDetails.Order_Instruction,
                        Order_Status = Convert.ToInt32(reads.OrderDetails.Order_Status),
                        Order_Img = reads.OrderDetails.Order_Img,
                    },
                });
            }

            return order_placed;
        }

        public IEnumerable<CustomerDTO> CustomerGetByID(int id)
        {
            List<CustomerDTO> Customers = new List<CustomerDTO>();
            var result = _customer.GetcustomerByID(id);

            foreach (var reads in result)
            {
                Customers.Add(new CustomerDTO
                {
                    Username = reads.Username,
                    First_Name = reads.First_Name,
                    Middle_Name = reads.Middle_Name,
                    Last_Name = reads.Last_Name,
                    Email_Address = reads.Email_Address,
                    Telephone = reads.Telephone,
                    Subscription_ID = reads.Subscription_ID,
                    Role_ID = reads.Role_ID,
                    AddressesDTO = new AddressesDTO
                    {
                        City = reads.Addresses.City,
                        Province = reads.Addresses.Province,
                        Postal_Code = reads.Addresses.Postal_Code,
                        Street_Address = reads.Addresses.Street_Address,
                        Customer_ID = Convert.ToInt32(reads.Addresses.Customer_ID),
                    },
                    SubcriptionsLevel = new SubscriptionLevelDTO
                    {
                        Subscription_ID = reads.SubcriptionsLevel.Subscription_ID,
                        Name = reads.SubcriptionsLevel.Name,
                        Price = reads.SubcriptionsLevel.Price,
                    }
                });
            }

            return Customers;
        }




        public IEnumerable<CustomerLoginDTO> CustomerLogin(CustomerLogin customerLogin)
        {
            List<CustomerLoginDTO> Customers = new List<CustomerLoginDTO>();

            if (string.IsNullOrEmpty(customerLogin.Username))
            {
                throw new ArgumentException("Username is required");
            }
            if (string.IsNullOrEmpty(customerLogin.Password))
            {
                throw new ArgumentException("Password is required");
            }
            try
            {
                var result = _customer.Login(customerLogin.Username, Helper.GetHash(customerLogin.Password));
                if (result == null)
                {
                    throw new ArgumentException("Username or Password is wrong");
                }
                foreach (var reads in result)
                {
                    Customers.Add(new CustomerLoginDTO
                    {
                        Customer_ID = reads.Customer_ID,
                        Username = reads.Username,
                        First_Name = reads.First_Name,
                        Middle_Name = reads.Middle_Name,
                        Last_Name = reads.Last_Name,
                        Email_Address = reads.Email_Address,
                        Telephone = reads.Telephone,
                        Subscription_ID = reads.Subscription_ID,
                        Role_ID = reads.Role_ID,
                    });
                }

                return Customers;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }

        public int InsertCustomer(CustomerCreateDTO Customer)
        {
            if (string.IsNullOrEmpty(Customer.First_Name))
            {
                throw new ArgumentException("FirstName is required");
            }

            if (string.IsNullOrEmpty(Customer.Last_Name))
            {
                throw new ArgumentException("Last Name is required");
            }
            if (string.IsNullOrEmpty(Customer.Telephone))
            {
                throw new ArgumentException("telp is required");
            }
            if (string.IsNullOrEmpty(Customer.Username))
            {
                throw new ArgumentException("telp is required");
            }
            if (Customer.Password != Customer.RePassword)
            {
                throw new ArgumentException("Password and Re-Password must be same");
            }

            try
            {
                var customers = new Customer
                {
                    First_Name = Customer.First_Name,
                    Middle_Name = Customer.Middle_Name,
                    Last_Name = Customer.Last_Name,
                    Telephone = Customer.Telephone,
                    Username = Customer.Username,
                    Password = Helper.GetHash(Customer.Password),
                    Email_Address = Customer.Email_Address
                };
                int result = _customer.addCustomer(customers);
                return result;
            }
            catch (System.Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }

}

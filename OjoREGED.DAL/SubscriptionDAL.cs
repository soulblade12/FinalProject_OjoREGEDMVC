using Dapper;
using OjoREGED.BO;
using OjoREGED.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace OjoREGED.DAL
{
    public class SubscriptionDAL : Isubscription
    {

        private string GetConnString()
        {
            return Helper.GetConnectionString();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Subcriptions_Level> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var sqlstr = @"select * from Subcriptions_Level";
                var results = conn.Query<Subcriptions_Level>(sqlstr);
                return results;
            };
        }

        public Subcriptions_Level GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Subcriptions_Level entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Subcriptions_Level entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateSubscription(int subcriptions_level, int Customer_ID)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                var strsp = "dbo.UpdateCustomerSubscription";
                var param = new
                {
                    NewSubscriptionID = subcriptions_level,
                    CustomerID = Customer_ID,
                };
                try
                {
                    int result = Convert.ToInt32(conn.Execute(strsp, param, commandType: System.Data.CommandType.StoredProcedure));
                    //if (result != 1)
                    //{
                    //    throw new ArgumentException("Update Subscription data failed..");
                    //}

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
    }
}

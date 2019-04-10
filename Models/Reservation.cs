using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;

namespace RentaCar.Models
{
    public class Reservation
    {
        protected static string dbConnect = (WebConfigurationManager.ConnectionStrings["RentaCar"]).ConnectionString;
        private List<Cars> TypeList = new List<Cars>();
        private List<Cars> GetCar = new List<Cars>();
        private List<Cars> CarMileage = new List<Cars>();

        public bool CreateReservation(Customer CustomerInput)
        {

            try
            {
                
                using (SqlConnection con = new SqlConnection(dbConnect))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(
                            "INSERT INTO Customer (customerfirstname, customerlastname, customeremail, customerpickup,  customerdropoff,cartorentId, cartype, carfee, mileage) VALUES (@CustomerFirstName, @CustomerLastName,@CustomerEmail, @CustomerPickUp, @CustomerDropOff,@CarToRentId, @CarType, @CarFee, @Mileage)", con))
                    {

                        command.Parameters.AddWithValue("@CustomerFirstName", CustomerInput.CustomerFirstName);
                        command.Parameters.AddWithValue("@CustomerLastName", CustomerInput.CustomerLastName);
                        command.Parameters.AddWithValue("@CustomerEmail", CustomerInput.CustomerEmail);
                        command.Parameters.AddWithValue("@CustomerPickUp", CustomerInput.CustomerPickUp);
                        command.Parameters.AddWithValue("@CustomerDropOff", CustomerInput.CarToRentId);
                        command.Parameters.AddWithValue("@CarToRentId", CustomerInput.CustomerDropOff);
                        command.Parameters.AddWithValue("@CarType", CustomerInput.CarType);
                        command.Parameters.AddWithValue("@CarFee", CustomerInput.CarFee);
                        command.Parameters.AddWithValue("@Mileage", CustomerInput.Mileage);

                        command.ExecuteNonQuery();


                    }

                    con.Close();

                }
                return true;
            }
            catch (SqlException)
            {
                
                return false;
            }
        }

        public List<Cars> GetAvailablePackage()
        {

            using (SqlConnection con = new SqlConnection(dbConnect))
            {
                using (SqlCommand command = new SqlCommand(
                        "Select carid, carname, carpackage, carmileage, onrent,carconformation From Cars;", con))
                {
                    //command.ExecuteNonQuery();
                    con.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = (string)reader["carid"];
                            string name = (string)reader["carname"];
                            string package = (string)reader["carpackage"];
                            string mileage = (string)reader["carmileage"];
                            bool rent = Convert.ToBoolean(reader["onrent"]);
                            string conformation = (string)reader["carconformation"];


                            Cars AllCars = new Cars
                            {
                                CarId = id,
                                CarName = name,
                                CarPackage = package,
                                CarMileage = mileage,
                                OnRent = rent,
                                CarConformation = conformation


                            };
                            TypeList.Add(AllCars);
                        }
                    }
                }

                con.Close();

                return TypeList;




            }
        }

        public bool UpdateCarsAvailable(string selectedcar)
        {
           
            
            try
            {

                using (SqlConnection con = new SqlConnection(dbConnect))
                {
                   
                    using (SqlCommand command = new SqlCommand("UPDATE Cars SET onrent = 1 WHERE carid = @selectedcar ;", con))
                    {
                        con.Open();
                        command.Parameters.AddWithValue("@selectedcar", selectedcar);
                        command.ExecuteNonQuery();
                        
                    }
                  
                    con.Close();

                }
                return true;

            }
            
            catch (SqlException)
            {

                return false;
            }

        }


        public bool ReturnCar( string conformation)
        {
            try
            {

                using (SqlConnection con = new SqlConnection(dbConnect))
                {

                    using (SqlCommand command = new SqlCommand(
                            " UPDATE Cars SET onrent = False WHERE carconformation = @conformation ; ", con))
                    {

                        con.Open();

                        command.Parameters.AddWithValue("@conformation", conformation);
                        

                        command.ExecuteNonQuery();


                    }

                    con.Close();


                    return true;

                }
            }


            catch (SqlException)
            {

                return false;
            }

        }


        public bool UpdateMileage(string usermiles, string carid)
        {

            try
            {

                using (SqlConnection con = new SqlConnection(dbConnect))
                {

                    using (SqlCommand command = new SqlCommand(
                            " UPDATE Cars SET carmileage = '@usermiles' WHERE carid = '@carid'; ", con))
                    {

                        con.Open();

                        command.Parameters.AddWithValue("@carid", carid);
                        command.Parameters.AddWithValue("@usermiles", usermiles);
                        command.ExecuteNonQuery();

                    }

                    con.Close();

                }
                return true;

            }
            catch (SqlException)
            {

                return false;
            }
        }

        public List<Cars> GetCarById(string Id)
        {
            using (SqlConnection con = new SqlConnection(dbConnect))
            {
                using (SqlCommand command = new SqlCommand(
                        "select  carname, carpackage, carmileage, onrent, carconformation From Cars where carid = '" + Id + "' ; ", con))
                {
                    con.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            string name = (string)reader["carname"];
                            string package = (string)reader["carpackage"];
                            string mileage = (string)reader["carmileage"];
                            bool rent = Convert.ToBoolean(reader["onrent"]);
                            string conform = (string)reader["carconformation"];

                            Cars aCar = new Cars
                            {
                                CarId = Id,
                                CarName = name,
                                CarPackage = package,
                                CarMileage = mileage,
                                OnRent = rent,
                                CarConformation = conform
                            };
                            GetCar.Add(aCar);
                        }
                    }
                }


                con.Close();

                return GetCar;



            }

        }

       


    }
}
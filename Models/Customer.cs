using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace RentaCar.Models
{
    public class Customer
    {
        private int customerid = 777777777;
        private string customerfirstname = "N/A";
        private string customerlastname = "N/A";
        private string customeremail = "notreal@example.com";
        private string customerpickup = "03/03/1987";
        private string customerdropoff = "03/04/1987";
        private string cartorentId = "88888888";
        private string cartype = "N/A";
        private string carfee = "0";
        private string mileage = "1000000.00";


        public int CustomerId { get { return customerid; } set { customerid = value; } }
        [StringLengthValidator(1, 10, Ruleset = "RuleSetA", MessageTemplate = "First Name must be between 1 and 10 characters")]
        public string CustomerFirstName { get { return customerfirstname; } set { customerfirstname = value; } }
        [StringLengthValidator(1, 10, Ruleset = "RuleSetA", MessageTemplate = "Last Name must be between 1 and 10 characters")]
        public string CustomerLastName { get { return customerlastname; } set { customerlastname = value; } }
        [RegexValidator(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", Ruleset = "RuleSetA")]
        public string CustomerEmail { get { return customeremail; } set { customeremail = value; } }
        public string CustomerPickUp { get { return customerpickup; } set { customerpickup = value; } }
        public string CustomerDropOff { get { return customerdropoff; } set { customerdropoff = value; } }
        public string CarType { get { return cartype; } set { cartype = value; } }
        public string CarToRentId { get { return cartorentId; } set { cartorentId = value; } }
        public string CarFee { get { return carfee; } set { carfee = value; } }
        public string Mileage { get { return mileage; } set { mileage = value; } }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace RentaCar.Models
{
    public class Cars
    {
        private string carid = "99999";
        private string carname = "N/a";
        private string carpackage = "N/a";
        private bool onrent = false;
        private string carmileage = "N/a";
        private string carconformation = "N/a";




        public string CarId { get { return carid; } set { carid = value; } }
        public string CarName { get { return carname; } set { carname = value; } }
        public string CarPackage { get { return carpackage; } set { carpackage = value; } }
        public bool OnRent { get { return onrent; } set { onrent = value; } }
        public string CarMileage { get { return carmileage; } set { carmileage = value; } }
        [StringLengthValidator(1, 10, Ruleset = "RuleSetA", MessageTemplate = "Conformation must be between 1 and 10 characters")]
        public string CarConformation { get { return carconformation; } set { carconformation = value; } }
    }
 }
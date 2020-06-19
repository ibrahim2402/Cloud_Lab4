using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.Models
{
    public class CarBooking
    {
        public string CarRentalCode { get; set; }
        public int NumberOfSeats { get; set; }
        public int CarModel { get; set; }
        public int DriverAge { get; set; }
        public string FuelType { get; set; }
        public int Price { get; set; }

    }
}
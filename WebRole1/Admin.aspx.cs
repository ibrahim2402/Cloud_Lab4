using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebRole1.Models;

namespace WebRole1
{
    public partial class Admin : Page
    {

        private DbHelper dbHelper;

        protected void Page_Load(object sender, EventArgs e)
        {
            dbHelper = new DbHelper();
            string role = getUserRole();

            RedirectThePage();

            // Button disable if not admin
            if (role != "Administrator")
            {
                Label1.Text = "You have to be logged as admin to use this page";
                GetAirlineButton.Enabled = false;
                GetAirportButton.Enabled = false;
                GetCarRentalButton.Enabled = false;
                GetRouteButton.Enabled = false;

                AddAirlineButton.Enabled = false;
                AddAirportButton.Enabled = false;
                AddCarRentalButton.Enabled = false;
                AddRouteButton.Enabled = false;

                ActivateButton.Enabled = false;
                DeactivateButton.Enabled = false;

            }



        }
        // Redirect user to log page
        protected async void RedirectThePage()
        {
            if (User.Identity.GetUserId() != null)
            {
                bool isEnabled = await dbHelper.GetlockoutStatus(User.Identity.GetUserId());

                if (!isEnabled)
                {
                    Response.Redirect("/Account/Lockout");
                }
            }

        }

        // Get User role
        protected string getUserRole()
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (User.Identity.GetUserId() == null) return null;
            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var roles = roleManager.Roles.ToList();
            foreach (IdentityRole role in roles)
            {
                string roleName = role.Name;
                if (User.IsInRole(roleName))
                {
                    return role.Name;
                }
            }
            return null;
        }

        // Deactivate User
        protected async void DeactivateButton_Click(object sender, EventArgs e)
        {
            if (User.Identity.GetUserId() == ddlActiveList.SelectedItem.Value)
            {
                ActivateResults.Text = "you cannot deactivate yourself";
            }
            else
            {
                await dbHelper.UpdateLockoutStatus(ddlActiveList.SelectedItem.Value, false);
                ActivateResults.Text = ddlActiveList.SelectedItem.Text + " is Deactivated";

                
                ddlActiveList.DataBind();
                sdcDeactivate.DataBind();
                ddlDeactiveList.DataBind();
                sdcDeactivate.DataBind();
            }

        }

                 //Activate User
        protected async void ActivateButton_Click(object sender, EventArgs e)
        {
            await dbHelper.UpdateLockoutStatus(ddlDeactiveList.SelectedItem.Value, true);
            ActivateResults.Text = ddlDeactiveList.SelectedItem.Text + " is activated";

            
            ddlDeactiveList.DataBind();
            sdcDeactivate.DataBind();
            ddlActiveList.DataBind();
            sdcDeactivate.DataBind();
        }


        // Get route
        protected async void GetRoute_Click(object sender, EventArgs e)
        {
            Route route = await dbHelper.GetRoute(ddlRoutes.SelectedItem.Text);

            // Check fields if not empty
            if (route != null)
            {

                RouteFlightNrTextBox.Text = route.FlightNumber;
                ddlRouteCarrier.Text = route.Carrier;
                ddlRouteDepCode.Text = route.Departure;
                ddlRouteArrCode.Text = route.Arrival;

                RouteFlightNrTextBox.Enabled = false;
                EditRouteButton.Enabled = true;

                AddRouteResults.Text = "Route is fetched";
            }
            else
            {
                AddRouteResults.Text = "CANNOT BE FETCHED";

            }

        }

        // Get airport
        protected async void GetAirport_Click(object sender, EventArgs e)
        {
            Airport airport = await dbHelper.GetAirport(ddlAirports.SelectedItem.Text);

            // Check fields if not empty
            if (airport != null)
            {

                AirportCodeTextBox.Text = airport.Code;
                AirportCityTextBox.Text = airport.City;
                AirportLatitudeTextBox.Text = airport.Latitude.ToString();
                AirportLongitudeTextBox.Text = airport.Longitude.ToString();

                AirportCodeTextBox.Enabled = false;
                EditAirportButton.Enabled = true;

                AddAirportResult.Text = "Airport is fetched";
            }
            else
            {
                AddAirportResult.Text = "CANNOT BE FETCHED";

            }
        }

        // GEt airline
        protected async void GetAirline_Click(object sender, EventArgs e)
        {
            Airline airline = await dbHelper.GetAirline(ddlAirlines.SelectedItem.Text);

            // Check fields if not empty
            if (airline != null)
            {
                AirlineCodeTextBox.Text = airline.Code;
                AirlineNameTextBox.Text = airline.Name;

                AirlineCodeTextBox.Enabled = false;
                EditAirlineButton.Enabled = true;

                AddAirlineResult.Text = "Airline is fetched";
            }
            else
            {
                AddAirlineResult.Text = "CANNOT BE FETCHED";

            }
        }

        // GEt car rental function
        protected async void GetCarRental_Click(object sender, EventArgs e)
        {
            CarRental carRental = await dbHelper.GetCarRental(ddlCarRentals.SelectedItem.Text);

            
            if (carRental != null)
            {
                CarRentalCodeTextBox.Text = carRental.Code;
                CarRentalNameTextBox.Text = carRental.Name;

                CarRentalCodeTextBox.Enabled = false;
                EditCarRentalButton.Enabled = true;

                AddCRResults.Text = "CarRental is fetched";
            }
            else
            {
                AddCRResults.Text = "CANNOT BE FETCHED";

            }

        }

               //Route edit function. 
        protected async void EditRoute_Click(object sender, EventArgs e)
        {
            // Check fields if not empty
            if (!string.IsNullOrWhiteSpace(ddlRouteCarrier.SelectedItem.Text) &&
                !string.IsNullOrWhiteSpace(ddlRouteArrCode.SelectedItem.Text) &&
                !string.IsNullOrWhiteSpace(ddlRouteDepCode.SelectedItem.Text) &&
                (ddlRouteArrCode.SelectedItem.Text != ddlRouteDepCode.SelectedItem.Text))
            {
                Route route = new Route()
                {
                    FlightNumber = RouteFlightNrTextBox.Text,
                    Carrier = ddlRouteCarrier.SelectedItem.Text,
                    Departure = ddlRouteDepCode.SelectedItem.Text,
                    Arrival = ddlRouteArrCode.SelectedItem.Text

                };

                await dbHelper.UpdateRoute(route);

                AddRouteResults.Text = "Updated";

                RouteFlightNrTextBox.Text = string.Empty;
                ddlRouteCarrier.SelectedItem.Text = string.Empty;

                RouteFlightNrTextBox.Enabled = true;
                EditRouteButton.Enabled = false;

                
                ddlRouteDepCode.DataBind();
                ddlRouteArrCode.DataBind();
                RoutesdcAirports.DataBind();
                ddlRouteCarrier.DataBind();
                RoutesdcAirlines.DataBind();

            }
            else
            {
                AddRouteResults.Text = "Make sure to fill the fields!";

            }

        }

        // Airport edit function. 
        
        protected async void EditAirport_Click(object sender, EventArgs e)
        {
            // Check fields if not empty
            if (!string.IsNullOrWhiteSpace(AirportCityTextBox.Text) &&
               !string.IsNullOrWhiteSpace(AirportLatitudeTextBox.Text) && !string.IsNullOrWhiteSpace(AirportLongitudeTextBox.Text))
            {
                
                Airport airport = new Airport()
                {
                    Code = AirportCodeTextBox.Text,
                    City = AirportCityTextBox.Text,
                    Latitude = double.Parse(AirportLatitudeTextBox.Text),
                    Longitude = double.Parse(AirportLongitudeTextBox.Text)
                };

                await dbHelper.UpdateAirport(airport);

                AddAirportResult.Text = "Updated";
                AirportCodeTextBox.Text = string.Empty;
                AirportCityTextBox.Text = string.Empty;
                AirportLatitudeTextBox.Text = string.Empty;
                AirportLongitudeTextBox.Text = string.Empty;

                AirportCodeTextBox.Enabled = true;
                EditAirportButton.Enabled = false;
            }
            else
            {
                AddAirportResult.Text = "Make sure to fill the fields!";

            }
        }

             // Airline edit function. 
        
        protected async void EditAirline_Click(object sender, EventArgs e)
        {
            // Check fields if not empty
            if (!string.IsNullOrWhiteSpace(AirlineNameTextBox.Text))
            {
                Airline airline = new Airline()
                {
                    Code = AirlineCodeTextBox.Text,
                    Name = AirlineNameTextBox.Text

                };
                await dbHelper.UpdateAirline(airline);

                AddAirlineResult.Text = "Updated";
                AirlineCodeTextBox.Text = string.Empty;
                AirlineNameTextBox.Text = string.Empty;

                AirlineCodeTextBox.Enabled = true;
                EditAirlineButton.Enabled = false;
            }
            else
            {
                AddAirlineResult.Text = "Make sure to fill the fields!";
            }
        }

        // Car rental update
        protected async void EditCarRental_Click(object sender, EventArgs e)
        {
            // Check fields if not empty
            if (!string.IsNullOrWhiteSpace(CarRentalNameTextBox.Text))
            {
                CarRental carRental = new CarRental()
                {
                    Code = CarRentalCodeTextBox.Text,
                    Name = CarRentalNameTextBox.Text
                };

                await dbHelper.UpdateCarRental(carRental);

                AddCRResults.Text = "Updated";
                CarRentalCodeTextBox.Text = string.Empty;
                CarRentalNameTextBox.Text = string.Empty;

                CarRentalCodeTextBox.Enabled = true;
                EditCarRentalButton.Enabled = false;

            }
            else
            {
                AddCRResults.Text = "Make sure to fill the fields!";
            }

        }

        protected async void AddRoute_Click(object sender, EventArgs e)
        {
            // Checking fields not empty
            if (!string.IsNullOrWhiteSpace(RouteFlightNrTextBox.Text) && !string.IsNullOrWhiteSpace(ddlRouteCarrier.SelectedItem.Text) &&
                !string.IsNullOrWhiteSpace(ddlRouteArrCode.SelectedItem.Text) && !string.IsNullOrWhiteSpace(ddlRouteDepCode.SelectedItem.Text))
            {
                Route route = new Route()
                {
                    FlightNumber = RouteFlightNrTextBox.Text,
                    Carrier = ddlRouteCarrier.SelectedItem.Text,
                    Departure = ddlRouteDepCode.SelectedItem.Text,
                    Arrival = ddlRouteArrCode.SelectedItem.Text

                };

                await dbHelper.AddRoute(route);

                AddRouteResults.Text = "Added";

                RouteFlightNrTextBox.Text = string.Empty;
                ddlRouteCarrier.SelectedItem.Text = string.Empty;


                sdcRoutes.DataBind();
                ddlRoutes.DataBind();
                ddlRouteDepCode.DataBind();
                ddlRouteArrCode.DataBind();
                RoutesdcAirports.DataBind();
                ddlRouteCarrier.DataBind();
                RoutesdcAirlines.DataBind();
            }
            else
            {
                AddRouteResults.Text = "Make sure to fill the fields!";

            }
        }

        protected async void AddAirport_Click(object sender, EventArgs e)
        {
            // Check fields if not empty
            if (!string.IsNullOrWhiteSpace(AirportCodeTextBox.Text) && !string.IsNullOrWhiteSpace(AirportCityTextBox.Text) &&
               !string.IsNullOrWhiteSpace(AirportLatitudeTextBox.Text) && !string.IsNullOrWhiteSpace(AirportLongitudeTextBox.Text))
            {
                
                Airport airport = new Airport()
                {
                    Code = AirportCodeTextBox.Text,
                    City = AirportCityTextBox.Text,
                    Latitude = double.Parse(AirportLatitudeTextBox.Text),
                    Longitude = double.Parse(AirportLongitudeTextBox.Text)
                };

                await dbHelper.AddAirport(airport);

                AddAirportResult.Text = "Added";
                AirportCodeTextBox.Text = string.Empty;
                AirportCityTextBox.Text = string.Empty;
                AirportLatitudeTextBox.Text = string.Empty;
                AirportLongitudeTextBox.Text = string.Empty;

               
                ddlAirports.DataBind();
                sdcAirports.DataBind();
                ddlRouteDepCode.DataBind();
                ddlRouteArrCode.DataBind();
                RoutesdcAirports.DataBind();

            }
            else
            {
                AddAirportResult.Text = "Make sure to fill the fields!";

            }

        }

        protected async void AddAirline_Click(object sender, EventArgs e)
        {

            // Check fields if not empty
            if (!string.IsNullOrWhiteSpace(AirlineCodeTextBox.Text) && !string.IsNullOrWhiteSpace(AirlineNameTextBox.Text))
            {
                Airline airline = new Airline()
                {
                    Code = AirlineCodeTextBox.Text,
                    Name = AirlineNameTextBox.Text

                };
                await dbHelper.AddAirline(airline);

                AddAirlineResult.Text = "Added";
                AirlineCodeTextBox.Text = string.Empty;
                AirlineNameTextBox.Text = string.Empty;

                
                ddlAirlines.DataBind();
                sdcAirlines.DataBind();
                ddlRouteCarrier.DataBind();
                RoutesdcAirlines.DataBind();


            }
            else
            {
                AddAirlineResult.Text = "Make sure to fill the fields!";
            }
        }
        protected async void AddCarRental_Click(object sender, EventArgs e)
        {
            // Check fields if not empty
            if (!string.IsNullOrWhiteSpace(CarRentalCodeTextBox.Text) && !string.IsNullOrWhiteSpace(CarRentalNameTextBox.Text))
            {
                CarRental carRental = new CarRental()
                {
                    Code = CarRentalCodeTextBox.Text,
                    Name = CarRentalNameTextBox.Text

                };
                await dbHelper.AddCarRental(carRental);

                AddCRResults.Text = "Added";
                CarRentalCodeTextBox.Text = string.Empty;
                CarRentalNameTextBox.Text = string.Empty;

                
                ddlCarRentals.DataBind();
                sdcCarRentals.DataBind();
            }
            else
            {
                AddCRResults.Text = "Make sure to fill the fields!";
            }
        }


    }
}
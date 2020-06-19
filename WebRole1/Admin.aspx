<%@ Page Title="Admin" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" EnableEventValidation="false" Inherits="WebRole1.Admin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: Title %>.</h2>
    <asp:Label runat="server" Font-Bold="true" ForeColor="Red" Font-Size="Larger" ID="Label1"></asp:Label>
    <br />
    <br />
    <div class="row">
        <div class="col-md-3" style="display: inline-block; border-style: outset;">
            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Insert/Edit Airports" Style="margin: 5px 0px 0px 80px;"></asp:Label>

                <asp:DropDownList ID="ddlAirports" DataSourceID="sdcAirports" DataTextField="Code" DataValueField="Code" runat="server" Width="120px" Style="margin: 5px 0px 0px 5px;">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdcAirports" runat="server" ConnectionString="<%$ ConnectionStrings:DBConnection %>" SelectCommand="SELECT [Code] FROM [Airports]"></asp:SqlDataSource>
                <asp:Button ID="GetAirportButton" runat="server" Backcolor="Orange" Text="Get Airport" class="btn btn-warning" Style="margin: 5px 0px 0px 5px;" Width="120px" OnClick="GetAirport_Click"></asp:Button>

                <asp:Label ID="Label3" runat="server" Width="120px" Text="Code:" Style="margin: 5px 0px 0px 5px;"></asp:Label>

                <asp:TextBox ID="AirportCodeTextBox" runat="server" Style="margin: 5px 0px 0px 5px;" Width="120px"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Width="120px" Text="City:" Style="margin: 5px 0px 0px 5px;"></asp:Label>

                <asp:TextBox ID="AirportCityTextBox" runat="server" Style="margin: 5px 0px 0px 5px;" Width="120px"></asp:TextBox>
                <asp:Label ID="Label5" runat="server" Width="120px" Text="Latitude:" Style="margin: 5px 0px 0px 5px;"></asp:Label>

                <asp:TextBox ID="AirportLatitudeTextBox" runat="server" Style="margin: 5px 0px 0px 5px;" Width="120px"></asp:TextBox>
                <asp:Label ID="Label6" runat="server" Width="120px" Text="Longitude:" Style="margin: 5px 0px 0px 5px;"></asp:Label>

                <asp:TextBox ID="AirportLongitudeTextBox" runat="server" Style="margin: 5px 0px 0px 5px;" Width="120px"></asp:TextBox>

                <asp:Button ID="AddAirportButton" runat="server" Backcolor="YellowGreen" Text="Add Airport" Style="margin: 10px 0px 0px 5px;" class="btn btn-primary" Width="250px" OnClick="AddAirport_Click" />
                <asp:Button ID="EditAirportButton" runat="server" Backcolor="YellowGreen" Enabled="false" Text="Update Airport" Style="margin: 10px 0px 0px 5px;" class="btn btn-success" Width="250px" OnClick="EditAirport_Click" />

                <asp:Label ID="AddAirportResult" runat="server" Style="margin: 5px 0px 0px 5px;" Width="250px" Text=""></asp:Label>

            </div>
        </div>
        <div class="col-md-3" style="display: inline-block; border-style: outset;">
            <div class="form-group">
                <asp:Label ID="Label7" runat="server" Text="Insert/Edit Airlines" Style="margin: 5px 0px 0px 80px;"></asp:Label>


                <asp:DropDownList ID="ddlAirlines" DataSourceID="sdcAirlines" DataTextField="Code" DataValueField="Code" runat="server" Width="120px" Style="margin: 5px 0px 0px 5px;">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdcAirlines" runat="server" ConnectionString="<%$ ConnectionStrings:DBConnection %>" SelectCommand="SELECT [Code] FROM [Airlines]"></asp:SqlDataSource>
                <asp:Button ID="GetAirlineButton" runat="server" Backcolor="Orange" Text="Get Airline" Style="margin: 5px 0px 0px 5px;" Width="120px" class="btn btn-warning" OnClick="GetAirline_Click"></asp:Button>
                <br />
                <br />
                <asp:Label ID="Label8" runat="server" Width="120px" Text="Code:" Style="margin: 5px 0px 0px 5px;"></asp:Label>

                <asp:TextBox ID="AirlineCodeTextBox" runat="server" Style="margin: 5px 0px 0px 5px;" Width="120px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label9" runat="server" Width="120px" Text="Name:" Style="margin: 5px 0px 0px 5px;"></asp:Label>

                <asp:TextBox ID="AirlineNameTextBox" runat="server" Style="margin: 5px 0px 0px 5px;" Width="120px"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="AddAirlineButton" runat="server" Backcolor="YellowGreen" Text="Add Airline" Style="margin: 12px 0px 0px 5px;" class="btn btn-primary" Width="250px" OnClick="AddAirline_Click" />
                <asp:Button ID="EditAirlineButton" runat="server" Backcolor="YellowGreen" Enabled="false" Text="Update Airline" Style="margin: 12px 0px 0px 5px;" class="btn btn-success" Width="250px" OnClick="EditAirline_Click" />

                <asp:Label ID="AddAirlineResult" runat="server" Style="margin: 5px 0px 0px 5px;" Width="250px" Text=""></asp:Label>



            </div>
        </div>

        <div class="col-md-3" style="display: inline-block; border-style: outset;">
            <div class="form-group">
                <asp:Label ID="Label10" runat="server" Text="Insert/Edit Routes" Style="margin: 5px 0px 0px 80px;"></asp:Label>

                <asp:DropDownList ID="ddlRoutes" DataSourceID="sdcRoutes" DataTextField="FlightNumber" DataValueField="FlightNumber" runat="server" Width="120px" Style="margin: 5px 0px 0px 5px;">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdcRoutes" runat="server" ConnectionString="<%$ ConnectionStrings:DBConnection %>" SelectCommand="SELECT [FlightNumber] FROM [Routes]"></asp:SqlDataSource>
                <asp:Button ID="GetRouteButton" runat="server" Backcolor="Orange" Text="Get Route" Style="margin: 5px 0px 0px 5px;" Width="120px" class="btn btn-warning" OnClick="GetRoute_Click"></asp:Button>

                <asp:Label ID="Label11" runat="server" Width="120px" Text="FlightNr:" Style="margin: 5px 0px 0px 5px;"></asp:Label>

                <asp:TextBox ID="RouteFlightNrTextBox" runat="server" Style="margin: 5px 0px 0px 5px;" Width="120px"></asp:TextBox>
                <asp:Label ID="Label12" runat="server" Width="120px" Text="Airline's Code:" Style="margin: 5px 0px 0px 5px;"></asp:Label>

                <asp:DropDownList ID="ddlRouteCarrier" Height="26px" DataSourceID="RoutesdcAirlines" DataTextField="Code" DataValueField="Code" runat="server" Width="120px" Style="margin: 5px 0px 0px 5px;">
                </asp:DropDownList>
                <asp:SqlDataSource ID="RoutesdcAirlines" runat="server" ConnectionString="<%$ ConnectionStrings:DBConnection %>" SelectCommand="SELECT [Code] FROM [Airlines]"></asp:SqlDataSource>

                <asp:Label ID="Label13" runat="server" Width="120px" Text="Dep's Code:" Style="margin: 5px 0px 0px 5px;"></asp:Label>

                <asp:DropDownList ID="ddlRouteDepCode" Height="26px" DataSourceID="RoutesdcAirports" DataTextField="Code" DataValueField="Code" runat="server" Width="120px" Style="margin: 5px 0px 0px 5px;">
                </asp:DropDownList>
                <asp:SqlDataSource ID="RoutesdcAirports" runat="server" ConnectionString="<%$ ConnectionStrings:DBConnection %>" SelectCommand="SELECT [Code] FROM [Airports]"></asp:SqlDataSource>
                <asp:Label ID="Label14" runat="server" Width="120px" Text="Arr's Code:" Style="margin: 5px 0px 0px 5px;"></asp:Label>

                <asp:DropDownList ID="ddlRouteArrCode" Height="26px" DataSourceID="RoutesdcAirports" DataTextField="Code" DataValueField="Code" runat="server" Width="120px" Style="margin: 5px 0px 0px 5px;">
                </asp:DropDownList>
                <asp:Button ID="AddRouteButton" runat="server" Backcolor="YellowGreen" Text="Add Route" Style="margin: 10px 0px 0px 5px;" class="btn btn-primary" Width="250px" OnClick="AddRoute_Click" />

                <asp:Button ID="EditRouteButton" runat="server" Backcolor="YellowGreen" Enabled="false" Text="Update Route" Style="margin: 10px 0px 0px 5px;" class="btn btn-success" Width="250px" OnClick="EditRoute_Click" />

                <asp:Label ID="AddRouteResults" runat="server" Style="margin: 5px 0px 0px 5px;" Width="250px" Text=""></asp:Label>

            </div>
        </div>

        <div class="col-md-3" style="display: inline-block; border-style: outset;">
            <div class="form-group">
                <asp:Label ID="Label15" runat="server" Text="Insert/Edit Car Retal" Style="margin: 5px 0px 0px 80px;"></asp:Label>

                <asp:DropDownList ID="ddlCarRentals" DataSourceID="sdcCarRentals" DataTextField="Code" DataValueField="Code" runat="server" Width="120px" Style="margin: 5px 0px 0px 5px;">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdcCarRentals" runat="server" ConnectionString="<%$ ConnectionStrings:DBConnection %>" SelectCommand="SELECT [Code] FROM [CarRentals]"></asp:SqlDataSource>

                <asp:Button ID="GetCarRentalButton" runat="server" Backcolor="Orange" Text="Get Car Rental" Style="margin: 5px 0px 0px 5px;" Width="120px" class="btn btn-warning" OnClick="GetCarRental_Click"></asp:Button>
                <br />
                <br />
                <asp:Label ID="Label16" runat="server" Width="120px" Text="Code:" Style="margin: 5px 0px 0px 5px;"></asp:Label>

                <asp:TextBox ID="CarRentalCodeTextBox" runat="server" Style="margin: 5px 0px 0px 5px;" Width="120px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label17" runat="server" Width="120px" Text="Name:" Style="margin: 5px 0px 0px 5px;"></asp:Label>

                <asp:TextBox ID="CarRentalNameTextBox" runat="server" Style="margin: 5px 0px 0px 5px;" Width="120px"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="AddCarRentalButton" runat="server" Backcolor="YellowGreen" Text="Add Car Rental" Style="margin: 12px 0px 0px 5px;" class="btn btn-primary" Width="250px" OnClick="AddCarRental_Click" />

                <asp:Button ID="EditCarRentalButton" class="btn btn-success" runat="server" Backcolor="YellowGreen" Enabled="false" Text="Update Car Rental" Style="margin: 12px 0px 0px 5px;" Width="250px" OnClick="EditCarRental_Click" />
                <asp:Label ID="AddCRResults" runat="server" Style="margin: 5px 0px 0px 5px;" Width="250px" Text=""></asp:Label>

            </div>
        </div>

        <div class="col-md-6" style="display: inline-block; border-style: outset;">
            <div class="form-group">
                <asp:Label ID="Label18" runat="server" Text="Activate/Deactivate " Style="margin: 5px 0px 0px 80px;"></asp:Label>

                <br />

                <asp:Label ID="Label19" runat="server" Width="120px" Text="Active users list:" Style="margin: 5px 0px 0px 5px;"></asp:Label>
                <asp:DropDownList ID="ddlActiveList" DataSourceID="sdcActivate" DataTextField="Email" DataValueField="Id" runat="server" Width="120px" Style="margin: 5px 0px 0px 5px;">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdcActivate" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Id], [Email] FROM [AspNetUsers] WHERE ([LockoutEnabled] = @LockoutEnabled)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="true" Name="LockoutEnabled" Type="Boolean" />
                    </SelectParameters>
                </asp:SqlDataSource>

                <asp:Label ID="Label20" runat="server" Width="120px" Text="Deactive users list:" Style="margin: 5px 0px 0px 40px;"></asp:Label>
                <asp:DropDownList ID="ddlDeactiveList" DataSourceID="sdcDeactivate" DataTextField="Email" DataValueField="Id" runat="server" Width="120px" Style="margin: 5px 0px 0px 5px;">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdcDeactivate" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Id], [Email] FROM [AspNetUsers] WHERE ([LockoutEnabled] = @LockoutEnabled)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="false" Name="LockoutEnabled" Type="Boolean" />
                    </SelectParameters>
                </asp:SqlDataSource>


                <asp:Button ID="DeactivateButton" class="btn btn-success" runat="server" Backcolor="Orange" Text="Deactivate" Style="margin: 12px 0px 0px 5px;" Width="250px" OnClick="DeactivateButton_Click" />
                <asp:Button ID="ActivateButton" class="btn btn-success" runat="server" Backcolor="Orange" Text="Activate" Style="margin: 12px 0px 0px 40px;" Width="250px" OnClick="ActivateButton_Click" />

                <asp:Label ID="ActivateResults" runat="server" Style="margin: 5px 0px 0px 5px;" Width="250px" Text=""></asp:Label>

            </div>
        </div>

    </div>
</asp:Content>

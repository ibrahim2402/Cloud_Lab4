<%@ Page Title="Home Page" Async="true" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebRole1._Default" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .gridview th {
            padding: 5px;
        }
        .gridview td {
            padding: 5px;
        }
    </style>
    <asp:Label runat="server" Font-Bold="true" ForeColor="Red" Font-Size="Larger" ID="Label1"></asp:Label>
    <br />
    <br />
    <div class="row">
        <div class="col-md-4" style="display: inline-block; border-style: outset;">
            <div class="form-group">
                <br />
                <asp:Label ID="Label6" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Blue" Text="CHATER-TRAVEL" Style="margin-left: 130px"></asp:Label>

                <br />
                <asp:Label ID="Label2" runat="server" Width="140px" Text="Passport Number:" Style="margin: 20px 0px 0px 20px;"></asp:Label>

                <asp:TextBox ID="PassportTextBox" runat="server" Style="margin: 20px 0px 0px 0px;" Width="180px"></asp:TextBox>

                <asp:Label ID="Label3" runat="server" Width="140px" Text="Passenger Name:" Style="margin: 20px 0px 0px 20px;"></asp:Label>

                <asp:TextBox ID="PassengerNameTextBox" runat="server" Style="margin: 2px 0px 0px 0px;" Width="180px"></asp:TextBox>

                <asp:Label ID="Label4" runat="server" Width="140px" Text="Flight Number:" Style="margin: 15px 0px 0px 20px;"></asp:Label>

               

                <asp:DropDownList ID="ddlRoutes" Height="25px" DataSourceID="sdcRoutes" DataTextField="FlightNumber" DataValueField="FlightNumber" runat="server" Width="180px" Style="margin: 10px 0px 0px 0px;">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdcRoutes" runat="server" ConnectionString="<%$ ConnectionStrings:DBConnection %>" SelectCommand="SELECT [FlightNumber] FROM [Routes]"></asp:SqlDataSource>

                <asp:Label ID="Label5" runat="server" Width="210px" Text="Departure's Date(yyyy-mm-dd):" Style="margin: 15px 0px 0px 20px;"></asp:Label>

                <asp:TextBox ID="DepartureDateTextBox" runat="server" Style="margin: 15px 0px 0px 0px;" Width="110px"></asp:TextBox>
                <asp:Label ID="Label7" runat="server" Width="140px" Text="Passenger Type:" Style="margin: 15px 0px 0px 20px;"></asp:Label>

                <asp:DropDownList runat="server" ID="ddlPassengerType" Style="margin: 15px 0px 0px 0px;" Width="180px">
                    <asp:ListItem>Infant</asp:ListItem>
                    <asp:ListItem>Child</asp:ListItem>
                    <asp:ListItem>Adult</asp:ListItem>
                    <asp:ListItem>Senior</asp:ListItem>
                </asp:DropDownList>



                <asp:Button ID="BookFlightButton" runat="server" class="btn btn-primary" Text="Book Flight" BackColor="YellowGreen" Style="margin: 15px 0px 0px 20px;" Width="330px" Height="30" OnClick="BookFlightButton_Click" />

                <asp:Label ID="FlightResults" runat="server" ForeColor="Red" Width="330px" Style="margin: 15px 0px 0px 20px;"></asp:Label>

            </div>
        </div>

        <div class="col-md-4" style="display: inline-block; border-style: outset;">
            <div class="form-group">
                <br />
                <asp:Label ID="Label8" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Blue" Text="CAR SERVICE" Style="margin-left: 130px"></asp:Label>

                <br />
                <asp:Label ID="Label9" runat="server" Width="140px" Text="Car Rental Company:" Style="margin: 10px 0px 0px 20px;"></asp:Label>

                <asp:DropDownList ID="ddlCarRentals" DataSourceID="sdcCarRentals" Height="25px" DataTextField="Code" DataValueField="Code" runat="server" Width="180px" Style="margin: 10px 0px 0px 0px;">
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdcCarRentals" runat="server" ConnectionString="<%$ ConnectionStrings:DBConnection %>" SelectCommand="SELECT [Code] FROM [CarRentals]"></asp:SqlDataSource>
                <asp:Label ID="Label20" runat="server" Width="140px" Text="Car Model(yyyy):" Style="margin: 10px 0px 0px 20px;"></asp:Label>

                <asp:TextBox ID="CarModelTextBox" runat="server" Style="margin: 10px 0px 0px 0px;" Width="180px"></asp:TextBox>


                <asp:Label ID="Label10" runat="server" Width="140px" Text="Number of Seats:" Style="margin: 10px 0px 0px 20px;"></asp:Label>

                <asp:DropDownList runat="server" ID="ddlNumberOfSeats" Height="25" Style="margin: 10px 0px 0px 0px;" Width="180px">
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label11" runat="server" Width="140px" Text="Driver's age:" Style="margin: 10px 0px 0px 20px;"></asp:Label>

                <asp:TextBox ID="DriverAgeTextBox" runat="server" Style="margin: 10px 0px 0px 0px;" Width="180px"></asp:TextBox>

                <asp:Label ID="Label13" runat="server" Width="140px" Text="Fuel Type:" Style="margin: 10px 0px 0px 20px;"></asp:Label>
                <asp:DropDownList runat="server" ID="ddlFuelType" Style="margin: 20px 0px 0px 0px;" Width="180px">
                    <asp:ListItem>Benzene</asp:ListItem>
                    <asp:ListItem>Diesel </asp:ListItem>
                </asp:DropDownList>

                <asp:Button ID="BookCarRentalButton" runat="server" class="btn btn-primary" Text="Rent A Car" BackColor="YellowGreen" Style="margin: 36px 0px 0px 20px;" Width="330px" Height="30" OnClick="BookCarRentalButton_Click" />

                <asp:Label ID="CarBookingResults" runat="server" ForeColor="Red" Width="330px" Style="margin: 10px 0px 0px 20px;"></asp:Label>
                <br />

            </div>
        </div>

        <div class="col-md-4" style="display: inline-block; border-style: outset;">
            <div class="form-group">
                <br />
                <asp:Label ID="Label12" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Blue" Text="PAYMENT" Style="margin-left: 130px"></asp:Label>

                <br />
                <br />
                <asp:Label ID="Label15" runat="server" Width="140px" Text="Flight price:" Style="margin: 10px 0px 0px 20px;"></asp:Label>

                <asp:TextBox ID="FlightPriceTextBox" runat="server" Text="0" Enabled="false" Style="margin: 10px 0px 0px 0px; text-align: right" Width="140px"></asp:TextBox>
                <asp:Label ID="Label18" runat="server" Width="40px" Text="SEK" Style="margin: 10px 0px 0px 0px;"></asp:Label>

                <br />
                <br />
                <asp:Label ID="Label16" runat="server" Width="140px" Text="Car booking price:" Style="margin: 10px 0px 0px 20px;"></asp:Label>

                <asp:TextBox ID="CarBookingPrice" runat="server" Enabled="false" Text="0" Style="margin: 10px 0px 0px 0px; text-align: right" Width="140px"></asp:TextBox>
                <asp:Label ID="Label17" runat="server" Width="40px" Text="SEK" Style="margin: 10px 0px 0px 0px;"></asp:Label>

                <br />
                <br />
                <asp:Label ID="Label14" runat="server" Width="140px" Text="Total:" Style="margin: 10px 0px 0px 20px;"></asp:Label>

                <asp:TextBox ID="TotalPriceTextBox" runat="server" Text="0" Enabled="false" Style="margin: 10px 0px 0px 0px; text-align: right" Width="140px"></asp:TextBox>
                <asp:Label ID="Label19" runat="server" Width="40px" Text="SEK" Style="margin: 10px 0px 0px 0px;"></asp:Label>

                <br />
                <br />



                <asp:Button ID="CreatePayment" Enabled="false" runat="server" class="btn btn-success" Text="Make Payment!" BackColor="Orange" Style="margin: 29px 0px 0px 20px;" Width="330px" Height="30" OnClick="CreatePayment_Click" />

                <asp:Label ID="PaymentResults" runat="server" Width="330px" Style="margin: 10px 0px 0px 20px;"></asp:Label>
                <br />

            </div>
        </div>


        <div class="col-md-6" style="display: inline-block; border-style: outset;">
            <div class="form-group">
                <br />
                <asp:Label ID="Label21" runat="server" Font-Bold="true" Font-Size="Large" Text="Routes Specifications" Style="margin-left: 130px"></asp:Label>

                <br />
                <br />
                <asp:GridView class="auto-style3" DataSourceID="sdcRouteSpec" ID="routesTable" runat="server" AutoGenerateColumns="False">
                    <HeaderStyle CssClass="gridview" />
                    <RowStyle  CssClass="gridview"/>
                    <Columns>
                        <asp:BoundField DataField="FlightNumber" HeaderText="FlightNumber" SortExpression="FlightNumber" />
                        <asp:BoundField DataField="Carrier" HeaderText="Carrier" SortExpression="Carrier" />
                        <asp:BoundField DataField="Departure" HeaderText="Departure" SortExpression="Departure" />
                        <asp:BoundField DataField="Arrival" HeaderText="Arrival" SortExpression="Arrival" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="sdcRouteSpec" runat="server" ConnectionString="<%$ ConnectionStrings:DBConnection %>" SelectCommand="SELECT * FROM [Routes]"></asp:SqlDataSource>
                <br />

            </div>
        </div>
    </div>

</asp:Content>

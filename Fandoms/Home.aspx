<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Fandoms.Home" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <asp:Panel ID="pnlTitle" runat="server" HorizontalAlign="Center">
        <h1>
            <asp:Label ID="lblWelcome" runat="server" Text="Welcome to InfoFandoms!" Font-Bold="True" Font-Italic="False" Font-Size="XX-Large" Style="font-family: 'Baskerville Old Face'"></asp:Label>
        </h1>

    </asp:Panel>


    <asp:Panel ID="pnlWelcome" runat="server" HorizontalAlign="Center">
        <div class="container">
            <div class="row">
                <div class="col-xs-4">

                    <asp:Label ID="lblWho" runat="server" Text="Who We Are!" Font-Bold="True" Style="font-size: x-large; font-family: Arial"></asp:Label>

                </div>
                <div class="col-xs-4">

                    <asp:Label ID="lblWhat" runat="server" Text="What We Do!" Font-Bold="True" Style="font-size: x-large; font-family: Arial"></asp:Label>

                </div>
                <div class="col-xs-4">

                    <asp:Label ID="lblWhy" runat="server" Text="Why We Do It!" Font-Bold="True" Style="font-size: x-large; font-family: Arial"></asp:Label>

                </div>
            </div>
        </div>
        <br /> 
        <txt>

            We are a group of college students that have been assigned the task of creating
                        a client-server database. So why no have some fun with it?
                        We chose to create a database that allows us to input fandoms
                        and the movies associated with those fandoms! Follow the flow to add your own!</txt>

        <br />
        <br />
        <br /> 

    </asp:Panel>

    <asp:Panel ID="pnlButtons" runat="server" HorizontalAlign="Center" Height="179px">

        <div class="row">
            <div class="col-xs-2">
                <img alt="HerryPotter Poster" class="auto-style1" src="Images/HarryPotter.jpg" /></div>
            <div class="col-xs-2">
                <img alt="MCU Poster" class="auto-style3" src="Images/MCU.png" /></div>
            <div class="col-xs-2">
                <asp:Button ID="btnAddFandom" runat="server" Text="Add/Edit Fandom" OnClick="Button1_Click" ToolTip="Clicking this allows you to view, add, and edit the Fandoms databse!" BackColor="#61278F" BorderColor="#61278F" CssClass="btn" Font-Bold="True" Height="65px" Width="150px" />
            </div>
            <div class="col-xs-2">
                <asp:Button ID="btnAddMovie" runat="server" Text="Add/Edit Movie" OnClick="btnAddMovie_Click" ToolTip="Click this allows you to view, add, and edit movies of a specified fandom!" BackColor="#61278F" BorderColor="#61278F" CssClass="btn" Font-Bold="True" Height="65px" Width="150px" />
            </div>
            <div class="col-xs-2">
                <img alt="StarWars Poster" class="auto-style4" src="Images/StarWars.jpg" />
            </div>
            <div class="col-xs-2">
                <img alt="Transfromers Poster" class="auto-style5" src="Images/Transfromers.png" />
            </div>



            &nbsp;

        </div>
    </asp:Panel>

    <style>
        #btnAddFandom {
            margin-top: 50px;
            border-radius: 5px 25px;
            font-weight: 700;
        }

        #btnAddMovie {
            margin-top: 50px;
            border-radius: 5px 75px;
            font-weight: 700;
        }

        .auto-style1 {
            width: 150px;
            height: 225px;
        }

        .auto-style3 {
            width: 150px;
            height: 225px;
        }

        .auto-style4 {
            width: 150px;
            height: 225px;
        }

        .auto-style5 {
            width: 150px;
            height: 225px;
        }

    </style>

</asp:Content>



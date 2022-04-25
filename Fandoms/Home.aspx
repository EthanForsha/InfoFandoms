<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Fandoms.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <asp:Panel ID="pnlTitle" runat="server" HorizontalAlign="Center">
        <h1>
            <asp:Label ID="lblWelcome" runat="server" Text="Welcome to InfoFandoms!" Font-Bold="True" Font-Italic="True" Font-Size="XX-Large"></asp:Label>
        </h1>
    </asp:Panel>


    <asp:Panel ID="pnlWelcome" runat="server" HorizontalAlign="Center">
        <h2>
            <asp:Label ID="lblWWW" runat="server" Text="Who we are. What we do. Why we do it." Font-Bold="True"></asp:Label>
        </h2>
        <txt>
            We are a group of college students that have been assigned the task of creating
                        a client-server database. So why no have some fun with it?
                        We chose to create a database that allows us to input fandoms
                        and the movies associated with those fandoms! Follow the flow to add your own!</txt>

        <br />
        <br />

    </asp:Panel>
    
    <asp:Panel ID="pnlButtons" runat="server" HorizontalAlign="Center" Height="179px">


        <img alt="HerryPotter Poster" class="auto-style1" src="Images/HarryPotter.jpg" />

        <img alt="MCU Poster" class="auto-style3" src="Images/MCU.png" />

        <asp:Button ID="btnAddFandom" runat="server" Text="Add/Edit Fandom" CssClass="btn btn-primary" OnClick="Button1_Click" ToolTip="Clicking this allows you to view, add, and edit the Fandoms databse!" />
        &nbsp;
        <asp:Button ID="btnAddMovie" runat="server" Text="Add/Edit Movie" CssClass="btn btn-primary" OnClick="btnAddMovie_Click" ToolTip="Click this allows you to view, add, and edit movies of a specified fandom!" />

        <img alt="StarWars Poster" class="auto-style4" src="Images/StarWars.jpg" />
        <img alt="Transfromers Poster" class="auto-style5" src="Images/Transfromers.png" />

    </asp:Panel>

    <style>
        #btnAddFandom {
            margin-top: 25px;
            width: 200px;
            height: 50px;
            border-radius: 5px 25px;
        }

        #btnAddMovie {
            margin-top: 25px;
            width: 200px;
            height: 50px;
        }
        .auto-style1 {
            width: 150px;
            height: 174px;
        }
        .auto-style3 {
            width: 150px;
            height: 175px;
        }
        .auto-style4 {
            width: 150px;
            height: 175px;
        }
        .auto-style5 {
            width: 150px;
            height: 175px;
        }
    </style>

</asp:Content>



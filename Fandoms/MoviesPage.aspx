<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoviesPage.aspx.cs" Inherits="Fandoms.MoviesPage" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <title>View or Manage Movies</title>
    <h2>View or Manage Movies</h2>
    <body>
        <asp:Panel ID="pnlAddMovie" runat="server" ClientIDMode="Static">
            <div>
                <asp:Label ID="lblMoviesPage" runat="server" Text="Here you can view all the movies in a chosen Fandom, and you can even add your own movies to the Fandom if they are not already there!"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblSelectFandom" runat="server" Text="Please Select a Fandom: " Visible="False"></asp:Label>
                <asp:DropDownList ID="drpFandoms" runat="server" Visible="False" Width="604px">
                </asp:DropDownList>
                <br />
                <br />
                <asp:Button ID="btnAddMovie" runat="server" CssClass="btn btn-primary" Text="Add Movie" />
                <br />
                <br />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMovieList" runat="server">
            <asp:GridView ID="gvMoviesList" runat="server" CssClass="table table-striped" AllowPaging="True" AllowSorting="True" DataKeyNames="MovieId" AutoGenerateColumns="False" Height="435px" Width="1713px" Style="margin-top: 0px">
                <Columns>
                    <asp:BoundField HeaderText="Movie ID" />
                    <asp:BoundField HeaderText="Movie Title" />
                    <asp:BoundField HeaderText="Fandom" />
                    <asp:BoundField HeaderText="Synopsis" />
                    <asp:BoundField HeaderText="Runtime" />
                    <asp:BoundField HeaderText="Release Date" />
                    <asp:BoundField HeaderText="Rating" />
                    <asp:BoundField HeaderText="Score" />
                    <asp:BoundField HeaderText="Box Office Earnings" />
                    <asp:BoundField HeaderText="Price" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnEditMovie" runat="server" CssClass="btn btn-primary" Text="Edit" />
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" />
                            <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-success" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Button ID="btnEditMovie" runat="server" CssClass="btn-primary" Text="Edit" />
                </EmptyDataTemplate>
            </asp:GridView>
        </asp:Panel>
    </body>
</asp:Content>

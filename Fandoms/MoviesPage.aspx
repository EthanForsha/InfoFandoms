﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoviesPage.aspx.cs" Inherits="Fandoms.MoviesPage" MasterPageFile="~/Site.Master" %>

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
                <asp:Label ID="lblSelectFandom" runat="server" Text="Please Select a Fandom: "></asp:Label>
                <asp:DropDownList ID="ddlFandoms" runat="server" Width="604px" AppendDataBoundItems="True">
                </asp:DropDownList>
                <asp:Label ID="lblFandomNameFeedback" runat="server" Visible="False"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblMovieName" runat="server" Text="Please enter the name of the Movie: "></asp:Label>
                <asp:TextBox ID="txtMovieName" runat="server" Width="525px"></asp:TextBox>
                <asp:Label ID="lblMovieNameFeedback" runat="server" Visible="False"></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnAddMovie" runat="server" CssClass="btn btn-primary" Text="Add Movie" OnClick="btnAddMovie_Click" />
                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" Visible="False" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" Visible="False" />
                <br />
                <br />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMovieList" runat="server">
            <asp:GridView ID="gvMoviesList" runat="server" CssClass="table table-striped" AllowPaging="True" AllowSorting="True" DataKeyNames="MovieId" AutoGenerateColumns="False" Height="435px" Width="1713px" Style="margin-top: 0px">
                <Columns>
                    <asp:BoundField HeaderText="Movie ID" DataField="MovieId" />
                    <asp:BoundField HeaderText="Movie Title" DataField="MovieName" />
                    <asp:BoundField HeaderText="Fandom" DataField="MovieFandomName" />
                    <asp:BoundField HeaderText="Runtime" DataField="MovieRuntime" />
                    <asp:BoundField HeaderText="Release Date" DataField="MovieReleaseDate" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="btnView" runat="server" CssClass="btn btn-success" Text="View" CommandName="ViewMovie" />
                            <asp:Button ID="btnEditMovie" runat="server" CssClass="btn btn-primary" Text="Edit" CommandName="EditMovie" />
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" CommandName="DeleteMovie" CommandArgument="MovieId" OnClientClick="return confirm('Are you sure you want to delete this movie?');" />
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

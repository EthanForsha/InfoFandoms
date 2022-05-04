﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoviesPage.aspx.cs" Inherits="Fandoms.MoviesPage" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <asp:Panel ID="pnlManageMoviesHeader" runat="server" ClientIDMode="Static">
        <div class="form-group">
            <h2>View or Manage Movies</h2>
            <asp:Label ID="lblMoviesPage" runat="server" Text="Here you can view all the movies in a chosen Fandom, and you can even add your own movies to the Fandom if they are not already there!"></asp:Label>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlViewMovies" runat="server" ClientIDMode="Static">
        <div class="form-group">
            <asp:DropDownList ID="ddlFandomsView" runat="server" AppendDataBoundItems="True" Visible="False" Width="604px">
            </asp:DropDownList>
            <asp:Button ID="btnViewMoviesInFandom" runat="server" CssClass="btn-primary" OnClick="btnViewMoviesInFandom_Click" Text="View Movies Only in this Fandom" />
            <br />
            <br />
            <asp:Button ID="btnAddNewMovie" runat="server" Text="Add or Edit a Movie" CssClass="btn btn-primary" OnClick="btnAddNewMovie_Click" />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlAddMovie" runat="server" ClientIDMode="Static" Visible="False">
        <div class="form-group">
            <br />
            <asp:Label ID="lblEditMovieInFandom" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblSelectFandom" runat="server" Text="Please select a fandom: "></asp:Label>
            <asp:DropDownList ID="ddlFandoms" runat="server" Width="604px" AppendDataBoundItems="True">
            </asp:DropDownList>
            <asp:Label ID="lblFandomNameRequired" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
            <asp:Label ID="lblFandomNameFeedback" runat="server" Visible="False" ForeColor="#FF3300">Please select a Fandom.</asp:Label>
            <br />
            <br />
            <asp:Label ID="lblMovieName" runat="server" Text="Please enter the name of the movie: "></asp:Label>
            <asp:TextBox ID="txtMovieName" runat="server" Width="525px"></asp:TextBox>
            <asp:Label ID="lblMovieNameRequired" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
            <asp:Label ID="lblMovieNameFeedback" runat="server" Visible="False" ForeColor="#FF3300">Please enter a valid movie name.</asp:Label>
            <br />
            <br />
            <asp:Label ID="lblMovieRuntime" runat="server" Text="Please enter the runtime of the movie (mins): "></asp:Label>
            <asp:TextBox ID="txtMovieRuntime" runat="server"></asp:TextBox>
            <asp:Label ID="lblMovieRuntimeFeedback" runat="server" ForeColor="#FF3300" Visible="False">Please enter a valid movie runtime.</asp:Label>
            <br />
            <br />
            <asp:Label ID="lblMovieRelease" runat="server" Text="Please enter the release date of the movie (MM/DD/YYYY): "></asp:Label>
            <asp:TextBox ID="txtMovieRelease" runat="server"></asp:TextBox>
            <asp:Label ID="lblMovieReleaseFeedback" runat="server" ForeColor="#FF3300" Visible="False">Please enter a valid movie release date.</asp:Label>
            <br />
            <br />
            <asp:Label ID="lblMovieRating" runat="server" Text="Please enter the rating of the movie (ex. PG-13): "></asp:Label>
            <asp:TextBox ID="txtMovieRating" runat="server"></asp:TextBox>
            <asp:Label ID="lblMovieRatingFeedback" runat="server" ForeColor="#FF3300" Visible="False">Please enter a valid movie rating.</asp:Label>
            <br />
            <br />
            <asp:Label ID="lblMovieScore" runat="server" Text="Please enter the Rotten Tomatoes score (ex. 95): "></asp:Label>
            <asp:TextBox ID="txtMovieScore" runat="server"></asp:TextBox>
            <asp:Label ID="lblMovieScoreFeedback" runat="server" ForeColor="#FF3300" Visible="False">Please enter a valid Rotten Tomatoes movie score.</asp:Label>
            <br />
            <br />
            <asp:Label ID="lblMovieInfo" runat="server" Text="Please enter a synopsis of the movie: "></asp:Label>
            <asp:Label ID="lblMovieInfoRequired" runat="server" ForeColor="#FF3300" Text="*"></asp:Label>
            <asp:Label ID="lblMovieInfoFeedback" runat="server" ForeColor="#FF3300" Visible="False">Please enter a movie synopsis.</asp:Label>
            <br />
            <asp:TextBox ID="txtMovieInfo" runat="server" Height="120px" Width="800px" TextMode="MultiLine" Rows="4"></asp:TextBox>

            <br />
            <br />
            <asp:Label ID="lblMovieImage" runat="server" Text="Please add a movie cover image: "></asp:Label>
            <asp:FileUpload ID="fuMovieImage" runat="server" />
            <asp:Label ID="lblMovieImageFeedback" runat="server" ForeColor="#FF3300" Visible="False">Image was not provided or failed to add image.</asp:Label>
        </div>
        <div class="form-group">
            <asp:Button ID="btnAddMovie" runat="server" CssClass="btn btn-primary" Text="Add Movie" OnClick="btnAddMovie_Click" />
            <asp:Button ID="btnCancelAddMovie" runat="server" CssClass="btn btn-danger" OnClick="btnCancelAddMovie_Click" Text="Cancel" />
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" Visible="False" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" Visible="False" OnClick="btnCancel_Click" />
            <asp:Label ID="lblMovieNameFeedbackSuccess" runat="server" ForeColor="#32CD32" Visible="False"></asp:Label>
            <asp:Label ID="lblMovieId" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblFandomName" runat="server" Visible="False"></asp:Label>
            <br />
            <br />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlMovieList" runat="server">
        <asp:GridView ID="gvMoviesList" runat="server" CssClass="table table-striped" AllowPaging="True" AllowSorting="True" DataKeyNames="MovieId" AutoGenerateColumns="False" Width="1200px" Style="margin-top: 0px" OnRowCommand="gvMoviesList_RowCommand" OnRowDataBound="gvMoviesList_RowDataBound">
            <Columns>
                <asp:BoundField DataField="MovieId" HeaderText="Movie ID" />
                <asp:BoundField DataField="MovieFandomName" HeaderText="Fandom" />
                <asp:BoundField HeaderText="Movie Title" DataField="MovieName" />
                <asp:BoundField HeaderText="Runtime" DataField="MovieRuntime" />
                <asp:BoundField DataField="MovieRating" HeaderText="Rating" />
                <asp:BoundField HeaderText="Release Date" DataField="MovieReleaseDate" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="btnViewMovie" runat="server" CssClass="btn btn-success" Text="View" CommandName="ViewMovie" CommandArgument="MovieId" />
                        <asp:Button ID="btnEditMovie" runat="server" CssClass="btn btn-primary" Text="Edit" CommandName="EditMovie" CommandArgument="MovieId" />
                        <asp:Button ID="btnDeleteMovie" runat="server" CssClass="btn btn-danger" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this movie?');" CommandName="DeleteMovie" CommandArgument="MovieId" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
    </asp:Panel>
    <asp:Panel ID="pnlViewMovie" runat="server">
        <div class="row">
            <div class="col-sm-3">
                <br />
                <asp:Image ID="imgMovie" runat="server" />
            </div>
            <div class="col-sm-5">
                <br />
                <asp:Label ID="lblViewMovieName" runat="server"> <strong>Movie Name: </strong></asp:Label>
                <asp:Label ID="lblViewMovieNameText" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblViewMovieInfo" runat="server"> <strong>Synopsis: </strong></asp:Label>
                <asp:Label ID="lblViewMovieInfoText" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblViewMovieRuntime" runat="server"> <strong>Runtime: </strong></asp:Label>
                <asp:Label ID="lblViewMovieRuntimeText" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblViewMovieRating" runat="server"> <strong>Rating: </strong></asp:Label>
                <asp:Label ID="lblViewMovieRatingText" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblViewMovieReleaseDate" runat="server"> <strong>Released: </strong></asp:Label>
                <asp:Label ID="lblViewMovieReleaseDateText" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblViewMovieScore" runat="server"><strong>Rotten Tomatoes Score: </strong></asp:Label>
                <asp:Label ID="lblViewMovieScoreText" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-1">
                <asp:Button ID="btnReturn" runat="server" Text="Go Back" CssClass="btn btn-primary" OnClick="btnReturn_Click" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>

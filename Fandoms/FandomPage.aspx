<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FandomPage.aspx.cs" Inherits="Fandoms.FandomPage" MasterPageFile="~/Site.Master" %>
  <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<!DOCTYPE html>
	<H2><strong>Fandoms</strong></H2>
    <asp:Label ID="lblHeader" runat="server"><strong>Here you can add your own Fandom to the list!</strong></asp:Label>
	<br>
	<br>
    <asp:Panel ID="pnlAddFandom" runat="server" ClientIDMode="Static">   
		<div class="form-group">
				<Label for="lblFandomName" CssClass="form-control">Fandom Name: </Label>
				<asp:TextBox ID="txtFandomName" runat="server" CssClass="form-control"></asp:TextBox> 
				<asp:RequiredFieldValidator ID="rfvFandomName" runat="server" ControlToValidate="txtFandomName" ValidationGroup="AddFandom" ErrorMessage="Name must be entered" ForeColor="#FF3300"></asp:RequiredFieldValidator> 
				<br>
				<br>

				<Label for="lblFandomInfo" CssClass="form-control">Fandom Information: </Label>
				<asp:TextBox ID="txtFandomInfo" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox> 
				<asp:RequiredFieldValidator ID="rfvFandomInfo" runat="server" ControlToValidate="txtFandomInfo" ValidationGroup="AddFandom" ErrorMessage="Description must be entered" ForeColor="#FF3300"></asp:RequiredFieldValidator>
				<br>
				<label for="fuFandomImage">Image: </label>
				<asp:FileUpload ID="fuFandomImage" runat="server" ClientIDMode="Static" CssClass="form-control-file" /> 
				<asp:Label ID="lblImage" Visible="false" ForeColor="Red" runat="server"></asp:Label>
		</div>
		<div class="form-group">
				<asp:Button CssClass="btn btn-primary" ID="btnAddFandom" runat="server" Text="Add" ValidationGroup="AddFandom" OnClick="btnAddFandom_Click" BackColor="#61278F" BorderColor="#61278F"/>
				<asp:Button ID="btnSaveFandom" runat="server" CssClass="btn btn-primary" Text="Save" Visible="False" OnClick="btnSaveFandom_Click"/>
				<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" Text="Cancel" Visible="False" OnClick="btnCancel_Click"/>
				<asp:Label ID="lblFandomId" runat="server" Visible="False"></asp:Label>
				<asp:Label ID="lblFeedbackFandomName" runat="server" ForeColor="Green" Visible="false"></asp:Label>
		</div>
	</asp:Panel> 
	<asp:Panel ID="pnlFandomList" runat="server">
		<asp:GridView ID="gvFandomList" runat="server" CssClass="table table-striped" AllowPaging="True" AllowSorting="True" DataKeyNames="FandomId" Width="1200px" AutoGenerateColumns="False" OnRowCommand="gvFandomList_RowCommand" OnRowDataBound="gvFandomList_RowDataBound">
			<Columns>
				<asp:BoundField DataField="FandomId" HeaderText="ID" />
				<asp:BoundField DataField="FandomName" HeaderText="Fandom Name" />
				<asp:BoundField DataField="FandomInfo" HeaderText="Fandom Information" />
				<asp:BoundField DataField="FandomImagePath" HeaderText="Fandom Image Path" Visible="False" />
				<asp:TemplateField HeaderText="Actions">
					<ItemTemplate>
						<asp:Button ID="btnView" runat="server" CssClass="btn btn-success" Text="View" CommandName="ViewFandom" />
						<asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary" Text="Edit" CommandName="EditFandom" />
						<asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this fandom?');" CommandArgument="FandomId" CommandName="DeleteFandom"/>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
	</asp:Panel>
	<asp:Panel ID="pnlViewFandomImages" runat="server" ClientIDMode="Static">
		 <div class="row">
            <div class="col-sm-3">
                <br />
				<asp:Image ID="imgFandom" runat="server" ImageAlign="Left" width="250px" height="250px" style="object-fit: contain; padding: 5px;"/>
			</div>
            <div class="col-sm-5">
                <br />
                <br />
                <asp:Label ID="lblViewFandomName" runat="server"> <strong>Fandom Name: </strong></asp:Label>
                <asp:Label ID="lblViewFandomNameText" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblViewFandomInfo" runat="server"> <strong>Fandom Information: </strong></asp:Label>
                <asp:Label ID="lblViewFandomInfoText" runat="server"></asp:Label>
                <br />
                <br />
            </div>
        </div>
        <div class="row">
            <div class="col-sm-1">
                <br />
                <br />
                <br />
                <asp:Button ID="btnGoBack" runat="server" CssClass="btn btn-primary" Text="Go Back" OnClick="btnGoBack_Click" BackColor="#61278F" BorderColor="#61278F"/>
            </div>
        </div>
	  </asp:Panel>
</asp:Content>
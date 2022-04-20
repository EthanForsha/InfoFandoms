<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FandomPage.aspx.cs" Inherits="Fandoms.FandomPage" MasterPageFile="~/Site.Master" %>
  <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	  <!DOCTYPE html>
		 <title>Fandoms</title>
	<h2>Fandoms</h2>
    <asp:Panel ID="pnlAddFandom" runat="server" ClientIDMode="Static">   
		<div class="form-group">
				<Label for="lblFandomName" CssClass="form-control">Fandom Name: </Label>
				<asp:TextBox ID="txtFandomName" runat="server" CssClass="form-control"></asp:TextBox> 
				<asp:RequiredFieldValidator ID="rfvFandomName" runat="server" ControlToValidate="txtFandomName" ValidationGroup="AddFandom" ErrorMessage="Name must be entered" ForeColor="#FF3300" CssClass="form-control"></asp:RequiredFieldValidator>
				<asp:Label ID="lblFeedbackFandomName" runat="server" Visible="false" CssClass="form-control"></asp:Label>

				<Label for="lblFandomInfo" CssClass="form-control">Fandom Information: </Label>
				<asp:TextBox ID="txtFandomInfo" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox> 
				<asp:RequiredFieldValidator ID="rfvFandomInfo" runat="server" ControlToValidate="txtFandomName" ValidationGroup="AddFandom" ErrorMessage="Description must be entered" ForeColor="#FF3300" CssClass="form-control"></asp:RequiredFieldValidator>
				<asp:Label ID="lblFeedbackFandomInfo" runat="server" Visible="false" CssClass="form-control"></asp:Label>

				<label for="fuFandomImage">Image: </label>
				<asp:FileUpload ID="fuFandomImage" runat="server" ClientIDMode="Static" CssClass="form-control-file" /> 
		</div>
		<div class="form-group">
				<asp:Button CssClass="btn btn-primary" ID="btnAddFandom" runat="server" Text="Add" ValidationGroup="AddFandom" OnClick="btnAddFandom_Click"/>
				<asp:Button ID="btnSaveFandom" runat="server" CssClass="btn btn-primary" Text="Save" Visible="False" OnClick="btnSaveFandom_Click"/>
				<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" Text="Cancel" Visible="False" OnClick="btnCancel_Click"/>
				<asp:Label ID="lblFandomId" runat="server" Visible="False"></asp:Label>
		</div>
	</asp:Panel> 
	<asp:Panel ID="pnlFandomList" runat="server">
		<asp:GridView ID="gvFandomList" runat="server" CssClass="table table-striped" AllowPaging="True" AllowSorting="True" DataKeyNames="FandomId" Width="1200px" AutoGenerateColumns="False" OnRowCommand="gvFandomList_RowCommand" OnRowDataBound="gvFandomList_RowDataBound">
			<Columns>
				<asp:BoundField DataField="FandomId" HeaderText="ID" />
				<asp:BoundField DataField="FandomName" HeaderText="Fandom Name" />
				<asp:BoundField DataField="FandomInfo" HeaderText="Fandom Information" />
				<asp:BoundField DataField="FandomImage" HeaderText="Fandom Image" />
				<asp:TemplateField HeaderText="Actions">
					<ItemTemplate>
						<asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary" Text="Edit" CommandName="EditFandom" />
						<asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this fandom?');" CommandArgument="FandomId" CommandName="DeleteFandom"/>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
	</asp:Panel>
</asp:Content>
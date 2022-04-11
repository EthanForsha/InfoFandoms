using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;  
using System.Web.Configuration; 
using System.Data;

namespace Fandoms
{
	public partial class FandomPage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack) 
			{
				BindFandomList();
			}
		}


		protected void BindFandomList()
		{
			using (SqlConnection conn = new SqlConnection()) 
			{
				conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

				SqlCommand cmd = new SqlCommand();
				cmd.CommandText = "SELECT * FROM Fandoms";
				cmd.Connection = conn;

				SqlDataAdapter sda = new SqlDataAdapter();
				sda.SelectCommand = cmd;

				DataTable dt = new DataTable();
				conn.Open();

				sda.Fill(dt);

				gvFandomList.DataSource = dt;
				gvFandomList.DataBind(); 
			}
		}

		protected void gvFandomList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow) 
			{
				// find the Edit button
				Button editButton = e.Row.FindControl("btnEdit") as Button;
				Button deleteButton = e.Row.FindControl("btnDelete") as Button;

				// find the categoryId of the row to use buttons
				editButton.CommandArgument = e.Row.Cells[0].Text;
				deleteButton.CommandArgument = e.Row.Cells[0].Text;
			}
		}

		protected void gvFandomList_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "EditFandom")
			{
				int fandomId = int.Parse(e.CommandArgument.ToString()); 
				lblFandomId.Text = e.CommandArgument.ToString();
				EditFandomById(fandomId);
			}
			else if (e.CommandName == "DeleteFandom")
			{
				int fandomId = int.Parse(e.CommandArgument.ToString());

				DeleteFandomById(fandomId);
			}
		}

		private void EditFandomById(int fandomId)
		{
			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

				SqlCommand cmd = new SqlCommand();
				cmd.CommandText = "SELECT * FROM Fandoms WHERE FandomId = " + fandomId;
				cmd.Connection = conn;
				conn.Open();

				SqlDataReader sdr = cmd.ExecuteReader();
				if (sdr.Read()) 
				{
					txtFandomName.Text = sdr["FandomName"].ToString(); 
					btnAddFandom.Visible = false;
					btnSaveFandom.Visible = true;
					btnCancel.Visible = true;
					pnlFandomList.Visible = false;
				}
			}
		}

		private void DeleteFandomById(int fandomId)
		{
			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

				SqlCommand cmd = new SqlCommand();
				cmd.CommandText = "DELETE FROM Fandoms WHERE FandomId = " + fandomId;
				cmd.Connection = conn;
				conn.Open();

				cmd.ExecuteNonQuery();
				BindFandomList();
			}
		}

		private void SaveFandomById(int fandomId)
		{
			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

				SqlCommand cmd = new SqlCommand();
				// change SQL statements later
				cmd.CommandText = "UPDATE Fandoms SET FandomName = '" + txtFandomName.Text + "'WHERE FandomId = " + fandomId;
				cmd.Connection = conn;
				conn.Open();

				cmd.ExecuteNonQuery();
				BindFandomList();

				pnlFandomList.Visible = true;
				btnAddFandom.Visible = true;
				btnSaveFandom.Visible = false;
				btnCancel.Visible = false;
				txtFandomName.Visible = true;
			}
		}

		protected void btnAddFandom_Click(object sender, EventArgs e)
		{
			if (Page.IsValid)
			{
				// 1. Create a connection object to the database
				SqlConnection conn = new SqlConnection();
				conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

				// 2. Create a command object to insert data
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;

				// TODO
				// this is a bad way to create a SQL statement
				// SQL inject vulnerability
				cmd.CommandText = "INSERT INTO Fandoms(FandomName) VALUES ('" + txtFandomName.Text + "')";

				// 3. Run the command 
				conn.Open();
				cmd.ExecuteNonQuery();
				conn.Close();

				lblFeedback.Visible = true;
				lblFeedback.Text = "The fandom <strong>" + txtFandomName.Text + "</strong> was added successfully";
				BindFandomList();
			}
		}

		protected void btnSaveFandom_Click(object sender, EventArgs e)
		{
			int fandomId = int.Parse(lblFandomId.Text);
			SaveFandomById(fandomId);
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			btnSaveFandom.Visible = false;
			btnCancel.Visible = false;
			btnAddFandom.Visible = true;
			pnlFandomList.Visible = true;
			txtFandomName.Text = "";
		}
	}
}
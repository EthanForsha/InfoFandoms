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
			pnlViewFandomImages.Visible = false;

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
				Button viewButton = e.Row.FindControl("btnView") as Button;
				Button editButton = e.Row.FindControl("btnEdit") as Button;
				Button deleteButton = e.Row.FindControl("btnDelete") as Button;

				viewButton.CommandArgument = e.Row.Cells[0].Text;
				editButton.CommandArgument = e.Row.Cells[0].Text;
				deleteButton.CommandArgument = e.Row.Cells[0].Text;
			}
		}

		protected void gvFandomList_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "ViewFandom")
			{
				int fandomId = int.Parse(e.CommandArgument.ToString());
				lblFandomId.Text = e.CommandArgument.ToString();

				ViewFandomById(fandomId);
			}
			else if(e.CommandName == "EditFandom")
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

		private void ViewFandomById(int fandomId)
		{
			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

				SqlCommand cmd = new SqlCommand();
				cmd.CommandText = "SELECT FandomImagePath FROM Fandoms WHERE FandomId = @fandomId";
				cmd.Parameters.AddWithValue("@fandomId", fandomId);
				cmd.Connection = conn;
				conn.Open();

				cmd.ExecuteNonQuery();

				SqlDataReader sdr = cmd.ExecuteReader();

				if (sdr.HasRows)
				{
					sdr.Read();
					imgFandom.ImageUrl = "/Content/Fandoms/" + sdr["FandomImagePath"].ToString();
				}

				pnlViewFandomImages.Visible = true;
				pnlAddFandom.Visible = false;
				pnlFandomList.Visible = false;
				btnAddFandom.Visible = false;
				btnSaveFandom.Visible = false;
				btnCancel.Visible = false;
				lblHeader.Visible = false;
			}
		}

		private void EditFandomById(int fandomId)
		{
			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

				SqlCommand cmd = new SqlCommand();
				cmd.CommandText = "SELECT * FROM Fandoms WHERE FandomId = @fandomId";
				cmd.Parameters.AddWithValue("@fandomId", fandomId);
				cmd.Connection = conn;
				conn.Open();

				SqlDataReader sdr = cmd.ExecuteReader();
				if (sdr.Read())
				{
					txtFandomName.Text = sdr["FandomName"].ToString();
					txtFandomInfo.Text = sdr["FandomInfo"].ToString();
					imgFandom.ImageUrl = "/Content/Fandoms/" + sdr["FandomImagePath"].ToString();
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
				cmd.CommandText = "DELETE FROM Fandoms WHERE FandomId = @fandomId";
				cmd.Parameters.AddWithValue("@fandomId", fandomId);
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
				cmd.CommandText = "UPDATE Fandoms SET FandomName = @fandomName, FandomInfo = @fandomInfo, FandomImagePath = @fandomImagePath WHERE FandomId = @fandomId";
				cmd.Parameters.AddWithValue("@fandomName", txtFandomName.Text.Trim());
				cmd.Parameters.AddWithValue("@fandomInfo", txtFandomInfo.Text.Trim());
				cmd.Parameters.AddWithValue("@fandomImagePath", fuFandomImage.FileName);
				cmd.Parameters.AddWithValue("@fandomId", fandomId);
				cmd.Connection = conn;
				conn.Open();

				cmd.ExecuteNonQuery();
				BindFandomList();

				pnlFandomList.Visible = true;
				btnAddFandom.Visible = true;
				btnSaveFandom.Visible = false;
				btnCancel.Visible = false;
				txtFandomName.Visible = true;
				txtFandomInfo.Visible = true;
			}
		}

		protected void btnAddFandom_Click(object sender, EventArgs e)
		{
			if (Page.IsValid)
			{
				string imagePath = "";
				string fullPath;
				if (fuFandomImage.HasFile)
				{
					lblImage.Visible = false;
					lblFeedbackFandomInfo.Visible = false;
					lblFeedbackFandomName.Visible = false;

					imagePath = fuFandomImage.FileName;
					fuFandomImage.SaveAs(Server.MapPath(Request.ApplicationPath) + "Content/Fandoms/" + imagePath);

					using (SqlConnection conn = new SqlConnection())
					{
						conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

						SqlCommand cmd = new SqlCommand();
						cmd.CommandText = "INSERT INTO Fandoms(FandomName, FandomInfo, FandomImagePath) VALUES (@fandomName, @fandomInfo, @fandomImagePath)";
						cmd.Parameters.AddWithValue("@fandomName", txtFandomName.Text.Trim()); 
						cmd.Parameters.AddWithValue("@fandomInfo", txtFandomInfo.Text.Trim());
						cmd.Parameters.AddWithValue("@fandomImagePath", imagePath);
						cmd.Connection = conn;

						conn.Open();
						cmd.ExecuteNonQuery();

						lblFeedbackFandomName.Visible = true;
						lblFeedbackFandomName.Text = "The fandom <strong>" + txtFandomName.Text + "</strong> was added successfully";

						lblFeedbackFandomInfo.Visible = true;
						lblFeedbackFandomInfo.Text = "The fandom information was added successfully";
						BindFandomList();
					}
				}
				else
				{
					lblImage.Visible = true;
					lblImage.Text = "Please add an image";
				}
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
			txtFandomInfo.Text = "";

		}

		protected void btnGoBack_Click(object sender, EventArgs e)
		{
			pnlAddFandom.Visible = true;
			pnlFandomList.Visible = true;
			btnAddFandom.Visible = true;
		}
	}
}
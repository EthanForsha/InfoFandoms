using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fandoms
{
	public partial class MoviesPage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				BindMoviesList();
				BindFandomsDDL();
			}
		}

		protected void BindMoviesList()
		{
			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

				SqlCommand cmd = new SqlCommand();
				cmd.CommandText = "SELECT * FROM Movies";
				cmd.Connection = conn;

				SqlDataAdapter sda = new SqlDataAdapter();
				sda.SelectCommand = cmd;

				DataTable dt = new DataTable();
				conn.Open();

				sda.Fill(dt);

				gvMoviesList.DataSource = dt;
				gvMoviesList.DataBind();
				conn.Close();
			}
		}

		protected void BindFandomsDDL()
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

				ddlFandoms.DataSource = dt;
				ddlFandoms.DataTextField = "FandomName";
				ddlFandoms.DataValueField = "FandomId";
				ddlFandoms.DataBind();

				ListItem li = new ListItem("Select a Fandom", "-1");
				ddlFandoms.Items.Insert(0, li);
				ddlFandoms.SelectedIndex = 0;
			}	
		}

		protected void gvMoviesList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				Button editButton = e.Row.FindControl("btnEdit") as Button;
				Button deleteButton = e.Row.FindControl("btnDelete") as Button;

				editButton.CommandArgument = e.Row.Cells[0].Text;
				deleteButton.CommandArgument = e.Row.Cells[0].Text;
			}
		}

		protected void gvMoviesList_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "ViewMovie")
            {
				int movieId = int.Parse(e.CommandArgument.ToString());

				// ViewMovieById(movieId);
            }
			else if (e.CommandName == "EditMovie")
			{
				int movieId = int.Parse(e.CommandArgument.ToString());

				EditMovieById(movieId);
			}
			else if (e.CommandName == "DeleteMovie")
			{
				int movieId = int.Parse(e.CommandArgument.ToString());

				DeleteMovieById(movieId);
			}
		}

		private void EditMovieById(int movieId)
		{
			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

				SqlCommand cmd = new SqlCommand();
				cmd.CommandText = "SELECT * FROM Movies WHERE MovieId = " + movieId;
				cmd.Connection = conn;
				conn.Open();

				SqlDataReader sdr = cmd.ExecuteReader();
				if (sdr.Read())
				{
					txtMovieName.Text = sdr["MovieName"].ToString();
					//txtMovieInfo.Text = sdr["MovieInfo"].ToString();
					//txtMovieRuntime.Text = sdr["MovieRuntime"].ToString();
					btnAddMovie.Visible = false;
					btnSave.Visible = true;
					btnCancel.Visible = true;
					pnlMovieList.Visible = false;
				}
			}
		}

		private void DeleteMovieById(int movieId)
		{
			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

				SqlCommand cmd = new SqlCommand();
				cmd.CommandText = "DELETE FROM Movies WHERE MovieId = " + movieId;
				cmd.Connection = conn;
				conn.Open();

				cmd.ExecuteNonQuery();

				BindMoviesList();
			}
		}

        protected void btnAddMovie_Click(object sender, EventArgs e)
        {
			if(Page.IsValid)
            {
				if(ddlFandoms.SelectedIndex == 0)
                {
					lblFandomNameFeedback.Text = "Please select a Fandom.";
					lblFandomNameFeedback.Visible = true;
                }
				else if(txtMovieName.Text == null || txtMovieName.Text == "")
                {
					lblFandomNameFeedback.Visible = false;
					lblMovieNameFeedback.Text = "Please enter a valid Movie name.";
					lblMovieNameFeedback.Visible = true;
				}
				else
				{
					String MovieFandomName = (Convert.ToString(ddlFandoms.SelectedItem.Text));
					lblFandomNameFeedback.Visible = false;
					lblMovieNameFeedback.Visible = false;

					using (SqlConnection conn = new SqlConnection())
					{
						conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

						SqlCommand cmd = new SqlCommand();
						
						cmd.CommandText = "INSERT INTO Movies (MovieName, FandomId, MovieFandomName) VALUES ('" + txtMovieName.Text.Trim() + "', '" + ddlFandoms.SelectedValue + "','" + MovieFandomName + "')";

						cmd.Connection = conn;
						conn.Open();
						cmd.ExecuteNonQuery();

						lblMovieNameFeedback.Visible = true;
						lblMovieNameFeedback.Text = "The Movie <strong>" + txtMovieName.Text + "</strong> was added successfully";

						BindMoviesList();
					}
				}
            }
        }
    }
}
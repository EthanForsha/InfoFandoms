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

				ddlFandomsView.DataSource = dt;
				ddlFandomsView.DataTextField = "FandomName";
				ddlFandomsView.DataValueField = "FandomId";
				ddlFandomsView.DataBind();

				ListItem li2 = new ListItem("Select a Fandom", "-1");
				ddlFandomsView.Items.Insert(0, li2);
				ddlFandomsView.SelectedIndex = 0;
				conn.Close();
			}	
		}

		protected void gvMoviesList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				Button viewButton = e.Row.FindControl("btnViewMovie") as Button;
				Button editButton = e.Row.FindControl("btnEditMovie") as Button;
				Button deleteButton = e.Row.FindControl("btnDeleteMovie") as Button;

				viewButton.CommandArgument = e.Row.Cells[0].Text;
				editButton.CommandArgument = e.Row.Cells[0].Text;
				deleteButton.CommandArgument = e.Row.Cells[0].Text;
			}
		}

		protected void gvMoviesList_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int movieId = 0;
			if (e.CommandName == "ViewMovie")
            {
				movieId = int.Parse(e.CommandArgument.ToString());

				// ViewMovieById(movieId);
            }
			else if (e.CommandName == "EditMovie")
			{
				movieId = int.Parse(e.CommandArgument.ToString());

				EditMovieById(movieId);
			}
			else if (e.CommandName == "DeleteMovie")
			{
				movieId = int.Parse(e.CommandArgument.ToString());

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
					lblMovieNameFeedback.Visible = true;
                }
				else if(txtMovieName.Text == null || txtMovieName.Text == "")
                {
					lblFandomNameFeedback.Visible = false;
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

						lblMovieNameFeedbackSuccess.Visible = true;
						lblMovieNameFeedbackSuccess.Text = "The Movie <strong>" + txtMovieName.Text + "</strong> was added successfully";

						BindMoviesList();
					}
				}
            }
        }

        

        protected void ddlFandomsView_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (ddlFandomsView.SelectedIndex == 0)
            {
				BindMoviesList();
            }
			else
			{
				BindSelectedFandom();
			}
		}

		protected void BindSelectedFandom()
		{
			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

				SqlCommand cmd = new SqlCommand();
				cmd.CommandText = "SELECT * FROM Movies WHERE MovieId = " + ddlFandomsView.SelectedValue;
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

        protected void btnCancelAddMovie_Click(object sender, EventArgs e)
        {
			ViewMovies();
		}

		protected void btnAddNewMovie_Click(object sender, EventArgs e)
		{
			AddMovies();
		}

		protected void ViewMovies()
        {
			ddlFandomsView.SelectedIndex = 0;
			ddlFandoms.SelectedIndex = 0;
			txtMovieName.Text = "";
			lblFandomNameFeedback.Visible = false;
			lblMovieNameFeedback.Visible = false;
			lblMovieNameFeedbackSuccess.Visible = false;
			ddlFandomsView.Visible = true;
			btnAddNewMovie.Visible = true;
			pnlViewMovies.Visible = true;
			pnlAddMovie.Visible = false;
			pnlMovieList.Visible = true;
		}

		protected void AddMovies()
        {
			ddlFandoms.SelectedIndex = 0;
			txtMovieName.Text = "";
			lblFandomNameFeedback.Visible = false;
			lblMovieNameFeedback.Visible = false;
			lblMovieNameFeedbackSuccess.Visible = false;
			ddlFandomsView.SelectedIndex = 0;
			ddlFandomsView.Visible = false;
			btnAddNewMovie.Visible = false;
			pnlViewMovies.Visible = false;
			pnlAddMovie.Visible = true;
			pnlMovieList.Visible = true;
		}
    }
}
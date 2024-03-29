﻿using System;
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
				ViewMovies();
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
			if (e.CommandName == "ViewMovie")
			{
				int movieId = int.Parse(e.CommandArgument.ToString());
				lblMovieId.Text = e.CommandArgument.ToString();
				ViewMovieById(movieId);
				pnlAddMovie.Visible = false;
				pnlManageMoviesHeader.Visible = false;
				pnlMovieList.Visible = false;
				pnlViewMovies.Visible = false;
				pnlViewMovie.Visible = true;
			}
			else if (e.CommandName == "EditMovie")
			{
				int movieId = int.Parse(e.CommandArgument.ToString());
				lblMovieId.Text = movieId.ToString();
				pnlAddMovie.Visible = true;
				pnlViewMovies.Visible = false;
				ddlFandoms.Visible = false;
				lblFandomNameRequired.Visible = false;
				lblSelectFandom.Visible = false;
				EditMovieById(movieId);
			}
			else if (e.CommandName == "DeleteMovie")
			{
				int movieId = int.Parse(e.CommandArgument.ToString());
				lblMovieId.Text = e.CommandArgument.ToString();
				DeleteMovieById(movieId);
				if (pnlAddMovie.Visible)
				{
					AddMovies();
				}
				else
                {
					ViewMovies();
                }
			}
		}

		private void ViewMovieById(int movieId)
		{
			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

				SqlCommand cmd = new SqlCommand();
				cmd.CommandText = "SELECT * FROM Movies WHERE MovieId = " + movieId;
				cmd.Connection = conn;
				conn.Open();

				cmd.ExecuteNonQuery();

				SqlDataReader sdr = cmd.ExecuteReader();

				if (sdr.HasRows)
				{
					sdr.Read();
					imgMovie.ImageUrl = "/Content/Fandoms/Images/" + sdr["MovieImage"].ToString();
					lblViewMovieNameText.Text = sdr["MovieName"].ToString();
					lblViewMovieInfoText.Text = sdr["MovieInfo"].ToString();
					lblViewMovieRuntimeText.Text = sdr["MovieRuntime"].ToString() + " minutes";
					lblViewMovieReleaseDateText.Text = sdr["MovieReleaseDate"].ToString();
					lblViewMovieRatingText.Text = sdr["MovieRating"].ToString();
					lblViewMovieScoreText.Text = sdr["MovieScore"].ToString() + "% on Rotten Tomatoes";
				}
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
					ddlFandoms.SelectedIndex = 0;
					lblEditMovieInFandom.Text = "<strong>You are now editing " + sdr["MovieName"].ToString() + " in " + sdr["MovieFandomName"].ToString() + " fandom.<strong>";
					lblEditMovieInFandom.Visible = true;
					lblFandomName.Text = sdr["MovieFandomName"].ToString();
					lblMovieId.Text = sdr["MovieId"].ToString();
					txtMovieName.Text = sdr["MovieName"].ToString();
					txtMovieRuntime.Text = sdr["MovieRuntime"].ToString();
					txtMovieRelease.Text = sdr["MovieReleaseDate"].ToString();
					txtMovieRating.Text = sdr["MovieRating"].ToString();
					txtMovieScore.Text = sdr["MovieScore"].ToString();
					txtMovieInfo.Text = sdr["MovieInfo"].ToString();
					btnAddMovie.Visible = false;
					btnSave.Visible = true;
					btnCancel.Visible = true;
					btnCancelAddMovie.Visible = false;
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
			if (Page.IsValid)
			{
				lblFandomNameFeedback.Visible = false;
				lblMovieNameFeedback.Visible = false;
				lblMovieInfoFeedback.Visible = false;
				lblMovieImageFeedback.Visible = false;
				if (ddlFandoms.SelectedIndex == 0)
				{
					lblFandomNameFeedback.Visible = true;
				}
				if (txtMovieName.Text == null || txtMovieName.Text == "")
				{
					lblMovieNameFeedback.Visible = true;
				}
				if (txtMovieInfo.Text == null || txtMovieInfo.Text == "")
				{
					lblMovieInfoFeedback.Visible = true;
				}
				if (txtMovieRating.Text != "")
				{
					if (txtMovieRating.Text != "PG-13")
					{
						if (txtMovieRating.Text != "PG")
						{
							if (txtMovieRating.Text != "G")
							{
								if (txtMovieRating.Text != "R")
								{
									if (txtMovieRating.Text != "NC-17")
									{
										lblMovieRatingFeedback.Visible = true;
									}
								}
							}
						}
					}
				}
				if ((txtMovieRelease.Text != "") && (!txtMovieRelease.Text.Contains("-")))
				{
					if ((txtMovieRelease.Text != "") && (!txtMovieRelease.Text.Contains("/")))
					{
						lblMovieReleaseFeedback.Visible = true;
					}
				}
				if ((txtMovieScore.Text != "") && (!(int.Parse(txtMovieScore.Text) <= 100 && int.Parse(txtMovieScore.Text) >= 0)))
				{
					lblMovieScoreFeedback.Visible = true;
				}
				else
				{
					string imagePath = "";
					if ((fuMovieImage.HasFile) && (ddlFandoms.SelectedIndex != 0) && (txtMovieName.Text != "") && (txtMovieInfo.Text != ""))
					{
						imagePath = fuMovieImage.FileName;
						fuMovieImage.SaveAs(Server.MapPath(Request.ApplicationPath) + "Content/Fandoms/Images/" + imagePath);
						String MovieFandomName = (Convert.ToString(ddlFandoms.SelectedItem.Text));

						lblFandomNameFeedback.Visible = false;
						lblMovieNameFeedback.Visible = false;
						lblMovieInfoFeedback.Visible = false;
						lblMovieImageFeedback.Visible = false;

						using (SqlConnection conn = new SqlConnection())
						{
							conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

							SqlCommand cmd = new SqlCommand();

							cmd.CommandText = "INSERT INTO Movies VALUES ('" + txtMovieName.Text.Trim() + "', '" + ddlFandoms.SelectedValue + "', '" + MovieFandomName + "', '" + txtMovieRuntime.Text + "', '" + txtMovieRelease.Text + "', '" + txtMovieInfo.Text + "', '" + imagePath + "', '" + txtMovieRating.Text + "', '" + txtMovieScore.Text + "')";

							cmd.Connection = conn;
							conn.Open();
							cmd.ExecuteNonQuery();

							lblMovieNameFeedbackSuccess.Visible = true;
							lblMovieNameFeedbackSuccess.Text = "The Movie <strong>" + txtMovieName.Text + "</strong> was added successfully";

							txtMovieInfo.Text = "";
							txtMovieName.Text = "";
							txtMovieRating.Text = "";
							txtMovieRelease.Text = "";
							txtMovieRuntime.Text = "";
							txtMovieScore.Text = "";
							ddlFandoms.SelectedIndex = 0;
							fuMovieImage.Attributes.Clear();

							BindMoviesList();
						}
					}
					else if ((!fuMovieImage.HasFile) && (ddlFandoms.SelectedIndex != 0) && (txtMovieName.Text != "") && (txtMovieInfo.Text != ""))
					{
						String MovieFandomName = (Convert.ToString(ddlFandoms.SelectedItem.Text));

						lblFandomNameFeedback.Visible = false;
						lblMovieNameFeedback.Visible = false;
						lblMovieInfoFeedback.Visible = false;
						lblMovieImageFeedback.Visible = true;

						using (SqlConnection conn = new SqlConnection())
						{
							conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

							SqlCommand cmd = new SqlCommand();

							cmd.CommandText = "INSERT INTO Movies (MovieName, FandomId, MovieFandomName, MovieInfo, MovieRuntime, MovieReleaseDate, MovieRating, MovieScore) VALUES ('" + txtMovieName.Text.Trim() + "', '" + ddlFandoms.SelectedValue + "','" + MovieFandomName + "', '" + txtMovieInfo.Text + "', '" + txtMovieRuntime.Text + "', '" + txtMovieRelease.Text + "', '" + txtMovieRating.Text + "', '" + txtMovieScore.Text + "')";

							cmd.Connection = conn;
							conn.Open();
							cmd.ExecuteNonQuery();

							lblMovieNameFeedbackSuccess.Visible = true;
							lblMovieNameFeedbackSuccess.Text = "The Movie <strong>" + txtMovieName.Text + "</strong> was added successfully";

							txtMovieInfo.Text = "";
							txtMovieName.Text = "";
							txtMovieRating.Text = "";
							txtMovieRelease.Text = "";
							txtMovieRuntime.Text = "";
							txtMovieScore.Text = "";
							ddlFandoms.SelectedIndex = 0;
							fuMovieImage.Attributes.Clear();

							BindMoviesList();
						}
					}
				}
			}
		}

		protected void BindSelectedFandom()
		{
			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

				SqlCommand cmd = new SqlCommand();
				cmd.CommandText = "SELECT * FROM Movies WHERE FandomId = " + ddlFandomsView.SelectedValue;
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

		protected void btnSave_Click(object sender, EventArgs e)
		{
			int movieId = int.Parse(lblMovieId.Text);
			SaveMovieById(movieId);
			btnSave.Visible = false;
			btnAddMovie.Visible = true;
		}

		private void SaveMovieById(int movieId)
		{
			lblFandomNameFeedback.Visible = false;
			lblMovieNameFeedback.Visible = false;
			lblMovieInfoFeedback.Visible = false;
			lblMovieImageFeedback.Visible = false;
			if (txtMovieName.Text == null || txtMovieName.Text == "")
			{
				lblMovieNameFeedback.Visible = true;
			}
			if (txtMovieInfo.Text == null || txtMovieInfo.Text == "")
			{
				lblMovieInfoFeedback.Visible = true;
			}
			if (txtMovieRating.Text != "")
			{
				if (txtMovieRating.Text != "PG-13")
				{
					if (txtMovieRating.Text != "PG")
					{
						if (txtMovieRating.Text != "G")
						{
							if (txtMovieRating.Text != "R")
							{
								if (txtMovieRating.Text != "NC-17")
								{
									lblMovieRatingFeedback.Visible = true;
								}
							}
						}
					}
				}
			}
			if ((txtMovieRelease.Text != "") && (!txtMovieRelease.Text.Contains("-")))
			{
				if ((txtMovieRelease.Text != "") && (!txtMovieRelease.Text.Contains("/")))
				{
					lblMovieReleaseFeedback.Visible = true;
				}
			}
			if ((txtMovieScore.Text != "") && (!(int.Parse(txtMovieScore.Text) <= 100 && int.Parse(txtMovieScore.Text) >= 0)))
			{
				lblMovieScoreFeedback.Visible = true;
			}
			else
			{
				string imagePath = "";
				if ((fuMovieImage.HasFile) && (txtMovieName.Text != "") && (txtMovieInfo.Text != ""))
				{
					imagePath = fuMovieImage.FileName;
					fuMovieImage.SaveAs(Server.MapPath(Request.ApplicationPath) + "Content/Fandoms/Images/" + imagePath);
					String MovieFandomName = (lblFandomName.Text);

					lblFandomNameFeedback.Visible = false;
					lblMovieNameFeedback.Visible = false;
					lblMovieInfoFeedback.Visible = false;
					lblMovieImageFeedback.Visible = false;

					using (SqlConnection conn = new SqlConnection())
					{
						conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

						SqlCommand cmd = new SqlCommand();

						cmd.CommandText = "UPDATE Movies SET MovieName = '" + txtMovieName.Text + "', MovieFandomName = '" + MovieFandomName + "', MovieRuntime = '" + txtMovieRuntime.Text + "', MovieReleaseDate = '" + txtMovieRelease.Text + "', MovieInfo = '" + txtMovieInfo.Text + "', MovieImage = '" + imagePath + "', MovieRating = '" + txtMovieRating.Text + "', MovieScore = '" + txtMovieScore.Text + "'WHERE MovieId = " + movieId;

						cmd.Connection = conn;
						conn.Open();
						cmd.ExecuteNonQuery();

						lblMovieNameFeedbackSuccess.Visible = true;
						lblMovieNameFeedbackSuccess.Text = "The Movie <strong>" + txtMovieName.Text + "</strong> was edited successfully";

						AddMovies();
						lblMovieNameFeedbackSuccess.Visible = true;
						BindMoviesList();
					}
				}
				else if ((!fuMovieImage.HasFile) && (txtMovieName.Text != "") && (txtMovieInfo.Text != ""))
				{
					String MovieFandomName = lblFandomName.Text;

					lblFandomNameFeedback.Visible = false;
					lblMovieNameFeedback.Visible = false;
					lblMovieInfoFeedback.Visible = false;
					lblMovieImageFeedback.Visible = true;

					using (SqlConnection conn = new SqlConnection())
					{
						conn.ConnectionString = WebConfigurationManager.ConnectionStrings["FandomsConnectionString"].ConnectionString;

						SqlCommand cmd = new SqlCommand();

						cmd.CommandText = "UPDATE Movies SET MovieName = '" + txtMovieName.Text + "', MovieFandomName = '" + MovieFandomName + "', MovieRuntime = '" + txtMovieRuntime.Text + "', MovieReleaseDate = '" + txtMovieRelease.Text + "', MovieInfo = '" + txtMovieInfo.Text + "', MovieRating = '" + txtMovieRating.Text + "', MovieScore = '" + txtMovieScore.Text + "' WHERE MovieId = " + movieId;

						cmd.Connection = conn;
						conn.Open();
						cmd.ExecuteNonQuery();

						lblMovieNameFeedbackSuccess.Visible = true;
						lblMovieNameFeedbackSuccess.Text = "The Movie <strong>" + txtMovieName.Text + "</strong> was edited successfully";

						AddMovies();
						lblMovieNameFeedbackSuccess.Visible = true;
						BindMoviesList();
					}
				}
			}
		}

		private void ViewMovies()
		{
			lblEditMovieInFandom.Visible = false;
			fuMovieImage.Attributes.Clear();
			ddlFandomsView.SelectedIndex = 0;
			ddlFandoms.SelectedIndex = 0;
			txtMovieName.Text = "";
			txtMovieInfo.Text = "";
			txtMovieRating.Text = "";
			txtMovieRelease.Text = "";
			txtMovieScore.Text = "";
			lblFandomNameFeedback.Visible = false;
			lblMovieNameFeedback.Visible = false;
			lblMovieNameFeedbackSuccess.Visible = false;
			lblMovieInfoFeedback.Visible = false;
			lblMovieInfoFeedback.Visible = false;
			lblMovieImageFeedback.Visible = false;
			lblMovieRatingFeedback.Visible = false;
			lblMovieReleaseFeedback.Visible = false;
			lblMovieScoreFeedback.Visible = false;
			ddlFandomsView.Visible = true;
			btnAddNewMovie.Visible = true;
			pnlViewMovies.Visible = true;
			pnlAddMovie.Visible = false;
			pnlMovieList.Visible = true;
			pnlManageMoviesHeader.Visible = true;
			pnlViewMovie.Visible = false;
			BindMoviesList();
		}

		private void AddMovies()
		{
			pnlViewMovie.Visible = false;
			pnlManageMoviesHeader.Visible = false;
			lblFandomNameRequired.Visible = true;
			lblSelectFandom.Visible = true;	
			lblEditMovieInFandom.Visible = false;
			fuMovieImage.Attributes.Clear();
			ddlFandoms.SelectedIndex = 0;
			txtMovieName.Text = "";
			txtMovieInfo.Text = "";
			txtMovieRuntime.Text = "";
			txtMovieRating.Text = "";
			txtMovieRelease.Text = "";
			txtMovieScore.Text = "";
			lblFandomNameFeedback.Visible = false;
			lblMovieNameFeedback.Visible = false;
			lblMovieNameFeedbackSuccess.Visible = false;
			lblMovieInfoFeedback.Visible = false;
			lblMovieInfoFeedback.Visible = false;
			lblMovieImageFeedback.Visible = false;
			lblMovieRatingFeedback.Visible = false;
			lblMovieReleaseFeedback.Visible = false;
			lblMovieScoreFeedback.Visible = false;
			ddlFandomsView.SelectedIndex = 0;
			ddlFandomsView.Visible = false;
			ddlFandoms.Visible = true;
			btnAddNewMovie.Visible = false;
			pnlViewMovies.Visible = false;
			pnlAddMovie.Visible = true;
			pnlMovieList.Visible = true;
			BindMoviesList();
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			ViewMovies();
		}

        protected void btnReturn_Click(object sender, EventArgs e)
        {
			imgMovie.ImageUrl = "";
			lblViewMovieNameText.Text = "";
			lblViewMovieInfoText.Text = "";
			lblViewMovieRuntimeText.Text = "";
			lblViewMovieReleaseDateText.Text = "";
			lblViewMovieRatingText.Text = "";
			lblViewMovieScoreText.Text = "";
			pnlViewMovie.Visible = false;
			ViewMovies();
			BindMoviesList();
		}

        protected void btnViewMoviesInFandom_Click(object sender, EventArgs e)
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

        protected void ddlFandomsView_SelectedIndexChanged(object sender, EventArgs e)
        {
			if(ddlFandomsView.SelectedIndex == 0)
            {
				btnReset.Visible = false;
            }
            else
            {
				btnReset.Visible = true;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
			ddlFandomsView.SelectedIndex = 0;
			BindMoviesList();
			btnReset.Visible = false;
        }
    }
}
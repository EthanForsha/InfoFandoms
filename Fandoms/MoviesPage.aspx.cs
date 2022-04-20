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

				ddlFandoms.DataSource = dt;
				ddlFandoms.DataTextField = "FandomName";
				ddlFandoms.DataValueField = "FandomId";
				ddlFandoms.DataBind();

				ListItem li = new ListItem("Select a Fandom", "-1");
				ddlFandoms.Items.Insert(0, li);
			}
		}


	}
}
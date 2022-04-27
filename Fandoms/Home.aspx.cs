using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fandoms
{
    public partial class Home : System.Web.UI.Page
    {


        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("FandomPage.aspx");
        }

        protected void btnAddMovie_Click(object sender, EventArgs e)
        {
            Response.Redirect("MoviesPage.aspx");
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplicationOnOracle2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            //
            lblTest.Text = "avd";
            DataAccessLayer dal = new DataAccessLayer();
            string res = dal.SelectScalarValue();
            lblTest.Text = res;
        }
    }
}
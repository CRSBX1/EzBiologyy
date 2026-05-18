using System;
using System.IO;

namespace EzBiology.Pages
{
    public partial class Materials : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e){
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            if (!fuMaterial.HasFile)
            {
                lblMessage.Text      = "Please select a file first.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string allowed = ".pdf,.doc,.docx,.ppt,.pptx,.txt,.png,.jpg,.jpeg";
            string ext     = Path.GetExtension(fuMaterial.FileName).ToLower();
            if (!allowed.Contains(ext))
            {
                lblMessage.Text      = "File type not allowed.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string uploadDir = Server.MapPath("~/Uploads/");
            if (!Directory.Exists(uploadDir)) Directory.CreateDirectory(uploadDir);

            string safeName = Path.GetFileName(fuMaterial.FileName);
            fuMaterial.SaveAs(Path.Combine(uploadDir, safeName));

            // TODO: Persist file metadata to DB

            lblMessage.Text      = $"File '{safeName}' uploaded successfully.";
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }

        protected void btnFileClear_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }

        protected void btnContentUpload_Click(object sender, EventArgs e)
        {
            string name    = txtMaterialName.Text.Trim();
            string content = hfRteContent.Value;

            if (string.IsNullOrEmpty(name))
            {
                lblMessage.Text      = "Please enter a material name.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // TODO: Save name + content to DB as a LearningMaterial entity

            lblMessage.Text      = $"Material '{name}' saved successfully.";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            txtMaterialName.Text = "";
            hfRteContent.Value   = "";
            ClientScript.RegisterStartupScript(GetType(), "clearRTE",
                "document.getElementById('rteContent').innerHTML='';", true);
        }

        protected void btnContentClear_Click(object sender, EventArgs e)
        {
            txtMaterialName.Text = "";
            hfRteContent.Value   = "";
            lblMessage.Text      = "";
            ClientScript.RegisterStartupScript(GetType(), "clearRTE",
                "document.getElementById('rteContent').innerHTML='';", true);
        }
    }
}

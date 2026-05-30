using SelectPdf;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace EzBiology.Pages
{
    public partial class Materials : System.Web.UI.Page
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        private string checkQuery = "SELECT * FROM LearningMaterials WHERE MaterialName = @name";
        private string insertQuery = "INSERT INTO LearningMaterials(CreatedByUserID, MaterialName, MaterialType, FilePath, CreatedAt)" +
            "VALUES (@CreatedByUserID, @MaterialName, @MaterialType, @FilePath, @CreatedAt)";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnFileUpload_Click(object sender, EventArgs e)
        {
            if (!fuMaterial.HasFile)
            {
                lblMessage.Text = "Please select a file first.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (fuMaterial.PostedFile.ContentLength > 102400000) {
                lblMessage.Text = "File size is too large. Maximum allowed size is 100 MB.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string allowed = ".pdf,.doc,.docx,.ppt,.pptx,.txt,.png,.jpg,.jpeg";
            string ext = Path.GetExtension(fuMaterial.FileName).ToLower();
            if (!allowed.Contains(ext))
            {
                lblMessage.Text = "File type not allowed.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string uploadDir = "~/Uploads/";
            if (!Directory.Exists(Server.MapPath(uploadDir))) Directory.CreateDirectory(Server.MapPath(uploadDir));

            string safeName = Path.GetFileName(fuMaterial.FileName);
            string name = Path.GetFileNameWithoutExtension(fuMaterial.FileName);
            string fullPath = Path.Combine(uploadDir, safeName);
            string fullRootPath = Server.MapPath(fullPath);
            // TODO: Persist file metadata to DB
            conn.Open();
            SqlCommand cmd = new SqlCommand(checkQuery, conn);
            cmd.Parameters.AddWithValue("@name", name.ToLower());
            if (cmd.ExecuteScalar() != null)
            {
                lblMessage.Text = "A material with that name already exists.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                conn.Close();
                return;
            }
            lblMessage.Text = fullPath;
            cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@CreatedByUserID", Session["UserID"]);
            cmd.Parameters.AddWithValue("@MaterialName", name.ToLower());
            cmd.Parameters.AddWithValue("@MaterialType", "Biology"); //placeholder
            cmd.Parameters.AddWithValue("@FilePath", fullPath);
            cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
            cmd.ExecuteNonQuery();
            conn.Close();
            fuMaterial.SaveAs(fullRootPath);
            lblMessage.Text = $"File '{safeName}' uploaded successfully.";
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }

        protected void btnFileClear_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }

        protected void btnContentUpload_Click(object sender, EventArgs e)
        {
            string name = txtMaterialName.Text.Trim();
            string content = hfRteContent.Value;
            string category = txtCategory.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                lblMessage.Text = "Please enter a material name.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrWhiteSpace(content) || content == "<p><br></p>")
            {
                lblMessage.Text = "Please enter some content before uploading.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            //Convert HTML content to PDF
            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument doc = converter.ConvertHtmlString(content);
            string fullPath = "~/Uploads/" + name + ".pdf";
            string fullRootPath = Server.MapPath("~/Uploads/" + name + ".pdf");

            // TODO: Save name + content to DB as a LearningMaterial entity
            conn.Open();
            SqlCommand cmd = new SqlCommand(checkQuery, conn);
            cmd.Parameters.AddWithValue("@name", name.ToLower());
            if(cmd.ExecuteScalar() != null)
            {
                lblMessage.Text = "A material with that name already exists.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                conn.Close();
                return;
            }
            cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@CreatedByUserID", Session["UserID"]);
            cmd.Parameters.AddWithValue("@MaterialName", name.ToLower());
            cmd.Parameters.AddWithValue("@MaterialType", category);
            cmd.Parameters.AddWithValue("@FilePath", fullPath);
            cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
            cmd.ExecuteNonQuery();
            doc.Save(fullRootPath);
            conn.Close();

            lblMessage.Text = $"Material '{name}' saved successfully.";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            txtMaterialName.Text = "";
            hfRteContent.Value = "";
            ClientScript.RegisterStartupScript(GetType(), "clearQuill",
                "quill.setText('');", true);
        }

        protected void btnContentClear_Click(object sender, EventArgs e)
        {
            txtMaterialName.Text = "";
            hfRteContent.Value = "";
            txtCategory.Text = "";
            lblMessage.Text = "";
            ClientScript.RegisterStartupScript(GetType(), "clearQuill",
                "quill.setText('');", true);
        }
    }
}

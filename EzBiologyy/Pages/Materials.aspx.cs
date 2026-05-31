using SelectPdf;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace EzBiology.Pages
{
    //Additional feature: LLM-generated content
    public class OllamaGenerateRequest
    {
        public string model { get; set; }
        public string prompt { get; set; }
        public bool stream { get; set; }
    }

    public class OllamaGenerateResponse
    {
        public string model { get; set; }
        public string created_at { get; set; }
        public string response { get; set; }
        public bool done { get; set; }

        public long total_duration { get; set; }
        public long load_duration { get; set; }
        public int prompt_eval_count { get; set; }
        public long prompt_eval_duration { get; set; }
        public int eval_count { get; set; }
        public long eval_duration { get; set; }
        public string error { get; set; }
    }

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

        protected async void generateBtn_Click(object sender, EventArgs e)
        {
            string teacherPrompt = aiPromptTxt.Text.Trim();

            if (string.IsNullOrWhiteSpace(teacherPrompt))
            {
                lblMessage.Text = "Please enter a prompt before generating content.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                lblMessage.Text = "Generating content. Please wait...";
                lblMessage.ForeColor = System.Drawing.Color.White;

                string generatedHtml = await GenerateBiologyMaterialAsync(teacherPrompt);

                if (string.IsNullOrWhiteSpace(generatedHtml))
                {
                    lblMessage.Text = "The AI returned an empty response.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                hfRteContent.Value = generatedHtml;

                string safeHtml = Newtonsoft.Json.JsonConvert.SerializeObject(generatedHtml);

                ClientScript.RegisterStartupScript(
                    GetType(),
                    "insertGeneratedContent",
                    "quill.root.innerHTML = " + safeHtml + "; document.getElementById('" + hfRteContent.ClientID + "').value = " + safeHtml + ";",
                    true
                );

                lblMessage.Text = "AI content generated successfully. Review it before uploading.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Failed to generate content: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private async Task<string> GenerateBiologyMaterialAsync(string teacherPrompt)
        {
            string ollamaUrl = "http://localhost:11434/api/generate";

            var requestBody = new OllamaGenerateRequest
            {
                model = "gemma4:latest",
                prompt =
                    "You are helping a biology teacher create learning material. " +
                    "Generate clear, accurate biology learning content. " +
                    "Use headings, long paragraphs, and examples. Generated content length " +
                    "can be adjusted according to the teacher's request. " +
                    "Return clean HTML only using h1, h2, h3, p, and strong tags. " +
                    "DO NOT INCLUDE ANY OTHER HTML TAGS. " +
                    "DO NOT INCLUDE REVIEW QUESTIONS. " +
                    "Do not include markdown code fences. " +
                    "Teacher request: " + teacherPrompt,
                stream = false
            };

            string jsonBody = JsonConvert.SerializeObject(requestBody);

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(5);

                using (StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = await client.PostAsync(ollamaUrl, content);

                    string responseJson = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Ollama request failed: " + responseJson);
                    }

                    OllamaGenerateResponse ollamaResult =
                        JsonConvert.DeserializeObject<OllamaGenerateResponse>(responseJson);

                    if (ollamaResult == null)
                    {
                        throw new Exception("Ollama returned an empty response.");
                    }

                    if (!string.IsNullOrWhiteSpace(ollamaResult.error))
                    {
                        throw new Exception("Ollama error: " + ollamaResult.error);
                    }

                    return ollamaResult.response;
                }
            }
        }
    }
}


using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EzBiologyy.Pages
{
    public partial class StudentForum : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        private int studentId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                 Response.Redirect("Login.aspx");
            }
            
            if (!IsPostBack)
            {
                studentId = Convert.ToInt32(Session["UserID"]);
                LoadForumThreads("");
            }
        }

        private void LoadForumThreads(string searchText)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        ft.ThreadID,

                        CASE 
                            WHEN LEFT(ft.Title, 11) = '[Question] ' 
                                THEN SUBSTRING(ft.Title, 12, LEN(ft.Title))
                            WHEN LEFT(ft.Title, 13) = '[Discussion] ' 
                                THEN SUBSTRING(ft.Title, 14, LEN(ft.Title))
                            ELSE ft.Title
                        END AS Title,

                        ft.CreatedAt,
                        ISNULL(u.FullName, 'Unknown User') AS FullName,
                        LEFT(ISNULL(u.FullName, 'U'), 1) AS Initials,

                        CASE 
                            WHEN LEFT(ft.Title, 11) = '[Question] ' THEN 'question'
                            WHEN LEFT(ft.Title, 13) = '[Discussion] ' THEN 'discussion'
                            WHEN ft.Title LIKE '%?%' OR ft.Title LIKE '%help%' OR ft.Title LIKE '%confused%' THEN 'question'
                            ELSE 'discussion'
                        END AS ThreadType,

                        CASE 
                            WHEN LEFT(ft.Title, 11) = '[Question] ' THEN 'Question'
                            WHEN LEFT(ft.Title, 13) = '[Discussion] ' THEN 'Discussion'
                            WHEN ft.Title LIKE '%?%' OR ft.Title LIKE '%help%' OR ft.Title LIKE '%confused%' THEN 'Question'
                            ELSE 'Discussion'
                        END AS ThreadTypeText,

                        CASE 
                            WHEN LEFT(ft.Title, 11) = '[Question] ' THEN 'tag-question'
                            WHEN LEFT(ft.Title, 13) = '[Discussion] ' THEN 'tag-discussion'
                            WHEN ft.Title LIKE '%?%' OR ft.Title LIKE '%help%' OR ft.Title LIKE '%confused%' THEN 'tag-question'
                            ELSE 'tag-discussion'
                        END AS TagClass,

                        ISNULL((
                            SELECT COUNT(*) 
                            FROM ForumPosts fp 
                            WHERE fp.ThreadID = ft.ThreadID
                        ), 0) AS ReplyCount,

                        ISNULL((
                            SELECT TOP 1 fp.Content
                            FROM ForumPosts fp
                            WHERE fp.ThreadID = ft.ThreadID
                            ORDER BY fp.PostedAt ASC
                        ), 'Open this thread to view the discussion.') AS PreviewText

                    FROM ForumThreads ft
                    LEFT JOIN Users u ON ft.CreatedByUserID = u.UserID
                    WHERE 
                        (@SearchText = '' OR ft.Title LIKE '%' + @SearchText + '%')
                    ORDER BY ft.CreatedAt DESC;
                ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SearchText", searchText);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        rptForumThreads.DataSource = dt;
                        rptForumThreads.DataBind();

                        lblNoThreads.Visible = dt.Rows.Count == 0;
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadForumThreads(txtSearch.Text.Trim());
        }

        protected void btnCreatePost_Click(object sender, EventArgs e)
        {
            string title = txtNewTitle.Text.Trim();
            string content = txtNewContent.Text.Trim();
            string postType = ddlPostType.SelectedValue;

            if (title == "" || content == "")
            {
                lblPostMessage.Visible = true;
                lblPostMessage.Text = "Please enter both a title and content.";
                return;
            }

            string storedTitle = "[" + postType + "] " + title;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string insertThreadQuery = @"
                    INSERT INTO ForumThreads (CourseID, CreatedByUserID, Title, CreatedAt)
                    OUTPUT INSERTED.ThreadID
                    VALUES (NULL, @CreatedByUserID, @Title, GETDATE());
                ";

                int newThreadId;

                using (SqlCommand cmd = new SqlCommand(insertThreadQuery, con))
                {
                    cmd.Parameters.AddWithValue("@CreatedByUserID", studentId);
                    cmd.Parameters.AddWithValue("@Title", storedTitle);

                    newThreadId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string insertPostQuery = @"
                    INSERT INTO ForumPosts (ThreadID, UserID, Content, PostedAt)
                    VALUES (@ThreadID, @UserID, @Content, GETDATE());
                ";

                using (SqlCommand cmd = new SqlCommand(insertPostQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ThreadID", newThreadId);
                    cmd.Parameters.AddWithValue("@UserID", studentId);
                    cmd.Parameters.AddWithValue("@Content", content);

                    cmd.ExecuteNonQuery();
                }
            }

            txtNewTitle.Text = "";
            txtNewContent.Text = "";
            lblPostMessage.Visible = false;

            LoadForumThreads("");
        }
    }
}
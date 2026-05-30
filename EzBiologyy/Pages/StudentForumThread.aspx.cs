using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EzBiologyy.Pages
{
    public partial class StudentForumThread : System.Web.UI.Page
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        private int studentId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            studentId = Convert.ToInt32(Session["UserID"]);
            if (!IsPostBack)
            {
                System.Diagnostics.Debug.WriteLine(Session["UserID"]);
                LoadThread();
                LoadReplies();
            }
        }

        private int GetThreadId()
        {
            if (Request.QueryString["ThreadID"] == null)
            {
                Response.Redirect("StudentForum.aspx");
                return 0;
            }

            int threadId;

            if (!int.TryParse(Request.QueryString["ThreadID"], out threadId))
            {
                Response.Redirect("StudentForum.aspx");
                return 0;
            }

            return threadId;
        }

        private void LoadThread()
        {
            int threadId = GetThreadId();

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
                        END AS CleanTitle,

                        ft.Title AS OriginalTitle,
                        ft.CreatedAt,
                        ISNULL(u.FullName, 'Unknown User') AS FullName,
                        LEFT(ISNULL(u.FullName, 'U'), 1) AS Initials,

                        ISNULL((
                            SELECT TOP 1 fp.Content
                            FROM ForumPosts fp
                            WHERE fp.ThreadID = ft.ThreadID
                            ORDER BY fp.PostedAt ASC
                        ), '') AS FirstPostContent

                    FROM ForumThreads ft
                    LEFT JOIN Users u ON ft.CreatedByUserID = u.UserID
                    WHERE ft.ThreadID = @ThreadID;
                ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ThreadID", threadId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            Response.Redirect("StudentForum.aspx");
                            return;
                        }

                        DataRow row = dt.Rows[0];

                        string cleanTitle = row["CleanTitle"].ToString();
                        string originalTitle = row["OriginalTitle"].ToString();

                        lblBreadcrumbTitle.Text = cleanTitle;
                        lblThreadTitle.Text = cleanTitle;
                        lblThreadAuthor.Text = row["FullName"].ToString();
                        lblThreadInitials.Text = row["Initials"].ToString();
                        lblThreadTime.Text = row["CreatedAt"].ToString();
                        lblThreadBody.Text = row["FirstPostContent"].ToString();

                        if (originalTitle.StartsWith("[Question] "))
                        {
                            lblThreadType.Text = "Question";
                            lblThreadType.CssClass = "forum-tag tag-question";
                        }
                        else if (originalTitle.StartsWith("[Discussion] "))
                        {
                            lblThreadType.Text = "Discussion";
                            lblThreadType.CssClass = "forum-tag tag-discussion";
                        }
                        else
                        {
                            bool isQuestion = cleanTitle.Contains("?") ||
                                              cleanTitle.ToLower().Contains("question") ||
                                              cleanTitle.ToLower().Contains("help") ||
                                              cleanTitle.ToLower().Contains("confused");

                            lblThreadType.Text = isQuestion ? "Question" : "Discussion";
                            lblThreadType.CssClass = isQuestion ? "forum-tag tag-question" : "forum-tag tag-discussion";
                        }
                    }
                }
            }
        }

        private void LoadReplies()
        {
            int threadId = GetThreadId();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        fp.PostID,
                        fp.Content,
                        fp.PostedAt,
                        ISNULL(u.FullName, 'Unknown User') AS FullName,
                        LEFT(ISNULL(u.FullName, 'U'), 1) AS Initials
                    FROM ForumPosts fp
                    LEFT JOIN Users u ON fp.UserID = u.UserID
                    WHERE fp.ThreadID = @ThreadID
                    ORDER BY fp.PostedAt ASC;
                ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ThreadID", threadId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        rptReplies.DataSource = dt;
                        rptReplies.DataBind();

                        int replyCount = dt.Rows.Count;

                        lblReplyCount.Text = replyCount.ToString();
                        lblRepliesLabel.Text = replyCount + " replies";
                    }
                }
            }
        }

        protected void btnReply_Click(object sender, EventArgs e)
        {
            string replyText = txtReply.Text.Trim();

            if (replyText == "")
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please write a reply first.";
                return;
            }

            int threadId = GetThreadId();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO ForumPosts (ThreadID, UserID, Content, PostedAt)
                    VALUES (@ThreadID, @UserID, @Content, GETDATE());
                ";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ThreadID", threadId);
                    cmd.Parameters.AddWithValue("@UserID", studentId);
                    cmd.Parameters.AddWithValue("@Content", replyText);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            txtReply.Text = "";
            lblMessage.Visible = false;

            LoadThread();
            LoadReplies();
        }

        protected void btnDeleteThread_Click(object sender, EventArgs e)
        {
            int threadId = GetThreadId();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string deletePostsQuery = @"
                    DELETE FROM ForumPosts
                    WHERE ThreadID = @ThreadID;
                ";

                using (SqlCommand cmd = new SqlCommand(deletePostsQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ThreadID", threadId);
                    cmd.ExecuteNonQuery();
                }

                string deleteThreadQuery = @"
                    DELETE FROM ForumThreads
                    WHERE ThreadID = @ThreadID;
                ";

                using (SqlCommand cmd = new SqlCommand(deleteThreadQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ThreadID", threadId);
                    cmd.ExecuteNonQuery();
                }
            }

            Response.Redirect("StudentForum.aspx");
        }
    }
}
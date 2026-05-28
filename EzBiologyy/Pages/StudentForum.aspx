<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentForum.aspx.cs" Inherits="EzBiologyy.Pages.StudentForum" MasterPageFile="~/Student.Master"%>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-wrapper">
        <div class="page-inner">

            <div class="page-title">Forums</div>
            <div class="page-desc">Discuss topics, ask questions, and share resources with your class.</div>

            <div class="forums-toolbar">
                <asp:TextBox 
                    ID="txtSearch" 
                    runat="server" 
                    CssClass="search-box" 
                    placeholder="Search discussions...">
                </asp:TextBox>

                <asp:Button 
                    ID="btnSearch" 
                    runat="server" 
                    Text="Search" 
                    CssClass="new-post-btn" 
                    OnClick="btnSearch_Click" />

                <button type="button" class="new-post-btn" onclick="showNewPostForm()">+ New Post</button>
            </div>

            <div id="newPostForm" style="display:none; margin-bottom:24px;">
                <div class="course-detail-card" style="max-width:760px;">
                    <div class="course-detail-header">
                        <h3>Create New Forum Post</h3>
                        <p>Ask a question or start a discussion.</p>
                    </div>

                    <div style="padding:18px;">
                        <asp:DropDownList 

                          ID="ddlPostType" 
                          runat="server" 
                          CssClass="search-box" 
                          style="width:100%; margin-bottom:12px;">
                          <asp:ListItem Text="Question" Value="Question"></asp:ListItem>
                          <asp:ListItem Text="Discussion" Value="Discussion"></asp:ListItem>
                          </asp:DropDownList>

                        <asp:TextBox 
                            ID="txtNewTitle" 
                            runat="server" 
                            CssClass="search-box" 
                            placeholder="Post title..."
                            style="width:100%; margin-bottom:12px;">
                        </asp:TextBox>

                        <asp:TextBox 
                            ID="txtNewContent" 
                            runat="server" 
                            TextMode="MultiLine" 
                            CssClass="search-box" 
                            placeholder="Write your post..."
                            style="width:100%; min-height:90px; margin-bottom:12px;">
                        </asp:TextBox>

                        <asp:Button 
                            ID="btnCreatePost" 
                            runat="server" 
                            Text="Post" 
                            CssClass="new-post-btn" 
                            OnClick="btnCreatePost_Click" />

                        <button type="button" class="filter-btn" onclick="hideNewPostForm()">Cancel</button>

                        <br />
                        <asp:Label ID="lblPostMessage" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="filter-bar">
                <button type="button" class="filter-btn active" onclick="filterForum('all', this)">All</button>
                <button type="button" class="filter-btn" onclick="filterForum('question', this)">Questions</button>
                <button type="button" class="filter-btn" onclick="filterForum('discussion', this)">Discussions</button>
            </div>

            <div class="forum-list">
                <asp:Repeater ID="rptForumThreads" runat="server">
                    <ItemTemplate>
                        <a class="forum-post-card" 
                           data-type='<%# Eval("ThreadType") %>' 
                           href='StudentForumThread.aspx?ThreadID=<%# Eval("ThreadID") %>'>

                            <div class="forum-avatar">
                                <%# Eval("Initials") %>
                            </div>

                            <div class="forum-post-body">
                                <div class="forum-post-top">
                                    <span class="forum-post-title"><%# Eval("Title") %></span>
                                    <span class='forum-tag <%# Eval("TagClass") %>'><%# Eval("ThreadTypeText") %></span>
                                </div>

                                <div class="forum-post-preview">
                                    <%# Eval("PreviewText") %>
                                </div>

                                <div class="forum-post-stats">
                                    <span>💬 <%# Eval("ReplyCount") %> replies</span>
                                    <span style="color:var(--text-muted)"><%# Eval("FullName") %></span>
                                </div>
                            </div>

                            <span class="forum-time"><%# Eval("CreatedAt") %></span>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>

                <asp:Label 
                    ID="lblNoThreads" 
                    runat="server" 
                    Text="No forum threads found." 
                    Visible="false">
                </asp:Label>
            </div>

        </div>
    </div>

    <script>
        function showNewPostForm() {
            document.getElementById("newPostForm").style.display = "block";
        }

        function hideNewPostForm() {
            document.getElementById("newPostForm").style.display = "none";
        }

        function filterForum(type, button) {
            var cards = document.querySelectorAll(".forum-post-card");

            cards.forEach(function (card) {
                if (type === "all" || card.getAttribute("data-type") === type) {
                    card.style.display = "flex";
                } else {
                    card.style.display = "none";
                }
            });

            var buttons = document.querySelectorAll(".filter-btn");
            buttons.forEach(function (btn) {
                btn.classList.remove("active");
            });

            button.classList.add("active");
        }
    </script>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentForumThread.aspx.cs" Inherits="EzBiologyy.Pages.StudentForumThread" MasterPageFile="~/Student.Master" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-wrapper">
        <div class="page-inner">

            <div class="breadcrumb">
                <a href="StudentForum.aspx">Forums</a> › 
                <asp:Label ID="lblBreadcrumbTitle" runat="server" Text="Thread"></asp:Label>
            </div>

            <div class="thread-wrap">
                <div class="thread-post">

                    <div class="thread-post-header">
                        <div class="thread-meta">
                            <div class="thread-avatar">
                                <asp:Label ID="lblThreadInitials" runat="server" Text="U"></asp:Label>
                            </div>

                            <div>
                                <div class="thread-author">
                                    <asp:Label ID="lblThreadAuthor" runat="server" Text="Unknown User"></asp:Label>
                                    <asp:Label ID="lblThreadType" runat="server" CssClass="forum-tag tag-question" style="margin-left:6px;" Text="Discussion"></asp:Label>
                                </div>
                                <div class="thread-time">
                                    <asp:Label ID="lblThreadTime" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="thread-title">
                            <asp:Label ID="lblThreadTitle" runat="server" Text="Thread Title"></asp:Label>
                        </div>

                        <div class="thread-body">
                            <asp:Label ID="lblThreadBody" runat="server" Text=""></asp:Label>
                        </div>
                    </div>

<div class="thread-actions">
    <asp:Button 
        ID="btnDeleteThread" 
        runat="server" 
        Text="🗑 Delete Post" 
        CssClass="thread-action-btn" 
        OnClick="btnDeleteThread_Click"
        OnClientClick="return confirm('Are you sure you want to delete this forum post?');" />

    <span class="thread-action-btn">👁 Views</span>

    <span class="thread-action-btn">
        💬 <asp:Label ID="lblReplyCount" runat="server" Text="0"></asp:Label> replies
    </span>

    <button type="button" class="thread-action-btn" onclick="copyThreadLink()">↗ Share</button>
</div>

                    <div class="replies-label">
                        <asp:Label ID="lblRepliesLabel" runat="server" Text="0 replies"></asp:Label>
                    </div>

                    <asp:Repeater ID="rptReplies" runat="server">
                        <ItemTemplate>
                            <div class="reply">
                                <div class="reply-avatar">
                                    <%# Eval("Initials") %>
                                </div>

                                <div class="reply-content">
                                    <div class="reply-meta">
                                        <span class="reply-author"><%# Eval("FullName") %></span>
                                        <span class="reply-time"><%# Eval("PostedAt") %></span>
                                    </div>

                                    <div class="reply-body">
                                        <%# Eval("Content") %>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <div class="reply-input-row">
                        <div class="reply-me">ME</div>

                        <asp:TextBox 
                            ID="txtReply" 
                            runat="server" 
                            TextMode="MultiLine" 
                            CssClass="reply-input" 
                            placeholder="Write a reply..." 
                            Rows="1">
                        </asp:TextBox>

                        <asp:Button 
                            ID="btnReply" 
                            runat="server" 
                            Text="Reply" 
                            CssClass="reply-send-btn" 
                            OnClick="btnReply_Click" />
                    </div>

                    <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>

                </div>
            </div>

        </div>
    </div>
<script>
    function copyThreadLink() {
        navigator.clipboard.writeText(window.location.href)
            .then(function () {
                alert("Thread link copied!");
            })
            .catch(function () {
                alert("Could not copy link. Please copy the URL manually.");
            });
    }
</script>
</asp:Content>
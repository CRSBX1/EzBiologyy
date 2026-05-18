<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentForum.aspx.cs" Inherits="EzBiologyy.Pages.StudentForum" MasterPageFile="~/Student.Master"%>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server"> </asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
      <div class="page-wrapper">
      <div class="page-inner">

        <div class="page-title">Forums</div>
        <div class="page-desc">Discuss topics, ask questions, and share resources with your class.</div>

        <div class="forums-toolbar">
          <input class="search-box" type="text" placeholder="Search discussions..." />
          <button class="new-post-btn">+ New Post</button>
        </div>

        <div class="filter-bar">
          <button class="filter-btn active" onclick="setFilter('all', this)">All</button>
          <button class="filter-btn" onclick="setFilter('questions', this)">Questions</button>
          <button class="filter-btn" onclick="setFilter('discussions', this)">Discussions</button>
        </div>

        <div class="forum-list">

          <a class="forum-post-card" href="forum-thread.html">
            <div class="forum-avatar">SK</div>
            <div class="forum-post-body">
              <div class="forum-post-top">
                <span class="forum-post-title">How does meiosis differ from mitosis?</span>
                <span class="forum-tag tag-question">Question</span>
              </div>
              <div class="forum-post-preview">I keep getting confused between the two — can someone explain the key differences?</div>
              <div class="forum-post-stats">
                <span>💬 7 replies</span>
                <span>👁 54 views</span>
                <span style="color:var(--text-muted)">Sarah K.</span>
              </div>
            </div>
            <span class="forum-time">5h ago</span>
          </a>

          <div class="forum-post-card">
            <div class="forum-avatar">PM</div>
            <div class="forum-post-body">
              <div class="forum-post-top">
                <span class="forum-post-title">Is CRISPR the future of medicine?</span>
                <span class="forum-tag tag-discussion">Discussion</span>
              </div>
              <div class="forum-post-preview">After the gene editing topic in Plant Biology, got me thinking — how far should we go?</div>
              <div class="forum-post-stats">
                <span>💬 12 replies</span>
                <span>👁 89 views</span>
                <span style="color:var(--text-muted)">Priya M.</span>
              </div>
            </div>
            <span class="forum-time">2d ago</span>
          </div>

          <a class="forum-post-card" href="forum-thread.html">
            <div class="forum-avatar you">ME</div>
            <div class="forum-post-body">
              <div class="forum-post-top">
                <span class="forum-post-title">Confused about active vs passive transport</span>
                <span class="forum-tag tag-question">Question</span>
              </div>
              <div class="forum-post-preview">When exactly do cells use ATP for transport? I can't figure out the boundary...</div>
              <div class="forum-post-stats">
                <span>💬 5 replies</span>
                <span>👁 19 views</span>
                <span style="color:var(--text-muted)">You</span>
              </div>
            </div>
            <span class="forum-time">3d ago</span>
          </a>

        </div>
      </div>
    </div>
</asp:Content>

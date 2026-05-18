<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentForumThread.aspx.cs" Inherits="EzBiologyy.Pages.StudentForumThread" MasterPageFile="~/Student.Master" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="page-wrapper">
      <div class="page-inner">
        <div class="breadcrumb">
          <a href="forums.html">Forums</a> › How does meiosis differ from mitosis?
        </div>

        <div class="thread-wrap">
          <div class="thread-post">

            <div class="thread-post-header">
              <div class="thread-meta">
                <div class="thread-avatar">SK</div>
                <div>
                  <div class="thread-author">
                    Sarah K.
                    <span class="forum-tag tag-question" style="margin-left:6px;">Question</span>
                  </div>
                  <div class="thread-time">5 hours ago</div>
                </div>
              </div>
              <div class="thread-title">How does meiosis differ from mitosis?</div>
              <div class="thread-body">
                I keep getting confused between the two — can someone explain the key differences?
                Specifically, I'm struggling to understand why meiosis produces four cells and whether
                crossing over happens in mitosis at all. Any help would be appreciated!
              </div>
            </div>

            <div class="thread-actions">
              <button class="thread-action-btn">👁 54 views</button>
              <button class="thread-action-btn">💬 7 replies</button>
              <button class="thread-action-btn">↗ Share</button>
            </div>

            <div class="replies-label">7 replies</div>

            <div class="reply">
              <div class="reply-avatar">JL</div>
              <div class="reply-content">
                <div class="reply-meta">
                  <span class="reply-author">James L.</span>
                  <span class="reply-time">4h ago</span>
                </div>
                <div class="reply-body">
                  Great question! The main difference is the purpose: mitosis makes two identical daughter
                  cells for growth and repair, while meiosis makes four genetically unique cells for
                  reproduction. Crossing over only happens in meiosis during prophase I — that's what
                  creates genetic variation!
                </div>
              </div>
            </div>

            <div class="reply">
              <div class="reply-avatar" style="background:var(--accent-bg);color:var(--accent)">PM</div>
              <div class="reply-content">
                <div class="reply-meta">
                  <span class="reply-author">Priya M.</span>
                  <span class="reply-time">3h ago</span>
                </div>
                <div class="reply-body">
                  To add to James — think of it this way: mitosis = 1 division → 2 cells (diploid).
                  Meiosis = 2 divisions → 4 cells (haploid). The second division in meiosis is basically
                  like mitosis but without DNA replication beforehand.
                </div>
              </div>
            </div>

            <div class="reply">
              <div class="reply-avatar">TC</div>
              <div class="reply-content">
                <div class="reply-meta">
                  <span class="reply-author">Tom C.</span>
                  <span class="reply-time">2h ago</span>
                </div>
                <div class="reply-body">
                  A mnemonic that helped me:
                  <em style="color:rgba(255,255,255,0.85)">"Mitosis makes More, Meiosis makes Mates"</em>
                  — more body cells vs. gametes for reproduction.
                </div>
              </div>
            </div>

            <div class="reply-input-row">
              <div class="reply-me">ME</div>
              <textarea class="reply-input" placeholder="Write a reply..." rows="1"></textarea>
              <button class="reply-send-btn">Reply</button>
            </div>

          </div>
        </div>

      </div>
    </div>
</asp:Content>
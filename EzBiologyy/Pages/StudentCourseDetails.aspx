<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentCourseDetails.aspx.cs" Inherits="EzBiologyy.Pages.StudentCourseDetails" MasterPageFile="~/Student.Master" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server"> </asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-wrapper">
  <div class="page-inner">

    <div class="breadcrumb">
      <a href="index.html">Courses</a> › Cell Biology
    </div>
    <div class="page-title">Cell Biology</div>
    <div class="page-status">In Progress</div>

    <div class="course-detail-card">
      <div class="course-detail-header">
        <h3>Course materials</h3>
        <p>Select a section to begin</p>
      </div>

      <!-- MATERIALS -->
      <div class="acc-section">
        <div class="acc-header" onclick="toggleAcc('mat')">
          <div class="acc-left">
            <div class="acc-icon">
              <svg width="15" height="15" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.7)" stroke-width="1.5">
                <rect x="3" y="2" width="10" height="12" rx="1.5"/>
                <line x1="5.5" y1="5.5" x2="10.5" y2="5.5"/>
                <line x1="5.5" y1="8" x2="10.5" y2="8"/>
                <line x1="5.5" y1="10.5" x2="8.5" y2="10.5"/>
              </svg>
            </div>
            <div>
              <div class="acc-title">Materials</div>
              <div class="acc-sub">Lecture notes and readings</div>
            </div>
          </div>
          <span class="acc-chev" id="mat-chev">›</span>
        </div>
        <div class="acc-body" id="mat-body">
          <div class="acc-item">
            <div class="acc-item-left">
              <svg width="13" height="13" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.4)" stroke-width="1.3"><rect x="3" y="2" width="10" height="12" rx="1"/><line x1="5.5" y1="6" x2="10.5" y2="6"/><line x1="5.5" y1="9" x2="8.5" y2="9"/></svg>
              <div>
                <div class="acc-item-name">Chapter 1 – Cell Structure</div>
                <div class="acc-item-meta">PDF · 12 pages</div>
              </div>
            </div>
            <button class="mark-btn" onclick="toggleDone(this)">Mark as done</button>
          </div>
          <div class="acc-item">
            <div class="acc-item-left">
              <svg width="13" height="13" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.4)" stroke-width="1.3"><rect x="3" y="2" width="10" height="12" rx="1"/><line x1="5.5" y1="6" x2="10.5" y2="6"/><line x1="5.5" y1="9" x2="8.5" y2="9"/></svg>
              <div>
                <div class="acc-item-name">Chapter 2 – Organelles</div>
                <div class="acc-item-meta">PDF · 18 pages</div>
              </div>
            </div>
            <button class="mark-btn" onclick="toggleDone(this)">Mark as done</button>
          </div>
          <div class="acc-item">
            <div class="acc-item-left">
              <svg width="13" height="13" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.4)" stroke-width="1.3"><rect x="3" y="2" width="10" height="12" rx="1"/><line x1="5.5" y1="6" x2="10.5" y2="6"/><line x1="5.5" y1="9" x2="8.5" y2="9"/></svg>
              <div>
                <div class="acc-item-name">Chapter 3 – Cell Membrane &amp; Transport</div>
                <div class="acc-item-meta">PDF · 15 pages</div>
              </div>
            </div>
            <button class="mark-btn" onclick="toggleDone(this)">Mark as done</button>
          </div>
          <div class="acc-item">
            <div class="acc-item-left">
              <svg width="13" height="13" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.4)" stroke-width="1.3"><polygon points="3,2 13,8 3,14"/></svg>
              <div>
                <div class="acc-item-name">Lecture Recording – Week 3</div>
                <div class="acc-item-meta">Video · 42 min</div>
              </div>
            </div>
            <button class="mark-btn" onclick="toggleDone(this)">Mark as done</button>
          </div>
        </div>
      </div>

      <!-- QUIZ -->
      <div class="acc-section">
        <div class="acc-header" onclick="toggleAcc('quiz')">
          <div class="acc-left">
            <div class="acc-icon">
              <svg width="15" height="15" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.7)" stroke-width="1.5">
                <circle cx="8" cy="8" r="5.5"/>
                <path d="M8 5.5v.5a1.5 1.5 0 010 3"/>
                <circle cx="8" cy="11" r=".4" fill="rgba(255,255,255,0.7)" stroke="none"/>
              </svg>
            </div>
            <div>
              <div class="acc-title">Quiz</div>
              <div class="acc-sub">Test your knowledge</div>
            </div>
          </div>
          <span class="acc-chev" id="quiz-chev">›</span>
        </div>
        <div class="acc-body" id="quiz-body">
          <div class="acc-item">
            <div class="acc-item-left">
              <svg width="13" height="13" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.4)" stroke-width="1.3"><circle cx="8" cy="8" r="5.5"/><path d="M8 5.5v.5a1.5 1.5 0 010 3"/></svg>
              <div>
                <div class="acc-item-name">Quiz 1 – Cell Structure Basics</div>
                <div class="acc-item-meta">10 questions · Not started</div>
              </div>
            </div>
            <button class="mark-btn" onclick="toggleDone(this)">Mark as done</button>
          </div>
          <div class="acc-item">
            <div class="acc-item-left">
              <svg width="13" height="13" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.4)" stroke-width="1.3"><circle cx="8" cy="8" r="5.5"/><path d="M8 5.5v.5a1.5 1.5 0 010 3"/></svg>
              <div>
                <div class="acc-item-name">Quiz 2 – Organelles &amp; Functions</div>
                <div class="acc-item-meta">15 questions · Not started</div>
              </div>
            </div>
            <button class="mark-btn" onclick="toggleDone(this)">Mark as done</button>
          </div>
          <div class="acc-item">
            <div class="acc-item-left">
              <svg width="13" height="13" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.4)" stroke-width="1.3"><circle cx="8" cy="8" r="5.5"/><path d="M8 5.5v.5a1.5 1.5 0 010 3"/></svg>
              <div>
                <div class="acc-item-name">Quiz 3 – Transport Mechanisms</div>
                <div class="acc-item-meta">12 questions · Not started</div>
              </div>
            </div>
            <button class="mark-btn" onclick="toggleDone(this)">Mark as done</button>
          </div>
        </div>
      </div>

      <!-- ASSESSMENTS -->
      <div class="acc-section">
        <div class="acc-header" onclick="toggleAcc('assess')">
          <div class="acc-left">
            <div class="acc-icon">
              <svg width="15" height="15" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.7)" stroke-width="1.5">
                <rect x="2.5" y="2.5" width="11" height="11" rx="1.5"/>
                <path d="M5 8l2 2 4-4"/>
              </svg>
            </div>
            <div>
              <div class="acc-title">Assessments</div>
              <div class="acc-sub">Graded submissions</div>
            </div>
          </div>
          <span class="acc-chev" id="assess-chev">›</span>
        </div>
        <div class="acc-body" id="assess-body">
          <div class="acc-item">
            <div class="acc-item-left">
              <svg width="13" height="13" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.4)" stroke-width="1.3"><path d="M4 2h8a1 1 0 011 1v10a1 1 0 01-1 1H4a1 1 0 01-1-1V3a1 1 0 011-1z"/><path d="M6 6h4M6 9h2"/></svg>
              <div>
                <div class="acc-item-name">Module Descriptor</div>
                <div class="acc-item-meta">Reference document</div>
              </div>
            </div>
            <button class="mark-btn" onclick="toggleDone(this)">Mark as done</button>
          </div>
          <div class="acc-item">
            <div class="acc-item-left">
              <svg width="13" height="13" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.4)" stroke-width="1.3"><path d="M4 2h8a1 1 0 011 1v10a1 1 0 01-1 1H4a1 1 0 01-1-1V3a1 1 0 011-1z"/><path d="M6 6h4M6 9h2"/></svg>
              <div>
                <div class="acc-item-name">Module Handbook</div>
                <div class="acc-item-meta">Reference document</div>
              </div>
            </div>
            <button class="mark-btn" onclick="toggleDone(this)">Mark as done</button>
          </div>
          <div class="acc-item">
            <div class="acc-item-left">
              <svg width="13" height="13" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.4)" stroke-width="1.3"><path d="M4 2h8a1 1 0 011 1v10a1 1 0 01-1 1H4a1 1 0 01-1-1V3a1 1 0 011-1z"/><path d="M6 6h4M6 9h2"/></svg>
              <div>
                <div class="acc-item-name">Sample Coursework Papers, Model Answers &amp; Rubrics</div>
                <div class="acc-item-meta">Coursework prep</div>
              </div>
            </div>
            <button class="mark-btn" onclick="toggleDone(this)">Mark as done</button>
          </div>
          <div class="acc-item">
            <div class="acc-item-left">
              <svg width="13" height="13" viewBox="0 0 16 16" fill="none" stroke="rgba(255,255,255,0.4)" stroke-width="1.3"><path d="M4 2h8a1 1 0 011 1v10a1 1 0 01-1 1H4a1 1 0 01-1-1V3a1 1 0 011-1z"/><path d="M6 6h4M6 9h2"/></svg>
              <div>
                <div class="acc-item-name">Sample Final Assessment Papers, Model Answers &amp; Rubrics</div>
                <div class="acc-item-meta">Final exam prep</div>
              </div>
            </div>
            <button class="mark-btn" onclick="toggleDone(this)">Mark as done</button>
          </div>
        </div>
      </div>

    </div>
  </div>
</div>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentAssessment.aspx.cs" Inherits="EzBiologyy.Pages.StudentAssessment" MasterPageFile="~/Student.Master"%>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server" />
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">

    <h1>Plant Growth Lab Report</h1>

    <div class="quiz-container">

        <div class="quiz-sidebar">
            <h3>Questions</h3>
            <p>Question 1 of 5</p>

            <div class="question-grid">
                <button class="q-btn active">1</button>
                <button class="q-btn">2</button>
                <button class="q-btn">3</button>
                <button class="q-btn">4</button>
                <button class="q-btn">5</button>
            </div>
        </div>

        <div class="quiz-main">

            <div class="timer">
                Time Remaining: <b>30:00</b>
            </div>

            <h3>Question 1 (MCQ)</h3>
            <p>Which factor is most important for plant growth?</p>

            <div class="options">
                <label class="option"><input type="radio"> Sunlight</label>
                <label class="option"><input type="radio"> Oxygen</label>
                <label class="option"><input type="radio"> Carbon dioxide</label>
                <label class="option"><input type="radio"> Nitrogen</label>
            </div>

            <br><br>

            <h3>Question 2 (Fill in the blanks)</h3>
            <p>Plants make food using <input type="text" class="fill-input"></p>
            <p>This process is called <input type="text" class="fill-input"></p>

            <div class="quiz-actions">
                <button class="nav-btn">Previous</button>
                <button class="nav-btn">Next</button>
                <button class="submit-btn">Submit</button>
            </div>

        </div>
    </div>
</div>
</asp:Content>
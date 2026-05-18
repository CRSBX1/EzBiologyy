<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentQuiz.aspx.cs" Inherits="EzBiologyy.Pages.StudentQuiz" MasterPageFile="~/Student.Master"%>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server" />
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">

        <h1>Cell Structure Quiz</h1>

        <div class="quiz-container">

            <!-- LEFT PANEL -->
            <div class="quiz-sidebar">
                <h3>Questions</h3>
                <p>Question 1 of 10</p>

                <div class="question-grid">
                    <button class="q-btn active">1</button>
                    <button class="q-btn">2</button>
                    <button class="q-btn">3</button>
                    <button class="q-btn">4</button>
                    <button class="q-btn">5</button>
                    <button class="q-btn">6</button>
                    <button class="q-btn">7</button>
                    <button class="q-btn">8</button>
                    <button class="q-btn">9</button>
                    <button class="q-btn">10</button>
                </div>
            </div>

            <!-- RIGHT PANEL -->
            <div class="quiz-main">

                <div class="timer">
                    Time Remaining: <b>19:45</b>
                </div>

                <h3>Question 1</h3>
                <p>Which organelle is known as the powerhouse of the cell?</p>

                <div class="options">
                    <label class="option"><input type="radio" name="q1"> A. Nucleus</label>
                    <label class="option"><input type="radio" name="q1"> B. Mitochondria</label>
                    <label class="option"><input type="radio" name="q1"> C. Ribosome</label>
                    <label class="option"><input type="radio" name="q1"> D. Golgi Apparatus</label>
                </div>

                <div class="quiz-actions">
                    <button class="nav-btn">Previous</button>
                    <button class="nav-btn">Next</button>
                    <button class="submit-btn">Submit</button>
                </div>

            </div>

        </div>

    </div>
</asp:Content>
<%@ Page Language="C#" AutoEventWireup="true"
CodeBehind="TakeQuiz.aspx.cs"
Inherits="EzBiologyy.Pages.TakeQuiz"
MasterPageFile="~/Student.Master"%>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">

<script>

    var seconds;

    function startTimer() {
        var timer =
            document.getElementById(
                "timer");

        var countdown =
            setInterval(function () {
                seconds--;

                var mins =
                    Math.floor(
                        seconds / 60);

                var secs =
                    seconds % 60;

                timer.innerHTML =
                    mins + ":"
                    + (secs < 10 ? "0" : "")
                    + secs;

                if (seconds <= 0) {
                    clearInterval(
                        countdown);

                    alert(
                        "Time is up!");

                    document.getElementById(
            '<%= submitBtn.ClientID %>')
            .click();
        }

    },1000);
}

window.onload=function()
{
    seconds=
    <%= TimerSeconds %>;

        startTimer();
    }

</script>

</asp:Content>


<asp:Content
ContentPlaceHolderID="MainContent"
runat="server">

<div class="content">

<h1>

<asp:Label
ID="quizTitleLbl"
runat="server">
</asp:Label>

</h1>


<div style="
position:fixed;
top:90px;
right:30px;
background:rgba(0,0,0,0.75);
padding:15px 25px;
border-radius:12px;
color:white;
font-size:22px;
font-weight:bold;
z-index:9999;
box-shadow:0px 0px 10px rgba(0,0,0,0.5);">

⏱ Time Left:

<span id="timer"></span>

</div>


<asp:Repeater
ID="questionRepeater"
runat="server"
OnItemDataBound=
"questionRepeater_ItemDataBound">

<ItemTemplate>

<div class="course-card"
style="
padding:20px;
margin-bottom:20px;">

<h3>

Q<%# Eval("QuestionNumber") %> :

<%# Eval("QuestionText") %>

</h3>

<br/>


<asp:HiddenField
ID="questionIDHidden"
runat="server"
Value='<%# Eval("QuestionID") %>'/>


<asp:HiddenField
ID="questionTypeHidden"
runat="server"
Value='<%# Eval("QuestionType") %>'/>


<asp:Panel
ID="mcqPanel"
runat="server">

<asp:RadioButtonList
ID="optionList"
runat="server">
</asp:RadioButtonList>

</asp:Panel>


<asp:Panel
ID="fillPanel"
runat="server">

<asp:TextBox
ID="answerTextbox"
runat="server"
Width="300px"
CssClass="textbox">

</asp:TextBox>

</asp:Panel>

</div>

</ItemTemplate>

</asp:Repeater>


<br/>

<asp:Button
ID="submitBtn"
runat="server"
Text="Submit Quiz"
OnClick="submitBtn_Click"
CssClass="status open"/>

</div>

</asp:Content>
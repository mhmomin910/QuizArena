<%@ Page Title="Take Quiz" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TakeQuiz.aspx.cs" Inherits="TakeQuiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card">
                    <div class="card-header bg-dark text-white text-center">
                        <h4 class="mb-0">Quiz:
                            <asp:Label ID="lblQuizTitle" runat="server"></asp:Label></h4>
                        <div id="timerDisplay" class="h5 mt-2">Time Left: <span id="countdown"></span></div>
                        <asp:HiddenField ID="hiddenTimeLeft" runat="server" Value="0" />
                    </div>
                    <div class="card-body">
                        <div id="quiz-container">
                            <h5 id="question_text" runat="server" class="card-title"></h5>
                            <div id="options_container" runat="server" class="list-group">
                            </div>
                        </div>
                    </div>
                    <div class="card-footer text-center">
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
                        <asp:Button ID="btnNext" runat="server" Text="Next Question" CssClass="btn btn-primary mt-3" OnClick="btnNext_Click" />
                        <asp:Button ID="btnFinish" runat="server" Text="Finish Quiz" CssClass="btn btn-success mt-3" OnClick="btnFinish_Click" Visible="false" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        var timerInterval;
        var seconds;
        var countdownSpan = document.getElementById('countdown');
        var hiddenTimeLeft = document.getElementById('<%= hiddenTimeLeft.ClientID %>');

        // Prevent Back Button
        (function () {
            window.history.pushState(null, "", window.location.href);
            window.onpopstate = function () {
                alert("You cannot go back during the quiz. You will be disqualified!");
                window.location.href = 'Dashboard.aspx'; // Redirect to disqualification page
            };
        })();

        // Page load par timer ki value set karein
        if (hiddenTimeLeft.value == "0") {
            // First page load
            seconds = <%= GetTimeLimit() %> * 60;
        } else {
            // After a postback, hidden field se value lein
            seconds = parseInt(hiddenTimeLeft.value);
        }

        // Timer ko update karne ka function
        function updateTimer() {
            var displayMinutes = Math.floor(seconds / 60);
            var displaySeconds = seconds % 60;

            countdownSpan.textContent = `${displayMinutes.toString().padStart(2, '0')}:${displaySeconds.toString().padStart(2, '0')}`;

            if (seconds <= 0) {
                clearInterval(timerInterval);
                alert("Time's up! The quiz will now be submitted.");
                document.getElementById('<%= btnFinish.ClientID %>').click();
            } else {
                seconds--;
                hiddenTimeLeft.value = seconds;
            }
        }

        // Timer ko turant shuru karein, page load ka wait kiye bina
        timerInterval = setInterval(updateTimer, 1000);

    </script>
</asp:Content>

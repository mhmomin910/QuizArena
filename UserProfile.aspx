<%@ Page Title="User Profile" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card">
                    <div class="card-header bg-dark text-white text-center">
                        <h4><i class="fa fa-user-circle"></i> User Profile</h4>
                    </div>
                    <div class="card-body">
                        <div class="row mb-4">
                            <div class="col-sm-6">
                                <h5><i class="fa fa-id-badge"></i> Name:</h5>
                                <p><asp:Label ID="lblFullName" runat="server"></asp:Label></p>
                            </div>
                            <div class="col-sm-6">
                                <h5><i class="fa fa-envelope"></i> Email:</h5>
                                <p><asp:Label ID="lblEmail" runat="server"></asp:Label></p>
                            </div>
                        </div>
                        <hr />
                        <div class="text-center mb-4">
                            <h4><i class="fa fa-chart-line"></i> My Quiz Stats</h4>
                            <p class="lead">Total Quizzes Attempted: <asp:Label ID="lblTotalQuizzesAttempted" runat="server" Font-Bold="true"></asp:Label></p>
                        </div>
                        <div id="pnlHistory" runat="server">
                            <h5><i class="fa fa-history"></i> My Quiz History</h5>
                            <asp:Repeater ID="repeaterHistory" runat="server">
                                <HeaderTemplate>
                                    <table class="table table-striped table-bordered mt-3">
                                        <thead class="thead-dark">
                                            <tr>
                                                <th>Quiz Title</th>
                                                <th>Score</th>
                                                <th>Attempt Date</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("QuizTitle") %></td>
                                        <td><%# Eval("Score") %> / <%# Eval("TotalQuestions") %></td>
                                        <td><%# Eval("AttemptDate", "{0:dd-MMM-yyyy hh:mm tt}") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                        </tbody>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
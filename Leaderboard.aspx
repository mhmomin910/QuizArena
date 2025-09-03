<%@ Page Title="Leaderboard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Leaderboard.aspx.cs" Inherits="Leaderboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <div class="row justify-content-center">
            <div class="col-md-10">
                <div class="card">
                    <div class="card-header bg-dark text-white text-center">
                        <h4><i class="fa fa-chart-bar"></i> Quiz Leaderboard</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group row align-items-center">
                            <label for="ddlQuiz" class="col-sm-3 col-form-label">Select a Quiz:</label>
                            <div class="col-sm-9">
                                <asp:DropDownList ID="ddlQuiz" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlQuiz_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <hr />
                        <div id="leaderboard_table" runat="server" visible="false">
                            <h5 class="text-center mb-4">Top Scorers</h5>
                            <asp:Repeater ID="repeaterLeaderboard" runat="server">
                                <HeaderTemplate>
                                    <table class="table table-striped table-hover">
                                        <thead>
                                            <tr>
                                                <th>Rank</th>
                                                <th>Player</th>
                                                <th>Score</th>
                                                <th>Attempt Date</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Container.ItemIndex + 1 %></td>
                                        <td><%# Eval("FullName") %></td>
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
                        <div id="pnlMessage" runat="server" class="alert alert-info text-center" visible="false">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

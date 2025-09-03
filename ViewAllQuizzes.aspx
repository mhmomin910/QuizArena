<%@ Page Title="View All Quizzes" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ViewAllQuizzes.aspx.cs" Inherits="ViewAllQuizzes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <h2 class="text-center mb-4"><i class="fa fa-list"></i> Available Quizzes</h2>

        <div class="row mb-4">
            <div class="col-md-6">
                <div class="input-group">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search by quiz title or description..." onkeyup="instantSearch()"></asp:TextBox>
                    <div class="input-group-append" style="display: none;">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <asp:DropDownList ID="ddlCategoryFilter" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategoryFilter_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12">
                <asp:Repeater ID="repeaterQuizzes" runat="server">
                    <HeaderTemplate>
                        <div class="card-columns">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="card bg-light mb-3">
                            <div class="card-header bg-secondary text-white">
                                <h5 class="card-title mb-0">
                                    <%# Eval("QuizTitle") %>
                                </h5>
                                <small>Category: <%# Eval("CategoryName") %></small>
                            </div>
                            <div class="card-body">
                                <p class="card-text">
                                    <%# Eval("QuizDescription") %>
                                </p>
                                <p class="card-text"><small class="text-muted">Created on: <%# Eval("CreatedDate", "{0:dd-MMM-yyyy}") %></small></p>
                                <a href="TakeQuiz.aspx?quizId=<%# Eval("QuizId") %>" class="btn btn-info btn-block">
                                    <i class="fa fa-play-circle"></i>Start Quiz
                                </a>
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        </div>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="text-center mt-4">
            <asp:Label ID="lblNoQuizzesFound" runat="server" Text="No quizzes found." Visible="false" CssClass="text-muted"></asp:Label>
        </div>
    </div>

    <script src="js/jquery-3.4.1.slim.min.js"></script>
    <script>
        function instantSearch() {
            var searchTerm = $('#<%= txtSearch.ClientID %>').val().toLowerCase();
            $('.card-columns .card').each(function () {
                var cardText = $(this).text().toLowerCase();
                if (cardText.indexOf(searchTerm) > -1) {
                    $(this).show();
                } else {
                    $(this).hide();
                }
            });
        }
    </script>
</asp:Content>

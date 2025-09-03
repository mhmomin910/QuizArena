<%@ Page Title="User Dashboard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <div class="jumbotron text-center">
            <h1 class="display-4">Welcome, <asp:Label ID="lblUserFullName" runat="server"></asp:Label>!</h1>
            <p class="lead">Start your quiz journey by creating a new quiz or taking an existing one.</p>
            <hr class="my-4">
            
            <p>What would you like to do?</p>
            
            <div class="d-flex justify-content-center">
                <asp:Button ID="btnCreateQuiz" runat="server" Text="Create a New Quiz" CssClass="btn btn-primary btn-lg mr-3" OnClick="btnCreateQuiz_Click" />
                <a href="ViewAllQuizzes.aspx" class="btn btn-info btn-lg">
                    <i class="fa fa-list"></i> View All Quizzes
                </a>
            </div>
        </div>
        
        <hr class="my-5" />

        <div class="row">
            <div class="col-md-12">
                <h3 class="mb-4"><i class="fa fa-pencil-alt"></i> Your Quizzes</h3>
                <asp:Repeater ID="repeaterMyQuizzes" runat="server">
                    <HeaderTemplate>
                        <ul class="list-group">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li class="list-group-item d-flex justify-content-between align-items-center">
                            <div>
                                <h5 class="mb-1"><%# Eval("QuizTitle") %></h5>
                                <small class="text-muted">Created on: <%# Eval("CreatedDate", "{0:dd-MMM-yyyy}") %></small>
                            </div>
                            <div>
                                <a href="EditQuiz.aspx?quizId=<%# Eval("QuizId") %>" class="btn btn-sm btn-info mr-2"><i class="fa fa-edit"></i> Edit</a>
                                <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-sm btn-danger" CommandName="Delete" CommandArgument='<%# Eval("QuizId") %>' OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this quiz?');"><i class="fa fa-trash"></i> Delete</asp:LinkButton>
                            </div>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Label ID="lblNoQuizzesFound" runat="server" Text="You haven't created any quizzes yet." Visible="false" CssClass="text-muted mt-3"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>


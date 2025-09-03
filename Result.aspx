<%@ Page Title="Quiz Results" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Result.aspx.cs" Inherits="Result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card text-center">
                    <div class="card-header bg-success text-white">
                        <h4><i class="fa fa-trophy"></i> Your Quiz Results</h4>
                    </div>
                    <div class="card-body">
                        <h1 class="display-4"><i class="fa fa-star"></i> Score: <asp:Label ID="lblScore" runat="server"></asp:Label></h1>
                        <p class="lead">Congratulations, you've completed the quiz!</p>
                        <hr class="my-4" />
                        
                        <div class="text-left">
                            <asp:Repeater ID="repeaterResults" runat="server">
                                <ItemTemplate>
                                    <div class="card mb-3">
                                        <div class="card-body">
                                            <p><strong>Question <%# Container.ItemIndex + 1 %>:</strong> <%# Eval("QuestionText") %></p>
                                            <p>Your Answer: 
                                                <span class="font-weight-bold
                                                    <%# Eval("UserAnswer").ToString() == Eval("CorrectOption").ToString() ? "text-success" : "text-danger" %>">
                                                    <%# Eval(Eval("UserAnswer").ToString()) %>
                                                </span>
                                            </p>
                                            <p>Correct Answer: 
                                                <span class="font-weight-bold text-success">
                                                    <%# Eval(Eval("CorrectOption").ToString()) %>
                                                </span>
                                            </p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:HyperLink ID="btnDashboard" runat="server" NavigateUrl="~/Dashboard.aspx" CssClass="btn btn-info"><i class="fa fa-arrow-left"></i> Go to Dashboard</asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
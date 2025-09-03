<%@ Page Title="Create Quiz" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CreateQuiz.aspx.cs" Inherits="CreateQuiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card">
                    <div class="card-header bg-info text-white text-center">
                        <h4><i class="fa fa-pencil"></i>Create a New Quiz</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <label for="txtQuizTitle">Quiz Title</label>
                            <asp:TextBox ID="txtQuizTitle" runat="server" CssClass="form-control" placeholder="e.g., General Knowledge Quiz"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtQuizDescription">Quiz Description</label>
                            <asp:TextBox ID="txtQuizDescription" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" placeholder="A brief description of your quiz..."></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="ddlCategory">Quiz Category</label>
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="txtTimeLimit">Time Limit (in minutes)</label>
                            <asp:TextBox ID="txtTimeLimit" runat="server" TextMode="Number" Text="10" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtNegativeMarks">Negative Marks per Wrong Answer</label>
                            <asp:TextBox ID="txtNegativeMarks" runat="server" TextMode="Number" Text="0.00" CssClass="form-control" step="0.01"></asp:TextBox>
                        </div>
                        <asp:Button ID="btnCreateQuiz" runat="server" Text="Create Quiz and Add Questions" CssClass="btn btn-info btn-block" OnClick="btnCreateQuiz_Click" />
                        <br />
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

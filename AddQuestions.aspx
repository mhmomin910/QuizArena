<%@ Page Title="Add Questions" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddQuestions.aspx.cs" Inherits="AddQuestions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card">
                    <div class="card-header bg-warning text-dark text-center">
                        <h4><i class="fa fa-question-circle"></i>Add Questions to Quiz</h4>
                        <p class="mb-0">
                            Quiz:
                            <asp:Label ID="lblQuizTitle" runat="server" Font-Bold="true"></asp:Label>
                        </p>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <label for="txtQuestionText">Question Text</label>
                            <asp:TextBox ID="txtQuestionText" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Enter your question here"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtOptionA">Option A</label>
                            <asp:TextBox ID="txtOptionA" runat="server" CssClass="form-control" placeholder="Enter option A"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtOptionB">Option B</label>
                            <asp:TextBox ID="txtOptionB" runat="server" CssClass="form-control" placeholder="Enter option B"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtOptionC">Option C</label>
                            <asp:TextBox ID="txtOptionC" runat="server" CssClass="form-control" placeholder="Enter option C"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtOptionD">Option D</label>
                            <asp:TextBox ID="txtOptionD" runat="server" CssClass="form-control" placeholder="Enter option D"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtOptionE">Option E</label>
                            <asp:TextBox ID="txtOptionE" runat="server" CssClass="form-control" placeholder="Enter option E"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtOptionF">Option F</label>
                            <asp:TextBox ID="txtOptionF" runat="server" CssClass="form-control" placeholder="Enter option F"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="ddlCorrectOption">Correct Option</label>
                            <asp:DropDownList ID="ddlCorrectOption" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Select Correct Option" Value=""></asp:ListItem>
                                <asp:ListItem Text="Option A" Value="OptionA"></asp:ListItem>
                                <asp:ListItem Text="Option B" Value="OptionB"></asp:ListItem>
                                <asp:ListItem Text="Option C" Value="OptionC"></asp:ListItem>
                                <asp:ListItem Text="Option D" Value="OptionD"></asp:ListItem>
                                <asp:ListItem Text="Option E" Value="OptionE"></asp:ListItem>
                                <asp:ListItem Text="Option F" Value="OptionF"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group text-center">
                            <asp:Button ID="btnAddQuestion" runat="server" Text="Add Question" CssClass="btn btn-warning" OnClick="btnAddQuestion_Click1" />
                            <asp:Button ID="btnFinish" runat="server" Text="Finish Adding Questions" CssClass="btn btn-success ml-2" OnClick="btnFinish_Click1" />
                            <%--<asp:Button ID="btnAddQuestion" runat="server" Text="Add Question" CssClass="btn btn-warning" OnClick="btnAddQuestion_Click" />--%>
                            <%--<asp:Button ID="btnFinish" runat="server" Text="Finish Adding Questions" CssClass="btn btn-success ml-2" OnClick="btnFinish_Click" />--%>
                        </div>
                        <br />
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

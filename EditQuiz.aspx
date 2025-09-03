<%@ Page Title="Edit Quiz" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EditQuiz.aspx.cs" Inherits="EditQuiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <div class="row justify-content-center">
            <div class="col-md-10">
                <div class="card">
                    <div class="card-header bg-info text-white text-center">
                        <h4><i class="fa fa-edit"></i> Edit Quiz:
                            <asp:Label ID="lblQuizTitle" runat="server"></asp:Label></h4>
                    </div>
                    <div class="card-body">
                        <h5>Quiz Details</h5>
                        <div class="form-group">
                            <label for="txtEditQuizTitle">Quiz Title</label>
                            <asp:TextBox ID="txtEditQuizTitle" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtEditQuizDescription">Quiz Description</label>
                            <asp:TextBox ID="txtEditQuizDescription" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control"></asp:TextBox>
                        </div>
                        <asp:Button ID="btnUpdateQuizDetails" runat="server" Text="Update Details" CssClass="btn btn-info" OnClick="btnUpdateQuizDetails_Click" />
                        <hr />
                        <h5>Manage Questions</h5>
                        <asp:GridView ID="gridQuestions" runat="server"
                            AutoGenerateColumns="False"
                            CssClass="table table-bordered table-striped"
                            DataKeyNames="QuestionId"
                            OnRowEditing="gridQuestions_RowEditing"
                            OnRowCancelingEdit="gridQuestions_RowCancelingEdit"
                            OnRowUpdating="gridQuestions_RowUpdating"
                            OnRowDeleting="gridQuestions_RowDeleting"
                            EmptyDataText="No questions found for this quiz.">
                            <Columns>
                                <asp:BoundField DataField="QuestionId" HeaderText="ID" ReadOnly="True" />
                                <asp:TemplateField HeaderText="Question">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuestionText" runat="server" Text='<%# Eval("QuestionText") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditQuestionText" runat="server" Text='<%# Eval("QuestionText") %>' CssClass="form-control"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-success mt-3"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

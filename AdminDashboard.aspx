<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AdminDashboard.aspx.cs" Inherits="AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header bg-danger text-white text-center">
                        <h4><i class="fa fa-tachometer-alt"></i> Admin Dashboard</h4>
                    </div>
                    <div class="card-body">
                        <div class="mb-5">
                            <h5 class="mb-3">Manage Users</h5>
                            <asp:GridView ID="gridUsers" runat="server"
                                AutoGenerateColumns="False"
                                CssClass="table table-bordered table-striped"
                                DataKeyNames="UserId"
                                OnRowEditing="gridUsers_RowEditing"
                                OnRowCancelingEdit="gridUsers_RowCancelingEdit"
                                OnRowUpdating="gridUsers_RowUpdating"
                                OnRowDeleting="gridUsers_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="UserId" HeaderText="ID" ReadOnly="True" />
                                    <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                    <asp:TemplateField HeaderText="Is Admin">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkIsAdmin" runat="server" Enabled="false" Checked='<%# Eval("IsAdmin") %>'></asp:CheckBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="chkEditIsAdmin" runat="server" Checked='<%# Eval("IsAdmin") %>'></asp:CheckBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div>
                            <h5 class="mb-3">Manage Quizzes</h5>
                            <asp:GridView ID="gridQuizzes" runat="server"
                                AutoGenerateColumns="False"
                                CssClass="table table-bordered table-striped"
                                DataKeyNames="QuizId"
                                OnRowDeleting="gridQuizzes_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="QuizId" HeaderText="ID" ReadOnly="True" />
                                    <asp:BoundField DataField="QuizTitle" HeaderText="Quiz Title" />
                                    <asp:BoundField DataField="CreatorName" HeaderText="Created By" ReadOnly="True" />
                                    <asp:BoundField DataField="CreatedDate" HeaderText="Created On" DataFormatString="{0:dd-MMM-yyyy}" ReadOnly="True" />
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-sm btn-danger" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

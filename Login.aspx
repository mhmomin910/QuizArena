<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card my-5">
                    <div class="card-header bg-success text-white text-center" style="background: linear-gradient(90deg, #0d6efd, #6610f2); box-shadow: 0 2px 10px rgba(0, 0, 0, 0.2);">
                        <h4><i class="fa fa-sign-in"></i> Login to QuizArena</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <label for="txtUsername"><i class="fa fa-user"></i> Username</label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter username"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtPassword"><i class="fa fa-lock"></i> Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter password"></asp:TextBox>
                        </div>
                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn-block" OnClick="btnLogin_Click" />
                        <br />
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
                    </div>
                    <div class="card-footer text-center">
                        <p>Don't have an account? <a href="Registration.aspx">Register now</a></p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

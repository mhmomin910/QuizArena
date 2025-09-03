<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card my-5">
                    <div class="card-header bg-secondary text-white text-center" style="background: linear-gradient(90deg, #0d6efd, #6610f2); box-shadow: 0 2px 10px rgba(0, 0, 0, 0.2);">
                        <h4><i class="fa fa-user-plus"></i> Register Your Account</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <label for="txtFullName"><i class="fa fa-user"></i> Full Name</label>
                            <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Enter full name"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtUsername"><i class="fa fa-user-circle"></i> Username</label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Choose a username"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtEmail"><i class="fa fa-envelope"></i> Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="Enter email"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtPassword"><i class="fa fa-lock"></i> Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter password"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtConfirmPassword"><i class="fa fa-lock"></i> Confirm Password</label>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confirm password"></asp:TextBox>
                        </div>
                        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary btn-block" OnClick="btnRegister_Click" />
                        <br />
                        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
                    </div>
                    <div class="card-footer text-center">
                        <p>Already have an account? <a href="Login.aspx">Login here</a></p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


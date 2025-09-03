<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageCategories.aspx.cs" Inherits="ManageCategories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card">
                    <div class="card-header bg-danger text-white text-center">
                        <h4><i class="fa fa-cogs"></i> Manage Categories</h4>
                    </div>
                    <div class="card-body">
                        <div class="mb-4">
                            <h5>Add New Category</h5>
                            <div class="form-group">
                                <label for="txtCategoryName">Category Name</label>
                                <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" placeholder="e.g., Science, History"></asp:TextBox>
                            </div>
                            <asp:Button ID="btnAddCategory" runat="server" Text="Add Category" CssClass="btn btn-danger btn-block" OnClick="btnAddCategory_Click" />
                            <asp:Label ID="lblMessage" runat="server" CssClass="text-success mt-2"></asp:Label>
                        </div>

                        <hr />

                        <div class="mt-4">
                            <h5>Existing Categories</h5>
                            <asp:GridView ID="gridCategories" runat="server"
                                AutoGenerateColumns="False"
                                CssClass="table table-bordered table-striped"
                                DataKeyNames="CategoryId"
                                OnRowEditing="gridCategories_RowEditing"
                                OnRowCancelingEdit="gridCategories_RowCancelingEdit"
                                OnRowUpdating="gridCategories_RowUpdating"
                                OnRowDeleting="gridCategories_RowDeleting"
                                EmptyDataText="No categories found.">
                                <Columns>
                                    <asp:BoundField DataField="CategoryId" HeaderText="ID" ReadOnly="True" SortExpression="CategoryId" />
                                    <asp:TemplateField HeaderText="Category Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtEditCategoryName" runat="server" Text='<%# Eval("CategoryName") %>' CssClass="form-control"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

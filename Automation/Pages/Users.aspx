<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSite.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Automation.Pages.Users" NeedLogin="false"
    gref="C8C83C74-C989-47F2-9B81-77A3DB2C5EB3" gid="0DC6B854-3E62-4666-8081-94C8551B81A9" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <aut:GridView ID="grdUsers" ClientInstanceName="grdUsers" runat="server" KeyFieldName="ID" DataSourceID="OdsUsers" AutoGenerateColumns="false"
        ClientSideEvents-SelectionChanged="function(s, e){ if (e.isSelected) { $('#hdfRowID').val(s.GetRowKey(e.visibleIndex)); Get($('#hdfRowID').val()); ShowNewEdit();}}">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="Username" Caption="نام کاربری" />
        </Columns>
    </aut:GridView>

    <div class="row CustomMargin">
        <div class="col-lg-11 col-sm-11 col-md-11"></div>
        <div class="col-lg-1 col-sm-1 col-md-1">
            <button type="button" class="btn btn-primary btn-lg" onclick="ShowNewEdit()">جدید</button>
        </div>
    </div>

    <div class="panel panel-primary CustomPanel" id="pnlNewEdit">
        <div class="panel-heading">جدید ویرایش</div>
        <div class="panel-body">

            <div class="row">
                <div class="col-lg-8 col-md-8 col-sm-8">
                    <input type="text" class="form-control" id="txtUsername" placeholder="نام کاربری" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4">
                    <label for="txtUsername">نام کاربری</label>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-8 col-md-8 col-sm-8">
                    <input type="text" class="form-control" id="txtName" placeholder="نام" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4">
                    <label for="txtName">نام</label>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-8 col-md-8 col-sm-8">
                    <input type="text" class="form-control" id="txtFamily" placeholder="نام خانوادگی" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4">
                    <label for="txtFamily">نام خانوادگی</label>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-8 col-md-8 col-sm-8">
                    <input type="text" class="form-control" id="txtEmail" placeholder="ایمیل" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4">
                    <label for="txtEmail">ایمیل</label>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-8 col-md-8 col-sm-8">
                    <input type="text" class="form-control" id="txtAddress" placeholder="آدرس" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4">
                    <label for="txtAddress">آدرس</label>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-8 col-md-8 col-sm-8">
                    <input type="text" class="form-control" id="txtMobile" placeholder="موبایل" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4">
                    <label for="txtMobile">موبایل</label>
                </div>
            </div>

            <div class="row" style="display: none" id="devPass">
                <div class="col-lg-8 col-md-8 col-sm-8">
                    <input type="password" class="form-control" id="txtPassword" placeholder="رمز عبور" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4">
                    <label for="txtPassword">رمز عبور</label>
                </div>
            </div>

            <div class="row CustomMargin">
                <div class="col-lg-4 col-md-4 col-sm-4">
                    <button type="button" class="btn btn-danger btn-lg" onclick="Cancel()">انصراف</button>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8">
                    <button type="button" class="btn btn-primary btn-lg" onclick="Save()">ذخیره</button>
                </div>
            </div>

        </div>
    </div>

    <asp:SqlDataSource runat="server" ID="odsUsers" ConnectionString="<%$ ConnectionStrings : Automation %>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">

    <script type="text/javascript">

        function ShowNewEdit() {
            $("#pnlNewEdit").show(1000);

            if ($("#hdfRowID").val() != "") {
                $("#devPass").hide();
            }
        }

        function Get(key) {

        }

        function Save() {

        }

        function Cancel() {
            $("#pnlNewEdit").hide(1000);
            $("#hdfRowID").val("");
            $("#devPass").hide();
        }

    </script>

</asp:Content>

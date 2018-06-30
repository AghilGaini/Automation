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

            if ($("#hdfRowID").val() == "") {
                $("#txtPassword").val("");
                $("#devPass").show();
            }
        }

        function Get(key) {

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~") %>Pages/Users.aspx/Get',
                data: JSON.stringify({ RowID: key }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then
            (
            function (data) {
                if (data.d[0] == "1") {
                    var info = JSON.parse(data.d[1])

                    $("#txtUsername").val(info.Username);
                    $("#txtName").val(info.Name);
                    $("#txtFamily").val(info.Family);
                    $("#txtEmail").val(info.Email);
                    $("#txtAddress").val(info.Address);
                    $("#txtMobile").val(info.Mobile);

                }
                else if (data.d[1] == "0") {
                    ShowError("", "fail: " + data.d[1]);
                }
            }, function (data) {
                ShowError("", "عدم برقراری ارتباط");
            }
            )

        }

        function Save() {

            var entity = {};
            entity.Username = $("#txtUsername").val();
            entity.Name = $("#txtName").val();
            entity.Family = $("#txtFamily").val();
            entity.Email = $("#txtEmail").val();
            entity.Address = $("#txtAddress").val();
            entity.Mobile = $("#txtMobile").val();
            entity.Password = $("#txtPassword").val();
            entity.ID = $("#hdfRowID").val() == "" ? 0 : $("#hdfRowID").val();

            entity = JSON.stringify(entity);

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~") %>Pages/Users.aspx/Save',
                data: JSON.stringify({ info : entity }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then
            (
            function (data) {
                if (data.d[0] == "1") {
                    ShowSuccess("", data.d[1]);
                    grdUsers.Refresh();
                    Cancel();
                }
                else if (data.d[1] == "0") {
                    ShowError("", "fail: " + data.d[1]);
                }
            }, function (data) {
                ShowError("", "عدم برقراری ارتباط");
            }
            )
        }

        function Clear()
        {
            $("#txtUsername").val("");
            $("#txtName").val("");
            $("#txtFamily").val("");
            $("#txtEmail").val("");
            $("#txtAddress").val("");
            $("#txtMobile").val("");
            $("#txtPassword").val("");
        }

        function Cancel() {
            $("#pnlNewEdit").hide(1000);
            $("#hdfRowID").val("");
            $("#devPass").hide();
            Clear();
        }

    </script>

</asp:Content>

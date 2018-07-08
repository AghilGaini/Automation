<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSite.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Automation.Pages.ChangePassword"
    gref="C8C83C74-C989-47F2-9B81-77A3DB2C5EB3" gid="51097CED-DDE7-4FEA-B274-1C9915FB91A3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="panel panel-primary">
        <div class="panel-heading">تغییر رمز</div>
        <div class="panel-body">

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8">
                    <input type="password" class="form-control" id="txtCurrentPass" placeholder="رمز فعلی" />
                </div>
            </div>

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8">
                    <input type="password" class="form-control" id="txtNewPass" placeholder="رمز جدید" />
                </div>
            </div>

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8">
                    <input type="password" class="form-control" id="txtConfirmPass" placeholder="تایید رمز جدید" />
                </div>
            </div>

            <div class="row CustomMargin">
                <div class="col-lg-8 col-md-8 col-sm-8">
                    <button type="button" class="btn btn-primary btn-lg" onclick="Save()">ذخیره</button>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">

    <script type="text/javascript">

        function Save()
        {
            var entity = {};
            entity.CurrentPass = $("#txtCurrentPass").val();
            entity.NewPass = $("#txtNewPass").val();
            entity.ConfirmPass = $("#txtConfirmPass").val();

            entity = JSON.stringify(entity);

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~") %>Pages/ChangePassword.aspx/Save',
                data: JSON.stringify({ info: entity }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then
            (
            function (data) {
                if (data.d[0] == "1") {
                    ShowSuccess("", data.d[1]);
                    window.location = "../Default.aspx";
                }
                else if (data.d[0] == "0") {
                    ShowError("", data.d[1]);
                }
            }, function (data) {
                ShowError("", "عدم برقراری ارتباط");
            }
            )

        }

    </script>

</asp:Content>

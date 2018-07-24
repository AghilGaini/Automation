<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSite.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Automation.Default" IsDefault="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="panel panel-primary">
        <div class="panel-heading" >اطلاعات</div>

        <div class="panel-body">

            <div class="row FieldMargin">
                <div class="col-lg-4 col-md-4 col-sm-1"></div>
                <div class="col-lg-4 col-md-4 col-sm-1">
                    <img id="imgProfile" src="Pictures/Profiles/default-profile.png" class="img-circle" alt="عکس پروفایل" width="200" height="200" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-1"></div>
            </div>

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                    <label id="lblUsername"></label>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label for="txtUsername">نام کاربری</label>
                </div>
            </div>

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                    <label id="lblName"></label>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label for="txtName">نام</label>
                </div>
            </div>

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                    <label id="lblFamily"></label>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label for="txtFamily">نام خانوادگی</label>
                </div>
            </div>

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                    <label id="lblEmail"></label>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label for="txtEmail">ایمیل</label>
                </div>
            </div>

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                    <label id="lblAddress"></label>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label for="txtAddress">آدرس</label>
                </div>
            </div>

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                    <label id="lblMobile"></label>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label for="txtMobile">موبایل</label>
                </div>
            </div>

        </div>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {

            var a = $('[gid]');
            var MenuItems = new Array();

            for (i = 0 ; i < a.length ; i++) {
                var entity = {};
                entity.gid = $(a[i]).attr('gid');
                entity.gref = $(a[i]).attr('gref') == undefined ? null : $(a[i]).attr('gref');
                entity.title = $(a[i]).text();
                MenuItems[i] = entity;
            }

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~") %>Default.aspx/SetPrivilges',
                data: JSON.stringify({ MenuItems: MenuItems }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then
            (
            function (data) {
                if (data.d[0] != "0") {
                    if (data.d[0] == "1")
                        ShowSuccess("", data.d[1]);
                    if (data.d[0] == "2")
                        GetPrivilege();
                    GetInfo();
                }
                else if (data.d[1] == "0") {
                    ShowError("", "fail: " + data.d[1]);
                }
            }, function (data) {
                ShowError("", "عدم برقراری ارتباط");
            }
            )

        });

        function GetPrivilege() {
            var as = $('[gid]')

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~") %>Default.aspx/GetPrivilge',
                data: JSON.stringify({}),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then
                    (
                    function (data) {
                        if (data.d[0] == "1") {
                            var result = JSON.parse(data.d[1]);

                            if ($.inArray('manager', result) > -1)
                                return;

                            for (i = 0 ; i < as.length ; i++) {
                                if ($.inArray($(as[i]).attr('gid').toLowerCase(), result) == -1)
                                    $(as[i]).hide();
                                if ($(as[i]).text() == 'تغییر مشخصات')
                                    $(as[i]).show();
                            }

                        }
                        else if (data.d[1] == "0") {
                            for (i = 0 ; i < as.length ; i++)
                                $(as[i]).hide();
                        }
                    }, function (data) {
                        ShowError("", "عدم برقراری ارتباط");
                    }
                    )
        }

        function GetInfo() {

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~") %>Default.aspx/GetInfo',
                data: JSON.stringify({}),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then
            (
            function (data) {
                if (data.d[0] == "1") {
                    var Userinfo = JSON.parse(data.d[1])

                    $("#lblUsername").text(Userinfo.Username);
                    $("#lblName").text(Userinfo.Name);
                    $("#lblFamily").text(Userinfo.Family);
                    $("#lblEmail").text(Userinfo.Email);
                    $("#lblAddress").text(Userinfo.Address);
                    $("#lblMobile").text(Userinfo.Mobile);
                    $("#imgProfile").attr('src', 'Pictures/Profiles/' + Userinfo.PictureUrl);

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

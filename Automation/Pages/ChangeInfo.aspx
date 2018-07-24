<%@ Page Title="تغییر مشخصات" Language="C#" MasterPageFile="~/MasterSite.Master" AutoEventWireup="true" CodeBehind="ChangeInfo.aspx.cs" Inherits="Automation.Pages.ChangeInfo"
    gref="C8C83C74-C989-47F2-9B81-77A3DB2C5EB3" gid="C4FAC00C-7613-43B9-805E-7C0A1750DA8B" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="panel panel-primary CustomMargin" id="pnlChangeInfo">
        <div class="panel-heading">تغییر مشخصات</div>
        <div class="panel-body">

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                    <input type="text" class="form-control" id="txtName" placeholder="نام جدید" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label for="txtName">نام</label>
                </div>
            </div>

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                    <input type="text" class="form-control" id="txtFamily" placeholder="نام خانوادگی جدید" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label for="txtFamily">نام خانوادگی</label>
                </div>
            </div>

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                    <input type="text" class="form-control" id="txtEmail" placeholder="ایمیل جدید" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label for="txtEmail">ایمیل</label>
                </div>
            </div>

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                    <input type="text" class="form-control" id="txtAddress" placeholder="آدرس جدید" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label for="txtAddress">آدرس</label>
                </div>
            </div>

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                    <input type="text" class="form-control" id="txtMobile" placeholder="موبایل جدید" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label for="txtMobile">موبایل</label>
                </div>
            </div>

            <div class="row FieldMargin">
                <div class="col-lg-4 col-md-4 col-sm-8 col-xs-6">

                    <dx:ASPxUploadControl ID="fileUpload" ClientInstanceName="fileUpload" runat="server" UploadMode="Standard"
                        ShowProgressPanel="true" ShowUploadButton="false" Width="100%" OnFileUploadComplete="fileUpload_FileUploadComplete">
                        <AdvancedModeSettings EnableMultiSelect="False" />
                        <ValidationSettings MaxFileSize="4194304" AllowedFileExtensions=".jpg,.jpeg,.png" />
                    </dx:ASPxUploadControl>

                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label for="txtLevel">عکس پروفایل</label>
                </div>
            </div>

            <div class="row CustomMargin">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <button type="button" class="btn btn-danger btn-lg" onclick="Clear()">پاک کردن</button>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                    <button type="button" class="btn btn-primary btn-lg" onclick="Save()">ذخیره</button>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">

    <script type="text/javascript">

        function Clear()
        {
            $("#txtName").val("");
            $("#txtFamily").val("");
            $("#txtEmail").val("");
            $("#txtAddress").val("");
            $("#txtMobile").val("");
            aspxUClearFileInputClick('body_fileUpload', 0);
        }

        function Save()
        {
            var entity = {};
            entity.Name = $("#txtName").val();
            entity.Family = $("#txtFamily").val();
            entity.Email = $("#txtEmail").val();
            entity.Address = $("#txtAddress").val();
            entity.Mobile = $("#txtMobile").val();

            entity = JSON.stringify(entity);

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~") %>Pages/ChangeInfo.aspx/Save',
                data: JSON.stringify({ info: entity }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then
            (
            function (data) {
                if (data.d[0] == "1") {
                    ShowSuccess("", data.d[1]);
                    aspxUUploadFileClick('body_fileUpload');
                    Clear();
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

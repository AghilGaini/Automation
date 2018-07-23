<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSite.Master" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="Automation.Pages.UploadImage" NeedLogin="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

   <img id="picture" src="../Pictures/Profiles/default-profile.png" class="img-circle" alt="عکس پروفایل" width="304" height="236" />

    <input type="button" value="test" onclick="test()"/>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~") %>Pages/UploadImage.aspx/Get',
                data: JSON.stringify({ }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then
            (
            function (data) {
                if (data.d[0] == "1") {
                    $("#picture").attr('src', '../Pictures/Profiles/'+data.d[1]);
                }
                else if (data.d[0] == "0") {
                    ShowError("", data.d[1]);
                }
            }, function (data) {
                ShowError("", "عدم برقراری ارتباط");
            }
            )

        })

        function test()
        {
            $("#picture").attr('src', 'images/alt/imagename.jpg');
        }

    </script>
</asp:Content>

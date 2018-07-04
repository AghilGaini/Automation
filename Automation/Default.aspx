<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSite.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Automation.Default"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
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
                data: JSON.stringify({ MenuItems : MenuItems }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then
            (
            function (data) {
                if (data.d[0] == "1") {
                    ShowSuccess("",data.d[1]);
                }
                else if(data.d[1] == "0" ){
                    ShowError("","fail: " + data.d[1]);
                }
            }, function (data) {
                ShowError("","عدم برقراری ارتباط");
            }
            )

        });

    </script>

</asp:Content>

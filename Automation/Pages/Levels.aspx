<%@ Page Title="سطوح" Language="C#" MasterPageFile="~/MasterSite.Master" AutoEventWireup="true" CodeBehind="Levels.aspx.cs" Inherits="Automation.Pages.Levels"
    gref="C8C83C74-C989-47F2-9B81-77A3DB2C5EB3" gid="7D22C403-3EC3-4B94-B3BB-DF880D82CC6B" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <aut:GridView ID="grdLevels" ClientInstanceName="grdLevels" runat="server" KeyFieldName="ID" DataSourceID="odsLevels" AutoGenerateColumns="false"
        ClientSideEvents-SelectionChanged="function(s, e){ if (e.isSelected) { $('#hdfRowID').val(s.GetRowKey(e.visibleIndex)); Get($('#hdfRowID').val()); ShowNewEdit();}}">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="Title" Caption="عنوان" />
        </Columns>
    </aut:GridView>

    <div class="row CustomMargin">
        <div class="col-lg-11 col-sm-11 col-md-11 col-xs-6"></div>
        <div class="col-lg-1 col-sm-1 col-md-1 col-xs-6">
            <button type="button" class="btn btn-primary btn-lg" id="btnNewEdit" onclick="ShowNewEdit()">جدید</button>
        </div>
    </div>


    <div class="panel panel-primary CustomPanel" id="pnlNewEdit">
        <div class="panel-heading">جدید ویرایش</div>
        <div class="panel-body">

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                    <input type="text" class="form-control" id="txtTitle" placeholder="نام سطح" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label for="txtTitle">نام سطح</label>
                </div>
            </div>

            <div class="row CustomMargin">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <button type="button" class="btn btn-danger btn-lg" onclick="Cancel()">انصراف</button>
                </div>
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                    <button type="button" class="btn btn-primary btn-lg" onclick="Save()">ذخیره</button>
                </div>
            </div>

        </div>
    </div>


    <asp:SqlDataSource runat="server" ID="odsLevels" ConnectionString="<%$ ConnectionStrings : Automation %>" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">

    <script type="text/javascript">

        function ShowNewEdit()
        {
            $("#pnlNewEdit").show(1000);
        }

        function Get(key)
        {
            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~") %>Pages/Levels.aspx/Get',
                data: JSON.stringify({ RowID: key }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then
            (
            function (data) {
                if (data.d[0] == "1") {
                    var Levelinfo = JSON.parse(data.d[1])

                    $("#txtTitle").val(Levelinfo.Title);
                    $("#pnlNewEdit").show(1000);
                }
                else if (data.d[0] == "0") {
                    ShowError("", data.d[1]);
                }
            }, function (data) {
                ShowError("", "عدم برقراری ارتباط");
            }
            )
        }

        function Save()
        {
            var entity = {};
            entity.Title = $("#txtTitle").val();
            entity.ID = $("#hdfRowID").val();

            entity = JSON.stringify(entity);
            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~") %>Pages/Levels.aspx/Save',
                data: JSON.stringify({ info : entity }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then
            (
            function (data) {
                if (data.d[0] == "1") {
                    ShowSuccess("", data.d[1]);
                    grdLevels.Refresh();
                    Cancel();
                }
                else if (data.d[0] == "0") {
                    ShowError("", data.d[1]);
                }
            }, function (data) {
                ShowError("", "عدم برقراری ارتباط");
            }
            )
        }

        function Clear()
        {
            $("#txtTitle").val("");
            $("#hdfRowID").val("");
        }

        function Cancel()
        {
            Clear();
            $("#pnlNewEdit").hide(1000);
        }

    </script>

</asp:Content>

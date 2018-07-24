<%@ Page Title="نقش ها" Language="C#" MasterPageFile="~/MasterSite.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="Automation.Pages.Roles"
    gref="C8C83C74-C989-47F2-9B81-77A3DB2C5EB3" gid="16A8A08C-4325-4EDD-B568-6A26C0DC9087" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div>
        <aut:GridView runat="server" ID="grdRoles" ClientInstanceName="grdRoles" KeyFieldName="ID" AutoGenerateColumns="false" DataSourceID="odsRoles"
            ClientSideEvents-SelectionChanged="function(s, e){ if (e.isSelected) { $('#hdfRowID').val(s.GetRowKey(e.visibleIndex)); hdn.Set('RowID',$('#hdfRowID').val()) ; Get($('#hdfRowID').val()); ShowNewEdit(0);}}">
            <Columns>
                <dx:GridViewDataTextColumn Caption="شناسه" FieldName="ID" />
                <dx:GridViewDataTextColumn Caption="نام نقش" FieldName="RoleName" />
            </Columns>
        </aut:GridView>

        <div class="row CustomMargin">
            <div class="col-lg-11 col-sm-11 col-md-11 col-xs-6"></div>
            <div class="col-lg-1 col-sm-1 col-md-1 col-xs-6">
                <button type="button" id="btnNew" class="btn btn-primary btn-lg" onclick="ShowNewEdit(1)">جدید</button>
            </div>
        </div>

    </div>

    <div class="panel panel-primary CustomPanel" id="pnlNewEdit">
        <div class="panel-heading">جدید/ویرایش</div>
        <div class="panel-body">

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                    <input type="text" class="form-control" id="txtRoleName" placeholder="نام نقش" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label for="txtRoleName">نام تقش</label>
                </div>
            </div>

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                    <input type="text" class="form-control" id="txtRoleLevel" placeholder="سطح نقش" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label for="txtRoleLevel">سطح نقش</label>
                </div>
            </div>

            <div class="FieldMargin text-left">
                <aut:TreeList ID="TLPrivileges" ClientInstanceName="TLPrivileges" runat="server" KeyFieldName="Gid" ParentFieldName="Gref" PreviewFieldName="Title"
                    AutoGenerateColumns="false" Width="100%" OnCustomCallback="TLPrivileges_CustomCallback" DataSourceID="odsPrivilges">
                    <Settings GridLines="Horizontal" SuppressOuterGridLines="true" />
                    <SettingsBehavior ExpandCollapseAction="NodeDblClick" />
                    <Columns>
                        <dx:TreeListTextColumn Caption="عنوان" FieldName="Title" />
                    </Columns>
                </aut:TreeList>
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

    <asp:SqlDataSource runat="server" ID="odsRoles" ConnectionString="<%$ ConnectionStrings : Automation %>" />
    <asp:ObjectDataSource runat="server" ID="odsPrivilges" SelectMethod="GetAllCache" TypeName="Automation.Pages.Roles" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">

    <script type="text/javascript">

        function Save() {
            TLPrivileges.GetSelectedNodeValues('ID Gid Gref', function (selected) {

                var entity = {};
                entity.RoleName = $("#txtRoleName").val();
                entity.RoleLevel = $("#txtRoleLevel").val();
                entity.RoleID = $("#hdfRowID").val();
                entity.Selected = selected;

                entity = JSON.stringify(entity);

                $.ajax({
                    type: 'POST',
                    url: '<%= ResolveUrl("~") %>Pages/Roles.aspx/Save',
                    data: JSON.stringify({ info: entity }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                }).then
            (
            function (data) {
                if (data.d[0] == "1") {
                    ShowSuccess("", data.d[1]);
                    grdRoles.Refresh();
                    Cancel();
                }
                else if (data.d[0] == "0") {
                    ShowError("", data.d[1]);
                }
            }, function (data) {
                ShowError("", "عدم برقراری ارتباط");
            }
            )

            }, false)
        }

        function ShowNewEdit(clearKey) {
            $("#pnlNewEdit").show(1000);
            hdn.Set('Clear', clearKey);
            $("#btnNew").prop('disabled', true);
        }

        function Cancel() {
            Clear();
            $("#hdfRowID").val("");
            hdn.Set("RowID", null);
            $("#pnlNewEdit").hide(1000);
            $("#btnNew").prop('disabled', false);

            hdn.Set('Clear', 1);
            TLPrivileges.PerformCallback();
        }

        function Clear() {
            $("#txtRoleName").val("");
            $("#txtRoleLevel").val("");
        }

        function Get(key) {

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~") %>Pages/Roles.aspx/Get',
                data: JSON.stringify({ RowID: key }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then
            (
            function (data) {
                if (data.d[0] == "1") {
                    var info = JSON.parse(data.d[1])

                    $("#txtRoleName").val(info.RoleName);
                    $("#txtRoleLevel").val(info.RoleLevel);
                    hdn.Set('Clear', 0);
                    TLPrivileges.PerformCallback();
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

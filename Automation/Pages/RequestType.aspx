<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSite.Master" AutoEventWireup="true" CodeBehind="RequestType.aspx.cs" Inherits="Automation.Pages.RequestType"
    gref="6E4A466B-0BB7-4179-95DC-B7A3AD0B7827" gid="A5F88A38-3C62-4B1A-819E-75DDDA24E1E4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <aut:GridView ID="grdRequestType" ClientInstanceName="grdRequestType" runat="server" KeyFieldName="ID" DataSourceID="odsRequestType" AutoGenerateColumns="false"
        ClientSideEvents-SelectionChanged="function(s, e){ if (e.isSelected) { $('#hdfRowID').val(s.GetRowKey(e.visibleIndex)); hdn.Set('RowID',$('#hdfRowID').val()) ;Get($('#hdfRowID').val()); ShowNewEdit(false);}}">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="Title" Caption="عنوان" />
        </Columns>
    </aut:GridView>

    <div class="row CustomMargin">
        <div class="col-lg-11 col-sm-11 col-md-11 col-xs-6"></div>
        <div class="col-lg-1 col-sm-1 col-md-1 col-xs-6">
            <button type="button" class="btn btn-primary btn-lg" id="btnNewEdit" onclick="ShowNewEdit(true)">جدید</button>
        </div>
    </div>


    <div class="panel panel-primary CustomPanel" id="pnlNewEdit">

        <div class="panel-heading">جدید ویرایش</div>
        <div class="panel-body">

            <div class="row FieldMargin">
                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-6">
                    <input type="text" class="form-control" id="txtTitle" placeholder="عنوان" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label for="Title">عنوان</label>
                </div>
            </div>

            <div class="row FieldMargin">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <aut:ComboBox ID="cmbLevels" ClientInstanceName="cmbLevels" DataSourceID="odsLevels" runat="server" ValueType="System.Int64" ValueField="ID" TextField="Title">
                        <ClientSideEvents SelectedIndexChanged="function(s, e){Selection(s,e);}" />
                    </aut:ComboBox>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label>سطح</label>
                </div>
            </div>

            <div class="row FieldMargin">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <aut:ListBox ID="listLevel" ClientInstanceName="listLevel" runat="server" Width="150" Height="200" RightToLeft="True">
                        <ClientSideEvents SelectedIndexChanged="function(s,e){listLevel_SelectedIndexChanged(s,e)}" />
                    </aut:ListBox>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label>ترتیب مراحل</label>
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

    <asp:SqlDataSource ID="odsRequestType" runat="server" ConnectionString="<%$ ConnectionStrings : Automation %>" />
    <asp:ObjectDataSource ID="odsLevels" runat="server" TypeName="Automation.Pages.RequestType" SelectMethod="GetAllLevels" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">

    <script type="text/javascript">

        function Selection()
        {
            var last = listLevel.GetItemCount() - 1;
            if (listLevel.GetItemCount() == 0 || ( listLevel.GetItem(last).value != cmbLevels.GetValue() && listLevel.GetItem(last).text != cmbLevels.GetText() ) )
                listLevel.AddItem(cmbLevels.GetText(), cmbLevels.GetValue());
        }

        function listLevel_SelectedIndexChanged()
        {
            var index = listLevel.GetSelectedIndex();
            var before = index - 1, after = index + 1, last = listLevel.GetItemCount() - 1;
            var beforeItem = listLevel.GetItem(before);
            var afterItem = listLevel.GetItem(after);
            if (index == 0 || index == last || (beforeItem.value != afterItem.value && beforeItem.text != afterItem.text))
                listLevel.RemoveItem(index);
        }

        function Save()
        {
            var RequestDetail = new Array();

            for (i = 0 ; i < listLevel.GetItemCount() ; i++)
                RequestDetail[i] = listLevel.GetItem(i).text;

            var entity = {};
            entity.Title = $("#txtTitle").val();
            entity.RequestDetail = RequestDetail;
            entity.ID = $("#hdfRowID").val();

            entity = JSON.stringify(entity);

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~") %>Pages/RequestType.aspx/Save',
                data: JSON.stringify({ info : entity }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then
                (
                function (data) {
                    if (data.d[0] == "1") {
                        ShowSuccess("", data.d[1]);
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

        function Cancel()
        {
            $("#txtTitle").val("");
            cmbLevels.SetValue(null);
            listLevel.ClearItems();
            HideNewEdit();
        }
        
        function Get(key)
        {
            cmbLevels.SetValue(null);
            listLevel.ClearItems();

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~") %>Pages/RequestType.aspx/Get',
                data: JSON.stringify({ RowID: key }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then
            (
            function (data) {
                if (data.d[0] == "1") {
                    var Type = JSON.parse(data.d[1]);
                    var TypeDetails = JSON.parse(data.d[2]);

                    $("#txtTitle").val(Type.Title);
                    for (var i in TypeDetails)
                        listLevel.AddItem(TypeDetails[i].LevelTitle, TypeDetails[i].ID);
                }
                else if (data.d[0] == "0") {
                    ShowError("", data.d[1]);
                }
            }, function (data) {
                ShowError("", "عدم برقراری ارتباط");
            }
            )
        }

        function ShowNewEdit()
        {
            $("#pnlNewEdit").show(1000);
        }

        function HideNewEdit()
        {
            $("#pnlNewEdit").hide(1000);
        }

    </script>

</asp:Content>

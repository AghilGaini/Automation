<%@ Page Title="ثبت درخواست" Language="C#" MasterPageFile="~/MasterSite.Master" AutoEventWireup="true" CodeBehind="SubmitRequest.aspx.cs" Inherits="Automation.Pages.SubmitRequest"
    gref="A226246D-C7EB-49D4-AE4D-78C505DC5FE9" gid="4022C2BE-5509-4C54-9A17-51E9FB10AD45" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <aut:GridView ID="grdRequets" ClientInstanceName="grdRequets" runat="server" KeyFieldName="RequestID" AutoGenerateColumns="false" OnBeforeColumnSortingGrouping="grdRequets_BeforeColumnSortingGrouping" OnPageIndexChanged="grdRequets_PageIndexChanged" OnCustomCallback="grdRequets_CustomCallback" OnCustomButtonCallback="grdRequets_CustomButtonCallback"
        ClientSideEvents-SelectionChanged="function(s, e){ if (e.isSelected) { $('#hdfRowID').val(s.GetRowKey(e.visibleIndex)); hdn.Set('RowID',$('#hdfRowID').val()) ;Get($('#hdfRowID').val()); ShowNewEdit();}}">
        <Columns>
            <dx:GridViewDataTextColumn Caption="ردیف" Width="50" />
            <dx:GridViewDataTextColumn Caption="شناسه" FieldName="RequestID" />
            <dx:GridViewDataTextColumn Caption="ایجاد شده توسط" FieldName="CreatedBy" />
            <dx:GridViewDataTextColumn Caption="تاریخ ایجاد" FieldName="CreatedOn" />
            <dx:GridViewDataTextColumn Caption="توضیحات" FieldName="Description" />
            <dx:GridViewDataTextColumn Caption="نوع درخواست" FieldName="TypeTitle" />
            <dx:GridViewDataTextColumn Caption="وضعیت درخواست" FieldName="StatusTitle" />
            <dx:GridViewDataCheckColumn Caption="وضعیت ارسال" FieldName="Sent" />
            <dx:GridViewCommandColumn ButtonType="Button">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="btnSendG" Text="ارسال درخواست" />
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
    </aut:GridView>

     <div class="row CustomMargin">
        <div class="col-lg-11 col-sm-11 col-md-11 col-xs-6"></div>
        <div class="col-lg-1 col-sm-1 col-md-1 col-xs-6">
            <button type="button" class="btn btn-primary btn-lg" id="btnNewEdit" onclick="ShowNewEdit()">جدید</button>
        </div>
    </div>

    <div class="panel panel-primary CustomPanel" id="pnlNewEdit">
        <div class="panel-heading">جدید/ویرایش</div>
        <div class="panel-body">

            <div class="row FieldMargin">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <aut:ComboBox ID="cmbRequests" ClientInstanceName="cmbRequests" DataSourceID="odsRequests" runat="server" ValueType="System.Int64" ValueField="ID" TextField="Title" />
                </div>
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <label>نوع درخواست</label>
                </div>
            </div>

            <div class="row FieldMargin">
                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-6">
                    <textarea id="txtDescription" rows="5" cols="100" placeholder="توضیحات"></textarea>
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

    <asp:ObjectDataSource ID="odsRequests" runat="server" TypeName="Automation.Pages.SubmitRequest" SelectMethod="GetAllRequests" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {
            grdRequets.PerformCallback();
        });

        function Cancel() {

            Clear();
            HideNewEdit();
            $("#btnNewEdit").attr('disabled', false);
        }

        function Clear() {
            $('#hdfRowID').val("");
            hdn.Set('RowID', $('#hdfRowID').val());
        }

        function ShowNewEdit() {
            $("#pnlNewEdit").show(1000);
            $("#btnNewEdit").attr('disabled', true);

        }

        function HideNewEdit() {
            $("#pnlNewEdit").hide(1000);
        }

        function Save() {

            var entity = {};
            entity.TypeID = cmbRequests.GetValue();
            entity.Description = $("#txtDescription").val();
            entity.ID = $('#hdfRowID').val();

            entity = JSON.stringify(entity);

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~") %>Pages/SubmitRequest.aspx/Save',
                data: JSON.stringify({ info: entity }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then
                (
                function (data) {
                    if (data.d[0] == "1") {
                        ShowSuccess("", data.d[1]);
                        grdRequets.PerformCallback();
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

        function Get(RowID) {

            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~") %>Pages/SubmitRequest.aspx/Get',
                data: JSON.stringify({ RowID: RowID }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then(
                function (data) {
                    if (data.d[0] == "1") {

                        var values = JSON.parse(data.d[1]);
                        cmbRequests.SetValue(values.TypeID);
                        $("#txtDescription").val(values.Description);

                    } else {
                        ErrorMessage(data.d[1]);
                    }
                }, function (data) {
                    ErrorMessage();
                });

        }

    </script>

</asp:Content>

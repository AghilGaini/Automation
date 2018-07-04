<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Automation.Pages.Login" NeedLogin="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ورود</title>

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="~/Styles/bootstrap.min.css" />
    <link rel="stylesheet" href="~/Styles/toastr.min.css" />
    <link rel="stylesheet" href="~/Styles/style.css" />
    <script type="text/javascript" src='<%= ResolveUrl("~") %>scripts/jquery.min.js'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~") %>scripts/bootstrap.min.js'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~") %>scripts/toastr.min.js'></script>
    <script type="text/javascript" src='<%= ResolveUrl("~") %>scripts/script.js'></script>

    <script type="text/javascript">

        function CheckLogin()
        {
            var entity = {};
            entity.Username = $("#txtUsername").val();
            entity.Password = $("#txtPassword").val();

            entity = JSON.stringify(entity);


            $.ajax({
                type: 'POST',
                url: '<%= ResolveUrl("~") %>Pages/Login.aspx/CheckLogin',
                data: JSON.stringify({ info: entity }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            }).then
            (
            function (data) {
                if (data.d[0] == "1") {
                    window.location = "../Default.aspx";
                }
                else if (data.d[0] == "0") {
                    ShowError("",data.d[1]);
                }
            }, function (data) {
                ShowError("", "عدم برقراری ارتباط");
            }
            )
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <div class="panel panel-primary CustomMargin">
                <div class="panel-heading">ورود</div>
                <div class="panel-body">

                    <div class="row FieldMargin">
                        <div class="col-lg-8 col-md-8 col-sm-8">
                            <input type="text" class="form-control" id="txtUsername" placeholder="نام کاربری" />
                        </div>
                    </div>

                    <div class="row FieldMargin">
                        <div class="col-lg-8 col-md-8 col-sm-8">
                            <input type="password" class="form-control" id="txtPassword" placeholder="رمز عبور" />
                        </div>
                    </div>

                    <div class="row CustomMargin">
                        <div class="col-lg-8 col-md-8 col-sm-8">
                            <button type="button" class="btn btn-primary" onclick="CheckLogin()">ورود</button>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </form>
</body>
</html>

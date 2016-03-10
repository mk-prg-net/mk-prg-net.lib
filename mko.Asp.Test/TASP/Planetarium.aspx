<%@ Page Title="" Language="C#" MasterPageFile="~/MkoContent.Master" AutoEventWireup="true" CodeBehind="Planetarium.aspx.cs" Inherits="mko.Asp.Test.TASP.Planetarium" EnableTheming="true" Theme="Design1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Planetarium</title>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="leftPicBarContentPlaceHolder" runat="server">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/Pictures/Urknall.png"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <script src="../Scripts/jquery-2.1.0.js"></script>
   
    <script>
        $(document).ready(function () {
            $("a").click(function (event) {
                alert("Hallooooo");
            });
        });
    </script>

    <a href="http://www.mkoit.de">mkoit</a>

</asp:Content>

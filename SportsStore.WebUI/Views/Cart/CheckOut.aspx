<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SportsStore.Domain.Entities.ShippingDetails>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	SportsStore : Check Out
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <h2>Check out now</h2>
    Please enter your details, and we'll ship your goods right away!
    <% using (Html.BeginForm()) { %>
        <h3>Ship to</h3>    
        <%: Html.EditorForModel() %>

        <p align="center">
            <input type="submit" value="Complete order" />
        </p>
    <% } %>
</asp:Content>

﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SportsStore.WebUI.Models.CartIndexViewModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	SportsStore : Your Cart
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <h2>Your cart</h2>
    <table width="90%" align="center">
        <thead>
            <tr>
                <th align="center">Quantity</th>
                <th align="left">Item</th> 
                <th align="right">Price</th> 
                <th align="right">Subtotal</th> 
            </tr>
        </thead>
        <tbody>
            <% foreach (var line in Model.Cart.Lines) { %>
                <tr>
                    <td align="center"><%: line.Quantity %></td>
                    <td align="left"><%: line.Product.Name %></td>
                    <td align="right"><%: line.Product.Price.ToString("c") %></td>
                    <td align="right"><%: (line.Quantity * line.Product.Price).ToString("c") %></td>
                    <td>
                        <% using (Html.BeginForm("RemoveFromCart", "Cart")) { %>
                            <%: Html.Hidden("productId", line.Product.ProductID) %>
                            <%: Html.HiddenFor(x => x.ReturnUrl) %>
                            <input type="submit" value="Remove" />
                        <% } %>
                    </td>
                </tr>
            <% } %>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="3" align="right">Tolal:</td>
                <td align="right"><%: Model.Cart.ComputeTotalValue().ToString("c") %></td>
            </tr>
        </tfoot>
    </table>

    <p align="center" class="actionButtons">
        <a href="<%: Model.ReturnUrl %>">Continue Shopping</a>
        <%: Html.ActionLink("Check out now", "CheckOut") %>
    </p>
</asp:Content>

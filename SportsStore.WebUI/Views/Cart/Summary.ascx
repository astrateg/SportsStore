﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SportsStore.Domain.Entities.Cart>" %>

<% if (Model.Lines.Count > 0) { %>
    <div id="cart">
        <span class="caption">
            <strong>Your cart:</strong>
            <%: Model.Lines.Sum(x => x.Quantity) %> item(s),
            <%: Model.ComputeTotalValue().ToString("c") %>
        </span>
        <%: Html.ActionLink("Check out", "Index", "Cart", new { returnUrl = Request.Url.PathAndQuery}, null )%>
    </div>
<% } %>
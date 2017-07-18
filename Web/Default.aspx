<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WorldStateWeb._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    </asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="main-content-container">
    <div id="alerts">
        <h1>Alerts</h1> 
        <table>
            <%= alerts.ToString() %>
        </table>
    </div>
    <div id="sorties">
        <h1>Sorties</h1>
        <table>
            <%= sorties.ToString() %>
        </table>
    </div>
    <div id="invasions">
        <h1>Invasions</h1>
        <table>
            <%= invasions.ToString() %>
        </table>
    </div>
    <div id="fissures">
        <h1>Void Fissures</h1>
        <table>
            <%= fissures.ToString() %>
        </table>
    </div>
    <div id="voidTraders">
        <h1>Void Traders</h1>
        <table>
            <%= voidTraders.ToString() %>
        </table>
    </div>
    <div id="invEventStat">
        <h1>Invasion Event Progress</h1>
        <div>
            <%= invEventStat.ToString() %>
        </div>
    </div>
    <div id="events">
        <h1>Events (News)</h1>
        <div>
            <%= events.ToString() %>
        </div>
    </div>
    </div>

    <%--<ol class="round">
        <li class="one">
            <h5>Getting Started</h5>
            ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245146">Learn more…</a>
        </li>
        <li class="two">
            <h5>Add NuGet packages and jump-start your coding</h5>
            NuGet makes it easy to install and update free libraries and tools.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245147">Learn more…</a>
        </li>
        <li class="three">
            <h5>Find Web Hosting</h5>
            You can easily find a web hosting company that offers the right mix of features and price for your applications.
            <a href="http://go.microsoft.com/fwlink/?LinkId=245143">Learn more…</a>
        </li>
    </ol>--%>
</asp:Content>

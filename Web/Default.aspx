<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WorldStateWeb._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    </asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="main-content-container">
    <div id="alerts" class="content-element">
        <h1>Alerts</h1> 
        <table>
            <%= alerts.InnerWriter.ToString() %>
        </table>
    </div>
    <div id="sorties" class="content-element">
        <h1>Sorties</h1>
        <table>
            <%= sorties.InnerWriter.ToString() %>
        </table>
    </div>
    <div id="invasions" class="content-element">
        <h1>Invasions</h1>
        <table>
            <%= invasions.InnerWriter.ToString() %>
        </table>
    </div>
    <div id="invEventStat" class="content-element">
        <h1>Invasion Event Progress</h1>
        <div class="inner-content">
            <%= invEventStat.InnerWriter.ToString() %>
        </div>
    </div>
    <div id="fissures" class="content-element">
        <h1>Void Fissures</h1>
        <table>
            <%= fissures.InnerWriter.ToString() %>
        </table>
    </div>
    <div id="voidTraders" class="content-element">
        <h1>Baro Ki'Teer</h1>
        <table>
            <%= voidTraders.InnerWriter.ToString() %>
        </table>
    </div>
    <div id="events" class="content-element">
        <h1>News</h1>
        <div class="inner-content">
            <%= events.InnerWriter.ToString() %>
        </div>
    </div>
    <div id="darvoDeal" class="content-element">
        <h1>Daily Darvo Deal</h1>
        <div class="inner-content">
            <%= darvoDeal.InnerWriter.ToString() %>
        </div>
    </div>
    <div id="pvpChallenges" class="content-element">
        <h1>PVP Challenges</h1>
        <div class="inner-content">
            <%= pvpChallenges.InnerWriter.ToString() %>
        </div>
    </div>
    <div id="flashSales" class="content-element">
        <h1>Market Items and Flash Sales</h1>
        <table>
            <%= flashSales.InnerWriter.ToString() %>
        </table>
    </div>
    <div id="nodeOverrides" class="content-element">
        <h1>Node Overrides</h1>
        <table>
            <%= nodeOverrides.InnerWriter.ToString() %>
        </table>
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

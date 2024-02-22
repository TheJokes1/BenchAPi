# blazor app using FLuentUI

1. ** setup
Update Visual studio, use latest fluent UI version 4.1.1
follow instructions: https://fluentui-blazor.net/CodeSetup

2. ** Code using FLuentUI components:


@page "/cafes"
@using Microsoft.FluentUI.AspNetCore.Components
@using BlazorAppFluentUI.DataModel
@using Services
@inject ICafeService CafeService
@inject NavigationManager NavigationManager

<h3>Cafes</h3>
<FluentCard>
  <h2>Hello World!</h2>
    <FluentButton IconStart="@(new Icons.Filled.Size20.AirplaneTakeOff())" Appearance="@Appearance.Outline" OnClick="GetCafesAsync"
        Disabled="@isLoading">Click Me</FluentButton>
</FluentCard>
<br/>
<FluentCard>
 @if (isLoading)
    {
        <div style="width: 200px;display: grid; grid-gap: 12px; grid-auto-flow: column;">
            <FluentProgressRing></FluentProgressRing>
            @* dit is een spinner *@
        </div>
    }
@if (cafes != null)
{
    <FluentDataGrid Items="@cafes">
        <PropertyColumn Property="@(c => c.Name)" Sortable="true" />
        <PropertyColumn Property="@(c => c.OwnerName)" Sortable="true"/>
        <PropertyColumn Property="@(c => c.City)" Sortable="true" />
        <TemplateColumn Title="Actions" Align="@Align.End">
            <FluentButton IconEnd="@(new Icons.Filled.Size20.Edit())" Appearance="@Appearance.Outline" OnClick="@(() => GoToDetail(context))"></FluentButton>
            <FluentButton IconEnd="@(new Icons.Filled.Size20.List())" Appearance="@Appearance.Outline" OnClick="GoToOrders" />
        </TemplateColumn>
    </FluentDataGrid>

       @*  @foreach (Cafe cafe in cafes)
        {
            <p>@cafe.Name</p>
        } *@
}
</FluentCard>

@code {
    private List<Cafe> cafes1;
    private IQueryable<Cafe> cafes;
    private bool isLoading;

    private async Task GetCafesAsync()
    {
        isLoading = true;

        try
        {
            cafes1 = await CafeService.GetCafesAsync();
            cafes = cafes1.AsQueryable();
        }
        finally
        {
            isLoading = false;
        }
        StateHasChanged();
    }

    private void GoToOrders()
    {

    }

    private void GoToDetail(Cafe cafe)
    {
        int id = cafe.Id;
        string url = $"/cafes/{id}";
        NavigationManager.NavigateTo(url);
    }
}

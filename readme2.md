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

<h3>Cafes</h3>
<FluentCard>
  <h2>Hello World!</h2>
  <FluentButton IconStart="@(new Icons.Regular.Size16.Globe())" Appearance="@Appearance.Neutral" OnClick="GetCafesAsync"
        Disabled="@isLoading">Click Me</FluentButton>
</FluentCard>
<br/>
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
    </FluentDataGrid>

       @*  @foreach (Cafe cafe in cafes)
        {
            <p>@cafe.Name</p>
        } *@
}

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
   
}

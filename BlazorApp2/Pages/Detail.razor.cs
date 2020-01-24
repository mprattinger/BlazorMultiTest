using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Pages
{
    public partial class Detail
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string KopId { get; set; } = string.Empty;

        public string UnitId { get; set; } = string.Empty;

        public string GroupId { get; set; } = string.Empty;

        public string CurrentUri { get; set; } = string.Empty;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
                CurrentUri = uri.ToString();
                Console.WriteLine($"OnAfterRender {CurrentUri}");

                if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("unitid", out var unitid))
                {
                    UnitId = unitid.First();
                }

                StateHasChanged();
            }
        }

        private void NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            Console.WriteLine(e.Location);
            var uri = new Uri(e.Location);
            CurrentUri = uri.ToString();

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("unitid", out var unitid))
            {
                UnitId = unitid.First();
            }

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("groupid", out var groupid))
            {
                GroupId = groupid.First();
            } else
            {
                GroupId = string.Empty;
            }
            StateHasChanged();
        }

        public void ToItems(string groupId)
        {
            NavigationManager.NavigateTo($"/detail/{KopId}?unitid={UnitId}&groupid={groupId}");
        }

        public void SaveItem(string reasonId)
        {
            Console.WriteLine($"Storing for {UnitId} with errorid {KopId} with group {GroupId} and reason id {reasonId}!");
            NavigationManager.NavigateTo($"/overview/{UnitId}");
        }
    }
}

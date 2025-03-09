using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Orders.FrontEnd.Repositories;
using Orders.FrontEnd.Shared;
using Orders.Shared.Entities;
using System.Net;

namespace Orders.FrontEnd.Pages.States
{
    [Authorize(Roles = "Admin")]
    public partial class StateEdit
    {
        private State? State;
        private FormWithName<State>? StateForm;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        [Parameter] public int StateId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var responseHttp = await Repository.GetAsync<State>($"/api/states/{StateId}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    Return();
                }
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            State = responseHttp.Response;
        }

        private async Task SaveAsync()
        {
            var responseHttp = await Repository.PutAsync("/api/states", State);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            Return();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000,
            });    
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con exito");    

        }
        private void Return()
        {
            StateForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/countries/details/{State!.CountryId}");
        }
    }
}

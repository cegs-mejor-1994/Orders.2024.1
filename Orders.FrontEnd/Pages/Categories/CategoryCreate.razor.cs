using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Orders.FrontEnd.Pages.Countries;
using Orders.FrontEnd.Repositories;
using Orders.FrontEnd.Shared;
using Orders.Shared.Entities;

namespace Orders.FrontEnd.Pages.Categories
{
    [Authorize(Roles = "Admin")]
    public partial class CategoryCreate
    {
        private Category Category = new();
        private FormWithName<Category>? CategoryForm;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("api/categories", Category);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con exito");
        }

        private void Return()
        {
            CategoryForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/categories");
        }
    }
}

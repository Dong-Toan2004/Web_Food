using Assignment.Application.DataTransferObj.LoginDto;
using Microsoft.AspNetCore.Components;
using Web.Services.IServices;

namespace Web.Components.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject] private IUserService UserService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        private LoginRequest LoginRequest = new LoginRequest();     
        private bool ShowErrors;
        private string ErrorMessage = "";

        private async Task HandleLoginUser()
        {
            ShowErrors = false;
            var response = await UserService.Login(LoginRequest);
            if (response.Successfull)
            {
                ShowErrors = true;
                ErrorMessage = "Đăng nhập thành công";
                var token = response.Token;
                Console.WriteLine(token);
                NavigationManager.NavigateTo("/");
            }
            else
            {
                ShowErrors = true;
                ErrorMessage = "Đăng nhập thất bại";
            }
        }
    }
}

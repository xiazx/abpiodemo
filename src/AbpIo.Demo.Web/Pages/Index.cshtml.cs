using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace AbpIo.Demo.Web.Pages;

public class IndexModel : DemoPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}

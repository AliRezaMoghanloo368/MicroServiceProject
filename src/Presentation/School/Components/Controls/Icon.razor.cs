using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace School.Components.Controls
{
    public partial class Icon
    {
        [Parameter] public RenderFragment? Image { get; set; }
        [Parameter] public string? Title { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public string? IconBoxStyle { get; set; }
        [Parameter] public string? IconTopNavStyle { get; set; }
        [Parameter] public string? IconTitleStyle { get; set; }
    }
}

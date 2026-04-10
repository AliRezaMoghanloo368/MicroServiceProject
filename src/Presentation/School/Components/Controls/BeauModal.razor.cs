using Microsoft.AspNetCore.Components;
using static School.Components.Classes.BeauEnums;

namespace School.Components.Controls
{
    public partial class BeauModal
    {
        [Parameter] public bool IsVisible { get; set; }
        [Parameter] public EventCallback<bool> IsVisibleChanged { get; set; }
        [Parameter] public Action<DialogResult> ShowDialog { get; set; }
        [Parameter] public string Title { get; set; } = "";
        [Parameter] public RenderFragment Body { get; set; }

        private void CancelModal()
        {
            IsVisible = false;
            IsVisibleChanged.InvokeAsync(IsVisible);
            ShowDialog.Invoke(DialogResult.Cancel);
        }
        private void OKModal()
        {
            IsVisible = false;
            IsVisibleChanged.InvokeAsync(IsVisible);
            ShowDialog.Invoke(DialogResult.OK);
        }
    }
}

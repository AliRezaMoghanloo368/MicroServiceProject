using Microsoft.AspNetCore.Components;
using static School.Components.Classes.BeauEnums;

namespace School.Components.Controls
{
    public partial class TaraMenuModal
    {
        [Parameter] public bool IsVisible { get; set; }
        [Parameter] public EventCallback<bool> IsVisibleChanged { get; set; }
        [Parameter] public Action<DialogResult> ShowDialog { get; set; }
        [Parameter] public string Title { get; set; } = "";
        [Parameter] public RenderFragment Body { get; set; }
        [Parameter] public string? ModalContentStyle { get; set; }

        private void CancelModal()
        {
            IsVisible = false;
            IsVisibleChanged.InvokeAsync(false);
            ShowDialog.Invoke(DialogResult.Cancel);
            this.StateHasChanged();
        }
        private void OKModal()
        {
            IsVisible = false;
            IsVisibleChanged.InvokeAsync(IsVisible);
            ShowDialog.Invoke(DialogResult.OK);
        }
    }
}

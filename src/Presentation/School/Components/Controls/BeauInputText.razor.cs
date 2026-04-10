using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace School.Components.Controls
{
    public partial class BeauInputText
    {
        private string? _btnClose = "display:none";
        private bool? _showClose = false;
        [Parameter]
        public bool? ShowClose
        {
            get { return _showClose; }
            set
            {
                _showClose = value;
                if (value == false)
                    _btnClose = "display:none";
                else
                    _btnClose = "display:flex";
            }
        }

        private string? _btnDown = "display:none";
        private bool? _showDown = false;
        [Parameter] public EventCallback<MouseEventArgs> DownClick { get; set; }
        [Parameter]
        public bool? ShowDown
        {
            get { return _showDown; }
            set
            {
                _showDown = value;
                if (value == false)
                    _btnDown = "display:none";
                else
                    _btnDown = "display:flex";
            }
        }

        private string? _btnSearch = "display:none";
        private bool? _showSearch = false;
        [Parameter] public EventCallback<MouseEventArgs> SearchClick { get; set; }
        [Parameter]
        public bool? ShowSearch
        {
            get { return _showSearch; }
            set
            {
                _showSearch = value;
                if (value == false)
                    _btnSearch = "display:none";
                else
                    _btnSearch = "display:flex";
            }
        }

        private string? _btnPassword = "display:none";
        private bool? _showPassword = false;
        [Parameter] public EventCallback<MouseEventArgs> PasswordClick { get; set; }
        [Parameter]
        public bool? ShowPassword
        {
            get { return _showPassword; }
            set
            {
                _showPassword = value;
                if (value == false)
                    _btnPassword = "display:none";
                else
                    _btnPassword = "display:flex";
            }
        }

        private string? _btnCustomIcon = "display:none";
        private bool? _showCustomIcon = false;
        [Parameter] public RenderFragment? CustomIcon { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> CustomIconClick { get; set; }
        [Parameter]
        public bool? ShowCustomIcon
        {
            get { return _showCustomIcon; }
            set
            {
                _showCustomIcon = value;
                if (value == false)
                    _btnCustomIcon = "display:none";
                else
                    _btnCustomIcon = "display:flex";
            }
        }

        private bool ShowFloatingLabel => IsFocused || !string.IsNullOrWhiteSpace(Value);
        [Parameter] public bool? ShowTitle { get; set; } = false;
        [Parameter] public string? Class { get; set; }
        [Parameter] public string? Id { get; set; }
        [Parameter] public string? Mask { get; set; } = string.Empty;
        [Parameter] public string Title { get; set; } = string.Empty;
        [Parameter] public string PlaceHolder { get; set; } = string.Empty;
        [Parameter] public bool UpdateOnInput { get; set; } = true;
        [Parameter] public string Type { get; set; } = "text";
        protected string ComputedClass => $"txt-box font-w700 {Class}".Trim();
        private bool IsFocused { get; set; }
        private bool IsTypePassword { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && Id != null && Mask != null)
                await _js.InvokeVoidAsync("applyMask", Id, Mask);
        }

        private void OnInput(ChangeEventArgs e)
        {
            // CurrentChars = e?.Value?.ToString()?.Length ?? 0;
            if (UpdateOnInput)
                CurrentValueAsString = e?.Value?.ToString() ?? null;
        }

        private void OnChange(ChangeEventArgs e)
        {
            if (!UpdateOnInput)
                CurrentValueAsString = e?.Value?.ToString() ?? null;
        }

        private void CloseClick(MouseEventArgs e)
        {
            if (e.Button == 0)
            {
                this.CurrentValueAsString = "";
            }
        }

        private void OnFocus()
        {
            IsFocused = true;
        }

        private void OnBlur()
        {
            IsFocused = false;
        }

        private void OnPasswordClick(MouseEventArgs e)
        {
            IsTypePassword = !IsTypePassword;
            _js.InvokeVoidAsync("inputPassword", Id);
        }
    }
}

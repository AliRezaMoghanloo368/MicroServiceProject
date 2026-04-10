using Microsoft.AspNetCore.Components;

namespace School.Components.Controls
{
    public partial class BeauButton
    {
        [Parameter] public string? Id { get; set; }
        [Parameter] public string Title { get; set; } = "Button";
        [Parameter] public string? Class { get; set; }

        private string? _butttonStyle;
        private ButtonType? _buttonType;
        [Parameter] public ButtonType? Type 
        {
            get { return _buttonType; }
            set
            {
                _buttonType = value;
                switch(_buttonType)
                {   
                    case ButtonType.Primary:
                        _butttonStyle = "btn btn-primary";
                        break;
                    case ButtonType.Secondary:
                        _butttonStyle = "btn btn-secondary";
                        break;
                    case ButtonType.Success:
                        _butttonStyle = "btn btn-success";
                        break;
                    case ButtonType.Danger:
                        _butttonStyle = "btn btn-danger";
                        break;
                    case ButtonType.Warning:
                        _butttonStyle = "btn btn-warning";
                        break;
                    case ButtonType.Info:
                        _butttonStyle = "btn btn-info";
                        break;
                    case ButtonType.Light:
                        _butttonStyle = "btn btn-light";
                        break;
                    case ButtonType.Dark:
                        _butttonStyle = "btn btn-dark";
                        break;
                    case ButtonType.Link:
                        _butttonStyle = "btn btn-link";
                        break;
                }
            } 
        }
        [Parameter] public EventCallback OnClick { get; set; }
        [Parameter] public string? Style { get; set; }
        [Parameter] public string? IconClass { get; set; }

        public enum ButtonType
        {
            Primary, Secondary, Success, Danger, Warning, Info, Light, Dark, Link
        }
    }
}

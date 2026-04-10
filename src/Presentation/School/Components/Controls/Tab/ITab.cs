using Microsoft.AspNetCore.Components;

public interface ITab
{
    string Id { get; }
    string? Title { get; }
    RenderFragment? ChildContent { get; }
}

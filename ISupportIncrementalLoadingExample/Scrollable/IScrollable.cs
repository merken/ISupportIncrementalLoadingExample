
namespace ISupportIncrementalLoadingExample.Scrollable
{
    public interface IScrollable
    {
        float VerticalOffset { get; set; }

        float HorizontalOffset { get; set; }

        float ZoomFactor { get; set; }
    }
}

namespace Epilog.Classification.Contracts
{
    public interface ILabel
    {
        string Value { get; set; }

        ILabelFactory LabelFactory { get; }
    }
}

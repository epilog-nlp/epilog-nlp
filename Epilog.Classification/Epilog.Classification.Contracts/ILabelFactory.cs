namespace Epilog.Classification.Contracts
{
    public interface ILabelFactory
    {
        ILabel Create(string value);

        ILabel Create(string value, int options);

        ILabel Create(ILabel label);
    }
}

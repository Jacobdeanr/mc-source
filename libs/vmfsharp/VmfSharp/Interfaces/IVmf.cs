namespace VmfSharp.Interfaces
{
    public interface IVmf
    {
        public string Name { get; }

        public string ToString(string indentPadding);
        public string ToString(int indentation);
    }
}
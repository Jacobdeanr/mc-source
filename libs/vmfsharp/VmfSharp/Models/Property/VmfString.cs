namespace VmfSharp.Models.Property
{
    public class VmfString : Vmf<string>
    {
        public VmfString(string name) : base(name)
        {
        }
        
        public VmfString(string name, string value) : base(name, value)
        {
        }
    }
}
namespace dol_sdk.POCOs
{
    using Enums;

    public interface IArea
    {
        int X { get; set; }
        int Y { get; set; }
        string Region { get; set; }
        string Description { get; set; }
        bool IsCoastal { get; set; }
        Ecosystem Ecosystem { get; set; }
        Navigation Navigation { get; set; }
    }

    public class Area: IArea
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Region { get; set; }
        public string Description { get; set; }
        public bool IsCoastal { get; set; }
        public Ecosystem Ecosystem { get; set; }
        public Navigation Navigation { get; set; }
    }
}
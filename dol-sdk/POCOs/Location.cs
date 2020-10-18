namespace dol_sdk.POCOs
{
    using Enums;

    public interface ILocation
    {
        string Name { get; set; }
        string Description { get; set; }
        Place Place { get; set; }
    }

    public class Location : ILocation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Place Place { get; set; }
    }
}
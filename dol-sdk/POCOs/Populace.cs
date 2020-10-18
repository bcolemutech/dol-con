namespace dol_sdk.POCOs
{
    using System;
    using System.Drawing;

    public interface IPopulace
    {
        string Name { get; set; }
        string Description { get; set; }
        int Size { get; set; }
        bool HasPort { get; set; }
    }

    public class Populace : IPopulace
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public bool HasPort { get; set; }
    }
}
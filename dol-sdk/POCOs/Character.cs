namespace dol_sdk.POCOs
{
    public interface ICharacter
    {
        string Name { get; set; }
    }

    public class Character : ICharacter
    {
        public string Name { get; set; }
    }
}
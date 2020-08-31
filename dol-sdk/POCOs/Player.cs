using System.Collections.Generic;
using dol_sdk.Enums;

namespace dol_sdk.POCOs
{
    public class Player
    {
        public Authority Authority { get; set; }
        public IList<Character> Characters { get; set; }
    }
}

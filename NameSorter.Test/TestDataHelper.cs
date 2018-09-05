namespace NameSorter.Test
{
    using System.Collections.Generic;

    public static class TestDataHelper 
    {
        public static readonly string[] Names = {
            "Janet Parsons",
            "Vaughn Lewis",
            "Adonis Julius Archer",
            "Shelby Nathan Yoder",
            "Marin Alvarez",
            "London Lindsey",
            "Beau Tristan Bentley",
            "Leo Gardner",
            "Hunter Uriah Mathew Clarke",
            "Mikayla Lopez",
            "Frankie Conner Ritter",
            "Jack O'Neill",
            "Fred P. O'Neill",
            "Samantha Carter"
        };

        public static readonly string[] ExprectedSortedNames = {
            "Marin Alvarez",
            "Adonis Julius Archer",
            "Beau Tristan Bentley",
            "Samantha Carter",
            "Hunter Uriah Mathew Clarke",
            "Leo Gardner",
            "Vaughn Lewis",
            "London Lindsey",
            "Mikayla Lopez",
            "Fred P. O'Neill",
            "Jack O'Neill",
            "Janet Parsons",
            "Frankie Conner Ritter",
            "Shelby Nathan Yoder"
        };
    }
}

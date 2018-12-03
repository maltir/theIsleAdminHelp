namespace TheIsleAdminHelp.enumObj
{
    public enum Race
    {
        Allo,
        Carno,
        Cerato,
        Dilo,
        Giga,
        Rex,
        Utah,
        Acro,
        Spino,
        Velociraptor,
        Sucho,
        Herrera,
        Bary,
        Austro,
        Albert,

        Diablo,
        Dryo,
        Galli,
        Maia,
        Para,
        Trike,
        Pachy,
        Shan,
        Camara,
        Ava,
        Anky,
        Oro,
        Psittaco,
        Puerta,
        Stego,
        Therizino,

        Autre
    }

    static class RaceEnumMethod
    {

        public static bool isCarni(this Race s1)
        {

            switch (s1)
            {
                case Race.Allo:
                case Race.Carno:
                case Race.Cerato:
                case Race.Dilo:
                case Race.Giga:
                case Race.Rex:
                case Race.Utah:
                case Race.Acro:
                case Race.Spino:
                case Race.Velociraptor:
                case Race.Sucho:
                case Race.Herrera:
                case Race.Bary:
                case Race.Austro:
                case Race.Albert:
                    return true;
                default:
                    return false;
            }
        }
    }
}

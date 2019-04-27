using ConsoleTestes.Interfac;

namespace ConsoleTestes.OrganizarEnun
{
    public class Item
    {
        public enum Tipo
        {
            Poção,
            Arma,
            Container,
            Bebida,
            Comida,
            Livro,
            Móvel,
            Arco
        }

        public string Nome { get; set; }

        public Tipo TipoItem { get; set; }

        //public static string GetDescription<T>(this T e) where T : IConvertible
        //{
        //    if (e is Enum)
        //    {
        //        Type type = e.GetType();
        //        Array values = System.Enum.GetValues(type);

        //        foreach (int val in values)
        //        {
        //            if (val == e.ToInt32(CultureInfo.InvariantCulture))
        //            {
        //                var memInfo = type.GetMember(type.GetEnumName(val));
        //                var descriptionAttribute = memInfo[0]
        //                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
        //                    .FirstOrDefault() as DescriptionAttribute;

        //                if (descriptionAttribute != null)
        //                {
        //                    return descriptionAttribute.Description;
        //                }
        //            }
        //        }
        //    }

        //    return null; // could also return string.Empty
        //}
    }
}

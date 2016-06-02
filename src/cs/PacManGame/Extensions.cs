using System;
using System.Collections.Generic;
using System.Linq;

namespace PacManGame
{
    public interface IEnumHelper
    {
    }

    public abstract class EnumHelper
    {
        protected static IDictionary<Type, IEnumHelper> Helpers;

        static EnumHelper()
        {
            Helpers = new Dictionary<Type, IEnumHelper>();
        }
    }

    public class EnumHelper<T> : EnumHelper, IEnumHelper
    {
        internal static IEnumerable<T> GetValues()
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                var message = string.Format("Type {0} is not an enum type.", type);
                throw new InvalidOperationException(message);
            }
            foreach (T value in Enum.GetValues(type))
            {
                yield return value;
            }
        }

        private readonly IList<T> _values;

        private readonly Random _rand = new Random();

        public EnumHelper()
        {
            _values = GetValues().ToList();
            Helpers[typeof(T)] = this;
        }

        static EnumHelper()
        {
            if (!Helpers.ContainsKey(typeof(T)))
            {
                // ReSharper disable once ObjectCreationAsStatement
                new EnumHelper<T>();
            }
        }

        public static IEnumerable<T> GetPossibleValues(params T[] except)
        {
            // TODO: TBD: I'm sure a better flow field could be established to help govern Monster behavior than "random"
            var helper = (EnumHelper<T>) EnumHelper.Helpers[typeof(T)];
            return helper._values.Except(except).ToArray();
        }
    }

    public static class EnumerableExtensionMethods
    {
        private static readonly Random Rnd = new Random((int) (DateTime.UtcNow.Ticks%int.MaxValue));

        public static T ElementAtRandom<T>(this IEnumerable<T> values)
        {
            /* Helps with our distribution of possible answers, which also
             * encourages the monster out of any corners he may run into. */

            // ReSharper disable once PossibleMultipleEnumeration
            var i = Rnd.Next()%values.Count();

            // ReSharper disable once PossibleMultipleEnumeration
            return values.ElementAt(i);
        }
    }
}

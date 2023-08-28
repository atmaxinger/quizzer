using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Quizzer.TriviaServer.Attributes
{
    public class MinimumNumberOfItems : ValidationAttribute
    {
        private readonly int _minimumNumberOfItems;
        public MinimumNumberOfItems(int Number = 1)
        {
            _minimumNumberOfItems = Number;
        }

        public override bool IsValid(object? value)
        {
            var list = value as ICollection;
            if (list != null)
            {
                return list.Count >= _minimumNumberOfItems;
            }

            return false;
        }
    }
}

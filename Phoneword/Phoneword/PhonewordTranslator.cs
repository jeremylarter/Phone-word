using System.Text;

namespace Core
{
    public static class PhonewordTranslator
    {
        public static string ToNumber(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return null;
            raw = raw.ToUpperInvariant();

            var newNumber = new StringBuilder();
            foreach (var character in raw)
            {
                if (" -0123456789".Contains(character.ToString()))
                    newNumber.Append(character);
                else
                {
                    var result = TranslateToNumber(character);
                    if (result != null)
                        newNumber.Append(result);
                    else
                    {
                        return null;
                    }
                }
            }
            return newNumber.ToString();
        }

        private static readonly string[] Digits = {"ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"};

        private static int? TranslateToNumber(char character)
        {
            for (var i = 0; i < Digits.Length; i++)
            {
                if (Digits[i].Contains(character.ToString()))
                    return 2 + i;
            }
            return null;
        }
    }
}
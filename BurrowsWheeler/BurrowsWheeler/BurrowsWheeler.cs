

namespace BurrowsWheeler
{
    /// <summary>
    /// Class <c>Burrows-Wheeler Transform</c> which has methods of direct and inverse transform using Burrows-Wheeler algorithm.
    /// </summary>
    public class BWT
    {
        /// <summary>
        /// Transform string using the Burrows-Wheeler algorithm.
        /// </summary>
        /// <param name="originalString">String which will be transformed.</param>
        /// <returns>Transformed string by Burrows-Wheeler algorithm and index of position the original string begins with in table of shifts.</returns>
        public static (string, int) Transform(string originalString)
        {
            if (originalString == "")
            {
                return ("", 0);
            }

            int[] suffixArray = Enumerable.Range(0, originalString.Length).ToArray();

            Array.Sort(suffixArray, (i, j) => Compare(originalString, i, j));

            var resultArray = new char[originalString.Length];
            for (int i = 0; i < originalString.Length; ++i)
            {
                resultArray[i] = suffixArray[i] > 0 ? originalString[suffixArray[i] - 1] : originalString[^1];
            }

            int endPosition = Array.IndexOf(suffixArray, 0);

            return (new string(resultArray), endPosition);
        }

        /// <summary>
        /// Make inverse transform of Burrows-Wheeler string to original string.
        /// </summary>
        /// <param name="bwtString">Transformed string by Burrows-Wheeler algorithm.</param>
        /// <param name="endPosition">Index of position the original string begins with in table of shifts.</param>
        /// <returns>Original string.</returns>
        public static string InverseTransform(string bwtString, int endPosition)
        {
            if (bwtString == "")
            {
                return "";
            }

            var sortedChars = bwtString.ToCharArray();
            Array.Sort(sortedChars);

            var count = new Dictionary<char, int>();
            for (int i = 0; i < bwtString.Length; ++i)
            {
                if (!count.ContainsKey(sortedChars[i]))
                {
                    count[sortedChars[i]] = i;
                }
            }

            var positions = new int[bwtString.Length];
            for (int i = 0; i < bwtString.Length; ++i)
            {
                positions[count[bwtString[i]]] = i;
                ++count[bwtString[i]];
            }

            var resultArray = new char[bwtString.Length];
            int index = positions[endPosition];
            for (int i = 0; i < bwtString.Length; ++i)
            {
                resultArray[i] = bwtString[index];
                index = positions[index];
            }

            return new string(resultArray);
        }

        private static int Compare(string originalString, int firstIndex, int secondIndex)
        {
            for (int i = 0; i < originalString.Length; ++i)
            {
                if (originalString[(i + firstIndex) % originalString.Length] < originalString[(i + secondIndex) % originalString.Length])
                {
                    return -1;
                }
                if (originalString[(i + firstIndex) % originalString.Length] > originalString[(i + secondIndex) % originalString.Length])
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}

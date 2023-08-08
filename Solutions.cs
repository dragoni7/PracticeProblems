namespace PracticeProblems
{ 
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val=0, ListNode next=null)
        {
            this.val = val;
            this.next = next;
        }
    }
    public class Solutions
    {
        /// <summary>
        /// 1. Two Sum
        /// 
        /// Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target.
        /// You may assume that each input would have exactly one solution, and you may not use the same element twice.
        /// You can return the answer in any order.
        /// 
        /// Constraints:
        /// 2 <= nums.length <= 10^4
        /// -10^9 <= nums[i] <= 10^9
        /// -10^9 <= target <= 10^9
        /// Only one valid answer exists.
        /// </summary>
        public static int[] TwoSum(int[] nums, int target)
        {
            int[] solution = new int[2];

            Dictionary<int, int> dict = new();

            for (int i = 0; i < nums.Length; i++) // O(n)
            {
                int toFind = target - nums[i];

                if (dict.ContainsKey(toFind)) // O(1)
                {
                    solution[0] = dict[toFind];
                    solution[1] = i;

                    return solution;
                }

                if (!dict.ContainsKey(nums[i])) // O(1)
                {
                    dict.Add(nums[i], i);
                }
            }
            
            return solution;
        }
        /// <summary>
        /// You are given two non-empty linked lists representing two non-negative integers.
        /// The digits are stored in reverse order, and each of their nodes contains a single digit.
        /// Add the two numbers and return the sum as a linked list.
        /// You may assume the two numbers do not contain any leading zero, except the number 0 itself.
        /// </summary>
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            string n1 = "", n2 = "";

            while (l1.next != null)
            {
                n1 = n1.Insert(0, l1.val.ToString());
                l1 = l1.next;
            }

            while (l2.next != null)
            {
                n2 = n2.Insert(0, l2.val.ToString());
                l2 = l2.next;
            }

            int result = Convert.ToInt32(n1) + Convert.ToInt32(n2);
            string resultStr = result.ToString();

            int[] values = resultStr.Select(n => Convert.ToInt32(n.ToString())).ToArray();

            Array.Reverse(values);
            ListNode sol = BuildLinkedList(values);
            return sol;
        }
        public static ListNode BuildLinkedList(int[] values)
        {
            ListNode root = new();
            root.val = values[0];
            root.next = new();

            ListNode current = root.next;

            for (int i = 1; i < values.Length; i++)
            {
                current.val = values[i];
                current.next = new();
                current = current.next;
            }

            return root;
        }

        /// <summary>
        /// Given a string s, find the length of the longest substring without repeating characters.
        /// </summary>
        public static int LengthOfLongestSubstring(string s)
        {
            int i = 0, j = 0, max = 0;
            // dictionary to keep track of substring
            Dictionary<char, int> dict = new();

            while (j < s.Length)
            {
                // if the char is yet to be contained in the dict, add it and move j along
                if (!dict.ContainsKey(s[j]))
                {
                    dict.Add(s[j], 1);
                    j++;
                    // update max to be the new substring length
                    max = Math.Max(max, dict.Count);
                }
                // if the repeating char is found, update the i index and remove it from the substring
                else
                {
                    dict.Remove(s[i]);
                    i++;
                }
            }

            return max;
        }

        /// <summary>
        /// Given a string s, return the longest palindromic substring in s.
        /// </summary>
        public static string LongestPalindrome(string s)
        {
            if (s == null || s.Length < 1)
            {
                return "";
            }

            int start = 0, end = 0;

            // iterate through each char, expanding from that char at i to see if a palindrome exists there
            for (int i = 0; i < s.Length; i++)
            {
                // determine length of potential palindrome centered at i
                int len1 = ExpandFromMiddle(s, i, i); // odd palindromes 1
                int len2 = ExpandFromMiddle(s, i, i + 1); // even palindromes
                int len = Math.Max(len1, len2);

                // update palindrome indexes if larger than previously found one
                if (len > end - start)
                {
                    start = i - ((len - 1) / 2);
                    end = i + (len / 2);
                }
            }

            int subLen = end - start + 1;

            return s.Substring(start, subLen);
        }
        private static int ExpandFromMiddle(string s, int i, int j)
        {
            if (s == null || i > j)
            {
                return 0;
            }

            // expand indexes only when chars are the same and within string bounds
            while (i >= 0 && j < s.Length && s[i] == s[j])
            {
                i--;
                j++;
            }

            return j - i - 1;
        }
        public static string ZigZagConversion(string s, int numRows)
        {
            if (numRows == 1)
            {
                return s;
            }

            List<List<char>> zigzag = new();

            // init lists
            for(int i = 0; i < numRows; i++)
            {
                zigzag.Add(new List<char>());
            }

            // index to keep track of place
            int index = 0;
            // bool to keep track of zagging or not
            bool zag = false;

            foreach (char c in s)
            {
                // if index reaches beginning, stop zag
                if (index == 0)
                {
                    zag = false;
                }

                zigzag[index].Add(c);

                if (zag)
                {
                    index--;
                }
                else
                {
                    index++;
                }

                // if index reaches end, time to zag
                if (index == numRows)
                {
                    zag = true;
                    index = numRows - 2;
                }
            }

            string result = "";

            foreach (List<char> row in zigzag)
            {
                foreach (char c in row)
                {
                    result += c;
                }
            }

            return result;
        }
        
        /// <summary>
        /// Given an integer x, returns true if x is a palindrome, false otherwise
        /// </summary>
        public static bool PalindromeNumber(int x)
        {
            if (x < 0)
            {
                return false;
            }

            return x == ReverseNumber(x);
        }
        private static int ReverseNumber(int x)
        {
            int reversed = 0;
            int num = x;

            while (num > 0)
            {
                reversed = reversed * 10 + num % 10;
                num /= 10;
            }

            return reversed;
        }

        /// <summary>
        /// Given a roman numeral, convert it to an integer
        /// </summary>
        public static int RomanToInt(string s)
        {
            Dictionary<char, int> numeralsDict = new()
            {
                { 'I', 1 },
                { 'V', 5 },
                { 'X', 10 },
                { 'L', 50 },
                { 'C', 100 },
                { 'D', 500 },
                { 'M', 1000 }
            };

            int result = 0;

            for (int i = 0; i < s.Length; i++)
            {
                // roman numerals are ordered from largest to smallest, aside from special cases like 4 or 9 where the first numeral is less than the next
                if (i+1 < s.Length && numeralsDict[s[i]] < numeralsDict[s[i+1]])
                {
                    result -= numeralsDict[s[i]];
                }
                else
                {
                    result += numeralsDict[s[i]];
                }
            }

            return result;
        }
        
        /// <summary>
        /// Given an integer, convert it to a roman numeral
        /// </summary>
        public static string IntToRoman(int num)
        {
            Dictionary<int, string> numeralsDict = new()
            {
                { 1,"I" },
                { 4, "IV" },
                { 5, "V" },
                { 9, "IX" },
                { 10, "X" },
                { 40, "XL" },
                { 50, "L" },
                { 90, "XC" },
                { 100, "C" },
                { 400, "CD" },
                { 500, "D" },
                { 900, "CM" },
                { 1000, "M" }
            };

            string numString = num.ToString();
            string result = "";

            for (int i = 0; i < numString.Length; i++)
            {
                // get the decimal place of the current digit
                int length = numString.Length - i - 1;
                // convert the char to temp string
                string temp = numString[i].ToString();

                if (temp != "0")
                {
                    // add trailing zeros according to decimal place
                    for (int j = 0; j < length; j++)
                    {
                        temp += "0";
                    }

                    int digit = int.Parse(temp);

                    // handles cases value existing in dictionary
                    if (numeralsDict.TryGetValue(digit, out var value))
                    {
                        result += value;
                    }
                    // otherwise build numeral according to decimal place
                    // ones
                    else if (length == 0)
                    {
                        result += BuildNumeral(1, digit, numeralsDict);
                    }
                    // tens
                    else if (length == 1)
                    {
                        result += BuildNumeral(10, digit, numeralsDict);
                    }
                    // hundreds
                    else if (length == 2)
                    {
                        result += BuildNumeral(100, digit, numeralsDict);
                    }
                    // thousands
                    else if (length == 3)
                    {
                        result += BuildNumeral(1000, digit, numeralsDict);
                    }
                }
            }

            return result;
        }

        private static string BuildNumeral(int place, int digit, Dictionary<int, string> numeralsDict)
        {
            string result = "";
            int low = 4 * place;
            int mid = 5 * place;
            int high = 9 * place;

            if (digit < low)
            {
                for (int j = 0; j < digit / place; j++)
                {
                    result += numeralsDict[1 * place];
                }
            }
            else if (digit > mid && digit < high)
            {
                result += numeralsDict[mid];

                for (int j = 0; j < (digit - mid) / place; j++)
                {
                    result += numeralsDict[1 * place];
                }
            }

            return result;
        }
    }
}

﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace TalkSystem.Editor
{
    public static class Utils
    {
        private static StringBuilder _printArray;
        public static char[] SplitPattern = { ' ', '\n' };
        public static char[] SplitPatternNotNewline = { ' ', '\n' };
        private const int _whiteSpace = 1;

        static Utils()
        {
            _printArray = new StringBuilder();
        }

        public static void Print(this IEnumerable collection, string title = null)
        {
            _printArray.Clear();

            _printArray.Append("{ ");

            foreach (var item in collection)
            {
                _printArray.Append(item.ToString() + ", ");
            }

            _printArray.Remove(_printArray.Length - 2, 1);
            _printArray.Append("}");

            Debug.Log(_printArray.ToString());
        }

        /// <summary>Helper function to get the char index of a word in a text.</summary>
        public static int GetStartingCharIndex(string text, int wordIndex)
        {
            var splited = text.Split(SplitPattern, StringSplitOptions.RemoveEmptyEntries);
            var charIndex = 0;

            for (int i = 0; i < splited.Length; i++)
            {
                if (i == wordIndex)
                {
                    return charIndex;
                }

                charIndex += splited[i].Length + _whiteSpace;
            }

            return charIndex;
        }
         
        //Very inefficient.
        public static (int, string) GetWordIndex(string text, int charIndex)
        {
            var explit = text.Split(SplitPattern, StringSplitOptions.RemoveEmptyEntries);
            var charCount = 0;

            var word = "";

            for (int i = 0; i < explit.Length; i++)
            {
                for (int j = 0; j < explit[i].Length; j++)
                {
                    if (charCount == charIndex)
                    {
                        return (i, explit[i]);
                    }

                    charCount++;
                }

                charCount++;
            }

            return (0, word);
        }

        public static int GetChangedWordsCount(string current, string compare)
        {
            var currentSplit = current.Split(SplitPattern, StringSplitOptions.RemoveEmptyEntries);
            var compareSplit = compare.Split(SplitPattern, StringSplitOptions.RemoveEmptyEntries);

            return Mathf.Abs(currentSplit.Length - compareSplit.Length);
        }
    }
}

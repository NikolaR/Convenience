﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Convenience
{
    /// <summary>
    /// Helper methods for manipulating or checking strings
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// Removes diacritics (or "accents") from string and returns stripped string.
        /// </summary>
        /// <param name="text">Text which should be stripped of diacritics.</param>
        /// <returns>Stripped string.</returns>
        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(text.Length);

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}

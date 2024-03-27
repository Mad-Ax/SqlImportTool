namespace SqlImportTool.ImportFormats.DataTypes
{
    using System;

    public static class NullableDateType
    {
        public static DateTime? GetValue(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Trim() == "NULL")
            {
                return null;
            }

            return Convert.ToDateTime(input);
        }

        public static string GetColumnSql()
        {
            return "DATE NULL";
        }
    }
}

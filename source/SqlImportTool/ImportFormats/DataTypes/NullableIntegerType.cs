namespace SqlImportTool.ImportFormats.DataTypes
{
    using System;

    public static class NullableIntegerType
    {
        public static int? GetValue(string input)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Trim() == "NULL")
            {
                return null;
            }

            return Convert.ToInt32(input);
        }

        public static string GetColumnSql()
        {
            return "INT NULL";
        }
    }
}

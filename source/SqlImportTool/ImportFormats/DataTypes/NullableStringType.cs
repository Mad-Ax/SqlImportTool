namespace SqlImportTool.ImportFormats.DataTypes
{
    using System;

    public static class NullableStringType
    {
        public static string GetValue(string input)
        {
            if (input.Trim() == "NULL")
            {
                return null;
            }

            return input.Trim();
        }

        public static string GetColumnSql(int length)
        {
            return $"NVARCHAR({length}) NULL";
        }
    }
}

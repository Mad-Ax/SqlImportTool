namespace SqlImportTool.ImportFormats.DataTypes
{
    using System;

    public static class StringType
    {
        public static string GetValue(string input)
        {
            return input.Trim();
        }

        public static string GetColumnSql(int length)
        {
            return $"NVARCHAR({length}) NOT NULL";
        }
    }
}

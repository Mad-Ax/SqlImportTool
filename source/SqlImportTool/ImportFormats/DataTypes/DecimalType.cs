namespace SqlImportTool.ImportFormats.DataTypes
{
    using System;

    public static class DecimalType
    {
        public static decimal GetValue(string input)
        {
            return Convert.ToDecimal(input);
        }

        public static string GetColumnSql(int precision, int scale)
        {
            var actualPrecision = precision <= 38 ? precision : 38;

            return $"DECIMAL({actualPrecision},{scale}) NOT NULL";
        }
    }
}

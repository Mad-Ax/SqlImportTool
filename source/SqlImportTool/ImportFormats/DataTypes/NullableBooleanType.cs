namespace SqlImportTool.ImportFormats.DataTypes
{
    using System;

    public class NullableBooleanType
    {
        public static bool? GetValue(string input)
        {
            if (input.Trim() == "NULL")
            {
                return null;
            }

            if (input.Trim() == "0")
            {
                return false;
            }

            if (input.Trim() == "1")
            {
                return true;
            }

            return Convert.ToBoolean(input.Trim());
        }

        public static string GetColumnSql()
        {
            return "BIT NULL";
        }
    }
}

namespace SqlImportTool.ImportFormats
{
    public interface IImportColumnDefinition
    {
        string Name { get; set; }

        string RawValue { get; set; }

        string GetColumnSql();

        object GetValue();
    }

    public interface IImportColumnDefinition<T> : IImportColumnDefinition
    {
        new T GetValue();
    }
}

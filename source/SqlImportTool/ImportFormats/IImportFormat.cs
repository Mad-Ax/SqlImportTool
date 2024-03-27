namespace SqlImportTool.ImportFormats
{
    using System.Collections.Generic;

    public interface IImportFormat
    {
        string Filepath { get; set; }

        bool HasHeaderRow { get; set; }

        IList<IImportColumnDefinition> Columns { get; }

        string SchemaName { get; set; }

        string TableName { get; set; }

        UpdateMode UpdateMode { get; set; }

        void AddColumn(IImportColumnDefinition column);

        bool ReadNext(); // TODO: do we need this here, or do we handle inside ImportFormat?

        string GetInsertSql();

        object GetParamObject();
    }
}

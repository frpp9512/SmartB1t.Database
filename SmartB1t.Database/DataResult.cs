using System.Data;
using System.Collections.Generic;
using System;

namespace SmartB1t.Database
{
    /// <summary>
    /// Data result from a Datatable
    /// </summary>
    public class DataResult
    {
        DataTable dtResult;
        int currentRow;

        /// <summary>
        /// Creates a new DataResult object from a given DataTable.
        /// </summary>
        /// <param name="dtResult">DataTable containing result data</param>
        public DataResult(DataTable dtResult)
        {
            this.dtResult = dtResult;
        }

        /// <summary>
        /// Creates a new DataResult of a single row
        /// </summary>
        /// <param name="columns">Columns of the DataTable</param>
        /// <param name="rowData">Row items</param>
        private DataResult(DataColumnCollection columns, object[] rowData)
        {
            dtResult = new DataTable();
            for (int i = 0; i < columns.Count; i++)
                dtResult.Columns.Add(columns[i].ColumnName, columns[i].DataType);
            dtResult.Rows.Add(rowData);
        }

        /// <summary>
        /// Number of rows in the data result.
        /// </summary>
        public int RowsCount
        {
            get { return dtResult.Rows.Count; }
        }

        /// <summary>
        /// Number of columns in the data result.
        /// </summary>
        public int ColumnsCount
        {
            get { return dtResult.Columns.Count; }
        }

        /// <summary>
        /// Current row iterator.
        /// </summary>
        public int CurrentRow
        {
            get { return currentRow; }
            set
            {
                if (value < 0)
                    throw new IndexOutOfRangeException("Value cannot be less than zero.");
                if (value >= RowsCount)
                    throw new IndexOutOfRangeException("Value is out of the rows bounds.");
                currentRow = value;
            }
        }

        /// <summary>
        /// End of table. True if CurrentRow is equal to RowCount; otherwise, false.
        /// </summary>
        public bool EndOfTable { get => currentRow == RowsCount; }

        /// <summary>
        /// Begining of the table. True if CurrentRow is equal to zero; otherwise, false.
        /// </summary>
        public bool BeginingOfTable { get => currentRow == 0; }

        /// <summary>
        /// Gets objects array in the specified row.
        /// </summary>
        /// <param name="rowIndex">Row index</param>
        /// <returns>Row objects array</returns>
        public object[] this[int rowIndex]
        {
            get { return dtResult.Rows[rowIndex].ItemArray; }
        }

        /// <summary>
        /// Gets the object value in given row and column.
        /// </summary>
        /// <param name="rowIndex">Row index</param>
        /// <param name="columnIndex">Column index</param>
        /// <returns>Object value</returns>
        public object this[int rowIndex, int columnIndex]
        {
            get { return dtResult.Rows[rowIndex][columnIndex]; }
        }

        /// <summary>
        /// Gets the object value in given row and column.
        /// </summary>
        /// <param name="rowIndex">Row index</param>
        /// <param name="columnName">Column name</param>
        /// <returns>Object value</returns>
        public object this[int rowIndex, string columnName]
        {
            get { return dtResult.Rows[rowIndex][columnName]; }
        }

        /// <summary>
        /// Gets objects array of T type in given column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnIndex">Column index</param>
        /// <returns>Objects array of T type</returns>
        public T[] GetValues<T>(int columnIndex)
        {
            T[] columnData = new T[dtResult.Rows.Count];
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                if (typeof(T).IsEnum)
                    columnData[i] = (T)Enum.Parse(typeof(T), dtResult.Rows[i][columnIndex].ToString());
                else
                    columnData[i] = (T)dtResult.Rows[i][columnIndex];
            }
            return columnData;
        }

        /// <summary>
        /// Gets objects array of T type in given column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName">Column name</param>
        /// <returns>Objects array of T type</returns>
        public T[] GetValues<T>(string columnName)
        {
            T[] columnData = new T[dtResult.Rows.Count];
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                if (typeof(T).IsEnum)
                    columnData[i] = (T)Enum.Parse(typeof(T), dtResult.Rows[i][columnName].ToString());
                else
                    columnData[i] = (T)dtResult.Rows[i][columnName];
            }
            return columnData;
        }

        /// <summary>
        /// Gets object value of T type in current row and column given.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnIndex">Column index</param>
        /// <returns>Object value of T type</returns>
        public T GetValue<T>(int columnIndex)
        {
            if (typeof(T).IsEnum)
                return (T)Enum.Parse(typeof(T), this[currentRow, columnIndex].ToString());
            return (T)this[currentRow, columnIndex];
        }

        /// <summary>
        /// Gets value of T type. If value is DBNull then return default_value given.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnIndex">Column index</param>
        /// <param name="default_value">Default value if DBNull</param>
        /// <returns>Value</returns>
        public T GetValueWithDefault<T>(int columnIndex, T default_value)
        {
            if (this[currentRow, columnIndex] == DBNull.Value)
                return default_value;
            return GetValue<T>(columnIndex);
        }

        /// <summary>
        /// Gets object value of T type in current row and column given.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName">Column name</param>
        /// <returns>Object value of T type</returns>
        public T GetValue<T>(string columnName)
        {
            if (typeof(T).IsEnum)
                return (T)Enum.Parse(typeof(T), this[currentRow, columnName].ToString());
            var d = this[currentRow, columnName];
            return (T)this[currentRow, columnName];
        }

        /// <summary>
        /// Gets value of T type. If value is DBNull then return default_value given.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnName">Column name to get value</param>
        /// <param name="default_value">Default value if DBNull</param>
        /// <returns>Value</returns>
        public T GetValueWithDefault<T>(string columnName, T default_value)
        {
            if (this[currentRow, columnName] == DBNull.Value)
                return default_value;
            return GetValue<T>(columnName);
        }

        /// <summary>
        /// Gets object value of T type in given row and column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rowIndex">Row index</param>
        /// <param name="columnIndex">Column index</param>
        /// <returns>Object value of T type</returns>
        public T GetValue<T>(int rowIndex, int columnIndex)
        {
            if (typeof(T).IsEnum)
                return (T)Enum.Parse(typeof(T), this[rowIndex, columnIndex].ToString());
            return (T)this[rowIndex, columnIndex];
        }

        /// <summary>
        /// Gets value of T type. If value is DBNull then return default_value given.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rowIndex">Row index</param>
        /// <param name="columnIndex">Column index</param>
        /// <param name="default_value">Default value if DBNull</param>
        /// <returns>Value</returns>
        public T GetValueWithDefault<T>(int rowIndex, int columnIndex, T default_value)
        {
            if (this[rowIndex, columnIndex] == DBNull.Value)
                return default_value;
            return GetValue<T>(rowIndex, columnIndex);
        }

        /// <summary>
        /// Gets object value of T type in given row and column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rowIndex">Row index</param>
        /// <param name="columnName">Column name</param>
        /// <returns>Object value of T type</returns>
        public T GetValue<T>(int rowIndex, string columnName)
        {
            if (typeof(T).IsEnum)
                return (T)Enum.Parse(typeof(T), this[rowIndex, columnName].ToString());
            return (T)this[rowIndex, columnName];
        }

        /// <summary>
        /// Gets value of T type. If value is DBNull then return default_value given.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rowIndex">Row index</param>
        /// <param name="columnName">Column name</param>
        /// <param name="default_value">Default value if DBNull</param>
        /// <returns>Value</returns>
        public T GetValueWithDefault<T>(int rowIndex, string columnName, T default_value)
        {
            if (this[rowIndex, columnName] == DBNull.Value)
                return default_value;
            return GetValue<T>(rowIndex, columnName);
        }

        /// <summary>
        /// Increments CurrentRow iterator in one.
        /// </summary>
        public void MoveNextRow()
        {
            if (currentRow < dtResult.Rows.Count)
            {
                currentRow++;
            }
        }

        /// <summary>
        /// Decreases CurrentRow iterator in one.
        /// </summary>
        public void MovePrevRow()
        {
            if (currentRow != 0)
            {
                currentRow--;
            }
        }

        /// <summary>
        /// Gets a list of objects of T type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>List of object of T type</returns>
        public List<T> GetList<T>()
            where T : DBObject, new()
        {
            List<T> objs = new List<T>();
            for (int i = 0; i < RowsCount; i++)
            {
                objs.Add(GetDBObjectAt<T>(i));
            }
            return objs;
        }

        /// <summary>
        /// Gets a DBObject of T type object stored in given row
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rowIndex">Row index</param>
        /// <returns>DBObject of T type</returns>
        public T GetDBObjectAt<T>(int rowIndex)
            where T : DBObject, new()
        {
            T dbo = new T();
            dbo.PrimaryKeyValue = GetValue<object>(rowIndex, dbo.PrimaryKeyField);
            dbo.SetValues(new DataResult(dtResult.Columns, this[rowIndex]));
            return dbo;
        }
        
        /// <summary>
        /// Gets a DBObject of T type object stored in CurrentRow iterator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>DBObject of T type</returns>
        public T GetDBObject<T>()
            where T : DBObject, new()
        {
            return GetDBObjectAt<T>(currentRow);
        }

        /// <summary>
        /// Returns the DBObject referenced by id in a specified column.
        /// </summary>
        /// <typeparam name="T">The DBObject type.</typeparam>
        /// <param name="indexColumn_Name">The DBObject id column name.</param>
        /// <returns>DBObject referenced by id in the specified column.</returns>
        public T GetDBObjectAtColumn<T>(int rowIndex, string indexColumn_Name)
            where T : DBObject, new()
        {
            T dbo = new T();
            dbo.PrimaryKeyValue = GetValue<object>(rowIndex, indexColumn_Name);
            dbo.LoadMe();
            return dbo;
        }

        /// <summary>
        /// Returns the DBObject referenced by id in a specified column.
        /// </summary>
        /// <typeparam name="T">The DBObject type.</typeparam>
        /// <param name="indexColumn_Name">The DBObject id column name.</param>
        /// <returns>DBObject referenced by id in the specified column.</returns>
        public T GetDBObjectAtColumn<T>(int rowIndex, int indexColumn_Index)
            where T : DBObject, new()
        {
            T dbo = new T();
            dbo.PrimaryKeyValue = GetValue<object>(rowIndex, indexColumn_Index);
            dbo.LoadMe();
            return dbo;
        }

        /// <summary>
        /// Returns the DBObject referenced by id in a specified column.
        /// </summary>
        /// <typeparam name="T">The DBObject type.</typeparam>
        /// <param name="indexColumn_Name">The DBObject id column name.</param>
        /// <returns>DBObject referenced by id in the specified column.</returns>
        public T GetDBObjectAtColumn<T>(string indexColumn_Name)
            where T : DBObject, new()
        {
            T dbo = new T();
            dbo.PrimaryKeyValue = GetValue<object>(indexColumn_Name);
            dbo.LoadMe();
            return dbo;
        }

        /// <summary>
        /// Returns the DBObject referenced by id in a specified column.
        /// </summary>
        /// <typeparam name="T">The DBObject type.</typeparam>
        /// <param name="indexColumn_Name">The DBObject id column name.</param>
        /// <returns>DBObject referenced by id in the specified column.</returns>
        public T GetDBObjectAtColumn<T>(int indexColumn_Index)
            where T : DBObject, new()
        {
            T dbo = new T();
            dbo.PrimaryKeyValue = GetValue<object>(indexColumn_Index);
            dbo.LoadMe();
            return dbo;
        }
    }
}

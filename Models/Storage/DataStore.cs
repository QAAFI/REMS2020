namespace Models.Storage
{
    /// <summary>
    /// Reference to the SQLite database (.DB) storing output data 
    /// </summary>
    public class DataStore : ApsimNode
    {
        public DataStore()
        {
            Name = "DataStore";
        }
    }
}

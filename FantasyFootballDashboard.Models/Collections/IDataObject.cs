using System.Collections.Generic;

namespace FantasyFootballDashboard.Models.Collections
{
    /// <summary>
    /// Interface for objects that can be inserted into the IDataList
    /// </summary>
    public interface IDataObject
    {
        /// <summary>
        /// Unique key for the object
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Copies in data from the same type of object
        /// </summary>
        /// <param name="obj">Object to copy</param>
        /// <returns>True if copy successfu</returns>
        bool Copy(object obj);
    }
}

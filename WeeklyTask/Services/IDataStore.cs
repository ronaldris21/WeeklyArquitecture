using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeeklyTask.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<System.Collections.ObjectModel.ObservableCollection<T>> GetItemsAsync(bool forceRefresh = false);
    }
}

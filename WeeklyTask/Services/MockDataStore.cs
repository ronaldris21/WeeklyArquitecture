using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WeeklyTask.Models;

namespace WeeklyTask.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        public static ObservableCollection<Item> items { get; set; }
        public static void Reordenar()
        {
            items = new ObservableCollection<Item>(items.OrderByDescending(i => i.IsMain).ToList());
            Console.WriteLine("\n\n\n\n");
            foreach (var item in items)
            {
                Console.WriteLine(item.Text + "   " + item.IsMain);
            }
        }

        public MockDataStore()
        {
            items = new ObservableCollection<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description.",IsMain=false,Day=null },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description.",IsMain=false,Day=null },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description.",IsMain=true ,Day=null},
                new Item { Id = Guid.NewGuid().ToString(), Text = "item Random", Description="This is an item description.",IsMain=true ,Day=null},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Make Pancakes", Description="This is an item description.",IsMain=true ,Day=null},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description.",IsMain=false,Day="Lunes" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description.",IsMain=false,Day="Domingo" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Make Horchata", Description="This is an item description.",IsMain=true ,Day="Jueves"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Make the Essay", Description="This is an item description.",IsMain=true ,Day="Miércoles"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Finish the Essay item", Description="This is an item description.",IsMain=true ,Day="Jueves"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "item X", Description="This is an item description.",IsMain=true ,Day="Domingo"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Seventh item", Description="This is an item 7.",IsMain=true ,Day="Lunes"}
            };
            items = new ObservableCollection<Item>(items.OrderByDescending(i => i.IsMain).ToList());
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            int pos = items.IndexOf(item);
            items.RemoveAt(pos);
            items.Insert(pos, item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<ObservableCollection<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
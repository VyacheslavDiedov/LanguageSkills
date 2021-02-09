using System.Collections.Generic;

namespace DataAccessLayer.Crud.Interfaces
{
    interface ICrudDictionary<T> where T : class
        {
            /// <summary>
            /// Get all items from table DB
            /// </summary>
            /// <returns>List of items</returns>
             List<T> GetAll();

            /// <summary>
            /// Get item by Id from table DB
            /// </summary>
            /// <param name="id">Item Id</param>
            /// <returns>Item</returns>
            T Get(int id);

            /// <summary>
            /// Add new range of items and save
            /// </summary>
            /// <param name="items">List of items</param>
            void CreateRange(List<T> items);

            /// <summary>
            /// Update the item and save
            /// </summary>
            /// <param name="item">Item to update</param>
            void Update(T item);

            /// <summary>
            /// Delete item by Id from table DB
            /// </summary>
            /// <param name="id">Item Id to delete</param>
            void Delete(int id);
        }
    }

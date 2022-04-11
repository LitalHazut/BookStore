using BookStore.Data.Interfaces;
using BookStore.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BookStore.Data
{
    public class FileSystemDataContext : IRepository<ProductBase>
    {
        Dictionary<Guid, ProductBase> products;
        private static FileSystemDataContext instance;
        public static FileSystemDataContext Instance
        {
            get
            {
                if (instance == null)
                    instance = new FileSystemDataContext();
                return instance;
            }
        }

        private FileSystemDataContext()
        {
            products = new Dictionary<Guid, ProductBase>();
            Init();
        }

        //Load
        private void Init()
        {
            FileStream fs = new FileStream("DataFile.dat", FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                products = (Dictionary<Guid, ProductBase>)formatter.Deserialize(fs);
            }
            catch (SerializationException)
            {
                products = new Dictionary<Guid, ProductBase>();
            }
            finally
            {
                fs.Close();
            }
        }

        public void Dispose()//save
        {
            FileStream fs = new FileStream("DataFile.dat", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, products);
            }
            catch (SerializationException)
            {
                //Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                //exception of save
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        public bool Delete(Guid id)
        {
            return products.Remove(id);
        }
        public ProductBase Get(Guid id)
        {
            return products[id];
        }
        public IEnumerable<ProductBase> Get(Predicate<ProductBase> filter = null)
        {
            if (filter == null) return products.Values;
            return products.Values.Where(p => filter.Invoke(p));
        }


        public ProductBase Update(ProductBase item)
        {
            Delete(item.Id);
            Insert(item);
            return item;
        }

        public bool Insert(ProductBase item)
        {
            foreach (var product in products)
            {
                if (product.Value.IsSameProuduct(item))
                {
                    product.Value.Quantity += item.Quantity;
                    return false;
                }
            }
            products.Add(item.Id, item);
            return true;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Linq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.IO;
using System.Web;

namespace SportsStore.Domain.Concrete
{
    public class SqlProductsRepository : IProductsRepository
    {
        private Table<Product> productsTable;
        public SqlProductsRepository(string connectionString)
        {
            var dc = new DataContext(connectionString);
            dc.Log = (StringWriter) HttpContext.Current.Items["linqToSqlLog"];
            productsTable = dc.GetTable<Product>();
        }

        public IQueryable<Product> Products
        {
            get { return productsTable; }
        }

        public void SaveProduct(Product product)
        {
            // If it's a new product, just attach it to the DataContext 
            if (product.ProductID == 0)
                productsTable.InsertOnSubmit(product);
            else if (productsTable.GetOriginalEntityState(product) == null)
            {
                // We're updating an existing product, but it's not attached 
                // to this data context, so attach it and detect the changes
                productsTable.Attach(product);
                productsTable.Context.Refresh(RefreshMode.KeepCurrentValues, product);
            }

            productsTable.Context.SubmitChanges();
        }

        public void DeleteProduct(Product product)
        {
            productsTable.DeleteOnSubmit(product);
            productsTable.Context.SubmitChanges();
        }
    }
}

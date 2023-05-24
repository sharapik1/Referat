using Sharapova.model;
using System.Collections.ObjectModel;

namespace Sharapova.data
{
    public interface DataProvider
    {

        void SaveUser(User user);
        User? LoadUserByNickName(string nickname);

        void SaveProduct(Product product);
        Product? LoadProductById(int id);
        Collection<Product> LoadAllProducts();

    }
}

using System.Collections.ObjectModel;
using MySqlConnector;
using Sharapova.model;

namespace Sharapova.data.mysql
{
    public class MySqlDataProvider : DataProvider
    {
        private MySqlConnection con;

        public MySqlDataProvider(string Server, string Database, string Username, string Password)
        {
            con = new MySqlConnection($"server={Server};user={Username};password={Password};database={Database}");
            createTables();
        }

        public void SaveUser(User user)
        {
            con.Open();
            MySqlCommand command = new MySqlCommand(@"
            INSERT INTO `User`
            VALUES (@id, @nickname, @name, @password)
            ON DUPLICATE KEY UPDATE `nickname` = VALUES(`nickname`),
                            `name`     = VALUES(`name`),
                            `password` = VALUES(`password`);
            ", con);

            command.Parameters.AddWithValue("@id", user.id);
            command.Parameters.AddWithValue("@nickname", user.nickname);
            command.Parameters.AddWithValue("@name", user.name);
            command.Parameters.AddWithValue("@password", user.password);
            command.ExecuteNonQuery();
            con.Close();
        }

        public User? LoadUserByNickName(string nickname)
        {
            con.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `User` WHERE `nickname` = @nickname", con);
            command.Parameters.AddWithValue("@nickname", nickname);
            MySqlDataReader reader = command.ExecuteReader();

            User user = null;
            if (reader.Read())
            {
                user = new User(
                    reader.GetInt32("id"),
                    nickname,
                    reader.GetString("name"),
                    reader.GetString("password")
                );
            }

            con.Close();

            return user;
        }

        public void SaveProduct(Product product)
        {
            con.Open();
            MySqlCommand command = new MySqlCommand(@"
            INSERT INTO `Product`
            VALUES (@id, @title, @description, @price)
            ON DUPLICATE KEY UPDATE 
                         `title` = VALUES(`title`),
                         `description` = VALUES(`description`),
                         `price` = VALUES(`price`);
            ", con);
            command.Parameters.AddWithValue("@id", product.Id);
            command.Parameters.AddWithValue("@title", product.Title);
            command.Parameters.AddWithValue("@description", product.Description);
            command.Parameters.AddWithValue("@price", product.Price);
            command.ExecuteNonQuery();
            con.Close();
        }

        public Product? LoadProductById(int id)
        {
            con.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `Product` WHERE `id` = @id", con);
            command.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Product product = parseProduct(reader);
                con.Close();
                return product;
            }
            else
            {
                con.Close();
                return null;
            }
        }

        private Product parseProduct(MySqlDataReader reader)
        {
            return new Product(
                reader.GetInt32("id"),
                reader.GetString("title"),
                reader.GetString("description"),
                reader.GetDecimal("price")
            );
        }

        public Collection<Product> LoadAllProducts()
        {
            con.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `Product`;", con);
            MySqlDataReader reader = command.ExecuteReader();
            Collection<Product> products = new Collection<Product>();
            while (reader.Read())
            {
                products.Add(parseProduct(reader));
            }

            con.Close();
            return products;
        }

        private void createTables()
        {
            con.Open();
            MySqlCommand command = new MySqlCommand(@"
            START TRANSACTION;
            CREATE TABLE IF NOT EXISTS `User`
            (
                `id`       int          NOT NULL AUTO_INCREMENT,
                `nickname` VARCHAR(100) NOT NULL,
                `name`     VARCHAR(100) NOT NULL,
                `password` VARCHAR(100) NOT NULL,
                PRIMARY KEY (`id`),
                UNIQUE (`nickname`)
            
            );
            CREATE TABLE IF NOT EXISTS `Product`
            (
                `id`          int     NOT NULL AUTO_INCREMENT,
                `title`       TEXT    NOT NULL,
                `description` TEXT,
                `price`       decimal NOT NULL,
                PRIMARY KEY (`id`)
            );
            COMMIT;
            ", con);
            command.ExecuteNonQuery();
            con.Close();
        }
    }
}
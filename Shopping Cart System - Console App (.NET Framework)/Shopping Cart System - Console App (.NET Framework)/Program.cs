using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping_Cart_System___Console_App__.NET_Framework_
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice()
        {
            return Price * Quantity;
        }
    }

    public class ShoppingCart
    {
        private Product[] products;
        private int productCount;

        public ShoppingCart()
        {
            products = new Product[3];  
            productCount = 0;
        }

        public void AddProduct(Product product)
        {
            for (int i = 0; i < productCount; i++)
            {
                if (products[i].Id == product.Id)
                {
                    products[i].Quantity += product.Quantity;
                    return;
                }
            }

            if (productCount == products.Length)
            {
                Array.Resize(ref products, products.Length * 2);
            }

            products[productCount] = product;
            productCount++;
        }

        public void RemoveProduct(int id)
        {
            int index = -1;
            for (int i = 0; i < productCount; i++)
            {
                if (products[i].Id == id)
                {
                    index = i;
                    break;
                }
            }

            if (index >= 0)
            {
                for (int i = index; i < productCount - 1; i++)
                {
                    products[i] = products[i + 1];
                }
                products[productCount - 1] = null;
                productCount--;
            }
        }

        public void UpdateProductQuantity(int id, int quantity)
        {
            for (int i = 0; i < productCount; i++)
            {
                if (products[i].Id == id)
                {
                    products[i].Quantity = quantity;
                    if (quantity <= 0)
                    {
                        RemoveProduct(id);
                    }
                    return;
                }
            }
        }

        public void ViewCart()
        {
            Console.WriteLine("Your Shopping Cart:");
            for (int i = 0; i < productCount; i++)
            {
                Console.WriteLine($"Id: {products[i].Id}, Name: {products[i].Name}, Price: {products[i].Price}, Quantity: {products[i].Quantity}, Total: {products[i].TotalPrice()}");
            }
        }

        public decimal CalculateTotalPrice()
        {
            decimal total = 0;
            for (int i = 0; i < productCount; i++)
            {
                total += products[i].TotalPrice();
            }
            return total;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            ShoppingCart cart = new ShoppingCart();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Remove Product");
                Console.WriteLine("3. Update Product Quantity");
                Console.WriteLine("4. View Cart");
                Console.WriteLine("5. Calculate Total Price");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddProductToCart(cart);
                            break;
                        case 2:
                            RemoveProductFromCart(cart);
                            break;
                        case 3:
                            UpdateProductInCart(cart);
                            break;
                        case 4:
                            cart.ViewCart();
                            break;
                        case 5:
                            Console.WriteLine($"Total Price: {cart.CalculateTotalPrice()}");
                            break;
                        case 6:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }
        }

        private static void AddProductToCart(ShoppingCart cart)
        {
            Console.Write("Enter Product ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Product Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Product Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Product product = new Product { Id = id, Name = name, Price = price, Quantity = quantity };
            cart.AddProduct(product);
        }

        private static void RemoveProductFromCart(ShoppingCart cart)
        {
            Console.Write("Enter Product ID to Remove: ");
            int id = int.Parse(Console.ReadLine());
            cart.RemoveProduct(id);
        }

        private static void UpdateProductInCart(ShoppingCart cart)
        {
            Console.Write("Enter Product ID to Update: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter New Quantity: ");
            int quantity = int.Parse(Console.ReadLine());
            cart.UpdateProductQuantity(id, quantity);
        }
    }
}


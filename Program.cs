using System;
using System.Linq;
using System.Collections.Generic;

namespace cuisine_simulator
{
    class Program
    {
        private static List<string> command_list = new List<string> // liste des commandes que l'utilisateur peut entrer.
        {
            "/help",
            "/add_ingredient",
            "/add_recipe",
            "/show_ingredient",
            "/show_recipe",
            "/make",
            "/quit"
        };

        private static List<string> ingredient_list = new List<string> // liste des ingrédients que l'utilisateur a rentré.
        {

        };

        private static List<string[]> recipe_list = new List<string[]> // liste des recettes que l'utilisateur a rentré.
        {

        };

        private static void Main(string[] args)
        {
            simulator_handler();
        }

        private static void simulator_handler()
        {            
            while (!take_command()) // tant que le bool que retourne take_command est false, continue le programme.
            {
            }          
        }

        private static bool take_command()
        // prends une entrée de l'utilisateur, la décompose en strings pour chaque espaces et continue que si le premier string est une commande.
        // retourne false tant que /quit n'a pas été entré
        {
            Console.WriteLine("\n");
            string user_entry = Console.ReadLine();
            string[] user_entry_cmd = user_entry.Split(' ');

            while (!command_list.Contains(user_entry_cmd[0]))
            {
                Console.WriteLine("Entrez /help pour voir les commandes.");
                user_entry = Console.ReadLine();
                user_entry_cmd = user_entry.Split(' ');
            }

            // chaque commandes execute une fonction
            switch (user_entry_cmd[0])
            {
                case "/help":
                    help();                   
                    break;
                    
                case "/add_ingredient":                    
                    add_ingredient(user_entry_cmd);
                    Console.WriteLine(user_entry_cmd[0]);
                    break;

                case "/add_recipe":
                    add_recipe(user_entry_cmd);
                    break;

                case "/show_ingredient":
                    show_ingredient();
                    break;

                case "/show_recipe":
                    show_recipe();
                    break;

                case "/make":                   
                    make_recipe(user_entry_cmd);
                    break;

                case "/quit":
                    return true;                   

                default:
                    Console.WriteLine("Entrée invalide.");
                    break;

            }

            return false;
           
        }

        private static void help()
        {
            Console.WriteLine
                     ("/add_ingredient + ingrédient | Rajoute un ingrédient à la liste d'ingrédients." +
                    "\n/add_recipe + nom de la recette + ingrédients de la recette | Rajoute une recette à la liste de recettes." +
                    "\n/show_ingredient | Montre les ingrédients dans la liste d'ingrédients." +
                    "\n/show_recipe | Montre les recettes dans la liste de recettes." +
                    "\n/make + nom de la recette | Vérifie si la recette est faisable. Sinon, affiche les ingrédients manquants.");
        }

        private static void add_ingredient(string[] user_entry_cmd)
        {            
            for (int i = 1; i < user_entry_cmd.Length; i++)
            {
                ingredient_list.Add(user_entry_cmd[i]);
                Console.Write(user_entry_cmd[i] + " ; ");
            }

            Console.WriteLine("ajouté(s) à la liste d'ingrédients.");
        }

        private static void add_recipe(string[] user_entry_cmd)
        {
            // créer un nouveau string[] contenant tout les strings de user_entry_cmd sauf l'élément 0 (la commande)
            string[] new_recipe = new string[user_entry_cmd.Length - 1];
            for (int i = 1; i < user_entry_cmd.Length; i++)
            {
                new_recipe[i-1] = user_entry_cmd[i];
                
            }

            // ajoute new_recipe                         
            recipe_list.Add(new_recipe);
            Console.WriteLine("recette ajouté : " + new_recipe[0]);
            
        }

        private static void show_ingredient()
        {
            Console.WriteLine("Liste d'ingrédients disponibles :");
            foreach (string words in ingredient_list)
            {
                Console.Write(words + " ; ");
                
            }
        }

        private static void show_recipe()
        {
            Console.WriteLine("Liste de recettes :");
            foreach (string[] recipe in recipe_list)
            {                
                foreach (string word in recipe)
                { 
                    Console.Write(word + " ; ");
                }

                Console.SetCursorPosition(0, Console.CursorTop + 1);
            }
        }
            
        private static void make_recipe(string[] user_entry_cmd)
        // vérifie le titre de la recette voulue (user_entry_cmd[recipe]), si elle existe vérifie si il y a les ingrédients
        {
            string desired_recipe = user_entry_cmd[1];
            int recipe_number;
            
            for (recipe_number = 0; recipe_number < recipe_list.Count; recipe_number++)
            {
                if (recipe_list[recipe_number][0] == desired_recipe)
                // si le nom (la première entrée de l'array, [0]) de la recette numéro [recipe_number] est le même que le titre de la recette voulue 
                {
                    if (are_ingredients_missing(recipe_number) == false)
                    {
                        Console.WriteLine(desired_recipe + " prêt(e), bonne appétit !");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("il manque ces ingrédients.");
                        return;
                    }
                }
            }

            Console.WriteLine ("La recette n'a pas été trouvé. Vérifiez la liste de recette en entrant /show_recipe ");

            
        }

        private static bool are_ingredients_missing (int recipe_number) 
        // si aucun ingrédient manque pour la recette, return false sinon true
        {   
            bool ingredients_missing = false;       

            for (int i = 1; i < recipe_list[recipe_number].Length; i++)
            {                
                if (!ingredient_list.Contains(recipe_list[recipe_number][i]))
                {
                    Console.Write(recipe_list[recipe_number][i] + " ; ");
                    ingredients_missing = true;
                }                
            }            
            return ingredients_missing;
        }

     

        

    }
}

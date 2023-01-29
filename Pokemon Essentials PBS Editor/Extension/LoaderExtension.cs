using Pokemon_Essentials_PBS_Editor.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Essentials_PBS_Editor.Extension
{
    public static class LoaderExtension
    {
        private static bool ConvertToBool(string value)
        {
            return value switch
            {
                "yes" => true,
                "no" => false,
                _ => false,
            };
        }
        public static void LoadTrainers(this TrainerEditor window)
        {
            string path = Directory.GetCurrentDirectory() + "\\PBS\\trainers.txt";
            Trainer trainer = new();
            Pokemon pokemon = new();

            foreach (string line in File.ReadLines(path))
            {
                if (line.StartsWith("#")) continue;
                if (line.StartsWith("["))
                {
                    if (trainer.TrainerName != null)
                    {
                        trainer.Pokemons.Add(pokemon);
                        window.Trainers.Add(trainer);
                        pokemon = new();
                    }
                    trainer = new Trainer()
                    {
                        TrainerClass = line.Split(",")[0].Replace("[", ""),
                        TrainerName = line.Split(",")[1].Replace("]", "")
                    };
                    try
                    {
                        trainer.Version = int.Parse(line.Split(",")[2].Replace("]", ""));
                    }
                    catch { }
                }
                else
                {
                    string attribute = line.Split(" = ")[0].Replace(" ", "");
                    string value = line.Split(" = ")[1];

                    switch (attribute)
                    {
                        case "LoseText":
                            trainer.LoseText = value;
                            break;
                        case "Items":
                            continue; // Trainer Items
                        case "Pokemon":
                            if (pokemon.Name != null)
                            {
                                trainer.Pokemons.Add(pokemon);
                            }
                            pokemon = new Pokemon()
                            {
                                Name = value.Split(",")[0],
                                Level = Convert.ToInt32(value.Split(",")[1])
                            };
                            break;
                        case "Gender":
                            pokemon.Gender = value;
                            break;
                        case "Moves":
                            pokemon.Moves = new List<Move>();
                            foreach(var name in value.Split(","))
                            {
                                pokemon.Moves.Add(new Move() { Name = name });
                            }
                            break;
                        case "AbilityIndex":
                            pokemon.AbilityIndex = Convert.ToInt32(value);
                            break;
                        case "IV":
                            pokemon.IVs = new();
                            foreach (var iv in value.Split(","))
                            {
                                pokemon.IVs.Add(Convert.ToInt32(iv));
                            }
                            break;
                        case "Shiny":
                            pokemon.Shiny = ConvertToBool(value);
                            break;
                        case "Item":
                            pokemon.Item = value;
                            break;
                        case "Shadow":
                            pokemon.Shadow = ConvertToBool(value);
                            break;
                        case "Ball":
                            pokemon.Ball = value;
                            break;
                        case "Name":
                            pokemon.Nickname = value;
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
            if (trainer.TrainerName != null)
            {
                trainer.Pokemons.Add(pokemon);
                window.Trainers.Add(trainer);
            }
            window.Trainers.Sort((x, y) => x.ToString().CompareTo(y.ToString()));
        }
    }
}

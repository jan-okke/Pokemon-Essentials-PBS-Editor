using Pokemon_Essentials_PBS_Editor.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Essentials_PBS_Editor.Extension
{
    public static class SaveExtension
    {
        public static void Save(this TrainerEditor window)
        {
            var trainers = window.Trainers;
            string path = Directory.GetCurrentDirectory() + "\\PBS\\trainers.txt";

            string text =   "##########################################################################\n" +
                            "# Automatically generated trainers by TrainerEditorTool made by Jan-Okke #\n" +
                            "##########################################################################\n";
            foreach (var trainer in trainers)
            {
                text += "#-------------------------------\n";
                text += $"{trainer}\n" +
                    $"LoseText = {trainer.LoseText}\n";
                foreach (Pokemon p in trainer.Pokemons)
                {
                    text += $"Pokemon = {p}\n";
                    text += p.Gender != null && p.Gender != "" ? $"\tGender = {p.Gender}\n" : "";
                    text += p.Moves != null && p.Moves.Count > 0 ? $"\tMoves = {string.Join(',', p.Moves.Select(m => m.Name))}\n" : "";
                    text += p.Ability != null && p.Ability != "" ? $"\tAbility = {p.Ability}\n" : "";
                    text += p.IVs != null && p.IVs.Count > 0 ? $"\tIV = {string.Join(',', p.IVs)}\n" : "";
                    text += p.Shiny != false ? $"\tShiny = yes\n" : "";
                    text += p.Shadow != false ? $"\tShadow = yes\n" : "";
                    text += p.Item != null && p.Item != ""  ? $"\tItem = {p.Item}\n" : "";
                    text += p.Ball != null && p.Ball != ""  ? $"\tBall = {p.Ball}\n" : "";
                    text += p.Nickname != null && p.Nickname != ""  ? $"\tName = {p.Nickname}\n" : "";
                }
            }
            Console.WriteLine(text);
            File.WriteAllText(path, text);
        }
    }
}

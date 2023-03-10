using Pokemon_Essentials_PBS_Editor.Entities;
using Pokemon_Essentials_PBS_Editor.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pokemon_Essentials_PBS_Editor
{
    /// <summary>
    /// Interaktionslogik für TrainerEditor.xaml
    /// </summary>
    public partial class TrainerEditor : Window
    {
        private bool _isProgramChange = false;
        public List<Trainer> Trainers { get; set; }
        public TrainerEditor()
        {
            Trainers = new();
            this.LoadTrainers();
            InitializeComponent();
        }

        private void UpdateTrainerList()
        {
            Trainer? t = TrainerList.SelectedItem as Trainer;
            TrainerList.ItemsSource = null;
            Trainers.Sort((x, y) => x.ToString().CompareTo(y.ToString()));
            TrainerList.ItemsSource = Trainers;
            if (t is null) return;
            TrainerList.SelectedIndex = Trainers.IndexOf(t);
        }

        private void UpdatePokemonList()
        {
            Pokemon? p = PokemonList.SelectedItem as Pokemon;
            if (p is null) return;
            var trainer = TrainerList.SelectedItem as Trainer;
            if (trainer is null) return;
            var index = PokemonList.SelectedIndex;
            PokemonList.ItemsSource = null;
            PokemonList.ItemsSource = trainer.Pokemons;
            PokemonList.SelectedIndex = index;
        }

        private void OnSelectTrainer(object sender, SelectionChangedEventArgs e)
        {
            var trainer = TrainerList.SelectedItem as Trainer;
            if (trainer is null) return;
            _isProgramChange = true;
            PokemonList.ItemsSource = trainer.Pokemons;
            TrainerClass.Text = trainer.TrainerClass;
            TrainerName.Text = trainer.TrainerName;
            TrainerVersionNumber.Text = trainer.Version.ToString();
            TrainerLoseText.Text = trainer.LoseText;
            PokemonListGrid.Visibility = Visibility.Visible;
            PokemonInfoGrid.Visibility = Visibility.Hidden;
            _isProgramChange = false;
        }

        private void OnSelectPokemon(object sender, SelectionChangedEventArgs e)
        {
            PokemonInfoGrid.Visibility = Visibility.Visible;
            var pokemon = PokemonList.SelectedItem as Pokemon;
            if (pokemon is null) return;
            _isProgramChange = true;
            PokemonAbility.Text = pokemon.Ability;
            PokemonBall.Text = pokemon.Ball;
            PokemonItem.Text = pokemon.Item;
            PokemonIVHP.Text = pokemon.IVs != null && pokemon.IVs.Count > 0 ? pokemon.IVs[0].ToString() : "";
            PokemonIVATK.Text = pokemon.IVs != null && pokemon.IVs.Count > 1 ? pokemon.IVs[1].ToString() : "";
            PokemonIVDEF.Text = pokemon.IVs != null && pokemon.IVs.Count > 2 ? pokemon.IVs[2].ToString() : "";
            PokemonIVSPATK.Text = pokemon.IVs != null && pokemon.IVs.Count > 3 ? pokemon.IVs[3].ToString() : "";
            PokemonIVSPDEF.Text = pokemon.IVs != null && pokemon.IVs.Count > 4 ? pokemon.IVs[4].ToString() : "";
            PokemonIVSPEED.Text = pokemon.IVs != null && pokemon.IVs.Count > 5 ? pokemon.IVs[5].ToString() : "";
            PokemonLevel.Text = pokemon.Level.ToString();
            PokemonMove1.Text = pokemon.Moves != null && pokemon.Moves.Count > 0 ? pokemon.Moves[0].Name : "";
            PokemonMove2.Text = pokemon.Moves != null && pokemon.Moves.Count > 1 ? pokemon.Moves[1].Name : "";
            PokemonMove3.Text = pokemon.Moves != null && pokemon.Moves.Count > 2 ? pokemon.Moves[2].Name : "";
            PokemonMove4.Text = pokemon.Moves != null && pokemon.Moves.Count > 3 ? pokemon.Moves[3].Name : "";
            PokemonName.Text = pokemon.Name;
            PokemonNickname.Text = pokemon.Nickname;
            PokemonShadow.IsChecked = pokemon.Shadow;
            PokemonShiny.IsChecked = pokemon.Shiny;
            _isProgramChange = false;
        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            SavePokemonChanges();
            SaveTrainerChanges();
            this.Save();
        }
        private void SavePokemonChanges()
        {
            var pokemon = PokemonList.SelectedItem as Pokemon;
            if (pokemon is null) return;
            _isProgramChange = true;
            pokemon.Ability = PokemonAbility.Text.Length > 0 ? PokemonAbility.Text : "";
            pokemon.Ball = PokemonBall.Text.Length > 0 ? PokemonBall.Text : "";
            pokemon.Item = PokemonItem.Text.Length > 0 ? PokemonItem.Text : "";
            pokemon.IVs = PokemonIVHP.Text.Length > 0 || PokemonIVATK.Text.Length > 0 || PokemonIVDEF.Text.Length > 0 || PokemonIVSPATK.Text.Length > 0 || PokemonIVSPDEF.Text.Length > 0 || PokemonIVSPEED.Text.Length > 0 ? new List<int>
            {
                PokemonIVHP.Text.Length > 0 ? int.Parse(PokemonIVHP.Text) : default,
                PokemonIVATK.Text.Length > 0 ? int.Parse(PokemonIVATK.Text) : default,
                PokemonIVDEF.Text.Length > 0 ? int.Parse(PokemonIVDEF.Text) : default,
                PokemonIVSPATK.Text.Length > 0 ? int.Parse(PokemonIVSPATK.Text) : default,
                PokemonIVSPDEF.Text.Length > 0 ? int.Parse(PokemonIVSPDEF.Text) : default,
                PokemonIVSPEED.Text.Length > 0 ? int.Parse(PokemonIVSPEED.Text) : default
            } : new List<int>();
            pokemon.Level = PokemonLevel.Text.Length > 0 ? int.Parse(PokemonLevel.Text) : default;
            pokemon.Moves = new List<Move>();
            if (PokemonMove1.Text.Length > 0) pokemon.Moves.Add(new Move() { Name = PokemonMove1.Text });
            if (PokemonMove2.Text.Length > 0) pokemon.Moves.Add(new Move() { Name = PokemonMove2.Text });
            if (PokemonMove3.Text.Length > 0) pokemon.Moves.Add(new Move() { Name = PokemonMove3.Text });
            if (PokemonMove4.Text.Length > 0) pokemon.Moves.Add(new Move() { Name = PokemonMove4.Text });
            pokemon.Name = PokemonName.Text.Length > 0 ? PokemonName.Text : "";
            pokemon.Nickname = PokemonNickname.Text.Length > 0 ? PokemonNickname.Text : "";
            pokemon.Shadow = PokemonShadow.IsChecked != null ? (bool)PokemonShadow.IsChecked : false;
            pokemon.Shiny = PokemonShiny.IsChecked != null ? (bool)PokemonShiny.IsChecked : false;
            UpdatePokemonList();
            _isProgramChange = false;
        }

        private void SaveTrainerChanges()
        {
            var trainer = TrainerList.SelectedItem as Trainer;
            if (trainer == null) return;
            _isProgramChange = true;
            trainer.TrainerClass = TrainerClass.Text;
            trainer.TrainerName = TrainerName.Text;
            trainer.Version = int.TryParse(TrainerVersionNumber.Text, out int version) ? version : 0;
            trainer.LoseText = TrainerLoseText.Text;
            UpdateTrainerList();
            _isProgramChange = false;
        }

        private void SavePokemonChanges(object sender, TextChangedEventArgs e)
        {
            if (_isProgramChange) return;
            SavePokemonChanges();
        }

        private void SavePokemonChanges(object sender, DependencyPropertyChangedEventArgs e)
        {
            SavePokemonChanges();
        }

        private void SavePokemonChanges(object sender, RoutedEventArgs e)
        {
            SavePokemonChanges();
        }

        private void SavePokemonChanges(object sender, TextCompositionEventArgs e)
        {
            SavePokemonChanges();
        }

        private void SaveTrainerChanges(object sender, TextCompositionEventArgs e)
        {
            SaveTrainerChanges();
        }

        private void SavePokemonChanges(object sender, KeyEventArgs e)
        {
            SavePokemonChanges();
        }

        private void SaveTrainerChanges(object sender, TextChangedEventArgs e)
        {
            if (_isProgramChange) return;
            SaveTrainerChanges();
        }

        private void AddTrainer(object sender, RoutedEventArgs e)
        {
            Trainers.Add(new Trainer());
            UpdateTrainerList();
        }

        private void AddPokemon(object sender, RoutedEventArgs e)
        {
            Trainer? t = TrainerList.SelectedItem as Trainer;
            if (t == null) return;
            t.Pokemons.Add(new Pokemon());
            int index = TrainerList.SelectedIndex;
            PokemonList.ItemsSource = null;
            PokemonList.ItemsSource = t.Pokemons;
            PokemonList.SelectedIndex = index;
        }
    }
}

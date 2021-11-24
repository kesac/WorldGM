using Archigen;
using Loremaker;
using Spectre.Console;
using Syllabore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WorldGM.Entities;

namespace WorldGM.Console
{
    public class GameShell
    {

        private static readonly string SaveFile = "save.json";

        private Game Game;
        private SelectionPrompt<string> MainMenu;
        private SelectionPrompt<string> IngameMenu;

        public GameShell()
        {
            this.MainMenu = new SelectionPrompt<string>()
                                .HighlightStyle(Style.Parse("yellow"))
                                .PageSize(10)
                                .AddChoices(new[] {
                                    "New", "Load", "Exit",
                                });

            this.IngameMenu = new SelectionPrompt<string>()
                                .HighlightStyle(Style.Parse("yellow"))
                                .PageSize(10)
                                .AddChoices(new[] {
                                    "Manage", "Recruit", "Expand", "Raid", "Save", "Exit"
                                });
        }

        public void Start()
        {
            System.Console.Clear();

            var header = new Rule("[grey]WorldGM v0.1[/]")
                        .RuleStyle(Style.Parse("white dim"))
                        .LeftAligned();

            AnsiConsole.Write(header);

            var choice = AnsiConsole.Prompt(this.MainMenu);

            if(choice == "New")
            {
                this.GenerateNewWorld();   
            }
            else if(choice == "Load")
            {
                this.LoadExistingWorld();
            }
            else
            {
                // Exit
            }

        }

        private void GenerateNewWorld()
        {
            var g = new WorldGenerator()
                    .UsingMapGenerator(x => x
                        .UsingDimension(2048, 2048)
                        .UsingLandThreshold(0.3f));

            AnsiConsole.MarkupLine("[grey]Building world... standby...[/]");
            this.Game = new Game();
            this.Game.World = g.Next();
            this.Game.HomeCity = this.Game.World.PopulationCenters.Values.ToList().GetRandom();

            AnsiConsole.MarkupLine("[grey]Building initial character... standby...[/]");

            var names = new NameGenerator();

            var characters = new Generator<Character>()
                        .ForProperty<string>(x => x.FirstName, names)
                        .ForProperty<string>(x => x.FamilyName, names)
                        .ForEach(x =>
                        {
                            x.Level = 1;
                            x.Strength = 1;
                            x.Intelligence = 1;
                            x.Dexterity = 1;
                            x.Resistance = 1;
                            x.Speed = 10;
                            x.Vitality = 10;
                            x.Appearance = Chance.Roll(0.50) ? CharacterAppearance.Feminine : CharacterAppearance.Masculine;
                            x.Element = CharacterElement.Fire;
                        });

            this.Game.GuildMembers.Add(characters.Next());

            AnsiConsole.MarkupLine("[grey]World generated![/]", this.Game.HomeCity.Name);

            this.GameLoop();
        }

        private void LoadExistingWorld()
        {
            if(File.Exists(GameShell.SaveFile))
            {
                AnsiConsole.Markup("[blue]Loading...[/]");
                var json = File.ReadAllText(GameShell.SaveFile);
                this.Game = JsonSerializer.Deserialize<Game>(json);
                AnsiConsole.MarkupLine("[green]Done![/]");

                this.GameLoop();
            }
            else
            {
                AnsiConsole.MarkupLine("[red]A save file does not exist.[/]");
            }
            
        }

        private void GameLoop()
        {

            var header = new Rule($"\n[grey]World of {this.Game.World.Name}[/]")
                        .RuleStyle(Style.Parse("white dim"))
                        .LeftAligned();

            AnsiConsole.Write(header);

            AnsiConsole.MarkupLine("[green]You are currently in the city of {0}.[/]", this.Game.HomeCity.Name);
            AnsiConsole.MarkupLine("[green]There are {0} members in your guild.[/]", this.Game.GuildMembers.Count);

            string input = string.Empty;

            while(input != "exit")
            {
                AnsiConsole.Write(new Rule());

                input = AnsiConsole.Prompt(this.IngameMenu).ToLower();

                if(input == "save")
                {
                    this.SaveGame();
                }

            }

        }


        private void SaveGame()
        {
            AnsiConsole.Markup("[blue]Saving...[/]");
            var json = JsonSerializer.Serialize(this.Game);
            File.WriteAllText(GameShell.SaveFile, json);
            AnsiConsole.MarkupLine("[green]Done![/]");
        }

    }
}
